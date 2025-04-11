namespace KameraApp.Models
{
    public class Organization
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }

        public List<OrganizationDeviceRef> Device { get; set; }  // ✅ Düzeltildi

        public List<Device> MatchedDevices { get; set; } = new();
        public List<Organization> Children { get; set; } = new();
    }



}
