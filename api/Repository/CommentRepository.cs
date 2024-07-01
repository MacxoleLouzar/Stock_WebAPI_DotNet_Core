using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        async Task<List<Comments>> ICommentRepository.GetCommentAllAsync()
        {
            return await _context.Comments.Include(z => z.AppUser).ToListAsync();
        }
        async Task<Comments?> ICommentRepository.GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(z => z.AppUser).FirstOrDefaultAsync(c => c.id == id);
            if (comment == null) return null;
            return comment;
        }

        async Task<Comments> ICommentRepository.CreateCommentAsync(Comments comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        async Task<Comments> ICommentRepository.UpdateCommentAsync(int id, Comments comment)
        {
            var CommentModel = await _context.Comments.FindAsync(id);
            if (CommentModel == null){
                return null;
            }

            CommentModel.title = comment.title;
            CommentModel.Content = comment.Content;

            await _context.SaveChangesAsync();
            return CommentModel;
        }

        async Task<Comments> ICommentRepository.DeleteCommentAsync(int id)
        {
            var commnet = await _context.Comments.FirstOrDefaultAsync(x =>x.id == id);
            if (commnet == null) return null;
            _context.Comments.Remove(commnet);
            await _context.SaveChangesAsync();
            return commnet;
        }
    }
}