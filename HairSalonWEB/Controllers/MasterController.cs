using HairSalonWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairSalonWEB.Interfaces;
using HairSalonWEB.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalonWEB.Controllers
{
    public class MasterController : Controller
    {
        public static string CurrentMasterLogin { get; set; }
        private string _login = "";
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IMaster _masterRepository;
        private readonly IAdministrator _administratorRepository;
        private readonly IClient _clientRepository;
        private readonly ICompany _companyRepository;
        private readonly IRecord _recordRepository;
        private readonly IProcedure _procedureRepository;
        public IActionResult Index()
        {
            return View();
        }
        public MasterController(DataContext context, IMaster masterRepository, IAdministrator administratorRepository, IClient clientRepository, ICompany companyRepository, IRecord recordRepository, IProcedure procedureRepository)
        {
            _context = context;
            _masterRepository = masterRepository;
            _administratorRepository = administratorRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _recordRepository = recordRepository;
            _procedureRepository = procedureRepository;
        }
        public IActionResult MasterTable(string login)
        {
            //var masters = _masterRepository.GetAllMasters();
            //return View(masters);

            login = MasterController.CurrentMasterLogin;
            var companies = _companyRepository.GetAllCompanies();

            ViewBag.Companies = companies;
            //return View(administrators);

            var masterRecords = _masterRepository.GetMasterByLogin(login);
            List<master> masterRecordsList = new List<master>();
            masterRecordsList.Add(masterRecords);
            return View(masterRecordsList);
        }
        public IActionResult ProceduresTable()
        {
            var procedures = _procedureRepository.GetAllProcedures();
            return View(procedures);
        }

        public IActionResult RecordTable(string login)
        {
            var companies = _companyRepository.GetAllCompanies();
            var clients = _clientRepository.GetAllClients();

            var procedures = _procedureRepository.GetAllProcedures();

            ViewBag.Companies = companies;
            ViewBag.Clients = clients;
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


            login = MasterController.CurrentMasterLogin;
            var master = _masterRepository.GetMasterByLogin(login);
            var masterCode = master.master_code;
            var masters = _masterRepository.GetMaster(masterCode);
            List<master> mastersList = new List<master>();
            mastersList.Add(masters);
            ViewBag.Masters = mastersList;
            var records = _recordRepository.GetRecordsByMasterCode(masterCode);
            //List<recordd>
            return View(records);
        }
        private List<recordd> GetExistingRecords()
        {
            List<recordd> existingRecords = _recordRepository.GetAllRecords().ToList(); // Пример использования репозитория

            return existingRecords;
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
        public IActionResult AddProcedure(procedures procedure)
        {
            if (procedure.procedure_code != 0)
            {
                var existingProcedure = _procedureRepository.GetProcedure(procedure.procedure_code);
                if (existingProcedure != null)
                {
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
            return RedirectToAction("ProceduresTable");
        }
        [HttpPost]
        public IActionResult AddRecord(recordd record)
        {
            if (record.record_code != 0)
            {
                var existingRecord = _recordRepository.GetRecord(record.record_code);
                if (existingRecord != null)
                {
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
    }
}
