using HairSalonWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HairSalonWEB.Interfaces;
using HairSalonWEB.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalonWEB.Controllers
{
    public class ClientController : Controller
    {
        public static string CurrentClientLogin { get; set; }
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
        public ClientController(DataContext context, IMaster masterRepository, IAdministrator administratorRepository, IClient clientRepository, ICompany companyRepository, IRecord recordRepository, IProcedure procedureRepository)
        {
            _context = context;
            _masterRepository = masterRepository;
            _administratorRepository = administratorRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _recordRepository = recordRepository;
            _procedureRepository = procedureRepository;
        }
        public IActionResult RecordTable(string login)
        {
            var companies = _companyRepository.GetAllCompanies();
            var masters = _masterRepository.GetAllMasters();

            var procedures = _procedureRepository.GetAllProcedures();

            ViewBag.Companies = companies;
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


            login = ClientController.CurrentClientLogin;
            var client = _clientRepository.GetClientByLogin(login);
            var clientCode = client.client_code;
            var clients = _clientRepository.GetClient(clientCode);
            List<client> clientsList = new List<client>();
            clientsList.Add(clients);
            ViewBag.Clients = clientsList;
            var records = _recordRepository.GetRecordsByClientCode(clientCode);
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
