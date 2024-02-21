using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos.Stations;

namespace Business.Abstract
{
    public interface IStationService
    {
        IResult Add(CreateStationDto createStationDto);
        IResult AddRange(List<Station> stations); // AddRange metodu
        IResult Update(UpdateStationDto updateStationDto);
        IResult Delete(int stationId);
        IDataResult<StationDto> GetById(int stationId);
        IDataResult<List<StationDto>> GetList();
       
    }
}
