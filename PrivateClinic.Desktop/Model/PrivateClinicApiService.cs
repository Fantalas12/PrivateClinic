using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.DTO;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;

namespace PrivateClinic.Desktop.Model
{
    public class PrivateClinicApiService
    {
        private readonly HttpClient _client;

        public PrivateClinicApiService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        #region Authentication

        public async Task<bool> LoginAsync(string name, string password)
        {
            LoginDTO user = new LoginDTO
            {
                UserName = name,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response);
        }

        #endregion

        #region Booking


        public async Task<IEnumerable<BookingDTO>> LoadBookingsAsync()
        {
            var response = await _client.GetAsync("api/Bookings/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<BookingDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task DeleteBookingAsync(Int32 bookingid)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Bookings/{bookingid}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }


        #endregion


        #region MedicalRecord

        public async Task<IEnumerable<MedicalRecordDTO>> LoadMedicalRecordsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/MedRecords/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<MedicalRecordDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateMedicalRecordAsync(MedicalRecordDTO medicalRecord)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/MedRecords/", medicalRecord);
            medicalRecord.Id = (await response.Content.ReadAsAsync<MedicalRecordDTO>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response " + response);
            }
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecordDTO medicalRecord)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/MedRecords/{medicalRecord.Id}", medicalRecord);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response " + response);
            }
        }



        #endregion


        #region Treatment

        public async Task<IEnumerable<TreatmentDTO>> LoadTreatmentsAsync(int medrecordid)
        {
            HttpResponseMessage response = await _client.GetAsync(
                QueryHelpers.AddQueryString("api/Treatments/", "medrecordid", medrecordid.ToString()));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<TreatmentDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateTreatmentAsync(TreatmentDTO treatment)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Treatments/", treatment);
            treatment.Id = (await response.Content.ReadAsAsync<TreatmentDTO>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateTreatmentAsync(TreatmentDTO treatment)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Treatments/{treatment.Id}", treatment);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteTreatmentAsync(Int32 treatmentid)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Treatments/{treatmentid}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }




        #endregion



    }

}
