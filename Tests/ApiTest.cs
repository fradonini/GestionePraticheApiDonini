using Bogus;
using FakeItEasy;
using GestionePraticheApiDonini.Data;
using GestionePraticheApiDonini.DTOs.Input;
using GestionePraticheApiDonini.DTOs.Output;
using GestionePraticheApiDonini.Infrastructure;
using GestionePraticheApiDonini.Services;
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
        var gottenPraticaDTO = new GottenPraticaDTO
        {
            Name = "Francesco",
            Surname = "Donini",
            BirthDate = DateTime.Now.AddHours(-30),
            PraticaId = 100,
            CreatedDate = DateTime.Now.AddYears(-2),
            StartedProcessingDate = DateTime.Now.AddYears(-1),
            CurrentStatus = PraticaStatus.Processing.ToString()
        };
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
        var gottenPraticaDTO = new GottenPraticaDTO
        {
            Name = "Francesco",
            Surname = "Donini",
            BirthDate = DateTime.Now.AddHours(-30),
            PraticaId = 100,
            CreatedDate = DateTime.Now.AddYears(-2),
            StartedProcessingDate = DateTime.Now.AddYears(-1),
            CurrentStatus = PraticaStatus.Processing.ToString()
        };
        A.CallTo(() => _service.GetPratica(praticaDTO))
         .Throws<KeyNotFoundException>();

        // Act
        var invocation = async () => await _service.GetPratica(praticaDTO);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(invocation);
    }
}