using System;
using System.Collections;
using Common;

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

    /// <summary>
    /// Throws a <see cref="T:System.ArgumentNullException"/> if the specified collection is
    /// a null reference or empty.
    /// </summary>
    /// <param name="paramName">The name of the method parameter.</param>
    /// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1"/></param>
    public static void CollectionNotNullOrEmpty(string paramName, IList collection)
    {
        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentException(ErrorMessages.Require_paramName_cannot_be_null_or_empty, nameof(paramName));
        }

        if (collection == null)
        {
            throw new ArgumentNullException(ErrorMessages.CollectionCannotBeEmpty, paramName);
        }

        if (collection.Count < 1)
        {
            throw new ArgumentException(ErrorMessages.CollectionCannotBeEmpty, paramName);
        }
    }

    /// <summary>
    /// Throws a <see cref="T:System.ArgumentNullException"/> if the specified collection is
    /// a null reference or empty.
    /// </summary>
    /// <param name="collection">The <see cref="T:System.Collections.Generic.IDictionary`1"/></param>
    /// <param name="parameter">The name of the method parameter.</param>
    public static void CollectionNotNullOrEmpty(string paramName, IDictionary collection)
    {
        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentException(ErrorMessages.Require_paramName_cannot_be_null_or_empty, nameof(paramName));
        }

        if (collection == null)
        {
            throw new ArgumentNullException(paramName, ErrorMessages.CollectionCannotBeEmpty);
        }

        if (collection.Count < 1)
        {
            throw new ArgumentException(ErrorMessages.CollectionCannotBeEmpty, paramName);
        }
    }

    /// <summary>
    /// Throws a <see cref="T:System.ArgumentNullException"/> if the specified collection is
    /// a null reference or empty.
    /// </summary>
    /// <param name="collection">The <see cref="T:System.Collections.Generic.IDictionary`1"/></param>
    /// <param name="parameter">The name of the method parameter.</param>
    public static void IsCorrectType<T>(string paramName, object obj) where T: class
    {
        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentException(ErrorMessages.Require_paramName_cannot_be_null_or_empty, nameof(paramName));
        }

        var result = obj as T;

        if (result == null)
        {
            throw new ArgumentException(paramName, ErrorMessages.CollectionCannotBeEmpty);
        }
    }
}
