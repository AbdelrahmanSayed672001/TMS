using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TMS_V1.Areas.Backend.Controllers
{
    [Area(areaName: "Backend")]
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

        public async Task<IActionResult> Index(string titleSearch, int? page)
        {
            ViewBag.CurrentFilter = titleSearch;

            var tasks = await _taskBaseRepo.GetFiltered(t => t.Title.Contains(titleSearch)
                                                            || titleSearch == null,
                                                        new[] { USER, TEAM, COMMENTS, USER_COMMENTS });
            return View(await tasks.ToPagedListAsync(page ?? 1, 6));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Teams = await _teamBaseRepo.GetAll();
           
            return View();
        }

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
                        TeamId = (int)task.TeamId,
                        UserId = task.UserId,
                    };

                    ViewBag.Teams = await _teamBaseRepo.GetAll();
                    ViewBag.Users = await _regUserBaseRepo.GetAll(new[] { USER });
                    return View(taskEditViewModel);
                }
                TempData["Result"] = $"Not Found any task with ID {id}";
                return RedirectToAction(nameof(Index));
            }
            TempData["Result"] = $"Invalid ID {id}";
            return RedirectToAction(nameof(Index));
        }


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
                return RedirectToAction(nameof(Add));
            }

            var task = await _taskBaseRepo.GetById(t => t.Id == id, new[] { USER, TEAM });
            var newAttachment = task.Attachment;
            if (model.Attachment != null)
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Result"] = "Something went wrong";

            }
            return RedirectToAction(nameof(Edit));

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Result"] = $"Invalid ID {id}";
                return RedirectToAction(nameof(Index));
            }
            var task = await _taskBaseRepo.GetById(x => x.Id == id, new[] { COMMENTS });
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
    }
}