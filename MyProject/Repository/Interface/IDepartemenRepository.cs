using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.Repository.Interface
{
    public interface IDepartemenRepository
    {
        IEnumerable<Departemen> Get();
        Departemen Get(int Id_Departemen);
        int Insert(Departemen departemen);
        int Update(Departemen departemen);
        int Delete(int Id_Departemen);
        bool IsIdExists(int Id_Departemen);
    }
}
