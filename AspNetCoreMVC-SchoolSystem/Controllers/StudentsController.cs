using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers {
    public class StudentsController : Controller {
        StudentService _studentService;
        private readonly ILogger<HomeController> _logger;
        public StudentsController(StudentService studentService, ILogger<HomeController> logger) {
            _studentService = studentService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index() {
            var allStudents = _studentService.GetAll();
            return View(allStudents);
        }
        [HttpGet]
        public IActionResult Create() {
            _logger.LogWarning("volana metoda create (Get)");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentDTO newStudent) {
            await _studentService.CreateAsync(newStudent);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var studentToEdit = await _studentService.GetByIdAsync(id);
            return View(studentToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(StudentDTO studentDTO, int id) {
            await _studentService.UpdateAsync(studentDTO, id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _studentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
