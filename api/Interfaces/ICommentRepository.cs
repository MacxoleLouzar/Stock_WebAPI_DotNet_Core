using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetCommentAllAsync();
        Task<Comments?> GetCommentByIdAsync(int id);
        Task<Comments> CreateCommentAsync(Comments comment);
        Task<Comments> UpdateCommentAsync(int id, Comments comment);
        Task<Comments> DeleteCommentAsync(int id);
    }
}