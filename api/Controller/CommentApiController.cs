using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CommentDto;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentApiController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
      

        // Constructor
        public CommentApiController(ICommentRepository commentRepository, IStockRepository stockRepository,
         UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }
        // GET: api/CommentApi
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetCommentAllAsync();
            var CommentDto = comments.Select(s => s.ToCommentsDto());
            return Ok(CommentDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentsDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment(int stockId, [FromBody] CreateCommentDto createComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState Form!");
            }
            if (!await _stockRepository.StockExist(stockId))
            {
                return BadRequest("Stock does not exist!");
            }

            var username = User.GetUsername();
            await _userManager.FindByNameAsync(username);

            var comment = createComment.ToCreateComments(stockId);
            comment.AppUserId =  _userManager.GetUserId(User);
            await _commentRepository.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetAById), new { id = comment.id }, comment.ToCommentsDto());
        }
        // PUT: api/CommentApi/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState Form!");
            }
            var comment = await _commentRepository.UpdateCommentAsync(id, updateComment.ToUpdateComments());
            if (comment == null)
            {
                return NotFound("Comment not found!");
            }
            return Ok(comment.ToCommentsDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState Form!");
            }
            var comment = await _commentRepository.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found!");
            }
            return Ok(comment.ToCommentsDto());
        }
    }
}