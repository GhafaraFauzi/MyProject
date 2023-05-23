using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Tbl_Employee")]
    public class Employee
    {

        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        //Format yyyy-mm-dd
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public virtual Departemen? Departemen { get; set; }
        //[ForeignKey("Departemen")]
        //public int Id_Departemen { get; set; }
    }

    public enum Gender
    {
        male,
        felame
    }
}
