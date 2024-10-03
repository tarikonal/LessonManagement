using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.Family;

namespace Infrastructure.Services
{
    public interface IFamilyService
    {
        Task<IEnumerable<FamilyDto>> GetFamiliesAsync(Guid userId);
        Task<FamilyDto> GetFamilyByIdAsync(Guid id);
        Task<FamilyDto> CreateFamilyAsync(CreateFamilyDto createFamilyDto);
        Task<FamilyDto> UpdateFamilyAsync(UpdateFamilyDto updateFamilyDto);
        Task<int> DeleteFamilyAsync(Guid id);
    }
}
