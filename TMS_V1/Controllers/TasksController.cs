using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using TMS_V1.ViewModels;
using X.PagedList;

namespace TMS_V1.Controllers
{
    /// <summary>
    /// Provides actions for managing tasks within the system.
    /// Requires users to have the ADMIN role to access these actions.
    /// </summary>
    [Authorize(Roles =Role.ADMIN)]
    public class TasksController : Controller
    {
        private readonly IBaseRepo<TaskEntity> _taskBaseRepo;
        private readonly IBaseRepo<Team> _teamBaseRepo;
        private readonly IBaseRepo<RegUser> _regUserBaseRepo;
        private readonly IBaseRepo<Comment> _commentBaseRepo;
        private readonly IAttachment _attachment;
        private const string ROOT = "Attachments";
        private const string USER = "User";
        private const string TEAM = "Team";
        private const string COMMENTS = "Comments";
        private const string USER_COMMENTS = "Comments.User";
        public TasksController(IBaseRepo<TaskEntity> taskBaseRepo,
                               IBaseRepo<Team> teamBaseRepo,
                               IAttachment attachment,
                               IBaseRepo<RegUser> regUserBaseRepo,
                               IBaseRepo<Comment> commentBaseRepo)
        {
            _taskBaseRepo = taskBaseRepo;
            _teamBaseRepo = teamBaseRepo;
            _attachment = attachment;
            _regUserBaseRepo = regUserBaseRepo;
            _commentBaseRepo = commentBaseRepo;
        }


        /// <summary>
        /// Displays a list of tasks with optional search and pagination.
        /// </summary>
        /// <param name="titleSearch">The search string to filter tasks by title.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <returns>The view displaying the list of tasks.</returns>
        public async Task<IActionResult> Index(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var tasks = await _taskBaseRepo.GetFiltered(t=>t.Title.Contains(titleSearch) 
                                                            || titleSearch==null, 
                                                        new[] { USER, TEAM, COMMENTS, USER_COMMENTS });
            return View(await tasks.ToPagedListAsync(page ?? 1, 6));
        }



        /// <summary>
        /// Displays the form for adding a new task.
        /// </summary>
        /// <returns>The view for adding a new task.</returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Teams = await _teamBaseRepo.GetAll();
            return View();
        }



        /// <summary>
        /// Handles the submission of the form to add a new task.
        /// </summary>
        /// <param name="model">The model containing the task details.</param>
        /// <returns>Redirects to the index view or back to the add view if there are validation errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Result"] = "Insert valid data";
                ModelState.AddModelError(string.Empty, "Insert valid data");
                return RedirectToAction(nameof(Add));
            }
            if (!FileValidation.IsExtensionAllowed(model.Attachment))
            {
                TempData["Result"] = "Extension not allowed";
                ModelState.AddModelError(model.Attachment.FileName, "Extension not allowed");
                return RedirectToAction(nameof(Add));
            }
            if (!FileValidation.IsSizeAllowed(model.Attachment))
            {
                TempData["Result"] = "Size is big, max size is 1MB";
                ModelState.AddModelError(model.Attachment.FileName, "Size is big, max size is 1MB");
                return RedirectToAction(nameof(Add));
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
                };
                await _taskBaseRepo.Add(task);
                TempData["Result"] = $"{model.Title} is added successfully";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";
            }

            return RedirectToAction(nameof(Add));
        }



        /// <summary>
        /// Displays the form for editing an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to be edited.</param>
        /// <returns>The view for editing the task or redirects to the index view if the task is not found.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var task = await _taskBaseRepo.GetById(t => t.Id == id, new[] { USER, TEAM });

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

                    ViewBag.Teams = await _teamBaseRepo.GetAll();
                    ViewBag.Users = await _regUserBaseRepo.GetAll(new[] {USER});
                    return View(taskEditViewModel);
                }
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }



        /// <summary>
        /// Handles the submission of the form to update an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to be updated.</param>
        /// <param name="model">The model containing the updated task details.</param>
        /// <returns>Redirects to the index view or back to the edit view if there are validation errors.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskEditViewModel model)
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
                return RedirectToAction(nameof(Edit));
            }

            var task = await _taskBaseRepo.GetById(t => t.Id == id, new[] { USER, TEAM });
            var newAttachment = task.Attachment;
            if (model.Attachment != null)
            {
                if (!FileValidation.IsExtensionAllowed(model.Attachment))
                {
                    TempData["Result"] = "Extension not allowed";
                    ModelState.AddModelError(model.Attachment.FileName, "Extension not allowed");
                    return RedirectToAction(nameof(Edit));
                }
                if (!FileValidation.IsSizeAllowed(model.Attachment))
                {
                    TempData["Result"] = "Size is big, max size is 1MB";
                    ModelState.AddModelError(model.Attachment.FileName, "Size is big, max size is 1MB");
                    return RedirectToAction(nameof(Edit));
                }

                newAttachment = await _attachment.UploadFile(model.Attachment, ROOT);

                _attachment.DeleteFile(task.Attachment,ROOT);
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";

            }
            return RedirectToAction(nameof(Edit));

        }



        /// <summary>
        /// Deletes a task and its associated comments and attachment.
        /// </summary>
        /// <param name="id">The ID of the task to be deleted.</param>
        /// <returns>Redirects to the index view.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if(id<= 0 )
            {
                TempData["Result"] = $"Invalid ID {id}";
                return RedirectToAction(nameof(Index));
            }
            var task = await _taskBaseRepo.GetById(x => x.Id == id, new[] {COMMENTS});
            //var comments = await _commentBaseRepo.GetAll(x => x.TaskId == id);
            try
            {
                _attachment.DeleteFile(task.Attachment, ROOT);
                //await _commentBaseRepo.Delete(comments);
                await _taskBaseRepo.Delete(task);

                TempData["Result"] = $"{task.Title} with ID {id} is deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                TempData["Result"] = "Something went wrong";
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
