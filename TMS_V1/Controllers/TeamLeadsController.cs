using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS_V1.Data.Migrations;
using TMS_V1.Models;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Controller for managing Team Leads within the application.
    /// Only accessible by users with the Admin role.
    /// </summary>
    [Authorize(Roles =Role.ADMIN)]  
    public class TeamLeadsController : Controller
    {
        private readonly IBaseRepo<TeamLead> _teamLeadBaseRepo;
        private readonly IBaseRepo<ApplicationUser> _appUserBaseRepo;
        private readonly IBaseRepo<IdentityUserRole<string>> _userRoleBaseRepo;
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<UserTeam> _userTeamBaseRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string USER = "User";
        private const string TEAM_LEAD = "TeamLead";
        private const string TEAM = "User.UserTeams.Team";
        private const string USER_TEAM = "User.UserTeams";

        public TeamLeadsController(IBaseRepo<TeamLead> userBaseRepo, 
                                   IBaseRepo<UserTeam> userTeamBaseRepo, 
                                   IBaseRepo<Team> teamBaseRepo, 
                                   UserManager<ApplicationUser> userManager, 
                                   IBaseRepo<ApplicationUser> appUserBaseRepo,
                                   IBaseRepo<IdentityUserRole<string>> userRoleBaseRepo)
        {
            _teamLeadBaseRepo = userBaseRepo;
            _userTeamBaseRepo = userTeamBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _userManager = userManager;
            _appUserBaseRepo = appUserBaseRepo;
            _userRoleBaseRepo = userRoleBaseRepo;
        }

        /// <summary>
        /// Displays a paginated list of team leads with optional filtering by email.
        /// </summary>
        /// <param name="titleSearch">The search term to filter team leads by email.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <returns>The view with the list of team leads.</returns>
        public async Task<IActionResult> Index(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var teamLeads = await _teamLeadBaseRepo.GetFiltered(x => x.User.Email.Contains(titleSearch)
                                                        || titleSearch == null
                                                        , new[] { USER, USER_TEAM, TEAM });

            return View(await teamLeads.ToPagedListAsync(page ?? 1, 6));
        }



        /// <summary>
        /// Displays the view for adding a new team lead.
        /// </summary>
        /// <returns>The view for adding a new team lead.</returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Teams = await _teamBaseRepo.GetAll();
            return View();
        }



        /// <summary>
        /// Handles the form submission for adding a new team lead.
        /// </summary>
        /// <param name="model">The view model containing the details of the new team lead.</param>
        /// <returns>Redirects to the index view with success or error messages.</returns>
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
                    await _userManager.AddToRoleAsync(user, Role.TEAM_LEAD);

                    var teamLead = new TeamLead
                    {
                        UserId = user.Id,
                    };
                    await _teamLeadBaseRepo.Add(teamLead);

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
        /// Displays the view for editing an existing team lead.
        /// </summary>
        /// <param name="id">The ID of the team lead to edit.</param>
        /// <returns>The view for editing the team lead.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var teamLead = await _teamLeadBaseRepo.GetById(t => t.Id == id, new[] { USER, USER_TEAM, TEAM });

                if (teamLead is not null)
                {
                    var userEditViewModel = new UserEditViewModel
                    {
                        Id = teamLead.Id,
                        Email = teamLead.User.Email,
                        UserTeams = teamLead.User.UserTeams
                    };

                    ViewBag.Teams = await _teamBaseRepo.GetAll();
                    return View(userEditViewModel);
                }
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }



        /// <summary>
        /// Handles the form submission for updating an existing team lead.
        /// </summary>
        /// <param name="id">The ID of the team lead to update.</param>
        /// <param name="model">The view model containing the updated details of the team lead.</param>
        /// <returns>Redirects to the index view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserEditViewModel model)
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

            var teamLead = await _teamLeadBaseRepo.GetById(t => t.Id == id, new[] { USER, USER_TEAM, TEAM });

            try
            {
                if (teamLead.User.Email != model.Email)
                {
                    teamLead.User.Email = model.Email;
                    teamLead.User.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(teamLead.User);
                    if (!result.Succeeded)
                    {
                        TempData["Result"] = "Something went wrong";
                        return RedirectToAction(nameof(Edit));
                    }
                }

                if (model.NewPassword != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(teamLead.User, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        TempData["Result"] = "Something went wrong";
                        return RedirectToAction(nameof(Edit));
                    }
                }

                var existingUserTeams = await _userTeamBaseRepo.GetAll(x => x.UserId == teamLead.UserId, new[] { TEAM });

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
                        var newUserTeam = new UserTeam { UserId = teamLead.UserId, TeamId = teamId };
                        await _userTeamBaseRepo.Add(newUserTeam);
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
        /// Deletes a team lead and their associated data.
        /// </summary>
        /// <param name="id">The ID of the team lead to delete.</param>
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
                var teamLead = await _teamLeadBaseRepo.GetById(t => t.Id == id, new[] { USER, USER_TEAM, TEAM });

                var userTeams = await _userTeamBaseRepo.GetAll(ut => ut.UserId == teamLead.UserId);

                var appUser = await _appUserBaseRepo.GetById(x => x.Id == teamLead.UserId);

                var userRole = await _userRoleBaseRepo.GetById(x => x.UserId == teamLead.UserId);

                foreach (var item in userTeams)
                {
                    await _userTeamBaseRepo.Delete(item);
                }

                await _userRoleBaseRepo.Delete(userRole);
                await _teamLeadBaseRepo.Delete(teamLead);
                await _appUserBaseRepo.Delete(appUser);

                TempData["Result"] = $"Team Leader {teamLead.User.Email} is deleted successfully";

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
