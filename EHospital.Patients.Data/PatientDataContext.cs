using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EHospital.Patients.Model;

namespace EHospital.Patients.Data
{
    public class PatientDataContext : DbContext, IPatientData
    {
        /// <summary>
        /// Initializes new instance of LibraryContext
        /// </summary>
        public PatientDataContext()
        { }

        /// <summary>
        /// Initializes new instance of LibraryContext using options passed on application launch
        /// in the file Startup.cs
        /// </summary>
        /// /// <param name="options"> An options for connecting to data source </param>
        public PatientDataContext(DbContextOptions<PatientDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientInfo>()
                .ToTable("PatientInfo");
            modelBuilder.Entity<Image>()
                .ToTable("Images");
            modelBuilder.Entity<Image>()
                .Property(i => i.ImageContent)
                .HasColumnName("Image");

        }

        /// <summary>
        /// Gets or sets the collection of Images using EF models
        /// </summary>
        public DbSet<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets the collection of Patients using EF models
        /// </summary>
        public DbSet<PatientInfo> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Adds new image to the collection
        /// </summary>
        /// <param name="image">Image instance</param>
        public void AddImage(Image image)
        {
            Images.Add(image);
            SaveChanges();
        }

        /// <summary>
        /// Adds new PatientInfo to the collection
        /// </summary>
        /// <param name="patient">PatientInfo instance</param>
        public void AddPatient(PatientInfo patient)
        {
            Patients.Add(patient);
            SaveChanges();
        }

        /// <summary>
        /// Gets Image object with specified Id
        /// </summary>
        /// <param name="id">Id of Image to look for</param>
        /// <returns>Image object with requested Id or NULL if not found</returns>
        public Image GetImage(int? id)
        {
            return Images.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Gets entire collection of Images from Database
        /// </summary>
        /// <returns>Collection of Images</returns>
        public IEnumerable<Image> GetImages()
        {
            return Images.ToList();
        }

        /// <summary>
        /// Gets PatientInfo object with specified Id
        /// </summary>
        /// <param name="id">Id of Patient to look for</param>
        /// <returns>Patient with requested Id or NULL if not found</returns>
        public PatientInfo GetPatient(int id)
        {
            return Patients.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Gets all Patiens from database
        /// </summary>
        /// <returns>Collection of PatientInfos</returns>
        public IEnumerable<PatientInfo> GetPatients()
        {
            return Patients.ToList();
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return Appointments.ToList();
        }

        /// <summary>
        /// Saves changes in the collections
        /// Calls DbContext base SaveChanges Method
        /// </summary>
        public void Save()
        {
            this.SaveChanges();
        }
    }
}
