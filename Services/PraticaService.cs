using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionePraticheApiDonini.Data;
using GestionePraticheApiDonini.DTOs.Input;
using GestionePraticheApiDonini.DTOs.Output;
using GestionePraticheApiDonini.Infrastructure;
using GestionePraticheApiDonini.Services.Interfaces;

namespace GestionePraticheApiDonini.Services
{
    public class PraticaService : IPraticaService
    {
        private readonly PraticheDB _praticheDB;
        private readonly ILogger<PraticaService> _logger;
        public PraticaService(ILogger<PraticaService> logger, PraticheDB praticheDB)
        {
            _logger = logger;
            _praticheDB = praticheDB;
        }
        public async Task<CreatedPraticaDTO> CreatePratica(CreatePraticaDTO dto)
        {
            int insertedId;

            var byn = FileToByte(dto.Attachment);
            var result = await _praticheDB.Pratiche.AddAsync(new Pratica()
            {
                Id = 1,
                Attachment = byn,
                Status = PraticaStatus.Created,
                CreatedDate = DateTime.Now,
                BirthDate = dto.BirthDate,
                Name = dto.Name,
                Surname = dto.Surname
            });
            insertedId = result.Entity.Id;

            return new CreatedPraticaDTO { Id = insertedId };
        }

        public async Task<DownloadedPraticaDTO> DownloadPratica(DownloadPraticaDTO dto)
        {
            var file = _praticheDB.Pratiche.FirstOrDefault(p => p.Id == dto.PraticaId);
            if (file == null)
                throw new KeyNotFoundException($"file id not found");

            return await Task.FromResult<DownloadedPraticaDTO>(new DownloadedPraticaDTO { DownloadedFile = file.Attachment });
        }

        public Task<GottenPraticaDTO> GetPratica(GetPraticaDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdatedPraticaDTO> UpdatePratica(UpdatePraticaDTO dto)
        {
            var fileToUpdate = _praticheDB.Pratiche.FirstOrDefault(p => p.Id == dto.PraticaId);
            if (fileToUpdate == null)
                throw new KeyNotFoundException($"file id not found");
            if (fileToUpdate.Status != PraticaStatus.Created)
                throw new TypeAccessException($"file may not be modified");

            var result = _praticheDB.Pratiche.Update(new Pratica()
            {
                Attachment = dto.Attachment ?? fileToUpdate.Attachment,
                Name = dto.Name ?? fileToUpdate.Name,
                Surname = dto.Surname ?? fileToUpdate.Surname,
                BirthDate = dto.BirthDate ?? fileToUpdate.BirthDate

            });
            return await Task.FromResult<UpdatedPraticaDTO>(new UpdatedPraticaDTO { Id = result.Entity.Id });
        }

        private async Task<string> EncodeFile(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }

        private static byte[] FileToByte(IFormFile file)
        {
            byte[] fileContents = null;
            using (Stream stream = file.OpenReadStream())
            {
                fileContents = ToByteArray(stream);
            }
            return fileContents;
        }

        private static byte[] ToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}