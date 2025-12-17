using Domain.Filters;
using Domain.Models;
using Domain.ViewModels;

namespace Infastructure.Services.Interfaces;

public interface ITagService
{
    Task<List<Tag>> GetTagsAsync(TagFilter f);
    Task<Tag> AddTagAsync(TagAddViewModel model);
    Task<Tag> UpdateTagAsync(TagUpdateViewModel model);
    Task DeleteTagAsync(int Id);
    public Task<Tag?>? GetTagByIdAsync(int id);
}