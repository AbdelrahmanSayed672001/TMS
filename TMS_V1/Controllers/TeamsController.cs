using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMS_V1.Models;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Controller for managing teams within the application.
    /// Only accessible by users with the Admin role.
    /// </summary>
    [Authorize(Roles = Role.ADMIN)]
    public class TeamsController : Controller
    {
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<TaskEntity> _taskBaseRepo;
        private readonly IBaseRepo<UserTeam> _userTeamBaseRepo;
        private const string USER_TEAM = "UserTeams";
        private const string USER = "UserTeams.User";
        private const string TASK = "Tasks";
        private const string USER_TASKS = "UserTeams.User.Tasks";


        public TeamsController(IBaseRepo<Team> teamBaseRepo, IBaseRepo<UserTeam> userTeamBaseRepo, IBaseRepo<TaskEntity> taskBaseRepo)
        {
            _teamBaseRepo = teamBaseRepo;
            _userTeamBaseRepo = userTeamBaseRepo;
            _taskBaseRepo = taskBaseRepo;
        }


        /// <summary>
        /// Displays a paginated list of teams with optional filtering by name.
        /// </summary>
        /// <param name="titleSearch">The search term to filter teams by name.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <returns>The view with the list of teams.</returns>
        public async Task<IActionResult> Index(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var teams = await _teamBaseRepo.GetFiltered(x => x.Name.Contains(titleSearch) 
                                                        || titleSearch == null, 
                                                        new[] { USER_TEAM,TASK,USER, USER_TASKS });
             

            return View(await teams.ToPagedListAsync(page ?? 1, 6));
        }


        /// <summary>
        /// Displays the view for adding a new team.
        /// </summary>
        /// <returns>The view for adding a new team.</returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }



        /// <summary>
        /// Handles the form submission for adding a new team.
        /// </summary>
        /// <param name="model">The view model containing the details of the new team.</param>
        /// <returns>Redirects to the index view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(TeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Result"] = "Insert valid data";
                ModelState.AddModelError(string.Empty, "Insert valid data");
                return RedirectToAction(nameof(Add));
            }
            try
            {
                var team = new Team { Name = model.Name };

                await _teamBaseRepo.Add(team);

                TempData["Result"] = $"{model.Name} is added successfully";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Add));
        }



        /// <summary>
        /// Displays the view for editing an existing team.
        /// </summary>
        /// <param name="id">The ID of the team to edit.</param>
        /// <returns>The view for editing the team.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var team = await _teamBaseRepo.GetById(t => t.Id == id);

                if (team is not null)
                {
                    var teamEditViewModel = new TeamEditViewModel
                    {
                        Id = team.Id,
                        Name = team.Name,
                    };

                    return View(teamEditViewModel);
                }

                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }



        /// <summary>
        /// Handles the form submission for updating an existing team.
        /// </summary>
        /// <param name="id">The ID of the team to update.</param>
        /// <param name="model">The view model containing the updated details of the team.</param>
        /// <returns>Redirects to the index view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamEditViewModel model)
        {
            if (id <= 0)
            {
                TempData["Result"] = $"Invalid ID {id}";
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                TempData["Result"] = "Insert valid data";
                ModelState.AddModelError(string.Empty, "Insert valid data");
                return RedirectToAction(nameof(Add));
            }

            var team = await _teamBaseRepo.GetById(t => t.Id == id);

            try
            {
                team.Name = model.Name;
                await _teamBaseRepo.Update(team);

                TempData["Result"] = $"{model.Name} is updated successfullty";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";

            }
            return RedirectToAction(nameof(Edit));

        }


        /// <summary>
        /// Deletes a team and its associated data.
        /// </summary>
        /// <param name="id">The ID of the team to delete.</param>
        /// <returns>Redirects to the index view with success or error messages.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Result"] = $"Invalid ID {id}";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var team = await _teamBaseRepo.GetById(t => t.Id == id, new[] { USER_TEAM });

                //var userTeams = await _userTeamBaseRepo.GetAll(ut => ut.TeamId == team.Id);

                //if (userTeams.Any())
                //{
                //    foreach (var item in userTeams)
                //    {
                //        await _userTeamBaseRepo.Delete(item);
                //    }
                //}

                //var tasksTeams= await _taskBaseRepo.GetAll(x=>x.TeamId == id);
                //foreach (var task in tasksTeams)
                //{
                //    await _taskBaseRepo.Delete(task);
                //}

                await _teamBaseRepo.Delete(team);

                TempData["Result"] = $"Team {team.Name} is deleted successfully";

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
