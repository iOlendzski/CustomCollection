using System;


namespace Collections
{
    /// <summary>
    ///     Defines a generic interface for composite key with Id and Name properties.
    /// </summary>
    /// <typeparam name="TId">The type of <see cref="Id" />.</typeparam>
    /// <typeparam name="TName">The type of <see cref="Name" />.</typeparam>
    public interface ICompositeKey<out TId, out TName>
            where TId : IEquatable<TId>
            where TName : IEquatable<TName>
    {
        TId Id { get; }

        TName Name { get; }
    }
}