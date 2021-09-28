using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;
using ZavenDotNetInterview.App.Services;

namespace ZavenDotNetInterview.App.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobProcessorService _jobProcessorService;
        public JobsController(IJobProcessorService jobProcessorService)
        {
            _jobProcessorService = jobProcessorService;
        }

        // GET: Tasks
        public  ActionResult Index()
        {
            using (ZavenDotNetInterviewContext _ctx = new ZavenDotNetInterviewContext())
            {
                JobsRepository jobsRepository = new JobsRepository(_ctx);
                List<Job> jobs = jobsRepository.GetAllJobs();
                return View(jobs);
            }
        }

        // POST: Tasks/Process
        [HttpGet]
        public ActionResult Process()
        {
            _jobProcessorService.ProcessJobs();

            return RedirectToAction("Index");
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(string name, DateTime? doAfter)
        {
            try
            {
                using (ZavenDotNetInterviewContext _ctx = new ZavenDotNetInterviewContext())
                {
                    Job newJob = new Job() { Id = Guid.NewGuid(), DoAfter = doAfter, Name = name, Status = JobStatus.New };
                    Log newLog = new Log() { Id = Guid.NewGuid(), Description = "New Job has been created", CreatedAt = DateTime.UtcNow, JobId = newJob.Id, Job = newJob };
                    newJob = _ctx.Jobs.Add(newJob);
                    newLog = _ctx.Logs.Add(newLog);
                    _ctx.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(Guid jobId)
        {
            return View();
        }
    }
}
