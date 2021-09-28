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
        }
    }
}