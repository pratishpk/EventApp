using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Model;

namespace EventManagementFrontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5199/");
        }

        // Method to set the Authorization header with the token
        public void SetToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            else
                _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Login method, retrieves token after successful authentication
        public async Task<string> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/login", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenJson = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Extract the token from the response
            return tokenJson != null && tokenJson.TryGetValue("token", out var token) ? token : null;
        }

        // Example of using the SetToken method to set the token in the request headers
        public async Task<List<EventDetails>> GetEventsAsync()
        {
            // You can call this method after setting the token
            var response = await _httpClient.GetAsync("api/EventDetails");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EventDetails>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<EventDetails> GetEventByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/EventDetails/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventDetails>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> CreateEventAsync(EventDetails eventDetails)
        {
            var json = JsonSerializer.Serialize(eventDetails);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/EventDetails", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEventAsync(EventDetails eventDetails)
        {
            var json = JsonSerializer.Serialize(eventDetails);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/EventDetails/{eventDetails.EventId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/EventDetails/{id}");
            return response.IsSuccessStatusCode;
        }

        // The following methods for Session and Speakers can be similarly modified to use SetToken
        public async Task<List<SessionInfo>> GetSessionsAsync()
        {
            var response = await _httpClient.GetAsync("api/SessionInfo");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SessionInfo>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SessionInfo> GetSessionByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/SessionInfo/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SessionInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> CreateSessionAsync(SessionInfo session)
        {
            var json = JsonSerializer.Serialize(session);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/SessionInfo", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSessionAsync(SessionInfo session)
        {
            var json = JsonSerializer.Serialize(session);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/SessionInfo/{session.SessionId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSessionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/SessionInfo/{id}");
            return response.IsSuccessStatusCode;
        }

        // Speaker methods
        public async Task<List<SpeakersDetails>> GetSpeakersAsync()
        {
            var response = await _httpClient.GetAsync("api/SpeakersDetails");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SpeakersDetails>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SpeakersDetails> GetSpeakerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/SpeakersDetails/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SpeakersDetails>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> CreateSpeakerAsync(SpeakersDetails speaker)
        {
            var json = JsonSerializer.Serialize(speaker);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/SpeakersDetails", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSpeakerAsync(SpeakersDetails speaker)
        {
            var json = JsonSerializer.Serialize(speaker);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/SpeakersDetails/{speaker.SpeakerId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSpeakerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/SpeakersDetails/{id}");
            return response.IsSuccessStatusCode;
        }

        // Get user details
        public async Task<UserInfo> GetUserByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"api/userinfo/{email}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Register new user
        public async Task<bool> RegisterUserAsync(UserInfo user)
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/userinfo", content);
            return response.IsSuccessStatusCode;
        }
    }
}
