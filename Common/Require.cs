using System;


public static class Require
{
    /// <summary>
    /// Checks to see if <paramref name="item"/> is null.
    /// </summary>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="item">The item being checked.</param>
    /// <exception cref="System.ArgumentException">
    /// Thrown if <paramref name="paramName"/> is null or empty.
    /// </exception>
    /// <exception cref="System.ArgumentNullException">
    /// Thrown if <paramref name="item"/> is null.
    /// </exception>
    public static void IsNotNull(string paramName, object item)
    {
        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentException(ErrorMessages.Require_paramName_cannot_be_null_or_empty, nameof(paramName));
        }

        if (item == null)
        {
            throw new ArgumentNullException(paramName, string.Format(ErrorMessages.Require_cannot_be_null, paramName));
        }
    }

    /// <summary>
    /// Checks to see if <paramref name="item"/> is null.
    /// </summary>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <param name="item">The item being checked.</param>
    ///  <exception cref="System.ArgumentNullException">
    /// Thrown if <paramref name="item"/> is null.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    /// Thrown if <paramref name="paramName"/> is null or empty.
    /// Thrown if <paramref name="item"/> is empty.
    /// </exception>
    public static void IsNotNullOrEmpty(string paramName, string item)
    {
        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentException(ErrorMessages.Require_paramName_cannot_be_null_or_empty, nameof(paramName));
        }

        if (item == null)
        {
            throw new ArgumentNullException(paramName, string.Format(ErrorMessages.Require_cannot_be_null, paramName));
        }

        if (item.Equals(string.Empty))
        {
            throw new ArgumentException(string.Format(ErrorMessages.Require_cannot_be_empty, paramName), paramName);
        }
    }
}
