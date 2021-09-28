using System.Data.Entity;

namespace ZavenDotNetInterview.App.Models.Context
{
    public class ZavenDotNetInterviewContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ZavenDotNetInterviewContext() : base("name=DefaultConnection")
        {
        }
    }
}