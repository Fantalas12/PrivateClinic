using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using PrivateClinic.DTO;


namespace PrivateClinic.Desktop.ViewModel
{
    public class BookingViewModel : ViewModelBase //,IEditableObject
    {
        
        private Int32 _id;
        private String _patientID;
        private String _doctorID;
        private DateTime _dateTime;
        private String _specName;
        private String _patientName;

        //private Boolean _isDirty = false;
        //private BookingViewModel _backup;


        public Int32 Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public String PatientId
        {
            get => _patientID;
            set
            {
                _patientID = value;
                OnPropertyChanged();
            }
        }

        public String DoctorId
        {
            get => _doctorID;
            set
            {
                _doctorID = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        public String SpecName
        {
            get => _specName;
            set
            {
                _specName = value;
                OnPropertyChanged();
            }
        }
        public String PatientName
        {
            get => _patientName;
            set
            {
                _patientName = value;
                OnPropertyChanged();
            }
        }

        /*
        public event EventHandler EditEnded;

        public void BeginEdit()
        {
            if (!_isDirty)
            {
                _backup = (BookingViewModel)this.MemberwiseClone();
                _isDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (_isDirty)
            {
                Id = _backup.Id;
                _patientID = _backup.PatientId;
                _doctorID = _backup.DoctorId;
                _dateTime = _backup.DateTime;
                _specName = _backup.SpecName;
                _isDirty = false;
                _backup = null;

            }
        }

        public void EndEdit()
        {
            if (_isDirty)
            {
                EditEnded?.Invoke(this, EventArgs.Empty);
                _isDirty = false;
                _backup = null;
            }
        }
        */

        public static explicit operator BookingViewModel(BookingDTO dto) => new BookingViewModel
        {
            Id = dto.Id,
            PatientId = dto.PatientID,
            DoctorId = dto.DoctorID,
            DateTime = dto.DateTime,
            SpecName = dto.SpecName,
            PatientName = dto.PatientName
        };

        public static explicit operator BookingDTO(BookingViewModel vm) => new BookingDTO
        {
            Id = vm.Id,
            PatientID = vm.PatientId,
            DoctorID = vm.DoctorId,
            DateTime = vm.DateTime,
            SpecName = vm.SpecName,
            PatientName = vm.PatientName
        };






    }
}
