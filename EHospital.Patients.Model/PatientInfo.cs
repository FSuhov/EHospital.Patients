using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHospital.Patients.Model
{
    /// <summary>
    /// Represents a model of Patient as stored in Database
    /// </summary>
    public class PatientInfo
    {
        /// <summary>
        /// Gets or sets a Unique number to identify the book and store in the Database
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }

        public byte Gender { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [ForeignKey("Image")]
        public int? ImageId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
