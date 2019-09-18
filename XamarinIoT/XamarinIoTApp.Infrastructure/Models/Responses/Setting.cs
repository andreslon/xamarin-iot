using System;
namespace XamarinIoTApp.Infrastructure.Models.Responses
{
    public class Setting
    {
        public string settingsId { get; set; }
        public Theme theme { get; set; }
        public Client client { get; set; }
    }
}
