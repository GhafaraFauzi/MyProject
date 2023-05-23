using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository.Interface;
using System.Linq;
using MyProject.ViewModels;

namespace MyProject.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<Account> Get()
        {
            return myContext.Accounts.ToList();
        }

        public int Insert(Account account)
        {
            myContext.Entry(account).State = EntityState.Added;
            var save = myContext.SaveChanges();
            return save;
        }

        public Account Login(LoginVM loginVM)
        {
            var login = myContext.Accounts
                            .Include(e => e.Employee)
                            .Where(e => e.Employee.Email == loginVM.Email).SingleOrDefault();
            return login;
        }
    }
}
