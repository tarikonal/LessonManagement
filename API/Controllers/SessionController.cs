﻿using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.DTOs.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            var sessions = await _sessionService.GetSessionsAsync();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSessionById(Guid id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost]
        public async Task<ActionResult<SessionDto>> CreateSession(CreateSessionDto createSessionDto)
        {
            var session = await _sessionService.CreateSessionAsync(createSessionDto);
            return CreatedAtAction(nameof(GetSessionById), new { id = session.Id }, session);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SessionDto>> UpdateSession(Guid id, UpdateSessionDto updateSessionDto)
        {
            if (id != updateSessionDto.Id)
            {
                return BadRequest();
            }
            
            var updatedSession = await _sessionService.UpdateSessionAsync(updateSessionDto);
            if (updatedSession == null)
            {
                return NotFound();
            }

            return Ok(updatedSession);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(Guid id)
        {
            var result = await _sessionService.DeleteSessionAsync(id);
            if (result == -1)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
