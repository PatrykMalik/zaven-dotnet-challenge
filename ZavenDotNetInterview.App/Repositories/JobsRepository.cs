using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly ZavenDotNetInterviewContext _ctx;
        private ILogsRepository _logsRepository;

        public JobsRepository(ZavenDotNetInterviewContext ctx, ILogsRepository logsRepository)
        {
            _ctx = ctx;
            _logsRepository = logsRepository;
        }

        public List<Job> Get()
        {
            return _ctx.Jobs.ToList();
        }
        public Job Get(Guid Id)
        {
            return _ctx.Jobs.Where(x => x.Id == Id).Include(y => y.Logs).SingleOrDefault();
        }
        public bool Create(Job job)
        {
            if(null == _ctx.Jobs.SingleOrDefault(x => x.Name == job.Name))
            {
                _ctx.Jobs.Add(job);
                _ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Update(Job job)
        {
            var _job = Get(job.Id);
            _job = job;
            _ctx.SaveChanges();
            _logsRepository.Create(_job);
        }
    }
}