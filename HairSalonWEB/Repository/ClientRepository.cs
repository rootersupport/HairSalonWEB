using HairSalonWEB.Interfaces;
using HairSalonWEB.Models;

namespace HairSalonWEB.Repository
{
    public class ClientRepository : IClient
    {
        private DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<client> GetAllClients()
        {
            return _context.Client;
        }

        public void AddClient(client client)
        {
            _context.Client.Add(client);
            _context.SaveChanges();
        }

        public client GetClient(int id)
        {
            return _context.Client.Find(id);
        }
        public client GetClientByLogin(string login)
        {
            return _context.Client.SingleOrDefault(client => client.client_login == login);
        }
        public void UpdateClient(client client)
        {
            _context.Client.Update(client);
            _context.SaveChanges();
        }

        public void DeleteClient(client client)
        {
            _context.Client.Remove(client);
            _context.SaveChanges();
        }
    }
}
