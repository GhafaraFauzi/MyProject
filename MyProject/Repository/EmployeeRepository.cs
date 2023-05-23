using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository.Interface;
using MyProject.ViewModels;
using System.Linq;

namespace MyProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // implementasi method-method lain di sini

        public Employee GetByEmail(string email)
        {
            return myContext.Employees.FirstOrDefault(e => e.Email == email);
        }

        public Employee GetByPhoneNumber(string phoneNumber)
        {
            return myContext.Employees.FirstOrDefault(e => e.Phone == phoneNumber);
        }

        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return myContext.Employees.FirstOrDefault(e => e.NIK == NIK);
        }

        public int Insert(Employee employee)
        {
            if (IsPhoneExists(employee.Phone) || IsEmailExists(employee.Email))
            {
                throw new Exception("Nomor Telepon dan Email sudah terdaftar didalam sistem");
            }

            string lastId = myContext.Employees
                                .OrderByDescending(e => e.NIK)
                                .Select(e => e.NIK)
                                .FirstOrDefault();
            //Generate NIK Kedua
            var newId = DateTime.Now.ToString("ddMMyy") + (myContext.Employees.Count() + 1).ToString("D3");

            //Generate NIK Pertama
            //string newId = GenerateNewNIK(lastId);

            employee.NIK = newId;

            myContext.Entry(employee).State = EntityState.Added;
            var save = myContext.SaveChanges();
            return save;
        }

        private bool IsPhoneExists(string phone)
        {
            return myContext.Employees.Any(e => e.Phone == phone);
        }

        private bool IsEmailExists(string email)
        {
            return myContext.Employees.Any(e => e.Email == email);
        }

        public int Update(Employee employee)
        {
            var emp = myContext.Employees.FirstOrDefault(e => e.NIK == employee.NIK);
            emp.NIK = employee.NIK;
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.Phone = employee.Phone;
            emp.BirthDate = employee.BirthDate;
            emp.Email = employee.Email;
            emp.Salary = employee.Salary;
            emp.Gender = employee.Gender;

            var update = myContext.SaveChanges();
            return update;
        }

        public int Delete(string NIK)
        {
            var employee = myContext.Employees.FirstOrDefault(e => e.NIK == NIK);
            if (employee != null)
            {
                myContext.Employees.Remove(employee);
                var change = myContext.SaveChanges();
                return change;
            }
            return 0;
        }

        public bool IsIdExists(string NIK)
        {
            return myContext.Employees.Any(e => e.NIK == NIK);
        }

        internal string GetLastNIK()
        {
         var lastEmployee = myContext.Employees
        .OrderByDescending(e => e.NIK)
        .FirstOrDefault();

            return lastEmployee.NIK;
        }

        //Generate NIK Pertama
        //internal string GenerateNewNIK(string lastNIK)
        //{
        //    string today = DateTime.Now.ToString("ddMMyy");
        //    int lastNumber = 0;

        //    if (lastNIK != null)
        //    {
        //        string lastNumberString = lastNIK.Substring(6, 2);
        //        lastNumber = int.Parse(lastNumberString);
        //    }

        //    int newNumber = lastNumber + 1;
        //    string newNumberString = newNumber.ToString().PadLeft(2, '0');
        //    string newNIK = today + newNumberString;

        //    return newNIK;
        //}

        public IEnumerable<Employee> IDepartemen()
        {
            return myContext.Employees.Include(e => e.Departemen);
        }

        public IEnumerable <object> Departemen()
        {
            var get = myContext.Employees.Include(e => e.Departemen)
                .Select (e=> new
            {
                NIK = e.NIK,
                Fullname = ($"{e.FirstName} {e.LastName}"),
                NamaDept = e.Departemen.Nama_Departemen
            }).ToList();
            return get;

        }

        public int Register(RegisterVM registerVM)
        {
            //Generate ID Otomatis
            var NIK = DateTime.Now.ToString("ddMMyy") + (myContext.Employees.Count() + 1).ToString("D3");
            Employee employee = new Employee
            {
                NIK = NIK, 
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Gender)registerVM.Gender,
                Departemen = new Departemen { Id_Departemen = registerVM.Id_Departemen}
                //Id_Departemen = registerVM.Id_Departemen

            };
            myContext.Entry(employee).State = EntityState.Added;
            Account account = new Account
            {
                NIK = employee.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
            };
            myContext.Accounts.Add(account);
            myContext.SaveChanges();
            return 1;
        }

    }
}
