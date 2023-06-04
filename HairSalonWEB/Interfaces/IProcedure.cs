using HairSalonWEB.Models;
namespace HairSalonWEB.Interfaces
{
    public interface IProcedure
    {
        IEnumerable<procedures> GetAllProcedures();
        void AddProcedure(procedures procedure);
        procedures GetProcedure(int id);
        void UpdateProcedure(procedures procedure);
        void DeleteProcedure(procedures procedure);
    }
}
