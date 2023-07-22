using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PrivateClinic.Persistence
{
    public static class DbInitializer
    {

        public static void Initialize(PrivateClinicDbContext context, UserManager<Doctor> userManager, UserManager<Patient> patientManager, string imageDirectory)
        {
            context.Database.Migrate();

            if (context.Doctors.Any())
            {
                return;
            }


            string OnePath = Path.Combine(imageDirectory, "1.png");
            string TwoPath = Path.Combine(imageDirectory, "2.png");
            string ThreePath = Path.Combine(imageDirectory, "3.png");
            string FourPath = Path.Combine(imageDirectory, "4.png");


            IList<Doctor> defaultDoctors = new List<Doctor>
            {
                new Doctor
                {
                    Id = "One",
                    Name = "Dr. Mészáros Kristóf",
                    Image = File.Exists(OnePath) ? File.ReadAllBytes(OnePath) : null,
                    
                    Specializations = new List<Specialization>()
                    {
                        new Specialization()
                        {
                            SpecName = SpecializationType.Surgery
                        }
                    },
                    UserName = "Kristóf"


                },

                new Doctor
                {
                    Id = "Two",
                    Name = "Dr. Boros Dominik",
                    Image = File.Exists(TwoPath) ? File.ReadAllBytes(TwoPath) : null,
                    
                    Specializations = new List<Specialization>()
                    {
                        new Specialization()
                        {
                            SpecName = SpecializationType.Surgery
                        },
                        new Specialization()
                        {
                            SpecName = SpecializationType.Toxicology
                        },
                    },
                    UserName = "Dominik"

                },

                new Doctor
                {
                    Id = "5dca1af9-b1d9-4c55-8e95-f6c9b4cb90dd",
                    Name = "Dr. Faragó Linda",
                    Image = File.Exists(ThreePath) ? File.ReadAllBytes(ThreePath) : null,
                    
                    Specializations = new List<Specialization>()
                    {
                        new Specialization()
                        {
                            SpecName = SpecializationType.Toxicology
                        },
                        new Specialization()
                        {
                            SpecName = SpecializationType.Dermatology
                        },
                    },
                    UserName = "Linda"
                },

                new Doctor
                {
                    Id = "Four",
                    Name = "Dr. Gáspár Bertalan",
                    Image = File.Exists(FourPath) ? File.ReadAllBytes(FourPath) : null,
                    
                    Specializations = new List<Specialization>()
                    {
                        new Specialization()
                        {
                            SpecName = SpecializationType.Surgery
                        },
                        new Specialization()
                        {
                            SpecName = SpecializationType.Toxicology
                        },
                        new Specialization()
                        {
                            SpecName = SpecializationType.Dermatology
                        },
                    },
                    UserName = "Bertalan"

                },

            };

            var password = "Almafa123";

            foreach (var doctor in defaultDoctors)
            {
                userManager.CreateAsync(doctor, password);
                // a jelszó egységesen Almafa123 az egyszerűség kedvéért
            }


            /*
            var doc = new Doctor
            {
                Id = "One",
                Name = "Dr. Mészáros Kristóf",
                Image = File.Exists(OnePath) ? File.ReadAllBytes(OnePath) : null,

                Specializations = new List<Specialization>()
                    {
                        new Specialization()
                        {
                            SpecName = SpecializationType.Surgery
                        }
                    },
                UserName = "Kristóf"


            };
            userManager.CreateAsync(doc, password);
            */



            /*
            var lindaId = defaultDoctors[2].Id;

            IList<MedicalRecord> defaultMedicalRecords = new List<MedicalRecord>
            {
                new MedicalRecord
                {
                    Id = 1,
                    DoctorId = "Two",
                    PatientId = "PatientId1",
                    DateTime = DateTime.Now.AddDays(1),
                },
                new MedicalRecord
                {
                    Id = 2,
                    DoctorId = lindaId,
                    PatientId = "PatientId2",
                    DateTime = DateTime.Now.AddDays(2),
                },
                new MedicalRecord
                {
                    Id = 3,
                    DoctorId = "Four",
                    PatientId = "PatientId3",
                    DateTime = DateTime.Now.AddDays(3),
                },

            };
            */

            //context.AddRange(defaultDoctors);
            //context.AddRange(defaultMedicalRecords);
            context.SaveChanges();

        }
    }

}
