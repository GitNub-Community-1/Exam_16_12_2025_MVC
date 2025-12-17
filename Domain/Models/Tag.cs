namespace Domain.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    
    
    public ICollection<PostTag> PostTags { get; set; }
}