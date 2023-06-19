using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Vehicle_Mono_DAL.Entity
{
    [Table("Makess")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Name), IsUnique = true)]
    public class MakeEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        public ICollection<ModelEntity> Models { get; set; }
    }
}