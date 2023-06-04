using HairSalonWEB.Models;
namespace HairSalonWEB.Interfaces
{
        public interface IMaster
        {
            IEnumerable<master> GetAllMasters();
            void AddMaster(master master);
            master GetMaster(int id);
            master GetMasterByLogin(string login);
            void UpdateMaster(master master);
            void DeleteMaster(master master);
        }
}
