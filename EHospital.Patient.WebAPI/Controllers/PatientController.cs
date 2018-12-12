using System.Collections.Generic;
using System.Linq;
using EHospital.Patients.BusinessLogic.Services;
using EHospital.Patients.Model;
using Microsoft.AspNetCore.Mvc;

namespace EHospital.Patient.WebAPI.Controllers
{
    /// <summary>
    /// Class serving as controller for handling the requests related to PatientInfos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        /// <summary>
        /// An instance of business logic class
        /// </summary>
        private IPatientService _service;

        /// <summary>
        /// Initializes new instanct of THIS setting business logic object (_service)
        /// </summary>
        /// <param name="service">Object implementing business logic for this controller</param>
        public PatientController(IPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles request GET: ../api/patient/
        /// Retrieves all PatientInfo objects available in the database table PatientInfo
        /// </summary>
        /// <returns>Collecton of PatientView models containing Id, firstname and lastname</returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<PatientView> patients = _service.GetPatients();

            return Ok(patients);
        }

        /// <summary>
        /// Handles request GET: ../api/patient/recent
        /// Selects all PatientInfo objects having recent appontments
        /// </summary>
        /// <returns>Collecton of PatientView models containing Id, firstname and lastname</returns>
        [HttpGet("recent")]
        public IActionResult GetRecent()
        {
            IEnumerable<PatientView> recentPatients = _service.GetRecentPatients();

            return Ok(recentPatients);
        }

        /// <summary>
        /// Handles request GET: ../api/patient/{userText}
        /// Tries to find all PatientInfo instances in the database containing text entered
        /// by User in the FirstName or LastName
        /// </summary>
        /// <param name="lookUp">Text entered by User in the search window</param>
        /// <returns>Collection of PatientViews matching search criteria</returns>
        [HttpGet("name={lookUp}")]
        public IActionResult Get(string lookUp)
        {
            List<PatientView> patients = _service.GetPatientsByText(lookUp).ToList();

            return Ok(patients);
        }

        /// <summary>
        /// Handles request GET: api/patient/5
        /// Looks up PatientInfo instance with specified Id
        /// </summary>
        /// <param name="id">Id of patient to look for</param>
        /// <returns>Detailed patient object (all fields) or Not Found</returns>
        [HttpGet("id={id}", Name = "GetPatient")]
        public IActionResult Get(int id)
        {
            PatientInfo patient = _service.GetPatientById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        /// <summary>
        /// Handles request POST: ../api/patient/+{Object in body}
        /// Adds new PatientInfo object to the database if valid
        /// </summary>
        /// <param name="patient">PatientInfo object</param>
        /// <returns>PatientInfo object created or Bad Request</returns>
        [HttpPost]
        public IActionResult Add([FromBody] PatientInfo patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            _service.AddPatient(patient);

            return Created("PatientInfo", patient);
        }

        /// <summary>
        /// Handles request PUT: ../api/patient/{id}+{Object in body}
        /// Looks for PatientInfo object in the database with requested Id
        /// Updates properties of tha object cloning it from Object passed in body
        /// </summary>
        /// <param name="id">Id to look for</param>
        /// <param name="patient">PatientInfo object to be cloned from </param>
        /// <returns>Ok result or BadRequest if wrong/invalid object passed</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]  PatientInfo patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            _service.UpdatePatient(id, patient);

            return Ok();
        }

        /// <summary>
        /// Handles request DELETE:  ../api/patient/{id}
        /// Looks for PatientInfo object with requested Id
        /// Sets to TRUE its property IsDisabled
        /// </summary>
        /// <param name="id">Id to look for</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.DeletePatient(id);

            return NoContent();
        }
    }
}