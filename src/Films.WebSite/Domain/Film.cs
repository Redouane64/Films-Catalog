using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Films.Website.Domain
{

    /// <summary>
    /// Represent a film entity.
    /// </summary>
    [Table("films", Schema = "catalog")]
    public class Film
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("title", TypeName = "nvarchar(MAX)")]
        [Required]
        public string Title { get; set; }

        [Column("description", TypeName = "nvarchar(MAX)")]
        [Required]
        public string Description { get; set; }

        [Column("year", TypeName = "smallint")]
        public int ReleaseYear { get; set; }

        [Column("director", TypeName = "nvarchar(MAX)")]
        [Required]
        public string Director { get; set; }

        [Column("image")]
        [Required]
        public byte[] Image { get; set; }

        [Column("creator_id")]
        public string CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
