using HairSalonWEB.Models;
namespace HairSalonWEB.Interfaces

{
    public interface IAdministrator
    {
        IEnumerable<administrator> GetAllAdmins();
        void AddAdmin(administrator admin);
        administrator GetAdmin(int id);
        administrator GetAdminByLogin(string login);
        administrator GetAdminRecords(string login);
        void UpdateAdmin(administrator administrator);
        void DeleteAdmin(administrator administrator);
    }
}
