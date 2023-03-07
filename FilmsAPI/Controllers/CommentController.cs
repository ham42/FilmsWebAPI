using FilmsAPI.Data;
using FilmsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment(Comment comment)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var film = await _context.Films.FindAsync(comment.FilmId);

            if(user != null && film != null)
            {
                comment.UserId = userId;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Comment>> GetComments(int id)
        {
            return await _context.Comments.Where(x => x.FilmId==id).ToListAsync();
        }
    }
}
