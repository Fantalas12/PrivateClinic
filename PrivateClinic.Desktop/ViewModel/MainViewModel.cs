using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Data;
using PrivateClinic.Desktop.Model;
using PrivateClinic.DTO;
using PrivateClinic.Persistence;

namespace PrivateClinic.Desktop.ViewModel
{
    /*
    public class MedicalRecordEventArg : EventArgs
    {
        private MedicalRecordViewModel  _medRecordvm;

        public MedicalRecordViewModel MedicalRecord
        {
            get { return _medRecordvm; }
            set { _medRecordvm = value; }
        }

        public MedicalRecordEventArg(MedicalRecordViewModel vm)
        {
            _medRecordvm = vm;
        }

    } */


    public class SelectedBookingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BookingViewModel)
                return value;
            return null;
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private readonly PrivateClinicApiService _service;

        private ObservableCollection<BookingViewModel> _bookings;
        private ObservableCollection<MedicalRecordViewModel> _medicalrecords;
        private ObservableCollection<TreatmentViewModel> _treatments;
        private BookingViewModel _selectedBooking;
        private MedicalRecordViewModel _selectedMedicalRecord;
        private TreatmentViewModel _selectedTreatment;
        private MedicalRecordViewModel _editableMedicalRecord;
        private Int32 _selectedMedicalRecordSum;
        private String _newTreatmentDescription;
        private Int32 _newTreatmentCost;


        public List<MedicalRecordViewModel> MedicalRecordsForCombo
        {
            get => MedicalRecords.ToList();
        }

        public ObservableCollection<BookingViewModel> Bookings
        {
            get { return _bookings; }
            set { _bookings = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MedicalRecordViewModel> MedicalRecords
        {
            get { return _medicalrecords; }
            set { _medicalrecords = value; OnPropertyChanged(); }
        }


        public ObservableCollection<TreatmentViewModel> Treatments
        {
            get { return _treatments; }
            set { _treatments = value; OnPropertyChanged(); }
        }


        public BookingViewModel SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged();
            }
        }

        public MedicalRecordViewModel SelectedMedicalRecord
        {
            get => _selectedMedicalRecord;
            set
            {
                _selectedMedicalRecord = value;
                OnPropertyChanged();
            }
        }


        public TreatmentViewModel SelectedTreatment
        {
            get => _selectedTreatment;
            set
            {
                _selectedTreatment = value;
                OnPropertyChanged();
            }
        }

        public Int32 SelectedMedicalRecordSum
        {
            get => _selectedMedicalRecordSum;
            set
            {
                _selectedMedicalRecordSum = value;
                OnPropertyChanged();
            }
        }

        public String NewTreatmentDescription
        {
            get => _newTreatmentDescription;
            set
            {
                _newTreatmentDescription = value;
                OnPropertyChanged();
            }
        }

        public Int32 NewTreatmentCost
        {
            get => _newTreatmentCost;
            set
            {
                _newTreatmentCost = value;
                OnPropertyChanged();
            }
        }

        public MedicalRecordViewModel EditableMedicalRecord
        {
            get => _editableMedicalRecord;
            set
            {
                _editableMedicalRecord = value;
                OnPropertyChanged();
            }
        }



        public DelegateCommand RefreshDataCommand { get; private set; }

        public DelegateCommand BookingSelectedCommand { get; private set; }
        public DelegateCommand SelectMedicalRecordCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand AddMedicalRecordCommand { get; private set; }

        public DelegateCommand AddTreatmentCommand { get; private set; }

        public DelegateCommand DeleteTreatmentCommand { get; private set; }

        public DelegateCommand SaveTreatmentEditCommand { get; private set; }

        public DelegateCommand CancelTreatmentEditCommand { get; private set; }

        public DelegateCommand SaveMedicalRecordEditCommand { get; private set; }

        public DelegateCommand NewMedicalRecordCommand { get; private set; }

        public DelegateCommand EndMedicalRecordEditCommand { get; private set; }


        public event EventHandler LogoutSucceeded;

        //public event EventHandler StartingTreatmentEdit;

        public event EventHandler MedicalRecordEditStarted;

        public event EventHandler MedicalRecordEditEnded;


        public MainViewModel(PrivateClinicApiService service)
        {
            _service = service;

            RefreshDataCommand = new DelegateCommand(_ => LoadDataAsync());
            BookingSelectedCommand = new DelegateCommand(_ => !(SelectedBooking is null), _ => BookingSelected(SelectedBooking));
            //SelectMedicalRecordCommand = new DelegateCommand(param => LoadTreatmentsAsync(SelectedMedicalRecord));
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());

            AddMedicalRecordCommand = new DelegateCommand(_ => AddMedicalRecord(SelectedBooking));
            AddTreatmentCommand = new DelegateCommand(_ => !(SelectedMedicalRecord is null) && SelectedMedicalRecord.Id != 0,
                _ => AddTreatment(SelectedMedicalRecord));
            DeleteTreatmentCommand = new DelegateCommand(_ => !(SelectedTreatment is null), _ => DeleteTreatment(SelectedTreatment));
            EndMedicalRecordEditCommand = new DelegateCommand(_ => EndMedicalRecordEdit());
            //CancelTreatmentEditCommand = new DelegateCommand(_ => CancelTreatmentEdit());


        }

        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                OnLogoutSuccess();
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private void OnLogoutSuccess()
        {
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);
        }

        private async void LoadBookingsAsync()
        {
            try
            {
                Bookings = new ObservableCollection<BookingViewModel>((await _service.LoadBookingsAsync()).Select(booking =>
                {
                    var bookingVm = (BookingViewModel)booking;
                    //medRecordVm.EditEnded += MedicalRecordViewModel_EditEnded;
                    return bookingVm;
                }));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            //SelectedMedicalRecord = (MedicalRecordViewModel)SelectedBooking;
        }

        private async void LoadMedicalRecordsAsync()
        {
            try
            {
                MedicalRecords = new ObservableCollection<MedicalRecordViewModel>((await _service.LoadMedicalRecordsAsync()).Select(medicalRecord =>
                {
                    var medRecordVm = (MedicalRecordViewModel)medicalRecord;
                    //medRecordVm.EditEnded += MedicalRecordViewModel_EditEnded;
                    return medRecordVm;
                }));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void LoadDataAsync()
        {
            LoadBookingsAsync();
            LoadMedicalRecordsAsync();
            if (SelectedMedicalRecord is not null) LoadTreatmentsAsync(SelectedMedicalRecord);
        }

        private void BookingSelected(BookingViewModel booking)
        {
            AddMedicalRecord(booking);
            //RemoveBooking(booking);
            LoadMedicalRecordsAsync();
            LoadBookingsAsync();
        }

        //async
        private async void AddMedicalRecord(BookingViewModel booking)
        {
            var newMedicalRecord = new MedicalRecordViewModel
            {
                //Id = booking.Id,
                PatientId = booking.PatientId,
                DoctorId = booking.DoctorId,
                DateTime = booking.DateTime,
                PatientName = booking.PatientName,
            };

            var medicalRecordDto = (MedicalRecordDTO)newMedicalRecord;

            try
            {
                await _service.CreateMedicalRecordAsync(medicalRecordDto);
                newMedicalRecord.Id = medicalRecordDto.Id;
                MedicalRecords.Add((MedicalRecordViewModel)medicalRecordDto);
                SelectedMedicalRecord = newMedicalRecord;

                await _service.DeleteBookingAsync(booking.Id);
                Bookings.Remove(SelectedBooking);
                SelectedBooking = null;
                OnMedicalRecordEditStarted();


            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }



            //Saját kód
            /*
            try
            {
                await _service.DeleteBookingAsync(SelectedBooking.Id);
                Bookings.Remove(SelectedBooking);
                SelectedBooking = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            */

        }

        //private async void LoadTreatmentsAsync(MedicalRecordViewModel medicalRecord)
            private async void LoadTreatmentsAsync(MedicalRecordViewModel medicalRecord)
            {
                if (medicalRecord is null)
                {
                    Treatments = null;
                    return;
                }

                try
                {
                    Treatments = new ObservableCollection<TreatmentViewModel>((await _service.LoadTreatmentsAsync(medicalRecord.Id))
                        .Select(treatment => (TreatmentViewModel)treatment));

                }
                catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
                {
                    OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
                }
            }
        

        private void OnMedicalRecordEditStarted()
        {
            MedicalRecordEditStarted?.Invoke(this, EventArgs.Empty);
        }

        private void OnMedicalRecordEditEnded()
        {
            MedicalRecordEditEnded?.Invoke(this, EventArgs.Empty);
        }

        private async void AddTreatment(MedicalRecordViewModel medicalRecord)
        {
            var newTreatment = new TreatmentViewModel
            {
                Description = NewTreatmentDescription,
                Price = NewTreatmentCost,
                MedRecordId = medicalRecord.Id
            };

            var treatmentDto = (TreatmentDTO)newTreatment;

            try
            {
                await _service.CreateTreatmentAsync(treatmentDto);
                newTreatment.Id = treatmentDto.Id;
                Treatments.Add(newTreatment);
                SelectedTreatment = newTreatment;
                medicalRecord.SumPrice = medicalRecord.SumPrice + newTreatment.Price;
                LoadTreatmentsAsync(SelectedMedicalRecord);
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async void DeleteTreatment(TreatmentViewModel treatment)
        {
            try
            {
                await _service.DeleteTreatmentAsync(treatment.Id);
                SelectedMedicalRecord.SumPrice = SelectedMedicalRecord.SumPrice - treatment.Price;
                Treatments.Remove(SelectedTreatment);
                SelectedTreatment = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async void EndMedicalRecordEdit()
        {
            try
            {
                //SelectedItem.CopyFrom(EditableItem);
                //await _service.UpdateMedicalRecordAsync((MedicalRecordDTO)SelectedMedicalRecord);
                LoadBookingsAsync();
                LoadMedicalRecordsAsync();
                Treatments.Clear(); 
                SelectedMedicalRecord = null;
                SelectedTreatment = null;
                OnMedicalRecordEditEnded();
                /*
                if (SelectedItem.ListId != SelectedList.Id)
                {
                    Items.Remove(SelectedItem);
                    SelectedItem = null;
                }
                */
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }




    }
}
