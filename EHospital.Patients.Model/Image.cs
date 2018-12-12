using System.ComponentModel.DataAnnotations;

namespace EHospital.Patients.Model
{
    /// <summary>
    /// Class representing Image if Patient
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets a Unique number to identify the book and store in the Database
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of Image
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the content of Image file
        /// </summary>
        public byte[] ImageContent { get; set; }
    }
}
