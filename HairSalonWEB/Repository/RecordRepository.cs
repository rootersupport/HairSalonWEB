using HairSalonWEB.Interfaces;
using HairSalonWEB.Models;

namespace HairSalonWEB.Repository
{
    public class RecordRepository : IRecord
    {
        private DataContext _context;

        public RecordRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<recordd> GetAllRecords()
        {
            return _context.Record;
        }
        public IEnumerable<recordd> GetRecordsByMasterCode(int masterCode)
        {
            return _context.Record.Where(r => r.master_code == masterCode).ToList();
        }
        public IEnumerable<recordd> GetRecordsByClientCode(int clientCode)
        {
            return _context.Record.Where(r => r.client_code == clientCode).ToList();
        }
        public void AddRecord(recordd recordd)
        {
            _context.Record.Add(recordd);
            _context.SaveChanges();
        }

        public recordd GetRecord(int id)
        {
            return _context.Record.Find(id);
        }

        public void UpdateRecord(recordd recordd)
        {
            _context.Record.Update(recordd);
            _context.SaveChanges();
        }

        public void DeleteRecord(recordd recordd)
        {
            _context.Record.Remove(recordd);
            _context.SaveChanges();
        }
    }
}
