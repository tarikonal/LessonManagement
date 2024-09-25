using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs.Family;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FamilyService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FamilyDto>> GetFamiliesAsync()
        {
            var families = await _context.Families.ToListAsync();
            return _mapper.Map<IEnumerable<FamilyDto>>(families);
        }

        public async Task<FamilyDto> GetFamilyByIdAsync(Guid id)
        {
            var family = await _context.Families.FindAsync(id);
            return _mapper.Map<FamilyDto>(family);
        }

        public async Task<FamilyDto> CreateFamilyAsync(CreateFamilyDto createFamilyDto)
        {
            var family = _mapper.Map<Family>(createFamilyDto);
            family.EklemeTarihi = DateTime.Now;
            family.EkleyenKullaniciId = 1;
            _context.Families.Add(family);
            await _context.SaveChangesAsync();
            return _mapper.Map<FamilyDto>(family);
        }

        public async Task<FamilyDto> UpdateFamilyAsync(UpdateFamilyDto updateFamilyDto)
        {
            var family = await _context.Families.FindAsync(updateFamilyDto.Id);
            if (family == null)
            {
                return null;
            }

            _mapper.Map(updateFamilyDto, family);
            _context.Families.Update(family);
            await _context.SaveChangesAsync();
            return _mapper.Map<FamilyDto>(family);
        }

        public async Task<int> DeleteFamilyAsync(Guid id)
        {
            var family = await _context.Families.FindAsync(id);
            if (family == null)
            {
                return -1;
            }

            _context.Families.Remove(family);
            return await _context.SaveChangesAsync();
        }
    }
}
