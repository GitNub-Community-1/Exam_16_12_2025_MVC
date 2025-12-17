using Domain.Filters;
using Domain.Models;
using Domain.ViewModels;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Simple_Blog_MVC_Application.Controllers;

public class PostController(IPostService _service) : Controller
{
    public async Task<IActionResult> Index()
    {
        var posts = await _service.GetPostsAsync(new PostFilter());
        return View(posts);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new PostAddViewModel
        {
            AvailableTags = await _service.GetAllTagsAsync() ?? new List<Tag>()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostAddViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.AvailableTags = new List<Tag>();
            return View(model);
        }

        await _service.AddPostAsync(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var post = await _service.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();
        var allTags = await _service.GetAllTagsAsync(); 
        var model = new PostUpdateViewModel
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            
            AvailableTags = allTags,
            
            SelectedTagIds = post.PostTags?.Select(pt => pt.TagId).ToList() ?? new List<int>()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PostUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.UpdatePostAsync(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeletePostAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Good()
    {
        return View();
    }
}
