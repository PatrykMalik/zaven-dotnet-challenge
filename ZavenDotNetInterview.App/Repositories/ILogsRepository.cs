using System;
using System.Collections.Generic;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Repositories
{
    public interface ILogsRepository
    {
        List<Log> Get(Guid jobId);
        Log Create(Job job);
    }
}