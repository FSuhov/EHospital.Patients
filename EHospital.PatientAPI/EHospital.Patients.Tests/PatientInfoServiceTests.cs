using EHospital.Patients.BusinessLogic.Services;
using EHospital.Patients.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using AutoMapper;

namespace EHospital.Patients.Tests
{
    [TestClass]
    public class PatientInfoServiceTests
    {
        private static IPatientService _service;
        private Mock<PatientTestingDataContext> _data;
        private static IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _data = new Mock<PatientTestingDataContext>();
            _data.SetupGet(i => i.Images).Returns(mockImages);
            _data.SetupGet(a => a.Appointments).Returns(mockAppointments);
            _data.SetupGet(p => p.Patients).Returns(mockPatients);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PatientInfo, PatientView>();
            });
            IMapper mapper = config.CreateMapper();
            _mapper = mapper;
            _service = new PatientInfoService(_data.Object, mapper);
        }

        [TestMethod]
        public void GetPatientsTest_ReturnsSorterCollection()
        {
            // Arrange
            List<PatientView> expected = _mapper.Map<List<PatientInfo>, List<PatientView>>(mockPatients);
            expected.Sort();

            // Act
            List<PatientView> actualPatients = _service.GetPatients().ToList();

            // Assert
            bool isAllEntriesEqual = true;
            for (int i = 0; i < expected.Count && i < actualPatients.Count; i++)
            {
                if (expected.ElementAt(i).FirstName != actualPatients.ElementAt(i).FirstName
                    && expected.ElementAt(i).LastName != actualPatients.ElementAt(i).LastName)
                {
                    isAllEntriesEqual = false;
                }
            }
            Assert.AreEqual(mockPatients.Count, actualPatients.Count);
            Assert.AreEqual(true, isAllEntriesEqual);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(9)]
        public void GetPatientByIdWhenValidId_ReturnsCorrectPatientInfo(int id)
        {
            // Arrange

            // Act
            PatientInfo patient = _service.GetPatientById(id);
            string expectedName = mockPatients.FirstOrDefault(p => p.Id == id).FirstName;
            string expectedSurname = mockPatients.FirstOrDefault(p => p.Id == id).LastName;

            // Assert
            Assert.AreEqual(expectedName, patient.FirstName);
            Assert.AreEqual(expectedSurname, patient.LastName);
        }

        [TestMethod]
        [DataRow(12)]
        public void GetPatientByIdWhenNotValidId_ReturnsNull(int id)
        {
            // Arrange

            // Act
            PatientInfo patient = _service.GetPatientById(id);

            // Assert
            Assert.AreEqual(null, patient);
        }

        [TestMethod]
        public void GetRecentPatients_ReturnsCollectionOfRecent()
        {
            // Arrange
            List<PatientInfo> recent = new List<PatientInfo>();
            recent.Add(mockPatients.First(p => p.Id == 10));
            recent.Add(mockPatients.First(p => p.Id == 2));
            recent.Add(mockPatients.First(p => p.Id == 1));
            List<PatientView> recentViews = _mapper.Map<List<PatientInfo>, List<PatientView>>(recent);

            // Act
            List<PatientView> actualRecent = _service.GetRecentPatients().ToList();

            // Assert
            bool isAllEntriesEqual = true;
            for (int i = 0; i < recentViews.Count && i < actualRecent.Count; i++)
            {
                if (recentViews.ElementAt(i).FirstName != actualRecent.ElementAt(i).FirstName
                    && recentViews.ElementAt(i).LastName != actualRecent.ElementAt(i).LastName)
                {
                    isAllEntriesEqual = false;
                }
            }
            Assert.AreEqual(true, isAllEntriesEqual);
        }

        [TestMethod]
        [DataRow("bo")]
        [DataRow("on")]
        [DataRow("ar")]
        public void GetPatientsByTextTest_ReturnsCollectionOfPatientViews(string input)
        {
            // Arrange
            List<PatientView> allPatientViews = _mapper.Map<List<PatientInfo>, List<PatientView>>(mockPatients);
            List<PatientView> matchingPatients = allPatientViews.Where(p => p.FirstName.ToLower().Contains(input.ToLower())
                                                                          || p.LastName.ToLower().Contains(input.ToLower())).ToList();

            // Act
            List<PatientView> actualPatients = _service.GetPatientsByText(input).ToList();

            // Assert
            bool isAllEntriesEqual = true;
            for (int i = 0; i < matchingPatients.Count && i < actualPatients.Count; i++)
            {
                if (matchingPatients.ElementAt(i).FirstName != actualPatients.ElementAt(i).FirstName
                    && matchingPatients.ElementAt(i).LastName != actualPatients.ElementAt(i).LastName)
                {
                    isAllEntriesEqual = false;
                }
            }
            Assert.AreEqual(true, isAllEntriesEqual);
        }


        [TestMethod]
        public void AddNewPatientText_AddsNewPatientToCollection()
        {
            // Arrange
            PatientInfo newPatient = new PatientInfo { Id = 11, FirstName = "Klavdia", LastName = "Popkina", BirthDate = new System.DateTime(1969, 1, 12), Gender = 2 };

            // Act
            _service.AddPatient(newPatient);

            // Assert
            int actualCount = _service.GetPatients().Count();
            int expectedCount = mockPatients.Count;

            bool isNewPatientAdded = _service.GetPatientById(11) != null;

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(true, isNewPatientAdded);
        }

        private static List<Image> mockImages = new List<Image>
        {
            new Image{Id = 1, ImageName = "James_Bond.jpg"},
            new Image{Id = 2, ImageName = "Ivy_Young.jpg"},
            new Image{Id = 3, ImageName = "Melissa_Brown.jpg"},
            new Image{Id = 4, ImageName = "John_Smith.jpg"},
            new Image{Id = 5, ImageName = "Kevin_McBribe.jpg"},
        };

        private static List<PatientInfo> mockPatients = new List<PatientInfo>
        {
            new PatientInfo{Id = 1, FirstName = "James", LastName = "Bond", Country ="UK", City = "London", Address = "Douning str, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "mi5@gmail.com", Gender = 1, ImageId = 1, IsDeleted = false},
            new PatientInfo{Id = 2, FirstName = "Ivy", LastName = "Young", Country ="Ireland", City = "Dublin", Address = "Yellow park, 1", BirthDate = new System.DateTime(1980, 9, 28), Phone="380671119999", Email = "ivy@gmail.com", Gender = 2, ImageId = 2, IsDeleted = false},
            new PatientInfo{Id = 3, FirstName = "Melissa", LastName = "Brown", Country ="Sweden", City = "Stokholm", Address = "Norcheping av, 110", BirthDate = new System.DateTime(1990, 3, 8), Phone="380635554334", Email = "mel@outlook.com", Gender = 2, ImageId = 3, IsDeleted = false},
            new PatientInfo{Id = 4, FirstName = "John", LastName = "Smith", Country ="UK", City = "London", Address = "Westminster, 11", BirthDate = new System.DateTime(1956, 1, 18), Phone="380975554334", Email = "jo@gmail.com", Gender = 1, ImageId = 4, IsDeleted = false},
            new PatientInfo{Id = 5, FirstName = "Kevin", LastName = "McBribe", Country ="UK", City = "Aberdin", Address = "Lake view sq, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "scottish@gmail.com", Gender = 1, ImageId = 5, IsDeleted = false},
            new PatientInfo{Id = 6, FirstName = "Elizabeth", LastName = "Parker", Country ="UK", City = "London", Address = "Douning str, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "mi5@gmail.com", Gender = 2, ImageId = 1, IsDeleted = false},
            new PatientInfo{Id = 7, FirstName = "Frank", LastName = "Connor", Country ="UK", City = "London", Address = "Douning str, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "mi5@gmail.com", Gender = 1, ImageId = 1, IsDeleted = false},
            new PatientInfo{Id = 8, FirstName = "Anna", LastName = "Green", Country ="UK", City = "London", Address = "Douning str, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "mi5@gmail.com", Gender = 1, ImageId = 1, IsDeleted = false},
            new PatientInfo{Id = 9, FirstName = "Sarah", LastName = "Zimmermann", Country ="UK", City = "London", Address = "Douning str, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "mi5@gmail.com", Gender = 1, ImageId = 1, IsDeleted = false},
            new PatientInfo{Id = 10, FirstName = "Maria", LastName = "Popkina", Country ="UK", City = "London", Address = "Douning str, 11", BirthDate = new System.DateTime(1970, 1, 18), Phone="380505554334", Email = "mi5@gmail.com", Gender = 1, ImageId = 1, IsDeleted = false},
        };

        private static List<Appointment> mockAppointments = new List<Appointment>
        {
            new Appointment{Id = 1, PatientId = 1, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,1), Purpose="Diagnostic", IsDeleted = false, UserId = 1},
            new Appointment{Id = 2, PatientId = 2, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,2), Purpose="Analysis", IsDeleted = false, UserId = 1},
            new Appointment{Id = 3, PatientId = 3, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,3), Purpose="Procedures", IsDeleted = false, UserId = 1},
            new Appointment{Id = 4, PatientId = 4, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,4), Purpose="Claims", IsDeleted = false, UserId = 2},
            new Appointment{Id = 5, PatientId = 5, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,5), Purpose="Diagnostic", IsDeleted = false, UserId = 2},
            new Appointment{Id = 6, PatientId = 6, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,6), Purpose="Seekness", IsDeleted = false, UserId = 2},
            new Appointment{Id = 7, PatientId = 7, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,7), Purpose="Claims", IsDeleted = false, UserId = 3},
            new Appointment{Id = 8, PatientId = 4, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,8), Purpose="Diagnostic", IsDeleted = false, UserId = 1},
            new Appointment{Id = 9, PatientId = 1, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,9), Purpose="Diagnostic", IsDeleted = false, UserId = 1},
            new Appointment{Id = 10, PatientId = 2, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,10), Purpose="Diagnostic", IsDeleted = false, UserId = 1},
            new Appointment{Id = 11, PatientId = 10, Duration = 30, AppointmentDateTime = new System.DateTime(2018, 10,11), Purpose="Diagnostic", IsDeleted = false, UserId = 1},

        };
    }
}
