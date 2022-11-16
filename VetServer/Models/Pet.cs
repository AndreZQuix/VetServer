using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetServer.Models
{
    public class Pet
    {
        [Key]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }


        [ForeignKey("Client")]
        public int ClientId { get; set; }
    }
}
