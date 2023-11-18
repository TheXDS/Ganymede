#pragma warning disable CS1591

namespace TheXDS.Ganymede.Models;

[Flags]
public enum LikeFlags : byte
{
    None,
    Dogs,
    Cats,
    Parrots = 4,
    Turtle = 8,
    Hamsters = 16,
    Mamals = Dogs | Cats | Hamsters,
    GoldFish = 32,
    Snakes = 64,
    Ants = 128,
    All = 255
}