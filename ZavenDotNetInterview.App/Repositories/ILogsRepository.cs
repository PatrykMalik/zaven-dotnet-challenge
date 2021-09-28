using System;
using System.Collections.Generic;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Repositories
{
    public interface ILogsRepository
    {
        List<Log> GetJobsLogs(Guid jobId);
        Log InsertLog(Log log);
        Log CreateLog(Job job);
    }
}