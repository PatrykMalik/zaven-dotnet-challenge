using System;
using System.Collections.Generic;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Repositories
{
    public interface IJobsRepository
    {
        List<Job> Get();
        Job Get(Guid Id);
        bool Create(Job job);
        void Update(Job job);
    }
}