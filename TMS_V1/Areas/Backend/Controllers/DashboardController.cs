using Microsoft.AspNetCore.Mvc;

namespace TMS_V1.Areas.Backend.Controllers
{
    [Area(areaName:"Backend")]
    public class DashboardController : Controller
    {
        private readonly IBaseRepo<TaskEntity> _taskBaseRepo;
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<TeamLead> _teamLeadBaseRepo;
        private readonly IBaseRepo<RegUser> _regUserBaseRepo;
        private readonly IBaseRepo<Comment> _commentBaseRepo;

        public DashboardController(IBaseRepo<TaskEntity> taskBaseRepo,
                                   IBaseRepo<Team> teamBaseRepo,
                                   IBaseRepo<RegUser> regUserBaseRepo,
                                   IBaseRepo<Comment> commentBaseRepo,
                                   IBaseRepo<TeamLead> teamLeadBaseRepo)
        {
            _taskBaseRepo = taskBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _regUserBaseRepo = regUserBaseRepo;
            _commentBaseRepo = commentBaseRepo;
            _teamLeadBaseRepo = teamLeadBaseRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AllTasksCount =await _taskBaseRepo.GetCount();
            ViewBag.CompletedTasksCount = await _taskBaseRepo.GetCount(x=>x.Status == Status.COMPLETED );
            ViewBag.ToDOTasksCount = await _taskBaseRepo.GetCount(x=>x.Status == Status.TODO );
            ViewBag.InProgressTasksCount = await _taskBaseRepo.GetCount(x=>x.Status == Status.IN_PROGRESS );
            ViewBag.HighPriorityTasksCount = await _taskBaseRepo.GetCount(x=>x.Priority == Priority.HIGH );
            ViewBag.MidPriorityTasksCount = await _taskBaseRepo.GetCount(x=>x.Priority == Priority.MID );
            ViewBag.EasyPriorityTasksCount = await _taskBaseRepo.GetCount(x=>x.Priority == Priority.EASY );
            ViewBag.AllTeamsCount = await _teamBaseRepo.GetCount();
            ViewBag.AllUsersCount = await _regUserBaseRepo.GetCount();
            ViewBag.AllTeamLeadsCount = await _regUserBaseRepo.GetCount();
            return View();
        }
    }
}
