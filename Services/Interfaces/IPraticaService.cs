using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionePraticheApiDonini.DTOs.Output;
using GestionePraticheApiDonini.DTOs.Input;

namespace GestionePraticheApiDonini.Services.Interfaces
{
    public interface IPraticaService
    {
        Task<CreatedPraticaDTO> CreatePratica(CreatePraticaDTO dto);
        Task<UpdatedPraticaDTO> UpdatePratica(UpdatePraticaDTO dto);
        Task<GottenPraticaDTO> GetPratica(GetPraticaDTO dto);
        Task<DownloadedPraticaDTO> DownloadPratica(DownloadPraticaDTO dto);
        Task<UpdatedPraticaDTO> UpdateStatus(UpdateStatusDTO dto);
    }
}