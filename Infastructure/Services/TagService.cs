using AutoMapper;
using Domain.Filters;
using Domain.Models;
using Domain.ViewModels;
using Infastructure.Data;
using Infastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Services;

public class TagService(ApplicationDbContext context, IMapper mapper) : ITagService
{
    public async Task<List<Tag>> GetTagsAsync(TagFilter f)
    {
        var query = context.Tags
            .AsQueryable();
        if (f.Id.HasValue) query = query.Where(x => x.Id == f.Id.Value);
        if (!string.IsNullOrEmpty(f.Name)) query = query.Where(x => x.Name.Contains(f.Name));
        return await query.ToListAsync();
    }

    public async Task<Tag> AddTagAsync(TagAddViewModel model)
    {
        var tag = mapper.Map<Tag>(model);
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> UpdateTagAsync(TagUpdateViewModel model)
    {
        var tag = await context.Tags.FindAsync(model.Id);
        if (tag is null)
            throw new KeyNotFoundException("Tag not found");
        tag.Name = model.Name;
        await context.SaveChangesAsync();
        return tag;
    }

    public async Task DeleteTagAsync(int Id)
    {
        var tag = await context.Tags
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (tag == null) return;

        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
    }

    public async Task<Tag?>? GetTagByIdAsync(int id)
    {
        var category = await context.Tags
            .FirstOrDefaultAsync(x => x.Id == id);
        return category;
    }
}