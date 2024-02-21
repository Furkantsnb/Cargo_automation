using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Agentas;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentaController : Controller
    {
        private readonly IAgentaService _agentaService;
        private readonly IAgentaDal _agentaDal;

        public AgentaController(IAgentaService agentaService,  IAgentaDal agentaDal)
        {
            _agentaService = agentaService;
            _agentaDal = agentaDal;
        }

        [HttpGet("getAgentaList")]
        public IActionResult GetAgentaList()
        {
            var result = _agentaService.GetList();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }

        [HttpGet("getAgenta")]
        public IActionResult GetById(int id)
        {
            var result = _agentaService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }


        [HttpPost("addAgenta")]
        public IActionResult AddAgenta(CreateAgentaDto agentaDto)
        {
            var result = _agentaService.Add(agentaDto);
            if (result != null)
            {
                return Ok(result);

            }
            return BadRequest();
        }

        [HttpPut("updateAgenta")]
        public IActionResult UpdateAgenta(UpdateAgentaDto agentaDto)
        {
            var result = _agentaService.Update(agentaDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpDelete("deleteAgenta")]
        public IActionResult DeleteAgenta(int id)
        {
            var result = _agentaService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("softdelete")]
        public IActionResult SoftDelete(int id)
        {
            var result = _agentaService.SoftDelete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
