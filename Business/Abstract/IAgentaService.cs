using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Agentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Abstract
{
    public interface IAgentaService
    {
        IResult Add(CreateAgentaDto agenta);
        IResult Update(UpdateAgentaDto agenta);
        IResult Delete(int id);
        IResult SoftDelete(int id);
        IDataResult<Agenta> GetById(int id);
        IDataResult<List<Agenta>> GetList();
        IResult AgentaExists(Agenta agenta);// acentaların veri tabanında olup olmadığını kontrol ediyor. 
    }
}
