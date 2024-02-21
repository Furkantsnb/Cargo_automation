using AutoMapper;
using Business.Abstract;
using Entities.Dtos.Stations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;
        private readonly IMapper _mapper;

        public StationController(IStationService stationService, IMapper mapper)
        {
            _stationService = stationService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult Add(CreateStationDto createStationDto)
        {
            var result = _stationService.Add(createStationDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        //[HttpPost("addrange")]
        //public IActionResult AddRange(List<CreateStationDto> createStationDtos)
        //{
        //    var result = _stationService.AddRange(createStationDtos);
        //    if (result.Success)
        //    {
        //        return Ok(result.Message);
        //    }
        //    return BadRequest(result.Message);
        //}

        [HttpPut("update")]
        public IActionResult Update(UpdateStationDto updateStationDto)
        {
            var result = _stationService.Update(updateStationDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int stationId)
        {
            var result = _stationService.Delete(stationId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int stationId)
        {
            var result = _stationService.GetById(stationId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _stationService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }
    }
}
