using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPurposeService
    {
        Task<long> AddPurpose(string userName, PurposeDto purposeDto);
        Task UpdatePurpose(string userName, PurposeDto purposeDto);
        Task<IEnumerable<PurposeDto>> GetPurposesByStatus(string userName, string purposeStatusCode);
    }
}
