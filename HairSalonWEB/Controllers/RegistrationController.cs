using HairSalonWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairSalonWEB.Interfaces;
using HairSalonWEB.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalonWEB.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IMaster _masterRepository;
        private readonly IAdministrator _administratorRepository;
        private readonly IClient _clientRepository;
        private readonly ICompany _companyRepository;
        private readonly IRecord _recordRepository;
        private readonly IProcedure _procedureRepository;
        public RegistrationController(DataContext context, IMaster masterRepository, IAdministrator administratorRepository, IClient clientRepository, ICompany companyRepository, IRecord recordRepository, IProcedure procedureRepository)
        {
            _context = context;
            _masterRepository = masterRepository;
            _administratorRepository = administratorRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _recordRepository = recordRepository;
            _procedureRepository = procedureRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AddClient(client client)
        {
            try
            {
                _clientRepository.AddClient(client);
                return RedirectToAction("Login", "Account");
            }
            catch
            {
                TempData["ErrorMessage"] = "Такой логин или пароль уже существует. Попробуйте новый.";
                return RedirectToAction("Register");
            }
        }
    }
}
