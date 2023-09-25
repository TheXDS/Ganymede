using TheXDS.MCART.Security;

namespace TheXDS.Ganymede.CrudGen.Descriptors;

/// <summary>
/// Defines a set of members to be implemented by a property descriptor that
/// includes methods to describe a password storage field.
/// </summary>
public interface IPasswordPropertyDescriptor : IBlobPropertyDescriptor
{
    /// <summary>
    /// Shortcut to set up the hashing algorithm to PBKDF2 for securely storing
    /// the password.
    /// </summary>
    /// <returns>
    /// This same descriptor instance, allowing the use of Fluent syntax.
    /// </returns>
    IPasswordPropertyDescriptor Pbkdf2() => Algorithm<Pbkdf2Storage>();

    /// <summary>
    /// Defines the encryption algorithm used to securely store the password in
    /// a binary blob.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the algorithm to use for computing the hash to store the
    /// password in. It must implement <see cref="IPasswordStorage"/>.
    /// </typeparam>
    /// <returns>
    /// This same descriptor instance, allowing the use of Fluent syntax.
    /// </returns>
    IPasswordPropertyDescriptor Algorithm<T>()
        where T : IPasswordStorage, new()
    {
        SetValue(typeof(T));
        return this;
    }

    /// <summary>
    /// Defines the encryption algorithm used to securely store the password in
    /// a binary blob.
    /// </summary>
    /// <typeparam name="TAlg">
    /// Type of the algorithm to use for computing the hash to store the
    /// password in. It must implement <see cref="IPasswordStorage{T}"/>, where
    /// the generic argument represents the algorithm settings type.
    /// </typeparam>
    /// <typeparam name="TSettings">
    /// Type of the settings struct used to configure the algorithm.
    /// </typeparam>
    /// <param name="settings">
    /// Settings used to configure the algorithm.
    /// </param>
    /// <returns>
    /// This same descriptor instance, allowing the use of Fluent syntax.
    /// </returns>
    IPasswordPropertyDescriptor Algorithm<TAlg, TSettings>(TSettings settings)
        where TSettings : struct
        where TAlg : IPasswordStorage<TSettings>, new()
    {
        SetValue(settings, "AlgorithmSettings");
        return Algorithm<TAlg>();
    }
}