using PrivateClinic.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PrivateClinic.Persistence.Services
{
    public class PrivateClinicService : IPrivateClinicService
    {
        private readonly PrivateClinicDbContext _context;

        public PrivateClinicService(PrivateClinicDbContext context)
        {
            _context = context;
        }


        public List<Doctor> GetDoctors()
        {
            return _context.Doctors
                .OrderBy(l => l.Name)
                .ToList();
        }

        public Doctor GetDoctorById(string id)
        {
            return _context.Doctors
                .Include(l => l.Specializations)
                .FirstOrDefault(l => l.Id == id);
        }

        public Booking GetBooking(int id)
        {
            return _context.Bookings
                .Single(l => l.Id == id);
        }


        public bool AddPatient(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public Doctor GetDoctorDetails(string id)
        {
            return _context.Doctors
               .Include(l => l.Specializations)
              .Single(l => l.Id == id);
        }

        public bool DeleteBookingById(Int32 id)
        {
            Booking? booking = _context.Bookings
               .FirstOrDefault(i => i.Id == id);

            if (booking == null) 
                return false;

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return true;
        }

        public Patient GetPatientById(String id)
        {
            return _context.Patients.
                Single(i => i.Id == id);
        }

        public List<Booking> GetBookings(string? doctorId = null, int? specializationId = null)
        {
            var bookings = _context.Bookings.AsQueryable();

            if (doctorId != null)
            {
                bookings = bookings.Where(b => b.DoctorID == doctorId);
            }

            if (specializationId != null)
            {
                bookings = bookings.Where(b => b.SpecializationId == specializationId);
            }

            return bookings.ToList();
        }

        public List<Specialization> GetSpecializations()
        {
            return _context
                .Specializations.ToList();
        }

        public Booking CreateBooking(Booking booking)
        {
            //Speciaziáció alapú foglalásnál a booking doctorID-ját még nem töltjük ki, így nem lesz hiba.
            var existingBooking = _context.Bookings.FirstOrDefault(b => b.DateTime == booking.DateTime && b.DoctorID == booking.DoctorID);
            if (existingBooking != null)
            {
                throw new InvalidOperationException("Az adott időpont már le lett foglalva");
            }

            if (booking.SpecializationId != null)
            {
                // Megkeressük a doktort a legkevesebb előjegyzéssel, aki rendelkezik az adott specializációval
                var doctors = _context.Doctors.Include(d => d.Specializations).Where(d => d.Specializations.Any(s => (Int32)s.SpecName == booking.SpecializationId)); // Rendelkezik megfelelő specializációval
                var doctor = doctors.OrderBy(d => _context.Bookings.Count(b => b.DoctorID == d.Id)).FirstOrDefault(); // Számlálunk, rendezünk és elsőt vesszük
                if (doctor == null)
                {
                    throw new InvalidOperationException("Nincs elérhető orvos a megfelelő szakterületre");
                }
                booking.DoctorID = doctor.Id;
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();

             return booking;
        }

        public Doctor LeastBookedDoctor(Booking booking)
        {
            var doctors = _context.Doctors.Include(d => d.Specializations).Where(d => d.Specializations.Any(s => (Int32)s.SpecName == booking.SpecializationId)); // Rendelkezik megfelelő specializációval
            var doctor = doctors.OrderBy(d => _context.Bookings.Count(b => b.DoctorID == d.Id)).FirstOrDefault(); // Számlálunk, rendezünk és elsőt vesszük

            /*
            if (doctor == null)
            {
                Doctor doc = new Doctor();
                doc.Id = -1;
                return doc;
            }
            */
            return doctor;
        }

        public List<Booking> GetBookingsByDoctorId(string docid)
        {
            return _context.Bookings.
                Where(i => i.DoctorID == docid && DateTime.Now <= i.DateTime).
                ToList();
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.
                Single(i => i.Id == id);

        }

        public MedicalRecord? CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            try
            {
                /*
                medicalRecord.Doctor = _context.Doctors.
                    Single(i => i.Id == medicalRecord.DoctorId);
                medicalRecord.Patient = _context.Patients.
                   Single(i => i.Id == medicalRecord.PatientId);
                */

                _context.MedicalRecords.Add(medicalRecord);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return medicalRecord;
        }

        public Treatment? CreateTreatment(Treatment treatment)
        {
            try
            {
                _context.Treatments.Add(treatment);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return treatment;
        }

        public bool DeleteTreatment(int id)
        {
            var treatment = _context.Treatments.Find(id);
            if (treatment == null)
            {
                return false;
            }

            try
            {
                _context.Remove(treatment);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public MedicalRecord GetMedicalRecordById(int id)
        {
            return _context.MedicalRecords.
                Single(i => i.Id == id);
        }

        public Treatment GetTreatmentById(int id)
        {
            return _context.Treatments.
                Single(i => i.Id == id);
        }

        public List<Treatment> GetTreatments()
        {
            return _context.Treatments.ToList();
        }

        public bool UpdateMedicalRecord(MedicalRecord medicalrecord)
        {
            try
            {
                _context.Update(medicalrecord);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public List<MedicalRecord> GetMedicalRecords(string docid)
        {
            return _context.MedicalRecords.
              Where(i => i.DoctorId == docid && DateTime.Now <= i.DateTime).
              ToList();
        }

        public bool UpdateTreatment(Treatment treatment)
        {
            try
            {
                _context.Update(treatment);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }


    }
}
