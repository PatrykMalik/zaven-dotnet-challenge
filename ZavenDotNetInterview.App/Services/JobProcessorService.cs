using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobProcessorService : IJobProcessorService
    {
        private ZavenDotNetInterviewContext _ctx;
        private IJobsRepository _jobsRepository;
        private ILogsRepository _logsRepository;
        public JobProcessorService(ZavenDotNetInterviewContext ctx, IJobsRepository jobsRepository, ILogsRepository logsRepository)
        {
            _ctx = ctx;
            _jobsRepository = jobsRepository;
            _logsRepository = logsRepository;
        }

        public void ProcessJobs()
        {
            //IJobsRepository jobsRepository = new JobsRepository(_ctx);
            var allJobs = _jobsRepository.GetAllJobs();
            var jobsToProcess = allJobs.Where(x => (x.Status == JobStatus.New || x.Status == JobStatus.Failed)).ToList();

            //jobsToProcess.ForEach(job => job.ChangeStatus(JobStatus.InProgress));
            foreach (var job in jobsToProcess)
            {
                job.ChangeStatus(JobStatus.InProgress);
                _logsRepository.CreateLog(job);
            }
            _ctx.SaveChanges();
            var task = Parallel.ForEach(jobsToProcess, (currentjob) =>
            {

                new Task(async () =>
                {
                    var response = await ProcessJob(currentjob);
                    //var result = response.Result;
                    if (response)
                    {
                        currentjob.ChangeStatus(JobStatus.Done);
                    }
                    else
                    {
                        currentjob.ChangeStatus(JobStatus.Failed);
                    }

                }).Start();
            });
            if (task.IsCompleted)
            {
                _ctx.SaveChanges();
            }
        }

        private async Task<bool> ProcessJob(Job job)
        {
            Random rand = new Random();
            if (rand.Next(10) < 5)
            {
                await Task.Delay(2000);
                return false;
            }
            else
            {
                await Task.Delay(1000);
                return true;
            }
        }
    }
}