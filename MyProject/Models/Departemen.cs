using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    [Table("Tbl_Departemen")]
    public class Departemen
    {
        [Key]
        public int Id_Departemen { get; set; }
        public string Nama_Departemen { get; set; }
    }
}
