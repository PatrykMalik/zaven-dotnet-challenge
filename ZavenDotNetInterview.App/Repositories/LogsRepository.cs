using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        public LogsRepository()
        {
        }
        public Log CreateLog(Job job)
        {
            return InsertLog(new Log { Id =  Guid.NewGuid(), CreatedAt = DateTime.UtcNow, Description = $"Job changed status to: {job.Status}", JobId = job.Id });
        }
        public List<Log> GetJobsLogs(Guid jobId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var logs = connection.Query<Log>($"SELECT * FROM Logs WHERE JobId = {jobId}").ToList();
                return logs;
            }
        }

        public Log InsertLog(Log log)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string sql = "INSERT INTO Logs (Id, Description, CreatedAt, JobId) Values (@Id, @Description, @CreatedAt, @JobId);";

                log.CreatedAt = DateTime.UtcNow;
                var newLog = connection.Execute(sql, new { Id = log.Id, Description = log.Description, CreatedAt = log.CreatedAt, JobId = log.JobId });

                return log;
            }
        }
    }
}