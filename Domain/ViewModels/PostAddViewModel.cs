using Domain.Models;

namespace Domain.ViewModels;

public class PostAddViewModel
{
    public string Title{get;set;}
    public string Description { get; set; }
    
    public List<int> SelectedTagIds { get; set; } = new List<int>();
    
    public List<Tag> AvailableTags { get; set; } = new List<Tag>();
}