using AspNetCoreMVC_SchoolSystem.DTO;
using AspNetCoreMVC_SchoolSystem.Services;
using AspNetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreMVC_SchoolSystem.Controllers {
    [Authorize]
    public class GradesController : Controller {
        GradeService _gradeService;

        public GradesController(GradeService gradeService) {
            _gradeService = gradeService;
        }

        [HttpGet]
        //[Route("znamky/[action]")]
        public IActionResult Index() {
            IEnumerable<GradeDTO> allGrades= _gradeService.GetAll();
            return View(allGrades);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Create() {
            FillDropdowns();
            return View();
        }

        private void FillDropdowns() {
            GradesDropdownsViewModel gradesDropdownsData = _gradeService.GetGradesDropdownsData();
            ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> CreateAsync(GradeDTO newGrade) {
            await _gradeService.CreateAsync(newGrade);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> EditAsync(int id, GradeDTO gradeDTO) {
            GradeDTO gradeToEdit = await _gradeService.FindByIdAsync(id);
            if (gradeToEdit == null) {
                return View("NotFound");
            }
            FillDropdowns();
            return View(gradeToEdit);
        }
        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> EditAsync(GradeDTO updatedGrade) {
            await _gradeService.UpdateAsync(updatedGrade);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> Delete(int id) {
            GradeDTO gradeToDelete = await _gradeService.FindByIdAsync(id);
            if (gradeToDelete == null) {
                return View("NotFound");
            }
            await _gradeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
