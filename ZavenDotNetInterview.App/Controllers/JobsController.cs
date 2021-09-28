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
        private IJobsRepository _jobsRepository;
        private ILogsRepository _logsRepository;
        public JobsController(IJobProcessorService jobProcessorService, IJobsRepository jobsRepository, ILogsRepository logsRepository)
        {
            _jobProcessorService = jobProcessorService;
            _jobsRepository = jobsRepository;
            _logsRepository = logsRepository;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            using (ZavenDotNetInterviewContext _ctx = new ZavenDotNetInterviewContext())
            {                
                List<Job> jobs = new List<Job>( _jobsRepository.Get().OrderBy(x => x.CreatedAt));
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
                Job newJob = new Job() { Id = Guid.NewGuid(), DoAfter = doAfter, Name = name, Status = JobStatus.New };
                if (_jobsRepository.Create(newJob))
                {
                    _logsRepository.Create(newJob);
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
            var job = _jobsRepository.Get(jobId);
            if(job != null)
            {
                var orderedList = job.Logs.OrderBy(x => x.CreatedAt);
                job.Logs = new List<Log>(orderedList);
                return View(job);
            }
            return RedirectToAction("Index");
        }
    }
}
