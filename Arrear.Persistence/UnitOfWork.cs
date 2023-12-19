using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public UserRepository Users => new UserRepository(
            _context.Customers, _context.Managers, _context);
        public SepRepository Seps =>
            new SepRepository(_context.Seps, _context) ??
            throw new NullReferenceException();

        public async void Save() => await _context.SaveChangesAsync();
    }
}
