using EHospital.Patients.Model;
using System.Collections.Generic;

namespace EHospital.Patients.Data
{
    public interface IPatientData
    {
        IEnumerable<Image> GetImages();
        IEnumerable<PatientInfo> GetPatients();
        IEnumerable<Appointment> GetAppointments();

        Image GetImage(int id);
        void AddImage(Image image);

        PatientInfo GetPatient(int id);
        void AddPatient(PatientInfo patient);

        void Save();
    }
}
