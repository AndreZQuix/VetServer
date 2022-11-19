using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VetServer.Models
{
    public class PetParameters
    {
        [Key]
        public virtual int Id { get; internal set; }

        public virtual int? HeartRate { get; set; }

        public virtual int? BreathingRate { get; set; }

        public virtual BloodPressure? Pressure { get; set; }

        public virtual float? Temperature { get; set; }

        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;


        [ForeignKey("Pet")]
        public virtual int PetId { get; set; }
    }

    [Keyless]
    [NotMapped]
    public class BloodPressure
    {
        public int TopPressure { get; set; }
        public int LowPressure { get; set; }
    }
}
