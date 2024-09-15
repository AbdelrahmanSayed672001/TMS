using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Handles CRUD operations and user management for regular users.
    /// Requires users to have the ADMIN role to access these actions.
    /// </summary>
    [Authorize(Roles =Role.ADMIN)]
    public class RegUsersController : Controller
    {
        private readonly IBaseRepo<RegUser> _regUserBaseRepo;
        private readonly IBaseRepo<ApplicationUser> _appUserBaseRepo;
        private readonly IBaseRepo<IdentityUserRole<string>> _userRoleBaseRepo;
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<TaskEntity> _taskBaseRepo;
        private readonly IBaseRepo<UserTeam> _userTeamBaseRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string USER = "User";
        private const string REG_USER = "regUser";
        private const string TASK = "User.Tasks";
        private const string TEAM = "User.UserTeams.Team";
        private const string USER_TEAM = "User.UserTeams";

        public RegUsersController(IBaseRepo<RegUser> regUserBaseRepo,
                                  IBaseRepo<ApplicationUser> appUserBaseRepo,
                                  IBaseRepo<IdentityUserRole<string>> userRoleBaseRepo,
                                  IBaseRepo<Team> teamBaseRepo,
                                  IBaseRepo<UserTeam> userTeamBaseRepo,
                                  UserManager<ApplicationUser> userManager,
                                  IBaseRepo<TaskEntity> taskBaseRepo)
        {
            _regUserBaseRepo = regUserBaseRepo;
            _appUserBaseRepo = appUserBaseRepo;
            _userRoleBaseRepo = userRoleBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _userTeamBaseRepo = userTeamBaseRepo;
            _userManager = userManager;
            _taskBaseRepo = taskBaseRepo;
        }


        /// <summary>
        /// Displays a list of regular users with optional filtering and pagination.
        /// </summary>
        /// <param name="titleSearch">Optional search term to filter users by email.</param>
        /// <param name="page">Optional page number for pagination.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        public async Task<IActionResult> Index(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var regUsers = await _regUserBaseRepo.GetFiltered(x => x.User.Email.Contains(titleSearch)
                                                        || titleSearch == null
                                                        , new[] { USER, TASK, USER_TEAM, TEAM });

            return View(await regUsers.ToPagedListAsync(page ?? 1, 6));
        }



        /// <summary>
        /// Displays the form for adding a new regular user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Teams = await _teamBaseRepo.GetAll();
            return View();
        }



        /// <summary>
        /// Processes the form submission for adding a new regular user.
        /// </summary>
        /// <param name="model">The model containing the data for the new user.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Result"] = "Insert valid data";
                ModelState.AddModelError(string.Empty, "Insert valid data");
                return RedirectToAction(nameof(Add));
            }

            try
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.REG_USER);

                    var regUser = new RegUser
                    {
                        UserId = user.Id,
                    };
                    await _regUserBaseRepo.Add(regUser);

                    foreach (var item in model.TeamsId)
                    {
                        var userTeam = new UserTeam
                        {
                            UserId = user.Id,
                            TeamId = item
                        };

                        await _userTeamBaseRepo.Add(userTeam);
                    }

                }
                TempData["Result"] = $"{model.Email} is added successfully";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Add));
        }



        /// <summary>
        /// Displays the form for editing an existing regular user.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var regUser = await _regUserBaseRepo.GetById(t => t.Id == id, new[] { USER,TASK, USER_TEAM, TEAM });

                if (regUser is not null)
                {
                    var userEditViewModel = new RegUserEditViewModel
                    {
                        Id = regUser.Id,
                        Email = regUser.User.Email,
                        UserTeams = regUser.User.UserTeams,
                        Tasks = regUser.User.Tasks
                    };

                    ViewBag.Teams = await _teamBaseRepo.GetAll();
                    ViewBag.Tasks = await _taskBaseRepo.GetAll(x => x.Status != Status.COMPLETED);
                    return View(userEditViewModel);
                }
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }




        /// <summary>
        /// Processes the form submission for updating an existing regular user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="model">The model containing the updated data for the user.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RegUserEditViewModel model)
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

            var regUser = await _regUserBaseRepo.GetById(t => t.Id == id, new[] { USER, USER_TEAM, TEAM });

            try
            {
                if (regUser.User.Email != model.Email)
                {
                    regUser.User.Email = model.Email;
                    regUser.User.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(regUser.User);
                    if (!result.Succeeded)
                    {
                        TempData["Result"] = "Something went wrong";
                        return RedirectToAction(nameof(Edit));
                    }
                }

                if (model.NewPassword != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(regUser.User, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        TempData["Result"] = "Something went wrong";
                        return RedirectToAction(nameof(Edit));
                    }
                }

                var existingUserTeams = await _userTeamBaseRepo.GetAll(x => x.UserId == regUser.UserId, new[] { TEAM });


                var selectedTeamIds = model.TeamsId;

                var teamsToRemove = existingUserTeams.Where(ut => !selectedTeamIds.Contains(ut.TeamId)).ToList();
                foreach (var teamToRemove in teamsToRemove)
                {
                    await _userTeamBaseRepo.Delete(teamToRemove);
                }

                foreach (var teamId in selectedTeamIds)
                {
                    if (!existingUserTeams.Any(ut => ut.TeamId == teamId))
                    {
                        var newUserTeam = new UserTeam { UserId = regUser.UserId, TeamId = teamId };
                        await _userTeamBaseRepo.Add(newUserTeam);
                    }
                }

                //var existingTasks = await _taskBaseRepo.GetAll(x => x.UserId == regUser.UserId);
                var tasks = await _taskBaseRepo.GetAll(x => x.Status != Status.COMPLETED);

                var selectedTasks = model.TasksId;
                if (selectedTasks.Any())
                {
                    foreach (var item in selectedTasks)
                    {
                        var newTask = tasks.FirstOrDefault(x => x.Id == item);

                        if (newTask.TeamId != null)
                            newTask.TeamId = null;

                        newTask.UserId = regUser.UserId;

                        await _taskBaseRepo.Update(newTask);
                    }
                }

                TempData["Result"] = $"{model.Email} is updated successfullty";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";

            }
            return RedirectToAction(nameof(Edit));

        }


        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Result"] = $"Invalid ID {id}";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var regUser = await _regUserBaseRepo.GetById(t => t.Id == id, new[] { USER, USER_TEAM, TEAM });

                var userTeams = await _userTeamBaseRepo.GetAll(ut => ut.UserId == regUser.UserId);

                var appUser = await _appUserBaseRepo.GetById(x => x.Id == regUser.UserId);

                var userRole = await _userRoleBaseRepo.GetById(x => x.UserId == regUser.UserId);

                foreach (var item in userTeams)
                {
                    await _userTeamBaseRepo.Delete(item);
                }

                await _userRoleBaseRepo.Delete(userRole);
                await _regUserBaseRepo.Delete(regUser);
                await _appUserBaseRepo.Delete(appUser);

                TempData["Result"] = $"User {regUser.User.Email} is deleted successfully";

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
