using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers {
    [Authorize]
    public class SubjectsController : Controller {
        SubjectService _subjectService;
        private readonly ILogger<HomeController> _logger;
        public SubjectsController(SubjectService subjectService, ILogger<HomeController> logger) {
            _subjectService = subjectService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index() {
            var allSubjects = _subjectService.GetAll();
            return View(allSubjects);
        }
        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Create() {
            _logger.LogWarning("volana metoda create (Get)");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> CreateAsync(SubjectDTO newSubject) {
            if (!ModelState.IsValid) {
                return View(newSubject);
            }
            await _subjectService.CreateAsync(newSubject);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> EditAsync(int id) {
            var subjectToEdit = await _subjectService.GetByIdAsync(id);
            if (subjectToEdit == null) {
                return View("NotFound");
            }
            return View(subjectToEdit);
        }
        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> EditAsync(SubjectDTO subjectDTO, int id) {
            await _subjectService.UpdateAsync(subjectDTO, id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Headmaster, Admin")]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _subjectService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
