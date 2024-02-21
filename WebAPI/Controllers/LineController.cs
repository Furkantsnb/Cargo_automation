using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Lines;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : Controller
    {
        private readonly ILineService _lineService;
        private readonly IMapper _mapper;

        public LineController(ILineService lineService, IMapper mapper)
        {
            _lineService = lineService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult Add(CreateLineDto createLineDto)
        {
           
            var result = _lineService.Add(createLineDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateLineDto updateLineDto)
        {
            var result = _lineService.Update(updateLineDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _lineService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _lineService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _lineService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
