using Arrear.Domain.AbstractCore;
using Arrear.Domain.Implementation;
using Microsoft.EntityFrameworkCore;

using Arrear.Domain.Request;
using Arrear.Domain.Enums;

namespace Arrear.Persistence
{
    public class UserRepository
    {
        DbSet<Customer> customers;
        DbSet<Manager> managers;
        ApplicationContext context;

        public UserRepository(DbSet<Customer> customers, DbSet<Manager> managers, ApplicationContext context)
        {
            this.customers = customers;
            this.managers = managers;
            this.context = context;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await customers.Include(x => x.seps).
                FirstOrDefaultAsync(x => x.Id == id) as User ??
                await managers.Include(x => x.seps).
                FirstOrDefaultAsync(x => x.Id == id) as User ??
                throw new ArgumentException("wrong Id");
        }


        public async Task<User> LoginAsync(LoginDetails details)
        {
            return await customers.Include(x => x.seps).
                FirstOrDefaultAsync(p => p.Name == details.Name && p.Password == details.Password) as User ??
                await managers.Include(x => x.seps).
                FirstOrDefaultAsync(p => p.Name == details.Name && p.Password == details.Password) as User ??
                throw new ArgumentException("wrong details");
        }

        public async Task PostUserAsync(User user)
        {
            if (user is Customer customer) customers.Add(customer);
            else if (user is Manager manager) managers.Add(manager);
            else throw new ArgumentException("?");
            await context.SaveChangesAsync();
        }

        public async Task PutUserAsync(User user)
        {
            if (user is Customer customer) customers.Update(customer);
            else if (user is Manager manager) managers.Update(manager);
            else throw new ArgumentException("?");
            await context.SaveChangesAsync();
        }
    }
}
