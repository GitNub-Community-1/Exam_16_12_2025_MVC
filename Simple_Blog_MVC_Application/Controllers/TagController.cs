using Domain.Filters;
using Domain.ViewModels;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Simple_Blog_MVC_Application.Controllers;

public class TagController(ITagService service) : Controller
{
    public async Task<IActionResult> Index()
    {
        var tags = await service.GetTagsAsync(new TagFilter());
        return View(tags);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Good()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TagAddViewModel model)
    {
        var tag = await service.AddTagAsync(model);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete([FromRoute] int Id)
    {
        await service.DeleteTagAsync(Id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var tag = await service.GetTagByIdAsync(id);

        if (tag == null)
            return NotFound();

        var model = new TagUpdateViewModel()
        {
            Id = tag.Id,
            Name = tag.Name
        };

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Update(TagUpdateViewModel model)
    {
        await service.UpdateTagAsync(model);
        return RedirectToAction("Index");
    }
}