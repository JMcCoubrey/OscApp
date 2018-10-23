using Microsoft.AspNetCore.Mvc;
using Osc.Db;
using OscApp.DAL;
using OscApp.Web.Controllers.Api.Dto;

namespace OscApp.Web.Controllers.Api
{
    [Route("api/staff")]
    public class StaffController : GenericDataApiController<Staff, StaffDto, ITrainerRepository>
    {

        public StaffController(ITrainerRepository repo) : base(repo)
        {
        }
	}
}
