using FakeItEasy;
using FluentAssertions;
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.CompanyInventory.Features.Shared;

namespace CompanyInventory.Tests.Features.Query
{
    public class GetCompanyBySymbolQueryHandlerTests
    {
        private readonly IQueryCompanyRepository _IQueryCompanyRepository;
        private readonly GetCompanyHistoryBySymbolQueryHandler _getCompanyBySymbolQueryHandler;
        public GetCompanyBySymbolQueryHandlerTests()
        {
            _IQueryCompanyRepository = A.Fake<IQueryCompanyRepository>();

            _getCompanyBySymbolQueryHandler = new GetCompanyHistoryBySymbolQueryHandler(_IQueryCompanyRepository);
        }

        [Fact]
        public async Task GetCompanyHistoryBySymbolQueryHandler_Handle_ReturnCompanyGetDTO()
        {
            // Arrange
            var query = new GetCompanyHistoryBySymbolQuery { Symbol = "M" };
            var company = Company.Create("MedLife S.A.", "M");

            A.CallTo(() => _IQueryCompanyRepository.GetCompanyBySymbol("M"))
                                                   .Returns(Task.FromResult(company));
            // Act
            var result = await _getCompanyBySymbolQueryHandler.Handle(query);
            var expectedResult = company.ToCompanyGetDTO();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async Task GetCompanyHistoryBySymbolQueryHandler_Handle_ReturnNull_InvalidSymbol()
        {
            // Arrange
            var query = new GetCompanyHistoryBySymbolQuery { Symbol = "INVALID" };

            A.CallTo(() => _IQueryCompanyRepository.GetCompanyBySymbol("INVALID"))
                                                   .Returns(Task.FromResult<Company>(null));
            // Act
            var result = await _getCompanyBySymbolQueryHandler.Handle(query);

            // Assert

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCompanyHistoryBySymbolQueryHandler_Handle_ReturnNull_NoInput()
        {
            // Arrange
            var query = new GetCompanyHistoryBySymbolQuery { Symbol = "" };

            A.CallTo(() => _IQueryCompanyRepository.GetCompanyBySymbol(""))
                                                   .Returns(Task.FromResult<Company>(null));

            // Act 
            var restult = await _getCompanyBySymbolQueryHandler.Handle(query);

            // Assert

            restult.Should().BeNull();
        }
    }
}
