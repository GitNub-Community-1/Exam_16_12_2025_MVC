namespace Domain.Models;

public class Post : BaseEntity
{
    public string Title{get;set;}
    public string Description { get; set; }
    
    public DateTime DateOfPublished { get; set; } = DateTime.UtcNow;
    
    public ICollection<PostTag> PostTags { get; set; }
}