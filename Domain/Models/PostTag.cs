namespace Domain.Models;

public class PostTag
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post post { get; set; }
    
    public int TagId { get; set; }
    public Tag tag { get; set; }
}