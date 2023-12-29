using Common.CustomErrorResponses;
using Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralValueController : BaseController
    {
        private readonly IGeneralValueRepository _repository;
        public GeneralValueController(IGeneralValueRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{typeId}")]
        public async Task<ActionResult<ApiResponse>> GetGeneralValues(int typeId)
        {
            try
            {
                var result = await _repository.GetGeneralValues(typeId);

                if (result == null || result.Count == 0)
                {
                    return NotFound("Records not found");
                }

                return CustomResponse(HttpStatusCode.OK, new List<string> { "Success" }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
