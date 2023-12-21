namespace TheXDS.Ganymede.Models;

/// <summary>
/// Represents the result of an input dialog.
/// </summary>
/// <param name="Success">
/// Set to <see langword="true"/> if the user entered a value successfully,
/// <see langword="false"/> if the user cancels the input dialog.
/// </param>
/// <param name="Result">
/// Value entered by the user. Will be equal to <see langword="default"/> for
/// the <typeparamref name="T"/> type if the users cancels the input dialog.
/// </param>
/// <typeparam name="T">
/// Type of value to request from the user.
/// </typeparam>
public record struct InputResult<T>(bool Success, T Result)
{
    /// <summary>
    /// Implicitly converts a <see cref="InputResult{T}"/> to a
    /// <see cref="bool"/> that represents either success or failure to get
    /// input from the user.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <returns>
    /// <see langword="true"/> if the user entered a value successfully,
    /// <see langword="false"/> if the user cancels the input dialog.
    /// </returns>
    public static implicit operator bool(InputResult<T> value) => value.Success;
    
    /// <summary>
    /// Implicitly converts a <see cref="InputResult{T}"/> to an instance of
    /// <typeparamref name="T"/> that represents the input entered by the user.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <returns>
    /// The value entered by the user. Will be equal to
    /// <see langword="default"/> for the <typeparamref name="T"/> type if the
    /// users cancels the input dialog.
    /// </returns>
    public static implicit operator T(InputResult<T> value) => value.Result;
}