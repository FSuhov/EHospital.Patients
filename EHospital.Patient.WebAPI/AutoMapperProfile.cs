using AutoMapper;
using EHospital.Patients.Model;

namespace EHospital.Patient.WebAPI
{
    /// <summary>
    /// Class that configures Automapper
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes new instance of AutoMapperProfile,
        /// setting mapping configuration for PatientInfo=>PatientView
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<PatientInfo, PatientView>();
        }
    }
}
