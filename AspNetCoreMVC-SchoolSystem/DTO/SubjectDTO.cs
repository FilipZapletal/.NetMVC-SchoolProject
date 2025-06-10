using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMVC_SchoolSystem.DTO {
    public class SubjectDTO {
        public int Id { get; set; }
        //[MinLength(2), MaxLength(50)]
        [StringLength(50, MinimumLength = 2, ErrorMessage ="Zadej nejmene 2 a nejvice 50 znaku")]
        public string Name { get; set; }
    }
}
