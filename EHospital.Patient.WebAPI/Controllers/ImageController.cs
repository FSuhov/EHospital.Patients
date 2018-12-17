using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EHospital.Patients.BusinessLogic.Services;
using EHospital.Patients.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHospital.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        /// <summary>
        /// An instance of business logic class
        /// </summary>
        private IPatientService _service;

        /// <summary>
        /// Initializes new instanct of THIS setting business logic object (_service)
        /// </summary>
        /// <param name="service">Object implementing business logic for this controller</param>
        public ImageController(IPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Handles request GET: ../api/image/
        /// Selects all Image objects available in the database table Images
        /// </summary>
        /// <returns>Collecton of Image models</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Image>> Get()
        {
            return _service.GetImages().ToList();
        }

        /// <summary>
        /// Handles request GET: api/patient/5
        /// Looks up Image instance with specified Id
        /// </summary>
        /// <param name="id">Id of Image to look for</param>
        /// <returns>Image model or Not Found</returns>
        [HttpGet("{id}", Name = "GetImage")]
        public ActionResult<Image> GetById(int id)
        {
            var image = _service.GetImageById(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        /// <summary>
        /// Handles request POST: api/image
        /// </summary>
        /// <param name="patientId">Id of patient whose image is being uploaded. </param>
        /// <param name="img">File to be uploaded.</param>
        /// <returns>Ok</returns>
        [HttpPost]
        public IActionResult AddImage(int patientId, IFormFile img)
        {

            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(img.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)img.Length);
            }

            _service.AddImage(patientId, imageData);

            return Ok();
        }

        [HttpGet("download")]
        public IActionResult DownloadImage(int patientId)
        {
            string fileType = "image/png";
            string fileName = "image.jpg";
            Byte[] data;
            try
            {
                data = _service.DownloadImage(patientId);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }

            return File(data, fileType, fileName);
        }
    }
}