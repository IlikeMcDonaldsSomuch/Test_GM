using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_GreenMoon.Models
{
    public class PersonModel
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // Define the navigation property for the parent
        public virtual PersonModel Parent { get; set; }

        // Define the foreign key for the parent
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
    }
}

