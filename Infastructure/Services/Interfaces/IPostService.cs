using Domain.Filters;
using Domain.Models;
using Domain.ViewModels;

namespace Infastructure.Services.Interfaces;

public interface IPostService
{
    Task<List<Post>> GetPostsAsync(PostFilter f);
    Task<Post> AddPostAsync(PostAddViewModel model);
    Task<Post> UpdatePostAsync(PostUpdateViewModel model);
    Task DeletePostAsync(int Id);
    public Task<Post?>? GetPostByIdAsync(int id);
    Task<List<Tag>> GetAllTagsAsync();
}