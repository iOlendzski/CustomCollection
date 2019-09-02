using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Collections
{
    /// <summary>
    ///     Dictionary for items with composite key <see cref="ICompositeKey{TId,TName}" />.
    ///     This implementation is not thread-safe.
    /// </summary>
    /// <typeparam name="TId">The type of Id component of key.</typeparam>
    /// <typeparam name="TName">The type of Name component of key.</typeparam>
    /// <typeparam name="TValue">The type of items.</typeparam>
    public class CompositeKeyDictionary<TId, TName, TValue> : ICompositeKeyDictionary<TId, TName, TValue>
            where TId : IEquatable<TId>
            where TName : IEquatable<TName>
            where TValue : ICompositeKey<TId, TName>
    {
        private readonly Dictionary<TId, HashSet<CompositeKey>> _idLookup;
        private readonly Dictionary<CompositeKey, TValue> _internalStore;
        private readonly Dictionary<TName, HashSet<CompositeKey>> _nameLookup;

        /// <summary>
        ///     Initializes new instance of <see cref="CompositeKeyDictionary{TId,TName,TValue}" /> class.
        /// </summary>
        public CompositeKeyDictionary()
        {
            _internalStore = new Dictionary<CompositeKey, TValue>();
            _idLookup      = new Dictionary<TId, HashSet<CompositeKey>>();
            _nameLookup    = new Dictionary<TName, HashSet<CompositeKey>>();
        }

        /// <summary>
        ///     Gets the number of elements contained in this dictionary.
        /// </summary>
        /// <returns>The number of elements contained in this dictionary.</returns>
        public int Count => _internalStore.Count;

        /// <inheritdoc />
        bool ICollection<TValue>.IsReadOnly { get; } = false;

        /// <inheritdoc />
        public IEnumerable<ICompositeKey<TId, TName>> Keys => _internalStore.Keys.Cast<ICompositeKey<TId, TName>>();

        /// <inheritdoc />
        public IEnumerable<TValue> Values => _internalStore.Values.AsEnumerable();

        private IReadOnlyCollection<TValue> GetList(IReadOnlyCollection<CompositeKey> keys)
        {
            var retultList = new List<TValue>(keys.Count);
            foreach (var key in keys)
            {
                retultList.Add(_internalStore[key]);
            }
            return retultList;
        }

        #region ICollection<TValue> members
        /// <inheritdoc />
        bool ICollection<TValue>.Remove(TValue item)
        {
            if (item == null)
            {
                return false;
            }
            return Remove(item.Id, item.Name);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _internalStore.Clear();
            _idLookup.Clear();
            _nameLookup.Clear();
        }

        /// <inheritdoc />
        bool ICollection<TValue>.Contains(TValue item)
        {
            if (item == null)
            {
                return false;
            }
            return TryGetValue(item.Id, item.Name, out _);
        }

        /// <inheritdoc />
        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
        {
            _internalStore.Values.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public void Add(TValue value)
        {
            if (value == null)
            {
                return;
            }

            if (value.Id == null)
            {
                throw new ArgumentNullException(nameof(value.Id));
            }
            if (value.Name == null)
            {
                throw new ArgumentNullException(nameof(value.Name));
            }

            var key = new CompositeKey(value.Id, value.Name);
            if (_internalStore.ContainsKey(key))
            {
                throw new ArgumentException($"Словарь уже содержит элемент с таким значением ключа {key.ToString()}.");
            }

            if (!_idLookup.ContainsKey(key.Id))
            {
                _idLookup.Add(key.Id, new HashSet<CompositeKey>());
            }

            if (!_nameLookup.ContainsKey(key.Name))
            {
                _nameLookup.Add(key.Name, new HashSet<CompositeKey>());
            }

            _idLookup[key.Id].Add(key);
            _nameLookup[key.Name].Add(key);
            _internalStore.Add(key, value);
        }
        #endregion

        #region ICompositeKeyDictionary<TId,TName,TValue> members
        /// <inheritdoc />
        public TValue GetValue(TId id, TName name)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var key = new CompositeKey(id, name);
            if (!_internalStore.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Словарь не содержит указанный ключ {key}.");
            }
            return _internalStore[key];
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TValue> GetValuesById(TId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (!_idLookup.ContainsKey(id))
            {
                throw new KeyNotFoundException($"Словарь не содержит указанный ключ [Id: {id}].");
            }
            return GetList(_idLookup[id]);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TValue> GetValuesByName(TName name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (!_nameLookup.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Словарь не содержит указанный ключ [Name: {name}]");
            }
            return GetList(_nameLookup[name]);
        }

        /// <inheritdoc />
        public bool Remove(TId id, TName name)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var key = new CompositeKey(id, name);
            if (!_internalStore.ContainsKey(key))
            {
                return false;
            }
            _idLookup[id].Remove(key);
            _nameLookup[name].Remove(key);
            _internalStore.Remove(key);
            return true;
        }

        /// <inheritdoc />
        public int RemoveAllById(TId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (!_idLookup.ContainsKey(id))
            {
                return 0;
            }
            var count = _idLookup[id].Count;
            foreach (var key in _idLookup[id])
            {
                _internalStore.Remove(key);
            }
            _idLookup.Remove(id);
            return count;
        }

        /// <inheritdoc />
        public int RemoveAllByName(TName name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (!_nameLookup.ContainsKey(name))
            {
                return 0;
            }
            var count = _nameLookup[name].Count;
            foreach (var key in _nameLookup[name])
            {
                _internalStore.Remove(key);
            }
            _nameLookup.Remove(name);
            return count;
        }

        /// <inheritdoc />
        public bool TryGetValue(TId id, TName name, out TValue value)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var key = new CompositeKey(id, name);
            if (_internalStore.TryGetValue(key, out value))
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public bool TryGetValuesById(TId id, out IReadOnlyCollection<TValue> values)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            values = null;
            if (_idLookup.TryGetValue(id, out var lookup))
            {
                values = GetList(lookup);
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public bool TryGetValuesByName(TName name, out IReadOnlyCollection<TValue> values)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            values = null;
            if (_nameLookup.TryGetValue(name, out var lookup))
            {
                values = GetList(lookup);
                return true;
            }
            return false;
        }
        #endregion

        #region IEnumerable members
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalStore.Values.GetEnumerator();
        }
        #endregion

        #region IEnumerable<TValue> members
        /// <inheritdoc />
        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return _internalStore.Values.GetEnumerator();
        }
        #endregion

        #region Inner classes
        private struct CompositeKey
        {
            public readonly TId Id;
            public readonly TName Name;

            public CompositeKey(TId id, TName name)
            {
                Id   = id;
                Name = name;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"[Id: {Id}; Name: {Name}]";
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(obj, null))
                {
                    return false;
                }
                if (!(obj is CompositeKey other))
                {
                    return false;
                }

                return Id?.Equals(other.Id) == true
                       && Name?.Equals(other.Name) == true;
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                const int MULTI = 23;
                var       hash  = 17;

                hash = hash * MULTI + Id?.GetHashCode() ?? 0;
                hash = hash * MULTI + Name?.GetHashCode() ?? 0;

                return hash;
            }
        }
        #endregion
    }
}