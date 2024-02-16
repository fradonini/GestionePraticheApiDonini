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
                Attachment = byn,
                Status = PraticaStatus.Created,
                CreatedDate = DateTime.Now,
                BirthDate = dto.BirthDate,
                Name = dto.Name,
                Surname = dto.Surname
            });

            await _praticheDB.SaveChangesAsync();
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

        public async Task<GottenPraticaDTO> GetPratica(GetPraticaDTO dto)
        {
            var file = _praticheDB.Pratiche.FirstOrDefault(p => p.Id == dto.PraticaId);
            if (file == null)
                throw new KeyNotFoundException($"file id not found");

            return await Task.FromResult<GottenPraticaDTO>(new GottenPraticaDTO
            {
                PraticaId = file.Id,
                BirthDate = file.BirthDate,
                Name = file.Name,
                Surname = file.Surname,
                CurrentStatus = file.Status.ToString().ToUpper(),
                CreatedDate = file.CreatedDate,
                StartedProcessingDate = file.StartedProcessingDate,
                CompletedDate = file.CompletedDate
            });
        }

        public async Task<UpdatedPraticaDTO> UpdatePratica(UpdatePraticaDTO dto)
        {
            var fileToUpdate = _praticheDB.Pratiche.FirstOrDefault(p => p.Id == dto.PraticaId);
            if (fileToUpdate == null)
                throw new KeyNotFoundException($"file id not found");
            if (fileToUpdate.Status != PraticaStatus.Created)
                throw new TypeAccessException($"file may not be modified");

            fileToUpdate.BirthDate = dto.BirthDate ?? fileToUpdate.BirthDate;
            fileToUpdate.Name = dto.Name ?? fileToUpdate.Name;
            fileToUpdate.Surname = dto.Surname ?? fileToUpdate.Surname;
            var result = _praticheDB.Pratiche.Update(fileToUpdate);
            await _praticheDB.SaveChangesAsync();
            return await Task.FromResult<UpdatedPraticaDTO>(new UpdatedPraticaDTO { Id = result.Entity.Id });
        }

        public async Task<UpdatedPraticaDTO> UpdateStatus(UpdateStatusDTO dto)
        {
            var fileToUpdate = _praticheDB.Pratiche.FirstOrDefault(p => p.Id == dto.PraticaId);
            if (fileToUpdate == null)
                throw new KeyNotFoundException($"file id not found");


            fileToUpdate.Status = (PraticaStatus)(int)dto.NewStatus;
            var result = _praticheDB.Pratiche.Update(fileToUpdate);
            await _praticheDB.SaveChangesAsync();
            return await Task.FromResult<UpdatedPraticaDTO>(new UpdatedPraticaDTO { Id = result.Entity.Id });
        }

        #region  private

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

        #endregion
    }
}