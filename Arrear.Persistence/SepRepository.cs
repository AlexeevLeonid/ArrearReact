using Arrear.Domain.Enums;
using Arrear.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Persistence
{
    public class SepRepository
    {
        private DbSet<Sep> seps;
        private ApplicationContext context;

        public async Task<List<Sep>> GetSepsByStatus(SepStatus status)
        {
            return await seps.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<Sep> GetSepById(Guid id)
        {
            return await seps.Include(x => x.Manager).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task PostSep(Sep sep)
        {
            seps.Add(sep);
            await context.SaveChangesAsync();
        }

        public async Task PutSep(Sep sep)
        {
            seps.Update(sep);
            await context.SaveChangesAsync();
        }
        public SepRepository(DbSet<Sep> seps, ApplicationContext context)
        {
            this.seps = seps;
            this.context = context;
        }
    }
}
