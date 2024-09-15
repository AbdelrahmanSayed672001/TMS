using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Handles operations related to team leads' roles, 
    /// including managing tasks, viewing task details, and performing CRUD operations on tasks.
    /// </summary>
    [Authorize(Roles =Role.TEAM_LEAD)]
    public class TeamLeadRolesController : Controller
    {
        private readonly IBaseRepo<TeamLead> _teamLeadBaseRepo;
        private readonly IBaseRepo<RegUser> _regUserBaseRepo;
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<TaskEntity> _taskBaseRepo;
        private readonly IBaseRepo<UserTeam> _userTeamBaseRepo;
        private readonly IBaseRepo<Comment> _commentBaseRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAttachment _attachment;
        private const string ROOT = "Attachments";
        private const string USER = "User";
        private const string TEAM_LEAD = "TeamLead";
        private const string TEAM = "User.UserTeams.Team";
        private const string TEAM_TASK = "Team";
        private const string TASK_COMMENT = "Comments";
        private const string USER_TEAM = "User.UserTeams";
        private const string TASKS = "User.Tasks";

        public TeamLeadRolesController(IBaseRepo<TeamLead> teamLeadBaseRepo,
                                       IBaseRepo<Team> teamBaseRepo,
                                       IBaseRepo<TaskEntity> taskBaseRepo,
                                       IBaseRepo<UserTeam> userTeamBaseRepo,
                                       UserManager<ApplicationUser> userManager,
                                       IBaseRepo<RegUser> regUserBaseRepo,
                                       IAttachment attachment,
                                       IBaseRepo<Comment> commentBaseRepo)
        {
            _teamLeadBaseRepo = teamLeadBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _taskBaseRepo = taskBaseRepo;
            _userTeamBaseRepo = userTeamBaseRepo;
            _userManager = userManager;
            _regUserBaseRepo = regUserBaseRepo;
            _attachment = attachment;
            _commentBaseRepo = commentBaseRepo;
        }


        /// <summary>
        /// Displays the index view for the team lead, showing task and member counts.
        /// </summary>
        /// <returns>An IActionResult representing the result of the action.</returns>
        public async Task<IActionResult> Index()
        {

            var teamLead = await GetLoggedTeamLeadDataAsync();
            
            var teamIds = teamLead.User.UserTeams.Select(ut => ut.TeamId).ToList();
            var numberOfTasks = await _taskBaseRepo.GetCount(x => teamIds.Contains((int)x.TeamId));

            var numberOfMembers = await _userTeamBaseRepo.GetCount(x => teamIds.Contains(x.TeamId) 
                                                                    && x.UserId != teamLead.UserId);

            ViewBag.TeamMembers = numberOfMembers+1;
            ViewBag.TeamTasksCount = numberOfTasks;
            return View();
        }


        /// <summary>
        /// Displays a list of tasks with optional filtering by title and pagination support.
        /// </summary>
        /// <param name="titleSearch">Optional search filter for task titles.</param>
        /// <param name="page">Optional page number for pagination.</param>
        /// <returns>An IActionResult representing the result of the action.</returns>
        public async Task<IActionResult> Tasks(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var teamLead = await GetLoggedTeamLeadDataAsync();


            var teamIds = teamLead.User.UserTeams.Select(ut => ut.TeamId).ToList();

            var tasks = await _taskBaseRepo.GetAll(x => teamIds.Contains((int)x.TeamId) 
                                                    && (x.Title.Contains(titleSearch) 
                                                        || titleSearch == null), 
                                                    new[] {USER,TEAM_TASK,TASK_COMMENT});
            return View(await tasks.ToPagedListAsync(page??1,6));
        }



        /// <summary>
        /// Displays the form for adding a new task, with options to select teams and users.
        /// </summary>
        /// <returns>An IActionResult representing the result of the action.</returns>
        [HttpGet]
        public async Task<IActionResult> AddTask()
        {
            var teamLead = await GetLoggedTeamLeadDataAsync();

            var teamIds = teamLead.User.UserTeams.Select(ut => ut.TeamId).ToList();
            var teams = await _teamBaseRepo.GetAll(x => teamIds.Contains(x.Id));
            ViewBag.Teams = teams;
            
            var userTeams = await _userTeamBaseRepo.GetAll(x => teamIds.Contains(x.TeamId)
                                                                && x.UserId != teamLead.UserId,
                                                                new[] { USER});
            ViewBag.UserTeams = userTeams;


            return View();
        }



        /// <summary>
        /// Handles the submission of the form for adding a new task, including validation and file upload.
        /// </summary>
        /// <param name="model">The view model containing task details.</param>
        /// <returns>An IActionResult representing the result of the action.</returns>
        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["Result"] = "Insert valid data";
                ModelState.AddModelError(string.Empty, "Insert valid data");
                return RedirectToAction(nameof(AddTask));
            }
            if (!FileValidation.IsExtensionAllowed(model.Attachment))
            {
                TempData["Result"] = "Extension not allowed";
                ModelState.AddModelError(model.Attachment.FileName, "Extension not allowed");
                return RedirectToAction(nameof(AddTask));
            }
            if (!FileValidation.IsSizeAllowed(model.Attachment))
            {
                TempData["Result"] = "Size is big, max size is 1MB";
                ModelState.AddModelError(model.Attachment.FileName, "Size is big, max size is 1MB");
                return RedirectToAction(nameof(AddTask));
            }
            try
            {
                var attachment = await _attachment.UploadFile(model.Attachment, ROOT);
                var task = new TaskEntity
                {
                    Attachment = attachment,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Priority = model.Priority,
                    Status = model.Status,
                    TeamId = model.TeamId,
                    Title = model.Title,
                    UserId = model.UserId,
                };
                await _taskBaseRepo.Add(task);
                TempData["Result"] = $"{model.Title} is added successfully";
                return RedirectToAction(nameof(Tasks));

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(AddTask));
        }


        /// <summary>
        /// Displays the form for editing an existing task, pre-filling it with current task details.
        /// </summary>
        /// <param name="id">The ID of the task to be edited.</param>
        /// <returns>An IActionResult representing the result of the action.</returns>
        [HttpGet]
        public async Task<IActionResult> EditTask(int id)
        {
            var teamLead = await GetLoggedTeamLeadDataAsync();

            if (id > 0)
            {
                var task = await _taskBaseRepo.GetById(t => t.Id == id, new[] { USER, TEAM_TASK });

                if (task is not null)
                {
                    var taskEditViewModel = new TaskEditViewModel
                    {
                        Id = id,
                        Description = task.Description,
                        DueDate = task.DueDate,
                        Priority = task.Priority,
                        Status = task.Status,
                        AttachmentName = task.Attachment,
                        Title = task.Title,
                        TeamId = task.TeamId,
                        UserId = task.UserId,
                    };

                    var teamIds = teamLead.User.UserTeams.Select(ut => ut.TeamId).ToList();
                    var teams = await _teamBaseRepo.GetAll(x => teamIds.Contains(x.Id));
                    ViewBag.Teams = teams;

                    var userTeams = await _userTeamBaseRepo.GetAll(x => teamIds.Contains(x.TeamId)
                                                                && x.UserId != teamLead.UserId,
                                                                new[] { USER });
                    ViewBag.UserTeams = userTeams;

                    return View(taskEditViewModel);
                }
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }



        /// <summary>
        /// Handles the submission of the form for editing an existing task, including validation and file upload.
        /// </summary>
        /// <param name="id">The ID of the task to be updated.</param>
        /// <param name="model">The view model containing updated task details.</param>
        /// <returns>An IActionResult representing the result of the action.</returns>
        [HttpPost]
        public async Task<IActionResult> EditTask(int id, TaskEditViewModel model)
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
                return RedirectToAction(nameof(AddTask));
            }

            var task = await _taskBaseRepo.GetById(t => t.Id == id, new[] { USER, TEAM_TASK });
            var newAttachment = task.Attachment;
            if (model.Attachment != null)
            {
                if (!FileValidation.IsExtensionAllowed(model.Attachment))
                {
                    TempData["Result"] = "Extension not allowed";
                    ModelState.AddModelError(model.Attachment.FileName, "Extension not allowed");
                    return RedirectToAction(nameof(AddTask));
                }
                if (!FileValidation.IsSizeAllowed(model.Attachment))
                {
                    TempData["Result"] = "Size is big, max size is 1MB";
                    ModelState.AddModelError(model.Attachment.FileName, "Size is big, max size is 1MB");
                    return RedirectToAction(nameof(AddTask));
                }

                newAttachment = await _attachment.UploadFile(model.Attachment, ROOT);

                _attachment.DeleteFile(task.Attachment, ROOT);
            }

            try
            {
                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate;
                task.Status = model.Status;
                task.Priority = model.Priority;
                task.Attachment = newAttachment;
                task.TeamId = model.TeamId;
                task.UserId = model.UserId;

                await _taskBaseRepo.Update(task);

                TempData["Result"] = $"{model.Title} is updated successfully";
                return RedirectToAction(nameof(Tasks));
            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";

            }
            return RedirectToAction(nameof(EditTask));

        }



        /// <summary>
        /// Deletes a task by its ID, including related comments and attachments.
        /// </summary>
        /// <param name="id">The ID of the task to be deleted.</param>
        /// <returns>An IActionResult representing the result of the action.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Result"] = $"Invalid ID {id}";
                return RedirectToAction(nameof(Index));
            }
            var task = await _taskBaseRepo.GetById(x => x.Id == id, new[] { TASK_COMMENT});
            var comments = await _commentBaseRepo.GetAll(x => x.TaskId == id);
            try
            {
                _attachment.DeleteFile(task.Attachment, ROOT);
                await _commentBaseRepo.Delete(comments);
                await _taskBaseRepo.Delete(task);

                TempData["Result"] = $"{task.Title} with ID {id} is deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }
            return RedirectToAction(nameof(Index));

        }


        /// <summary>
        /// Retrieves the team lead data for the currently logged-in user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, 
        /// with a <see cref="TeamLead"/> result.</returns>
        private async Task<TeamLead> GetLoggedTeamLeadDataAsync()
        {
            var appUser = await _userManager.GetUserAsync(User);
            return await _teamLeadBaseRepo.GetById(x => x.UserId == appUser.Id,
                                                                new[] { USER, USER_TEAM, TASKS });
        }
    }
}
