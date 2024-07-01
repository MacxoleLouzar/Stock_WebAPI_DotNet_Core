using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CommentDto;
using api.Model;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentsDto ToCommentsDto(this Comments commentModel){
            return new CommentsDto{
                id = commentModel.id,
                title = commentModel.title,
                Content = commentModel.Content,
                CreatedAt = commentModel.CreatedAt,
                CreatedBy = commentModel.AppUser.UserName,
                stockId = commentModel.stockId,
            };
        }

        public static Comments ToCreateComments(this CreateCommentDto createCommentDto, int stockId){
            return new Comments{
                title = createCommentDto.title,
                Content = createCommentDto.Content,
                stockId = stockId,
            };
       
        }
   
        public static Comments ToUpdateComments(this UpdateCommentRequestDto updateCommentDto){
            return new Comments{
                title = updateCommentDto.title,
                Content = updateCommentDto.Content          
            };
        }
    }
}