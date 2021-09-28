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
        private IJobsRepository _jobsRepository;
        public JobProcessorService(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        public void ProcessJobs()
        {
            var jobsToProcess = GetJobsToProcess();
            SetStatusInProgress(jobsToProcess);
            Process(jobsToProcess);
        }

        private void SetStatusInProgress(List<Job> jobs)
        {
            foreach (var job in jobs)
            {
                SetStatusOfJob(JobStatus.InProgress, job);
            }
        }

        private void SetStatusOfJob( JobStatus newStatus, Job jobToProcess)
        {
                jobToProcess.ChangeStatus(newStatus);
                _jobsRepository.Update(jobToProcess);
        }

        private List<Job> GetJobsToProcess()
        {
            var allJobs = _jobsRepository.Get();
            return allJobs.Where(x =>
                ((x.Status == JobStatus.New || x.Status == JobStatus.Failed)
                && ((x.DoAfter == null) || (DateTime.Compare(DateTime.Parse(x.DoAfter.ToString()), DateTime.Today) <= 0))))
                .ToList();
        }

        private void Process(List<Job> jobsToProcess)
        {
            //TODO: Async
            foreach (var currentjob in jobsToProcess)
            {
                var response = ProcessJob(currentjob);

                if (response)
                {
                    SetStatusOfJob(JobStatus.Done, currentjob);
                }
                else
                {
                    SetStatusOfJob(JobStatus.Failed, currentjob);
                }

            }            
        }

        private bool ProcessJob(Job job)
        {
            Random rand = new Random();
            if (rand.Next(10) < 5)            
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //private async Task Preproces(List<Job> jobsToProcess)
        //{
        //    Parallel.ForEach(jobsToProcess, (currentjob) =>
        //    {

        //        new Task(() =>
        //        {
        //            var response =  this.ProcessJob(currentjob).ConfigureAwait(true);
        //            var result = response.GetAwaiter();
        //            if (result.GetResult())
        //            {
        //                currentjob.ChangeStatus(JobStatus.Done);
        //            }
        //            else
        //            {
        //                currentjob.ChangeStatus(JobStatus.Failed);
        //            }
        //            _logsRepository.CreateLog(currentjob);
        //        }).Start();

        //    });
        //}

        //private async Task<bool> ProcessJob(Job job)
        //{
        //    Random rand = new Random();
        //    if (rand.Next(10) < 5)
        //    {
        //        await Task.Delay(2000);
        //        return false;
        //    }
        //    else
        //    {
        //        await Task.Delay(1000);
        //        return true;
        //    }
        //}
    }
}