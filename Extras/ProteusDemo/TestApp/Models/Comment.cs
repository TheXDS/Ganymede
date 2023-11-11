#pragma warning disable CS1591

using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Models;

public class Comment : Model<Guid>
{
    public User? Creator { get; set; }
    public Post? Post { get; set; }
    public string? Content { get; set; }
    public DateTime? CreationDate { get; set; }
    public override string ToString() => Content ?? Id.ToString();

}

