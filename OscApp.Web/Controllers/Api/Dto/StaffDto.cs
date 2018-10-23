namespace OscApp.Web.Controllers.Api.Dto
{
    public class StaffDto : BaseDto
    {
        public string id { get; set; }
        public string description { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
        public string lineManager { get; set; }
    }
}
