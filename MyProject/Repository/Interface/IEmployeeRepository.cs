using MyProject.Models;
using System.Collections.Generic;
namespace MyProject.Repository.Interface
{   
    public interface IEmployeeRepository
    {
        Employee GetByEmail(string email);
        Employee GetByPhoneNumber(string phoneNumber);
        IEnumerable<Employee> Get();
        Employee Get(string NIK);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);
        bool IsIdExists(string NIK);
    }
}
