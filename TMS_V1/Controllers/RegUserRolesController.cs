using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS_V1.Models;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Manages role-specific actions for regular users.
    /// This controller is accessible only by users with the <see cref="Role.REG_USER"/> role.
    /// </summary>
    [Authorize(Roles = Role.REG_USER)]
    public class RegUserRolesController : Controller
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

        public RegUserRolesController(IBaseRepo<TeamLead> teamLeadBaseRepo,
                                      IBaseRepo<RegUser> regUserBaseRepo,
                                      IBaseRepo<Team> teamBaseRepo,
                                      IBaseRepo<TaskEntity> taskBaseRepo,
                                      IBaseRepo<UserTeam> userTeamBaseRepo,
                                      IBaseRepo<Comment> commentBaseRepo,
                                      UserManager<ApplicationUser> userManager,
                                      IAttachment attachment)
        {
            _teamLeadBaseRepo = teamLeadBaseRepo;
            _regUserBaseRepo = regUserBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _taskBaseRepo = taskBaseRepo;
            _userTeamBaseRepo = userTeamBaseRepo;
            _commentBaseRepo = commentBaseRepo;
            _userManager = userManager;
            _attachment = attachment;
        }


        /// <summary>
        /// Displays the dashboard view with task statistics for the logged-in regular user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        public async Task<IActionResult> Index()
        {

            var regUser = await GetLoggedRegUserDataAsync();

            var numberOfAllTasks = await _taskBaseRepo.GetCount(x => x.UserId == regUser.UserId);

            var numberOfToDoTasks = await _taskBaseRepo.GetCount(x => x.UserId == regUser.UserId
                                                                    && x.Status == Status.TODO);

            var numberOfCompletedTasks = await _taskBaseRepo.GetCount(x => x.UserId == regUser.UserId
                                                                    && x.Status == Status.COMPLETED);

            var numberOfInProgressTasks = await _taskBaseRepo.GetCount(x => x.UserId == regUser.UserId
                                                                    && x.Status == Status.IN_PROGRESS);

            ViewBag.NumberOfToDoTasks = numberOfToDoTasks;
            ViewBag.NumberOfCompletedTasks = numberOfCompletedTasks;
            ViewBag.NumberOfInProgressTasks = numberOfInProgressTasks;
            ViewBag.NumberOfAllTasks = numberOfAllTasks;
            return View();
        }


        /// <summary>
        /// Displays a paginated list of tasks for the logged-in regular user, optionally filtered by title.
        /// </summary>
        /// <param name="titleSearch">The optional search term for filtering tasks by title.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        public async Task<IActionResult> Tasks(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var regUser = await GetLoggedRegUserDataAsync();

            var tasks = await _taskBaseRepo.GetAll(x => x.UserId == regUser.UserId
                                                    && (x.Title.Contains(titleSearch)
                                                        || titleSearch == null),
                                                    new[] { USER, TEAM_TASK, TASK_COMMENT });
            return View(await tasks.ToPagedListAsync(page ?? 1, 6));
        }



        /// <summary>
        /// Displays the view for adding a new task.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        [HttpGet]
        public async Task<IActionResult> AddTask()
        {
            var regUser = await GetLoggedRegUserDataAsync();

            var teamIds = regUser.User.UserTeams.Select(ut => ut.TeamId).ToList();
            var teams = await _teamBaseRepo.GetAll(x => teamIds.Contains(x.Id));
            ViewBag.Teams = teams;


            ViewBag.AssignedTo = regUser.UserId;


            return View();
        }



        /// <summary>
        /// Handles the form submission for adding a new task.
        /// </summary>
        /// <param name="model">The view model containing task details to add.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
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
        /// Displays the view for editing an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to edit.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
        [HttpGet]
        public async Task<IActionResult> EditTask(int id)
        {
            var teamLead = await GetLoggedRegUserDataAsync();

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


                    return View(taskEditViewModel);
                }
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }




        /// <summary>
        /// Handles the form submission for updating an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="model">The view model containing task details to update.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the operation.</returns>
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
            var task = await _taskBaseRepo.GetById(x => x.Id == id, new[] { TASK_COMMENT });
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

        [HttpGet]
        public async Task<IActionResult> AddComment(int id)
        {

            if (id <= 0)
            {
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Tasks));
            }

            var task = await _taskBaseRepo.GetById(id);
            var user = await GetLoggedRegUserDataAsync();
            if (task == null)
            {
                TempData["Result"] = $"Invalid ID {task}";
                return RedirectToAction(nameof(Tasks));
            }

            ViewBag.TaskTitle = task.Title;
            ViewBag.TaskId = task.Id;
            ViewBag.UserId = user.UserId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {

            if (model.TaskId <= 0)
            {
                TempData["Result"] = $"Not Found any task with ID {model.TaskId}";
                return RedirectToAction(nameof(Tasks));
            }

            if (!ModelState.IsValid)
            {
                TempData["Result"] = "Insert valid data";
                ModelState.AddModelError(string.Empty, "Insert valid data");
                return RedirectToAction(nameof(AddTask));
            }

            var task = await _taskBaseRepo.GetById(model.TaskId);
            if (task == null)
            {
                TempData["Result"] = $"Invalid ID {task}";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var comment = new Comment
                {
                    Description = model.Description,
                    TaskId = model.TaskId,
                    UserId = model.UserId,
                };

                await _commentBaseRepo.Add(comment);
                TempData["Result"] = $"Your comment is added successfully";
                return RedirectToAction(nameof(Tasks));

            }
            catch(Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(AddComment));

        }

        /// <summary>
        /// Retrieves the regular user data for the currently logged-in user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, 
        /// with a <see cref="RegUser"/> result.</returns>
        private async Task<RegUser> GetLoggedRegUserDataAsync()
        {
            var appUser = await _userManager.GetUserAsync(User);
            return await _regUserBaseRepo.GetById(x => x.UserId == appUser.Id,
                                                                new[] { USER, USER_TEAM, TASKS });
        }
    }
}
