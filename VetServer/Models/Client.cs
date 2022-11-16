using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace VetServer.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        [JsonIgnore]
        public string Salt { get; set; }

        [Required]
        [MinLength(5)]
        public string Email { get; set; }

        //public IEnumerable<Pet> Pets { get; set; }
    }
}
