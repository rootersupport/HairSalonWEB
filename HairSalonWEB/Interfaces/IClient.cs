using HairSalonWEB.Models;
namespace HairSalonWEB.Interfaces
{
    public interface IClient
    {
        IEnumerable<client> GetAllClients();
        void AddClient(client client);
        client GetClient(int id);
        client GetClientByLogin(string login);
        void UpdateClient(client client);
        void DeleteClient(client client);
    }
}
