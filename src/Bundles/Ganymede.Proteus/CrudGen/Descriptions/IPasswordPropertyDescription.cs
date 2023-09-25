using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.CrudGen.Descriptions;

/// <summary>
/// Defines an <see cref="IBlobPropertyDescription"/> used to describe
/// properties that securely store a password using a hashing algorithm.
/// </summary>
public interface IPasswordPropertyDescription : IBlobPropertyDescription
{
    /// <summary>
    /// Gets a new instance of the <see cref="IPasswordStorage"/> algorithm
    /// used to hash the password.
    /// </summary>
    IPasswordStorage Algorithm
    {
        get
        {
            var a = GetClassValue<IPasswordStorage>() ?? new Pbkdf2Storage();            
            if (GetValue("AlgorithmSettings") is { } settings && a.GetType().Implements(typeof(IPasswordStorage<>).MakeGenericType(settings.GetType())))
            {
                using var ms = new MemoryStream();
                using (var bw = new BinaryWriter(ms))
                {
                    bw.DynamicWrite(settings);
                    bw.Flush();
                };
                ms.Position = 0;
                using var br = new BinaryReader(ms);
                a.ConfigureFrom(br);
            }
            return a;
        }
    }
}