using Business.Abstract;
using Business.Concrete;
using Business.Constans;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Agentas;
using Moq;
using Xunit;
using FluentAssertions;
using AutoMapper;

namespace CargoAutomationTest.Test
{
    public class AgentaTest
    {
        private readonly Mock<IAgentaService> _mockAgentaService;
        private readonly Mock<IAgentaDal> _mockAgentaDal;
        private readonly AgentaManager _agentaManager;
        private readonly IMapper _mapper;

        public AgentaTest(Mock<IAgentaService> mockAgentaService, Mock<IAgentaDal> mockAgentaDal, AgentaManager agentaManager, IMapper mapper)
        {
            _mockAgentaService = mockAgentaService;
            _mockAgentaDal = mockAgentaDal;
            _agentaManager = agentaManager;
            _mapper = mapper;
        }

        [Fact]
        public void TestUpdateAgenta()
        {
            // Örnek bir Agenta nesnesi oluştur
            var agenta = new Agenta
            {
                UnitId = 1,
                UnitName = "Antalya",
                ManagerName = "Furkan",
                ManagerSurname = "Taşan",
                PhoneNumber = "123123123",
                Gsm = "123123",
                Email = "furkantsn@gmail.com",
                Description = "açıklama",
                City = "Antalya",
                District = "kepez",
                NeighbourHood = "Güneş Mh.",
                Street = "6033sk.",
                AddressDetail = "Adres Detay",
                IsDeleted = false,
                TransferCenterId = 4
            };

            // AutoMapper'ı kullanarak Agenta nesnesini UpdateAgentaDto'ya dönüştür
            var updateDto = _mapper.Map<UpdateAgentaDto>(agenta);

            // Dal katmanı mock'unun davranışını ayarla
            _mockAgentaDal.Setup(p => p.Get(u => u.UnitId == updateDto.UnitId)).Returns(agenta);

            // Manager'ın Update metodunu çağır
            _agentaManager.Update(updateDto);

            // Dal katmanı mock'unun Update metodunun bir kez çağrıldığını doğrula
            _mockAgentaDal.Verify(p => p.Update(agenta), Times.Once);
        }

    }
}
