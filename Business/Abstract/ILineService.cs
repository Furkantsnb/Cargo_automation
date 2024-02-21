using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos.Lines;

namespace Business.Abstract
{
    public interface ILineService
    {
        IDataResult<List<Line>> GetList();
        IDataResult<Line> GetById(int id);
        IResult Add(CreateLineDto createLineDto);
        IResult Update(UpdateLineDto updateLineDto);
        IResult Delete(int id);
    }
}
