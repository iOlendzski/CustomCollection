using System;
using System.Collections.Generic;


namespace Collections
{
    /// <summary>
    ///     Defines an interface for dictionary with composite key (<see cref="ICompositeKey{TId,TName}" />.
    /// </summary>
    /// <typeparam name="TId">The type of Id component of key.</typeparam>
    /// <typeparam name="TName">The type of Name component of key.</typeparam>
    /// <typeparam name="TValue">The type of items.</typeparam>
    public interface ICompositeKeyDictionary<in TId, in TName, TValue>
            where TValue : ICompositeKey<TId, TName>
            where TId : IEquatable<TId>
            where TName : IEquatable<TName>
    {
        /// <summary>
        ///     Adds specified item.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <exception cref="ArgumentNullException">Parameter <paramref name="item" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">This dictionary already contains this key.</exception>
        void Add(TValue item);

        /// <summary>
        ///     Gets the element with the specified key.
        /// </summary>
        /// <param name="id">The <see cref="ICompositeKey{TId,TName}.Id" /> to locate.</param>
        /// <param name="name">The <see cref="ICompositeKey{TId,TName}.Name" /> to locate.</param>
        /// <returns>The element that has the specified key.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> or the <paramref name="name" /> is <see langword="null" />.</exception>
        /// <exception cref="KeyNotFoundException">Specified key not found.</exception>
        TValue GetValue(TId id, TName name);

        /// <summary>
        ///     Gets all elements with the specified key.
        /// </summary>
        /// <param name="id">The <see cref="ICompositeKey{TId,TName}.Id" /> to locate.</param>
        /// <returns>The element that has the specified key.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> is <see langword="null" />.</exception>
        /// <exception cref="KeyNotFoundException">Specified key not found.</exception>
        IReadOnlyCollection<TValue> GetValues(TId id);

        /// <summary>
        ///     Gets all elements with the specified key.
        /// </summary>
        /// <param name="name">The <see cref="ICompositeKey{TId,TName}.Name" /> to locate.</param>
        /// <returns>The element that has the specified key.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="name" /> is <see langword="null" />.</exception>
        /// <exception cref="KeyNotFoundException">Specified key not found.</exception>
        IReadOnlyCollection<TValue> GetValues(TName name);

        /// <summary>
        ///     Removes the element with the specified key.
        /// </summary>
        /// <param name="id">The <see cref="ICompositeKey{TId,TName}.Id" /> to remove.</param>
        /// <param name="name">The <see cref="ICompositeKey{TId,TName}.Name" /> to remove.</param>
        /// <returns><see langword="true" /> if specified key found and removed; otherwise, <see langword="false" />.</returns>
        bool Remove(TId id, TName name);

        /// <summary>
        ///     Removes all elements with the specified key.
        /// </summary>
        /// <param name="id">The <see cref="ICompositeKey{TId,TName}.Id" /> to remove.</param>
        /// <returns>Number of removed elements.</returns>
        int RemoveAll(TId id);

        /// <summary>
        ///     Removes all elements with the specified key.
        /// </summary>
        /// <param name="name">The <see cref="ICompositeKey{TId,TName}.Name" /> to remove.</param>
        /// <returns>Number of removed elements.</returns>
        int RemoveAll(TName name);

        /// <summary>
        ///     Gets the element with the specified key.
        /// </summary>
        /// <param name="id">The <see cref="ICompositeKey{TId,TName}.Id" /> to locate.</param>
        /// <param name="name">The <see cref="ICompositeKey{TId,TName}.Name" /> to locate.</param>
        /// <param name="value">When this method returns, the value associated with specified key, if the key is found; otherwise the default value for <typeparamref name="TValue" />.</param>
        /// <returns><see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> or the <paramref name="name" /> is <see langword="null" />.</exception>
        bool TryGetValue(TId id, TName name, out TValue value);

        /// <summary>
        ///     Gets all elements with the specified key.
        /// </summary>
        /// <param name="id">The <see cref="ICompositeKey{TId,TName}.Id" /> to locate.</param>
        /// <param name="values">When this method returns, the values associated with specified key, if the key is found; otherwise, <see langword="null" />.</param>
        /// <returns><see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> is <see langword="null" />.</exception>
        bool TryGetValues(TId id, out IReadOnlyCollection<TValue> values);

        /// <summary>
        ///     Gets all elements with the specified key.
        /// </summary>
        /// <param name="name">The <see cref="ICompositeKey{TId,TName}.Name" /> to locate.</param>
        /// <param name="values">When this method returns, the values associated with specified key, if the key is found; otherwise, <see langword="null" />.</param>
        /// <returns><see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="name" /> is <see langword="null" />.</exception>
        bool TryGetValues(TName name, out IReadOnlyCollection<TValue> values);
    }
}