using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations; // Add this for Swagger annotations
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FMSLibrary.Models
{
    public partial class City
    {
        [Key]
        [SwaggerSchema(ReadOnly = true)]
        public int CityId { get; set; } // Auto-incremented by the database

        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CityName { get; set; }

        [Required]
        [StringLength(100)]
        public string State { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AirportCharge { get; set; }

        [Required]
        public bool Status { get; set; } // BIT column in SQL
    }
}
