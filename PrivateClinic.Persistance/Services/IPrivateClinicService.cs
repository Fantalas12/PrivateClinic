using PrivateClinic.Persistence;

namespace PrivateClinic.Persistence.Services
{
    public interface IPrivateClinicService
    {

        public List<Doctor> GetDoctors();
        public Doctor GetDoctorById(string id);
        //public bool AddBooking(Booking booking);
        public bool AddPatient(Patient patient);
        public Doctor GetDoctorDetails(string id);
        public Booking GetBooking(int id);

        public bool DeleteBookingById(Int32 id);

        public List<Booking> GetBookings(string? doctorId = null, int? specializationId = null);


        public List<Specialization> GetSpecializations();

        public Booking CreateBooking(Booking booking);


        public Patient GetPatientById(string id);

        public Doctor LeastBookedDoctor(Booking booking);

        public List<Booking> GetBookingsByDoctorId(string docid);

        public Booking GetBookingById(int id);

        public MedicalRecord? CreateMedicalRecord(MedicalRecord medicalRecord);

        public Treatment? CreateTreatment(Treatment treatment);

        public bool DeleteTreatment(int id);

        public MedicalRecord GetMedicalRecordById(int id);

        public Treatment GetTreatmentById(int id);

        public List<Treatment> GetTreatments();

        public bool UpdateMedicalRecord(MedicalRecord medicalrecord);


        public List<MedicalRecord> GetMedicalRecords(string docid);

        public bool UpdateTreatment(Treatment treatment);

    }
}
