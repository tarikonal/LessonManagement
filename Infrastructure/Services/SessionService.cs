using AutoMapper;
using Domain.DTOs.Lesson;
using Domain.DTOs.Session;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SessionDto>> GetSessionsAsync()
        {
            var sessions = await _context.Sessions.ToListAsync();
            return _mapper.Map<IEnumerable<SessionDto>>(sessions);
        }

        public async Task<SessionDto> GetSessionByIdAsync(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            return _mapper.Map<SessionDto>(session);
        }

        public async Task<SessionDto> CreateSessionAsync(CreateSessionDto createSessionDto)
        {
            var session = _mapper.Map<Session>(createSessionDto);
            session.EklemeTarihi = DateTime.Now;
            session.EkleyenKullaniciId = createSessionDto.EkleyenKullaniciId;
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return _mapper.Map<SessionDto>(session);
        }

        public async Task<SessionDto> UpdateSessionAsync(UpdateSessionDto updateSessionDto)
        {
            var session = await _context.Sessions.FindAsync(updateSessionDto.Id);
            if (session == null)
            {
                return null;
            }

            _mapper.Map(updateSessionDto, session);
            session.GuncellemeTarihi = DateTime.Now;
            session.GuncelleyenKullaniciId = updateSessionDto.GuncelleyenKullaniciId;
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
            return _mapper.Map<SessionDto>(session);
        }

        public async Task<int> DeleteSessionAsync(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return -1;
            }

            _context.Sessions.Remove(session);
            return await _context.SaveChangesAsync();
        }
    }
}
