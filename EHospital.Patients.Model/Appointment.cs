using System;
using System.ComponentModel.DataAnnotations;

namespace EHospital.Patients.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public int Duration { get; set; }
        public string Purpose { get; set; }
        public bool IsDeleted { get; set; }
    }
}
