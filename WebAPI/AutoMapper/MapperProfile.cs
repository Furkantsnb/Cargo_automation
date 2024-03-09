using AutoMapper;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dtos.Agentas;
using Entities.Dtos.Lines;
using Entities.Dtos.Mails;
using Entities.Dtos.OperationClaims;
using Entities.Dtos.Stations;
using Entities.Dtos.TransferCenter;

namespace WebAPI.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<MailParameter, UpdateMailParameterDto>().ReverseMap();

            CreateMap<OperationClaim, CreateOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimDto>().ReverseMap();

            CreateMap<UserOperationClaim, CreateUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimDto>().ReverseMap();


            CreateMap<Agenta, UpdateAgentaDto>().ReverseMap();
            CreateMap<Agenta, CreateAgentaDto>().ReverseMap();

            CreateMap<Line, CreateLineDto>().ReverseMap();
            CreateMap<Line, UpdateLineDto>().ReverseMap();
            CreateMap<Line, LineDto>().ReverseMap();

            CreateMap<Station, CreateStationDto>().ReverseMap();
            CreateMap<Station, UpdateStationDto>().ReverseMap();
            CreateMap<Station, StationDto>().ReverseMap();

            CreateMap<TransferCenter, UpdateTransferCenterDto>().ReverseMap();
            CreateMap<TransferCenter, CreateTransferCenterDto>().ReverseMap();

            CreateMap<CreateLineDto, Line>()
          .ForMember(dest => dest.Stations, opt => opt.Ignore());

            CreateMap<UpdateLineDto, Line>()
        .ForMember(dest => dest.Stations, opt => opt.Ignore());


            

        }
    }
}
