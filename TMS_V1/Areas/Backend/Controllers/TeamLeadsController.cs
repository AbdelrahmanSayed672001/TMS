using Microsoft.AspNetCore.Mvc;

namespace TMS_V1.Areas.Backend.Controllers
{

    [Area(areaName:"Backend")]
    public class TeamLeadsController : Controller
    {
        private readonly IBaseRepo<TeamLead> _teamBaseRepo;

        public TeamLeadsController(IBaseRepo<TeamLead> teamBaseRepo)
        {
            _teamBaseRepo = teamBaseRepo;
        }

        public IActionResult Index()
        {
            //var team
            return View();
        }
    }
}
