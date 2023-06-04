using HairSalonWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairSalonWEB.Interfaces;
using HairSalonWEB.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalonWEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IMaster _masterRepository;
        private readonly IAdministrator _administratorRepository;
        private readonly IClient _clientRepository;
        private readonly ICompany _companyRepository;
        private readonly IRecord _recordRepository;
        private readonly IProcedure _procedureRepository;
        public AccountController(DataContext context, IMaster masterRepository, IAdministrator administratorRepository, IClient clientRepository, ICompany companyRepository, IRecord recordRepository, IProcedure procedureRepository)
        {
            _context = context;
            _masterRepository = masterRepository;
            _administratorRepository = administratorRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _recordRepository = recordRepository;
            _procedureRepository = procedureRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var admin = _administratorRepository.GetAdminByLogin(model.login);
            var master = _masterRepository.GetMasterByLogin(model.login);
            var client = _clientRepository.GetClientByLogin(model.login);

            if (admin != null && admin.admin_password == model.password)
            {
                HomeController.CurrentAdminLogin = admin.admin_login;
                return RedirectToAction("Index", "Home", new { login = admin.admin_login });
            }
            else if (master != null && master.master_password == model.password)
            {
                MasterController.CurrentMasterLogin = master.master_login;
                return RedirectToAction("Index", "Master", new { login = master.master_login });
            }
            else if (client != null && client.client_password == model.password)
            {
                ClientController.CurrentClientLogin = client.client_login;
                return RedirectToAction("Index", "Client", new { login = client.client_login });
            }
            else
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View("Login", model);
            }
        }
    }
}
