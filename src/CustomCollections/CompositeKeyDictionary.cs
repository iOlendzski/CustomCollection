using System;
using System.Collections.Generic;


namespace Collections
{
    /// <summary>
    ///     Dictionary for items with composite key <see cref="ICompositeKey{TId,TName}" />.
    /// </summary>
    /// <typeparam name="TId">The type of Id component of key.</typeparam>
    /// <typeparam name="TName">The type of Name component of key.</typeparam>
    /// <typeparam name="TValue">The type of items.</typeparam>
    public class CompositeKeyDictionary<TId, TName, TValue> : ICompositeKeyDictionary<TId, TName, TValue>
            where TId : IEquatable<TId>
            where TName : IEquatable<TName>
            where TValue : ICompositeKey<TId, TName>
    {
        /// <inheritdoc />
        public int Count { get; }

        #region ICompositeKeyDictionary<TId,TName,TValue> members
        /// <inheritdoc />
        public void Add(TValue item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TValue GetValue(TId id, TName name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TValue> GetValues(TId id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TValue> GetValues(TName name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Remove(TId id, TName name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int RemoveAll(TId id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int RemoveAll(TName name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryGetValue(TId id, TName name, out TValue value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryGetValues(TId id, out IReadOnlyCollection<TValue> values)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryGetValues(TName name, out IReadOnlyCollection<TValue> values)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}