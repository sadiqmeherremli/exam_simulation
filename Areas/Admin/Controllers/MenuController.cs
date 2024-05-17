using Database.Entities.Concretes;
using Database.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Identity_Full.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(IDoctorRepository doctorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _doctorRepository = doctorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _doctorRepository.GetAllAsync();
            return View(datas);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }
            string path = _webHostEnvironment.WebRootPath + @"\Upload\manage\";
            string fileName = Guid.NewGuid() + doctor.ImgFile.FileName;
            string fullPath = Path.Combine(path, fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                doctor.ImgFile.CopyTo(stream);
            }
            doctor.ImgUrl = fileName;

            await _doctorRepository.AddAsync(doctor);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Remove(int id)
        {
            await _doctorRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _doctorRepository.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]

        public IActionResult Update(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }
            if (doctor.ImgFile != null)
            {
                string path = _webHostEnvironment.WebRootPath + @"\Upload\manage\";
                string fileName = Guid.NewGuid() + doctor.ImgFile.FileName;
                string fullPath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    doctor.ImgFile.CopyTo(stream);
                }
                doctor.ImgUrl = fileName;
            }
            _doctorRepository.UpdateAsync(doctor);
            return RedirectToAction("Index");
        }
    }
}
