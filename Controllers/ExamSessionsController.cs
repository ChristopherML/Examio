using Examio.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Examio.Controllers
{
    public class ExamSessionsController : Controller
    {
        private readonly IExamSessionService _examSessionService;

        public ExamSessionsController(IExamSessionService examSessionService)
        {
            _examSessionService = examSessionService;
        }

        // GET: ExamSessions
        public async Task<IActionResult> Index(ExamSessionSearchFilterDto search)
        {
            return View(await _examSessionService
                .ExamSessionsAllOrSearchedListVM(search));
        }

        // GET: ExamSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSessionEager = await _examSessionService.FindExamSessionById((int)id);
           
            if (examSessionEager == null)
            {
                return NotFound();
            }
            
            return View(examSessionEager);
        }

        // GET: ExamSessions/Create
        public async Task<IActionResult> Create()
        {
            var examSessionFormVM = await _examSessionService.ReturnBlankExamSessionForm();

            return View(examSessionFormVM);
        }

        // POST: ExamSessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> 
            Create([Bind("Id,Name,Description,StartDate,EndDate,ExamSiteId")] ExamSessionDto examSessionDto)
        {

            if (ModelState.IsValid)
            {
                _examSessionService.SaveNewExamSession(examSessionDto);

                return RedirectToAction(nameof(Index));
            }

            //TODO Toast Notification: Update Failed

            return View(await _examSessionService.ReturnPopulatedExamSessionFormDto(examSessionDto));
        }

        // GET: ExamSessions/Edit/5 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSession = await _examSessionService.FindExamSessionById((int)id);

            if (examSession == null)
            {
                return NotFound();
            }            

            return View(await _examSessionService.ReturnPopulatedExamSessionForm(examSession));
        }

        // POST: ExamSessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> 
            Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,ExamSiteId")] ExamSessionDto examSessionDto)
        {
            if (id != examSessionDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { 

                if (await _examSessionService.UpdateExamSession(examSessionDto))
                {
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    return NotFound();
                }
            }
            return View(await _examSessionService.ReturnPopulatedExamSessionFormDto(examSessionDto));
        }

        // GET: ExamSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var examSession = await _examSessionService.FindExamSessionById((int)id);

            if (examSession == null)
            {
                return NotFound();
            }

            return View(examSession);
        }

        // POST: ExamSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _examSessionService.DeleteExamSessionById(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
