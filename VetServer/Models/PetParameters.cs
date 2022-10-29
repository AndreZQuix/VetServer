using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetServer.Models
{
    public class PetParameters
    {
        [Key]
        public int Id { get; set; }

        public int HeartRate { get; set; }

        public float Temperature { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


        [ForeignKey("Pet")]
        public int PetId { get; set; }
    }
}
