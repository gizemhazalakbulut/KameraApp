namespace KameraApp.Models
{
    public class Organization
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }

        public string DeparmentsCount { get; set; }
        public string DomainId { get; set; }
        public string OrgType { get; set; }
        public string Sn { get; set; }
        public string SourceType { get; set; }

        public List<object> Channel { get; set; }  // Eğer detaylar belli değilse object kalabilir
        public List<object> Device { get; set; }

        public List<Organization> Children { get; set; } = new List<Organization>();
    }
}
