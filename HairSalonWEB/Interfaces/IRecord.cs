using HairSalonWEB.Models;
namespace HairSalonWEB.Interfaces
{
    public interface IRecord
    {
        IEnumerable<recordd> GetAllRecords();
        void AddRecord(recordd record);
        recordd GetRecord(int id);
        IEnumerable<recordd> GetRecordsByMasterCode(int masterCode);
        IEnumerable<recordd> GetRecordsByClientCode(int clientCode);
        void UpdateRecord(recordd record);
        void DeleteRecord(recordd record);
    }
}
