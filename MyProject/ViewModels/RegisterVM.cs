using MyProject.Models;

namespace MyProject.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        //Format yyyy-mm-dd
        public int Salary { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public int Id_Departemen { get; set; }
        public string Password { get; set; }
    }
}
