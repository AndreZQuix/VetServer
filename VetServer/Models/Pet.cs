using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VetServer.Models
{
    public class Pet
    {
        [Key]
        public virtual int Id { get; internal set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual int Age { get; set; }


        [ForeignKey("Client")]
        public virtual int ClientId { get; set; }
    }
}
