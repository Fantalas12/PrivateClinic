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
    public class TreatmentViewModel : ViewModelBase //, IEditableObject
    {

        private Int32 _id;
        private Int32 _medRecordId;
        private String _description;
        private Int32 _price;

        //private TreatmentViewModel _backup;

        public Int32 Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public Int32 MedRecordId
        {
            get => _medRecordId;
            set
            {
                _medRecordId = value;
                OnPropertyChanged();
            }
        }

        public String Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public Int32 Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public TreatmentViewModel ShallowClone()
        {
            return (TreatmentViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(TreatmentViewModel rhs)
        {
            Id = rhs.Id;
            Description = rhs.Description;
            Price = rhs.Price;
            MedRecordId = rhs.MedRecordId;
        }

        public static explicit operator TreatmentViewModel(TreatmentDTO dto) => new TreatmentViewModel
        {
            Id = dto.Id,
            MedRecordId = dto.MedRecordId,
            Description = dto.Description,
            Price = dto.Price
        };

        public static explicit operator TreatmentDTO(TreatmentViewModel vm) => new TreatmentDTO
        {
            Id = vm.Id,
            MedRecordId = vm.MedRecordId,
            Description = vm.Description,
            Price = vm.Price
        };

        /*
        public Boolean IsDirty { get; private set; } = false;

        public String Error => String.Empty;

        public void BeginEdit()
        {
            if (!IsDirty)
            {
                _backup = (TreatmentViewModel)this.MemberwiseClone();
                IsDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (IsDirty)
            {
                Id = _backup.Id;
                MedRecordId = _backup.MedRecordId;
                Description = _backup.Description;
                Price = _backup.Price;

                IsDirty = false;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (IsDirty)
            {
                IsDirty = false;
                _backup = null;
            }
        }
        */


    }
}
