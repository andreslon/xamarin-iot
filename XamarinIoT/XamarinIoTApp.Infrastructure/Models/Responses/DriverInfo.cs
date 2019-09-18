using System;
namespace XamarinIoTApp.Infrastructure.Models.Responses
{
    public class DriverInfo
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public string Token_type { get; set; }
        public string Scope { get; set; }
        public string DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
