using HairSalonWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairSalonWEB.Interfaces;
using HairSalonWEB.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalonWEB.Controllers
{
    public class HomeController : Controller
    {
        public static string CurrentAdminLogin { get; set; }
        private string _login = "";
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IMaster _masterRepository;
        private readonly IAdministrator _administratorRepository;
        private readonly IClient _clientRepository;
        private readonly ICompany _companyRepository;
        private readonly IRecord _recordRepository;
        private readonly IProcedure _procedureRepository;
        //public HomeController(DataContext context)
        //{
        //    _context = context;
        //}
        public HomeController(DataContext context, IMaster masterRepository, IAdministrator administratorRepository, IClient clientRepository, ICompany companyRepository, IRecord recordRepository, IProcedure procedureRepository)
        {
            _context = context;
            _masterRepository = masterRepository;
            _administratorRepository = administratorRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _recordRepository = recordRepository;
            _procedureRepository = procedureRepository;
        }
        public IActionResult Index(string login)
        {
            var admin = _administratorRepository.GetAdminByLogin(login);
            ViewBag.CurrentAdmin = admin;
            return View();
        }
        public IActionResult MasterTable()
        {
            var masters = _masterRepository.GetAllMasters();
            return View(masters);
        }

        public IActionResult AdminTable(string login)
        {
            //var administrators = _administratorRepository.GetAllAdmins();
            login = HomeController.CurrentAdminLogin;
            var companies = _companyRepository.GetAllCompanies();

            ViewBag.Companies = companies;
            //return View(administrators);

            var adminRecords = _administratorRepository.GetAdminByLogin(login);
            List<administrator> adminRecordsList = new List<administrator>();
            adminRecordsList.Add(adminRecords);
            return View(adminRecordsList);
        }
        public IActionResult ClientTable()
        {
            var clients = _clientRepository.GetAllClients();
            return View(clients);
        }

        public IActionResult CompanyTable()
        {
            var companies = _companyRepository.GetAllCompanies();
            return View(companies);
        }
        public IActionResult ProceduresTable()
        {
            var procedures = _procedureRepository.GetAllProcedures();
            return View(procedures);
        }

        public IActionResult RecordTable()
        {
            var records = _recordRepository.GetAllRecords();
            var companies = _companyRepository.GetAllCompanies();
            var clients = _clientRepository.GetAllClients();
            var masters = _masterRepository.GetAllMasters();
            var procedures = _procedureRepository.GetAllProcedures();

            ViewBag.Companies = companies;
            ViewBag.Clients = clients;
            ViewBag.Masters = masters;
            ViewBag.Procedures = procedures;
            // Получите список существующих записей
            List<recordd> existingRecords = GetExistingRecords();
            List<int> durations = new List<int>();
            foreach (var record in existingRecords)
            {
                // Получите код процедуры из записи
                var procedureCode = record.procedure_code;

                // Используйте ваш репозиторий или сервис для получения процедуры по коду
                var procedure = _procedureRepository.GetProcedure(procedureCode);

                // Проверка, что процедура найдена
                if (procedure != null)
                {
                    // Извлеките длительность процедуры
                    var procedureDuration = procedure.procedure_time;

                    // Добавьте длительность процедуры в объект записи
                    durations.Add(procedureDuration);
                }
            }
            // Передайте список существующих записей в представление через ViewBag или модель
            ViewBag.ExistingRecords = existingRecords;
            ViewBag.Durations = durations;
            // Возврат представления

            return View(records);
        }
        [HttpPost]
        public IActionResult DeleteRecord(int id)
        {
            var record = _recordRepository.GetRecord(id);
            if (record == null)
            {
                return NotFound();
            }

            _recordRepository.DeleteRecord(record);
            return RedirectToAction("RecordTable");
        }
        [HttpPost]
        public IActionResult DeleteMaster(int id)
        {
            var master = _masterRepository.GetMaster(id);
            if (master == null)
            {
                return NotFound();
            }

            _masterRepository.DeleteMaster(master);
            return RedirectToAction("MasterTable");
        }
        [HttpPost]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = _administratorRepository.GetAdmin(id);
            if (admin == null)
            {
                return NotFound();
            }

            _administratorRepository.DeleteAdmin(admin);
            return RedirectToAction("AdminTable");
        }
        [HttpPost]
        public IActionResult DeleteClient(int id)
        {
            var client = _clientRepository.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }

            _clientRepository.DeleteClient(client);
            return RedirectToAction("ClientTable");
        }
        [HttpPost]
        public IActionResult DeleteCompany(int id)
        {
            var company = _companyRepository.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }

            _companyRepository.DeleteCompany(company);
            return RedirectToAction("CompanyTable");
        }
        [HttpPost]
        public IActionResult DeleteProcedure(int id)
        {
            var procedure = _procedureRepository.GetProcedure(id);
            if (procedure == null)
            {
                return NotFound();
            }

            _procedureRepository.DeleteProcedure(procedure);
            return RedirectToAction("ProceduresTable");
        }
        [HttpPost]
        public IActionResult AddMaster(master master)
        {
            if (master.master_code != 0)
            {
                var existingMaster = _masterRepository.GetMaster(master.master_code);
                if (existingMaster != null)
                {
                    // Применить изменения к существующему администратору
                    existingMaster.master_code = master.master_code;
                    existingMaster.master_surname = master.master_surname;
                    existingMaster.master_name = master.master_name;
                    existingMaster.master_patronymic = master.master_patronymic;
                    existingMaster.master_gender = master.master_gender;
                    existingMaster.master_phone_number = master.master_phone_number;
                    existingMaster.master_status = master.master_status;
                    existingMaster.master_profile = master.master_profile;
                    existingMaster.master_salary = master.master_salary;
                    existingMaster.master_login = master.master_login;
                    existingMaster.master_password = master.master_password;

                    _masterRepository.UpdateMaster(existingMaster);
                    return RedirectToAction("MasterTable");
                }
            }
            else
            {
                try
                {
                    _masterRepository.AddMaster(master);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Такой логин или пароль уже существует. Попробуйте новый.";
                }
            }
            return RedirectToAction("MasterTable");
        }
        [HttpPost]
        public IActionResult AddAdmin(administrator admin)
        {
            if (admin.admin_id != 0)
            {
                var existingAdmin = _administratorRepository.GetAdmin(admin.admin_id);
                if (existingAdmin != null)
                {
                    // Применить изменения к существующему администратору
                    existingAdmin.admin_id = admin.admin_id;
                    existingAdmin.admin_surname = admin.admin_surname;
                    existingAdmin.admin_name = admin.admin_name;
                    existingAdmin.admin_patronymic = admin.admin_patronymic;
                    existingAdmin.admin_gender = admin.admin_gender;
                    existingAdmin.admin_phone_number = admin.admin_phone_number;
                    existingAdmin.admin_status = admin.admin_status;
                    existingAdmin.admin_login = admin.admin_login;
                    existingAdmin.admin_password = admin.admin_password;

                    _administratorRepository.UpdateAdmin(existingAdmin);
                    return RedirectToAction("AdminTable");
                }
            }
            else
            {
                try
                {
                    _administratorRepository.AddAdmin(admin);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Такой логин или пароль уже существует. Попробуйте новый.";
                }
            }
            return RedirectToAction("AdminTable");
        }
        [HttpPost]
        public IActionResult AddClient(client client)
        {
            if (client.client_code != 0)
            {
                var existingClient = _clientRepository.GetClient(client.client_code);
                if (existingClient != null)
                {
                    // Применить изменения к существующему администратору
                    existingClient.client_code = client.client_code;
                    existingClient.client_surname = client.client_surname;
                    existingClient.client_name = client.client_name;
                    existingClient.client_patronymic = client.client_patronymic;
                    existingClient.client_gender = client.client_gender;
                    existingClient.client_phone_number = client.client_phone_number;
                    existingClient.client_login = client.client_login;
                    existingClient.client_password = client.client_password;

                    _clientRepository.UpdateClient(existingClient);
                    return RedirectToAction("ClientTable");
                }
            }
            else
            {
                try
                {
                    _clientRepository.AddClient(client);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Такой логин или пароль уже существует. Попробуйте новый.";
                }
            }
            return RedirectToAction("ClientTable");
        }
        [HttpPost]
        public IActionResult AddCompany(company company)
        {
            if (company.company_code != 0)
            {
                var existingCompany = _companyRepository.GetCompany(company.company_code);
                if (existingCompany != null)
                {
                    // Применить изменения к существующему администратору
                    existingCompany.company_code = company.company_code;
                    existingCompany.company_name = company.company_name;
                    existingCompany.company_phone_number = company.company_phone_number;
                    existingCompany.company_address = company.company_address;
                    existingCompany.director_full_name = company.director_full_name;

                    _companyRepository.UpdateCompany(existingCompany);
                    return RedirectToAction("CompanyTable");
                }
            }
            else
            {
                try
                {
                    _companyRepository.AddCompany(company);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Ошибка!";
                }
            }
            return RedirectToAction("CompanyTable");
        }
        [HttpPost]
        public IActionResult AddProcedure(procedures procedure)
        {
            if (procedure.procedure_code != 0)
            {
                var existingProcedure = _procedureRepository.GetProcedure(procedure.procedure_code);
                if (existingProcedure != null)
                {
                    // Применить изменения к существующему администратору
                    existingProcedure.procedure_code = procedure.procedure_code;
                    existingProcedure.procedure_name = procedure.procedure_name;
                    existingProcedure.procedure_price = procedure.procedure_price;
                    existingProcedure.procedure_time = procedure.procedure_time;

                    _procedureRepository.UpdateProcedure(existingProcedure);
                    return RedirectToAction("ProceduresTable");
                }
            }
            else
            {
                try
                {
                    _procedureRepository.AddProcedure(procedure);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Ошибка!";
                }
            }
            return RedirectToAction("CompanyTable");
        }
        [HttpPost]
        public IActionResult AddRecord(recordd record)
        {
            if (record.record_code != 0)
            {
                var existingRecord = _recordRepository.GetRecord(record.record_code);
                if (existingRecord != null)
                {
                    // Применить изменения к существующему администратору
                    existingRecord.record_code = record.record_code;
                    existingRecord.company_code = record.company_code;
                    existingRecord.client_code = record.client_code;
                    existingRecord.master_code = record.master_code;
                    existingRecord.procedure_code = record.procedure_code;
                    existingRecord.record_time = record.record_time;

                    _recordRepository.UpdateRecord(existingRecord);
                    return RedirectToAction("RecordTable");
                }
            }
            else
            {
                try
                {
                    _recordRepository.AddRecord(record);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Ошибка!";
                }
            }
            return RedirectToAction("RecordTable");
        }
        private List<recordd> GetExistingRecords()
        {
            // Здесь вам нужно выполнить код для получения существующих записей из вашего источника данных.
            // Я предполагаю, что у вас есть доступ к соответствующему репозиторию или сервису для получения записей.

            List<recordd> existingRecords = _recordRepository.GetAllRecords().ToList(); // Пример использования репозитория

            return existingRecords;
        }
    }
}