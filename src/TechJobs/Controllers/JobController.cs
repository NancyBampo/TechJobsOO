using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;
        private Employer employer;
        private Location location;
        private CoreCompetency coreCompetency;
        private PositionType positionType;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job job = jobData.Find(id);
            return View(job);

           
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (Employer field in jobData.Employers.ToList())
                {
                    //Employer employer = new Employer();

                    if (field.ID == newJobViewModel.EmployerID)
                    {
                        employer = field;
                    }
                }

                foreach (Location field in jobData.Locations.ToList())
                {
                   // Location location = new Location();
                    if (field.ID == newJobViewModel.LocationID)
                    {
                        location = field;
                    }
                }

                foreach (CoreCompetency field in jobData.CoreCompetencies.ToList())
                {
                    //CoreCompetency coreCompetency = new CoreCompetency();
                    if (field.ID == newJobViewModel.CoreCompetencyID)
                    {
                        coreCompetency = field;
                    }
                }

                foreach (PositionType field in jobData.PositionTypes.ToList())
                {
                    //PositionType positionType = new PositionType();

                    if (field.ID == newJobViewModel.PositionTypeID)
                    {
                        positionType = field;
                    }
                }

                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = employer,
                    Location = location,
                    CoreCompetency = coreCompetency,
                    PositionType = positionType
                };

                jobData.Jobs.Add(newJob);
                

                return Redirect("/Job?id=" + newJob.ID.ToString());
            }
            return View(newJobViewModel);
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

     
        }
    }
}
