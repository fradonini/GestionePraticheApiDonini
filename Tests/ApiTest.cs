using Bogus;
using FakeItEasy;
using GestionePraticheApiDonini.Data;
using GestionePraticheApiDonini.DTOs.Input;
using GestionePraticheApiDonini.DTOs.Output;
using GestionePraticheApiDonini.Services.Interfaces;

namespace Tests;

// Should be done with DB mocking, no time for that
public class ApiTest
{
    private readonly IPraticaService _service;

    public ApiTest()
    {
        _service = A.Fake<IPraticaService>();
    }

    [Fact]
    public async void GetPraticaSuccessful()
    {
        // Arrange
        var praticaDTO = new GetPraticaDTO { PraticaId = 100 };
        var gottenPraticaDTO = new Faker<GottenPraticaDTO>()
            .RuleFor(x => x.Name, f => f.Person.FirstName)
            .RuleFor(x => x.Surname, f => f.Person.LastName)
            .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(x => x.PraticaId, f => 100)
            .RuleFor(x => x.CreatedDate, f => f.Date.Past(2))
            .RuleFor(x => x.StartedProcessingDate, f => f.Date.Past(1))
            .RuleFor(x => x.CurrentStatus, f => PraticaStatus.Created.ToString());

        A.CallTo(() => _service.GetPratica(praticaDTO))
         .Returns(gottenPraticaDTO);

        // Act
        var result = await _service.GetPratica(praticaDTO);

        // Assert
        Assert.Equal(100, result.PraticaId);
    }

    [Fact]
    public async void GetPraticaUnsuccessful()
    {
        // Arrange
        var praticaDTO = new GetPraticaDTO { PraticaId = 100 };
        var gottenPraticaDTO = new Faker<GottenPraticaDTO>()
            .RuleFor(x => x.Name, f => f.Person.FirstName)
            .RuleFor(x => x.Surname, f => f.Person.LastName)
            .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(x => x.PraticaId, f => 100)
            .RuleFor(x => x.CreatedDate, f => f.Date.Past(2))
            .RuleFor(x => x.StartedProcessingDate, f => f.Date.Past(1))
            .RuleFor(x => x.CurrentStatus, f => PraticaStatus.Created.ToString());

        A.CallTo(() => _service.GetPratica(praticaDTO))
         .Throws<KeyNotFoundException>();

        // Act
        var invocation = async () => await _service.GetPratica(praticaDTO);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(invocation);
    }

    //TODO Test remaing service methods
}