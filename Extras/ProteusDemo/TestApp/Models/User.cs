#pragma warning disable CS1591

using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.Models;

public class User : Model<string>
{
    public string? DisplayName { get; set; }
    public byte[] Password { get; set; } = Array.Empty<byte>();
    public bool Enabled { get; set; } = true;
    public string? Description { get; set; } = null;
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    public DayOfWeek FavoriteDay { get; set; }
    public LikeFlags LikeFlags { get; set; }
    public override string ToString() => DisplayName ?? Id;
}

[Flags]
public enum LikeFlags : byte
{
    None,
    Dogs,
    Cats,
    Parrots = 4,
    Turtle = 8,
    Mamals = Dogs | Cats,
    All = 255
}