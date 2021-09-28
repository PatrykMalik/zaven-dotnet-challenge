using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Job name is required")]
        //[Index(IsUnique = true)]
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoAfter { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int FailCounter { get; set; } = 0;
        public virtual List<Log> Logs { get; set; }
    }

    public enum JobStatus
    {
        Closed = -2,
        Failed = -1,
        New = 0,
        InProgress = 1,
        Done = 2
    }
}