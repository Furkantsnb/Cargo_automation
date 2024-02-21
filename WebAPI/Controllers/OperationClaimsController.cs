using Business.Abstract;
using Core.Entities.Concrete;
using DataAccsess.Abstract;
using Entities.Concrete;
using Entities.Dtos.OperationClaims;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : Controller
    {
       private readonly IOperationClaimService _operationClaiService;

        public OperationClaimsController(IOperationClaimService operationClaiService)
        {
            _operationClaiService = operationClaiService;
        }


        [HttpPost("add")]
        public IActionResult Add(CreateOperationClaimDto operationClaim)
        {
            var result = _operationClaiService.Add(operationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaiService.Delete(operationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("uptade")]
        public IActionResult Update(UpdateOperationClaimDto operationClaim)
        {
            var result = _operationClaiService.Update(operationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _operationClaiService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList(int unitId)
        {
            var result = _operationClaiService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
