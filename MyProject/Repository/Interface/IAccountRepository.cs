using MyProject.Models;
using MyProject.ViewModels;
using System.Collections.Generic;
namespace MyProject.Repository.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> Get();
        Account Login(LoginVM loginVM);
        int Insert(Account account);

    }
}
