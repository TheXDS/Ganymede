using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Models;

public class User : Model<string>
{
    public string? DisplayName { get; set; }

    public byte[] Password { get; set; } = Array.Empty<byte>();

    public bool Enabled { get; set; } = true;

    public string? Description { get; set; } = null;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

public class Post : Model<Guid>
{
    public User? Creator { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? CreationDate { get; set; }
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public class Comment : Model<Guid>
{
    public User? Creator { get; set; }
    public Post? Post { get; set; }
    public string? Content { get; set; }
    public DateTime? CreationDate { get; set; }
}