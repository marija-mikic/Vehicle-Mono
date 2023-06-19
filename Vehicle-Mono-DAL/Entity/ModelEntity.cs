using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Mono_DAL.Entity
{
    [Table("Models")]
    [Microsoft.EntityFrameworkCore.Index(nameof(MakeId), nameof(Name), IsUnique = true)]
    public class ModelEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }

        [ForeignKey("Make")]
        public int MakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        public MakeEntity Make { get; set; }
    }
}