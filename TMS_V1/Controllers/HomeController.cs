using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TMS_V1.Models;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Represents the home controller for handling administrative functionalities.
    /// This controller is accessible only by users with the <see cref="Role.ADMIN"/> role.
    /// </summary>
    [Authorize(Roles =Role.ADMIN)]
    public class HomeController : Controller
    {
        private readonly IBaseRepo<TaskEntity> _taskBaseRepo;
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<TeamLead> _teamLeadBaseRepo;
        private readonly IBaseRepo<RegUser> _regUserBaseRepo;
        private readonly IBaseRepo<Comment> _commentBaseRepo;

        public HomeController(IBaseRepo<TaskEntity> taskBaseRepo, 
                              IBaseRepo<Team> teamBaseRepo, 
                              IBaseRepo<TeamLead> teamLeadBaseRepo, 
                              IBaseRepo<RegUser> regUserBaseRepo, 
                              IBaseRepo<Comment> commentBaseRepo)
        {
            _taskBaseRepo = taskBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _teamLeadBaseRepo = teamLeadBaseRepo;
            _regUserBaseRepo = regUserBaseRepo;
            _commentBaseRepo = commentBaseRepo;
        }

        /// <summary>
        /// Retrieves and displays the dashboard view with various task and entity counts.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.AllTasksCount = await _taskBaseRepo.GetCount();
            ViewBag.CompletedTasksCount = await _taskBaseRepo.GetCount(x => x.Status == Status.COMPLETED);
            ViewBag.ToDOTasksCount = await _taskBaseRepo.GetCount(x => x.Status == Status.TODO);
            ViewBag.InProgressTasksCount = await _taskBaseRepo.GetCount(x => x.Status == Status.IN_PROGRESS);
            ViewBag.HighPriorityTasksCount = await _taskBaseRepo.GetCount(x => x.Priority == Priority.HIGH);
            ViewBag.MidPriorityTasksCount = await _taskBaseRepo.GetCount(x => x.Priority == Priority.MID);
            ViewBag.EasyPriorityTasksCount = await _taskBaseRepo.GetCount(x => x.Priority == Priority.EASY);
            ViewBag.AllTeamsCount = await _teamBaseRepo.GetCount();
            ViewBag.AllUsersCount = await _regUserBaseRepo.GetCount();
            ViewBag.AllTeamLeadsCount = await _regUserBaseRepo.GetCount();
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
