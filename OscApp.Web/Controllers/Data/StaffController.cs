using Microsoft.AspNetCore.Mvc;
using Osc.Db;
using OscApp.DAL;

namespace OscApp.Web.Controllers.Data
{
    [Route("data/staff")]
    public class StaffController : GenericDataController<Staff, ITrainerRepository>
    {
        public StaffController(ITrainerRepository repo) : base(repo)
        {
        }
    }
}