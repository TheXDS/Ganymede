#pragma warning disable CS1591

using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Models;

public class Post : Model<Guid>
{
    public User? Creator { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? CreationDate { get; set; }
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public override string ToString() => Title ?? Id.ToString();

}

