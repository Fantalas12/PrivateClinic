using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace PrivateClinic.WebApi.Tests
{
    public class TestDbInitilizer
    {
        
        //public static void Initialize(PrivateClinicDbContext context, UserManager<Doctor> userManager, UserManager<Patient> patientManager)
        public static void Initialize(PrivateClinicDbContext context)
        {
            /*
            IList<Doctor> defaultDoctors = new List<Doctor>
            {
                new Doctor
                {
                    Id = "DoctorId",
                    Name = "Doctor",


                    Specializations = new List<Specialization>()
                    {
                        new Specialization()
                        {
                            SpecName = SpecializationType.Surgery
                        }
                    },
                    UserName = "Doctor"


                }

            };
            */

            IList<MedicalRecord> defaultMedicalRecords = new List<MedicalRecord>
            {
                new MedicalRecord
                {
                    Id = 1,
                    DoctorId = "DoctorId1",
                    PatientId = "PatientId1",
                    DateTime = DateTime.Now.AddDays(1),
                },
                new MedicalRecord
                {
                    Id = 2,
                    DoctorId = "DoctorId2",
                    PatientId = "PatientId2",
                    DateTime = DateTime.Now.AddDays(2),
                },
                new MedicalRecord
                {
                    Id = 3,
                    DoctorId = "DoctorId3",
                    PatientId = "PatientId3",
                    DateTime = DateTime.Now.AddDays(3),
                },

            };


            //IList<Booking>
            //context.Doctors.AddRange(defaultDoctors);
            context.MedicalRecords.AddRange(defaultMedicalRecords);
            context.SaveChanges();


        }
    }
}
