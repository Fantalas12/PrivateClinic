using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace PrivateClinic.Persistence
{
    public class CalendarViewModel
    {

        public DateTime Date { get; set; }
        public List<Booking> Bookings { get; set; } = null!;
        public List<Specialization> Specializations { get; set; } = null!;
        public List<Patient> Patients { get; set; } = null!;

        public Doctor SelectedDoctor { get; set; } = null!;

        public Int32 SelectedSpecializationId { get; set; }

        [DisplayName("Megjegyzés")]
        public String? Comment { get; set; } = null!;

        public Booking booking { get; set; } =null!;

        public int pageid { get; set; }
    }

    //Forrás: stackoverflow...Segédkiegészítő osztály a hét elejének meghatározásához
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

}