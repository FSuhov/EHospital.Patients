using EHospital.Patients.Model;
using System.Collections.Generic;

namespace EHospital.Patients.BusinessLogic.Services
{
    /// <summary>
    /// Contains prototypes for Business Logic Methods for eHospital/Patients
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Gets the collection of PatientInfos existing in the database.
        /// </summary>
        /// <returns> The collection of PatientView objects.</returns>
        IEnumerable<PatientView> GetPatients();

        /// <summary>
        /// Gets the collection of PatientInfos that had appointments recently.
        /// </summary>
        /// <returns> The collection of PatientView objects.</returns>
        IEnumerable<PatientView> GetRecentPatients();

        /// <summary>
        /// Looks for PatientInfo object with requested Id
        /// </summary>
        /// <param name="patientId">Id of PatientInfo to look for</param>
        /// <returns>PatientInfo object with specified Id or NULL if not found</returns>
        PatientInfo GetPatientById(int patientId);

        /// <summary>
        /// Looks for PatientInfos matching the user input by FirstName or LastName 
        /// </summary>
        /// <param name="input">Text entered by User if "search"field</param>
        /// <returns>Collection of PatientView objects matching the query text</returns>
        IEnumerable<PatientView> GetPatientsByText(string input);

        /// <summary>
        /// Gets a collection of all Image objects available in the database
        /// </summary>
        /// <returns>Collection of Image objects</returns>
        IEnumerable<Image> GetImages();

        /// <summary>
        /// Looks for an Image with the requested Id
        /// </summary>
        /// <param name="imageId">Id of Image to look for</param>
        /// <returns>Image object with specified Id or NULL if not found</returns>
        Image GetImageById(int imageId);

        /// <summary>
        /// Adds PatientInfo object to the database
        /// </summary>
        /// <param name="patient">New PatientInfo object</param>
        void AddPatient(PatientInfo patient);

        /// <summary>
        /// Updates PatientInfo object with specified Id
        /// </summary>
        /// <param name="patientId">Id of PatientInfo object to be updated</param>
        /// <param name="patient">PatientInfo object to be cloned from</param>
        void UpdatePatient(int patientId, PatientInfo patient);

        /// <summary>
        /// Sets IsDisabled property of Patient instance to true 
        /// </summary>
        /// <param name="patientId">Id of patient to be disabled</param>
        void DeletePatient(int patientId);

        /// <summary>
        /// Adds new Image to database, updates Patient entry with ImageId foreign key. 
        /// </summary>
        /// <param name="patientId">Id of Patient to whom this imageData relates.</param>
        /// <param name="imageData">Byte array uploaded by user.</param>
        void AddImage(int patientId, byte[] imageData);

        byte[] DownloadImage(int patientId);
    }
}
