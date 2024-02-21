using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.TransferCenter;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferCenterController : Controller
    {
        private readonly ITransferCenterService _transferCenterService;

        public TransferCenterController(ITransferCenterService transferCenterService)
        {
            _transferCenterService = transferCenterService;
         
        }

        [HttpGet("getTransferCenterList")]
        public IActionResult GetTransferCenterList()
        {
            var result = _transferCenterService.GetList();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }

        [HttpGet("getTransferCenter")]
        public IActionResult GetById(int id)
        {
            var result = _transferCenterService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }

        [HttpPost("addTransferCenter")]

        public IActionResult AddTransferCenter(CreateTransferCenterDto transferCenterDto)
        {

            var result = _transferCenterService.Add(transferCenterDto);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }

        [HttpPut("updateTransferCenter")]
        public IActionResult UpdateTransferCenter(UpdateTransferCenterDto transferCenterDto)
        {
            var result = _transferCenterService.Update(transferCenterDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }

        [HttpDelete("deleteTransferCenter")]
        public IActionResult DeleteTransferCenter(int id)
        {
            var result = _transferCenterService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("SoftDeleteTransferCenter")]
        public IActionResult SoftDeleteTransferCenter(int id)
        {
            var result = _transferCenterService.SoftDelete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("getAgentasByTransferCenterId")]
        public IActionResult GetAgentasByTransferCenterId(int transferCenterId)
        {
            var result = _transferCenterService.GetAgentasByTransferCenterId(transferCenterId);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


    }
}
