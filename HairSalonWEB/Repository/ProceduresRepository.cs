using HairSalonWEB.Interfaces;
using HairSalonWEB.Models;

namespace HairSalonWEB.Repository
{
    public class ProceduresRepository : IProcedure
    {
        private DataContext _context;

        public ProceduresRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<procedures> GetAllProcedures()
        {
            return _context.Procedures;
        }

        public void AddProcedure(procedures procedure)
        {
            _context.Procedures.Add(procedure);
            _context.SaveChanges();
        }

        public procedures GetProcedure(int id)
        {
            return _context.Procedures.Find(id);
        }

        public void UpdateProcedure(procedures procedure)
        {
            _context.Procedures.Update(procedure);
            _context.SaveChanges();
        }

        public void DeleteProcedure(procedures procedure)
        {
            _context.Procedures.Remove(procedure);
            _context.SaveChanges();
        }
    }
}
