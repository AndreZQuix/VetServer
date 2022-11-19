using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VetServer.Models
{
    public class Client
    {
        [Key]
        public virtual int Id { get; internal set; }

        [Required]
        [MinLength(2)]
        public virtual string Name { get; set; }

        [Required]
        [MinLength(3)]
        public virtual string Username { get; set; }

        [Required]
        [MinLength(5)]
        public virtual string Password { get; set; }

        [JsonIgnore]
        public virtual string? Salt { get; set; }

        [Required]
        [MinLength(5)]
        public virtual string Email { get; set; }

        //public IEnumerable<Pet> Pets { get; set; }
    }


    /// <summary>
    /// Class for displaying client data (password is hidden).
    /// </summary>
    public class ClientModelGET : Client
    {
        [Required]
        [MinLength(5)]
        [JsonIgnore]
        public override string Password { get; set; }
    }
}
