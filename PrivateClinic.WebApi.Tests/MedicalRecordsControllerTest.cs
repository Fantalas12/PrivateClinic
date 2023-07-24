using Xunit;
using System;
using Microsoft.EntityFrameworkCore;
using PrivateClinic.Persistence;
using PrivateClinic.Persistence.Services;
using PrivateClinic.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrivateClinic.DTO;
using System.Collections.Generic;
using System.Linq;

namespace PrivateClinic.WebApi.Tests
{
    public class MedicalRecordsControllerTest : IDisposable
    {

        private readonly PrivateClinicDbContext _context;
        private readonly PrivateClinicService _service;
        private readonly MedRecordsController _controller;
        //private readonly UserManager<Patient> _patientManager;
        //private readonly UserManager<Doctor> _userManager;


        public MedicalRecordsControllerTest()
        {
            var options = new DbContextOptionsBuilder<PrivateClinicDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _context = new PrivateClinicDbContext(options);
            TestDbInitilizer.Initialize(_context);

            /* 
             * Ezzel az utas�t�ssal elengedj�k az adatb�zis objektumainak k�vet�s�t (tracking).
             * Ez a list�k �tnevez�s�nek tesztel�s�hez sz�ks�ges, mivel egy�bk�nt a
             * PutList megpr�b�lna �j objektumot l�trehozni az adatb�zisban.
             */
            _context.ChangeTracker.Clear();

            //_patientManager = new UserManager<Patient>();
            //_userManager = new UserManager<Doctor>();
            //TestDbInitilizer.Initialize(_context, userManager, patientManager);


            _service = new PrivateClinicService(_context);
            _controller = new MedRecordsController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        
        /*
        [Fact]
        public void GetMedicalRecordsTest()
        {
            // Act
            var result = _controller.GetMedicalRecords();

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<MedicalRecordDTO>>(result.Value);
            Assert.Equal(3, content.Count());
        }
        */

        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetMedicalRecordByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetMedicalRecord(id);

            // Assert
            var content = Assert.IsAssignableFrom<MedicalRecordDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidMedicalRecordTest()
        {
            // Arrange
            var id = 4;

            // Act
            var result = _controller.GetMedicalRecord(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void PutMedicalRecordTest(Int32 id)
        {
            // Arrange
            string newdocId = "Updated docid";
            var medrecorddto = new MedicalRecordDTO { Id = id, DoctorId = newdocId };

            // Act
            var result = _controller.PutMedicalRecord(id, medrecorddto);

            // Assert
            var requestResult = Assert.IsAssignableFrom<OkResult>(result);
            var updatedList = _controller.GetMedicalRecord(id);
            Assert.Equal(updatedList?.Value?.DoctorId, newdocId);
        }

        [Fact]
        public void PostMedicalRecordTest1()
        {
            // Arrange
            var newMedrecord = new MedicalRecordDTO { Id = 12, PatientId = "New Patient Id1", DoctorId = "New Doctor Id1", DateTime = DateTime.Now.AddDays(1)};
            var count = _context.MedicalRecords.Count();

            // Act
            var result = _controller.PostMedicalRecord(newMedrecord);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<MedicalRecordDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.MedicalRecords.Count());
        }

        [Fact]
        public void PostMedicalRecordTest2()
        {
            // Arrange
            var newBooking = new Booking { Id = 15, PatientID = "New Patient Id2", DoctorID = "New Doctor Id2", DateTime = DateTime.Now.AddDays(2) };
            //var newMedrecord = (MedicalRecordDTO)(BookingDTO)newBooking;
            var newMedrecordDto = new MedicalRecordDTO
            {
                PatientId = newBooking.PatientID,
                DoctorId = newBooking.DoctorID,
                DateTime = newBooking.DateTime,
            };


            var count = _context.MedicalRecords.Count();

            // Act
            var result = _controller.PostMedicalRecord(newMedrecordDto);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<MedicalRecordDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.MedicalRecords.Count());
        }







    }
}