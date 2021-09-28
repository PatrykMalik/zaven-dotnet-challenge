
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Extensions
{
    internal static class JobExtension
    {
        public static void ChangeStatus(this Job job, JobStatus newStatus)
        {
            job.Status = newStatus;
            job.LastUpdatedAt = DateTime.UtcNow;
            if(newStatus == JobStatus.Failed)
            {
                IncreaseFailCounter(job);
            }
        }
        private static void IncreaseFailCounter(this Job job)
        {
            job.FailCounter++;
            if (job.FailCounter > 4)
            {
                job.ChangeStatus(JobStatus.Closed);
            }
        }
    }
}