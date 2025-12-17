using AutoMapper;
using Domain.Filters;
using Domain.Models;
using Domain.ViewModels;
using Infastructure.Data;
using Infastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Services;

public class PostService(ApplicationDbContext context, IMapper mapper) : IPostService
{
   public async Task<List<Post>> GetPostsAsync(PostFilter f)
   {
       var query = context.Posts
           .Include(p => p.PostTags)
           .ThenInclude(pt => pt.tag)
           .AsQueryable();
   
       if (f.Id.HasValue)
           query = query.Where(x => x.Id == f.Id.Value);
   
       if (!string.IsNullOrEmpty(f.Title))
           query = query.Where(x => x.Title.Contains(f.Title));
   
       if (!string.IsNullOrEmpty(f.Description))
           query = query.Where(x => x.Description.Contains(f.Description));
   
       return await query.ToListAsync();
   }
   
   public async Task<Post> AddPostAsync(PostAddViewModel model)
   {
       var post = new Post
       {
           Title = model.Title,
           Description = model.Description,
           PostTags = model.SelectedTagIds
               .Select(id => new PostTag
               {
                   TagId = id
               })
               .ToList()
       };
   
       context.Posts.Add(post);
       await context.SaveChangesAsync();
       return post;
   }
   
   public async Task<Post> UpdatePostAsync(PostUpdateViewModel model)
   {
       var post = await context.Posts
           .Include(p => p.PostTags)
           .FirstOrDefaultAsync(p => p.Id == model.Id);
   
       if (post == null)
           throw new KeyNotFoundException("Post not found");
   
       post.Title = model.Title;
       post.Description = model.Description;
       
       post.PostTags.Clear();
       
       post.PostTags = model.SelectedTagIds
           .Select(id => new PostTag
           {
               PostId = post.Id,
               TagId = id
           })
           .ToList();
       await context.SaveChangesAsync();
       return post;
   }
   
   public async Task<Post?>? GetPostByIdAsync(int id)
   {
       var post = await context.Posts
           .Include(p => p.PostTags)
           .ThenInclude(pt => pt.tag)
           .FirstOrDefaultAsync(x => x.Id == id);
   
       return post;
   }

   public async Task<List<Tag>> GetAllTagsAsync()
   {
       return await context.Tags.ToListAsync();
   }

   public async Task DeletePostAsync(int Id)
    {
        var post = await context.Posts
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (post == null) return;

        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }

 
}