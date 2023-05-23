using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository.Interface;
using MyProject.ViewModels;
using System.Linq;

namespace MyProject.Repository
{
    public class DepartemenRepository : IDepartemenRepository
    {
        private readonly MyContext myContext;

        public DepartemenRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int Id_Departemen)
        {
            var departemen = myContext.Departemens.FirstOrDefault(e => e.Id_Departemen == Id_Departemen);
            if (departemen != null)
            {
                myContext.Departemens.Remove(departemen);
                var change = myContext.SaveChanges();
                return change;
            }
            return 0;
        }

        public IEnumerable<Departemen> Get()
        {
            return myContext.Departemens.ToList();
        }

        public Departemen Get(int Id_Departemen)
        {
            return myContext.Departemens.FirstOrDefault(e => e.Id_Departemen == Id_Departemen);
        }

        public int Insert(Departemen departemen)
        {
            myContext.Departemens.Add(departemen);
            var save = myContext.SaveChanges();
            return save;
        }

        public bool IsIdExists(int Id_Departemen)
        {
            return myContext.Departemens.Any(e => e.Id_Departemen == Id_Departemen);
        }

        public int Update(Departemen departemen)
        {
            var emp = myContext.Departemens.FirstOrDefault(e => e.Id_Departemen == departemen.Id_Departemen);
            emp.Id_Departemen = departemen.Id_Departemen;
            emp.Nama_Departemen = departemen.Nama_Departemen;

            var update = myContext.SaveChanges();
            return update;
        }

    }
}