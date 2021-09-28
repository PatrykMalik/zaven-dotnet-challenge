using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Job name is required")]
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoAfter { get; set; }
        public virtual List<Log> Logs { get; set; }
    }

    public enum JobStatus
    {
        Failed = -1,
        New = 0,
        InProgress = 1,
        Done = 2
    }
}