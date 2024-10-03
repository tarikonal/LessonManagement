using Domain.DTOs.Lesson;
using Domain.DTOs.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionDto>> GetSessionsAsync(Guid userId);
        Task<SessionDto> GetSessionByIdAsync(Guid id);
        Task<SessionDto> CreateSessionAsync(CreateSessionDto createSessionDto);
        Task<SessionDto> UpdateSessionAsync(UpdateSessionDto updateSessionDto);
        Task<int> DeleteSessionAsync(Guid id);
    }
}
