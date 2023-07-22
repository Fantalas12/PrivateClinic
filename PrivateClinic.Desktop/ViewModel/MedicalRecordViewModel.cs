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
    public class MedicalRecordViewModel : ViewModelBase //, IEditableObject
    {
        private Int32 _id;
        private String _patientID;
        private String _doctorID;
        private DateTime _dateTime;
        private Int32 _sumPrice;
        private String _patientName;



        //private Boolean _isDirty = false;
        //private MedicalRecordViewModel _backup;


        public Int32 Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string PatientId
        {
            get => _patientID;
            set
            {
                _patientID = value;
                OnPropertyChanged();
            }
        }

        public string DoctorId
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

        public int SumPrice
        {
            get => _sumPrice;
            set
            {
                _sumPrice = value;
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

        public MedicalRecordViewModel ShallowClone()
        {
            return (MedicalRecordViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(MedicalRecordViewModel rhs)
        {
            Id = rhs.Id;
            PatientId = rhs.PatientId;
            DoctorId = rhs.DoctorId;
            DateTime = rhs.DateTime;
            SumPrice = rhs.SumPrice;
            PatientName = rhs.PatientName;
        }


        public static explicit operator MedicalRecordViewModel(MedicalRecordDTO dto) => new MedicalRecordViewModel
        {
            Id = dto.Id,
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            DateTime = dto.DateTime,
            SumPrice = dto.SumPrice,
            PatientName = dto.PatientName,
        };

        public static explicit operator MedicalRecordDTO(MedicalRecordViewModel vm) => new MedicalRecordDTO
        {
            Id = vm.Id,
            PatientId = vm.PatientId,
            DoctorId = vm.DoctorId,
            DateTime = vm.DateTime,
            SumPrice = vm.SumPrice,
            PatientName = vm.PatientName,
        };

        /*
        public event EventHandler EditEnded;

        public void BeginEdit()
        {
            if (!_isDirty)
            {
                _backup = (MedicalRecordViewModel)this.MemberwiseClone();
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
                _sumPrice = _backup.SumPrice;
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

        /*
        public static explicit operator MedicalRecordViewModel(BookingViewModel vm) => new MedicalRecordViewModel
        {
            Id = vm.Id,
            PatientId = vm.PatientId,
            DoctorId = vm.DoctorId,
            DateTime = vm.DateTime,
            PatientName = vm.PatientName
        };

        public static explicit operator BookingViewModel(MedicalRecordViewModel vm) => new BookingViewModel
        {
            Id = vm.Id,
            PatientId = vm.PatientId,
            DoctorId = vm.DoctorId,
            DateTime = vm.DateTime,
            PatientName = vm.PatientName
        };
        */







    }
}
