/* *************************************************************************************
BSD-3-CLAUSE (a/k/a MODIFIED BSD) LICENSE

Copyright (c) 2025 John Merryweather Cooper

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
“AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*************************************************************************************** */
/* ****************************************************************************
BSD-3-CLAUSE (a/k/a MODIFIED BSD) LICENSE

Copyright (c) 2025 John Merryweather Cooper

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
“AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**************************************************************************** */

// Ignore Spelling: Cmdlet

using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements an <see cref="IDictionary{TKey, TValue}" /> class for manipulating PowerShell bound parameters.
    /// </summary>
    public class BoundParameter : IDictionary<string, object>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundParameter" /> class.
        /// </summary>
        /// <param name="parameters">
        /// Specifies a dictionary of PowerShell bound parameters to initialize with.
        /// </param>
        /// <remarks>
        /// A case insensitive <see cref="IEqualityComparer{T}" /> is specified to match PowerShell's expectations.
        /// </remarks>
        public BoundParameter(IDictionary<string, object> parameters)
            : this(parameters, new ParameterComparer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundParameter" /> class.
        /// </summary>
        /// <param name="parameters">
        /// Specifies a dictionary of PowerShell bound parameters to initialize with.
        /// </param>
        /// <param name="comparer">
        /// Specifies the <see cref="IEqualityComparer{T}" /> implementation to use when comparing keys, or null to use the default
        /// <see cref="EqualityComparer{T}" /> for the key type.
        /// </param>
        public BoundParameter(IDictionary<string, object> parameters, IEqualityComparer<string> comparer)
        {
            dictionary = new(parameters, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundParameter" /> class.
        /// </summary>
        /// <param name="parameters">
        /// Specifies the list of PowerShell bound parameters to initialize with.
        /// </param>
        /// <remarks>
        /// A case insensitive <see cref="IEqualityComparer{T}" /> is specified to match PowerShell's expectations.
        /// </remarks>
        public BoundParameter(IEnumerable<KeyValuePair<string, object>> parameters)
            : this(parameters, new ParameterComparer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundParameter" /> class.
        /// </summary>
        /// <param name="parameters">
        /// Specifies the list of PowerShell bound parameters to initialize with.
        /// </param>
        /// <param name="comparer">
        /// Specifies the <see cref="IEqualityComparer{T}" /> implementation to use when comparing keys, or null to use the default
        /// <see cref="EqualityComparer{T}" /> for the key type.
        /// </param>
        public BoundParameter(IEnumerable<KeyValuePair<string, object>> parameters, IEqualityComparer<string> comparer)
        {
            dictionary = new(parameters, comparer);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating the total number of elements the internal data structure can hold without resizing.
        /// </summary>
        public int Capacity => dictionary.Capacity;

        /// <summary>
        /// Gets a value indicating the <see cref="IEqualityComparer{T}" /> that is used to determine equality of keys for the dictionary.
        /// </summary>
        public IEqualityComparer<string> Comparer => dictionary.Comparer;

        /// <inheritdoc />
        public int Count => dictionary.Count;

        /// <inheritdoc />
        public bool IsReadOnly => Keys.IsReadOnly && Values.IsReadOnly;

        /// <inheritdoc />
        public ICollection<string> Keys => dictionary.Keys;

        /// <inheritdoc />
        public ICollection<object> Values => dictionary.Values;

        #endregion Public Properties

        #region Public Indexers

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="key" /> is not a valid PowerShell parameter name.
        /// </exception>
        public object this[string key]
        {
            get
            {
                if (!ParameterComparer.ValidateParameterName(key))
                {
                    throw new ArgumentException("Key is not a valid PowerShell parameter name", nameof(key));
                }
                else
                {
                    return dictionary.TryGetValue(key, out object? value) ? value : new object();
                }
            }

            set
            {
                Update(key, value);
            }
        }

        #endregion Public Indexers

        #region Public Methods

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="key" /> already exists in <see cref="BoundParameter" />, or is not a valid PowerShell
        /// parameter name.
        /// </exception>
        public void Add(string key, object value)
        {
            if (!ParameterComparer.ValidateParameterName(key))
            {
                throw new ArgumentException("Key is not a valid PowerShell parameter name", nameof(key));
            }
            else if (this.ContainsKey(key))
            {
                throw new ArgumentException("Key already exists in BoundParameter", nameof(key));
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        public void Clear()
        {
            dictionary.Clear();
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        /// Throws if the item's key is null or empty.
        /// </exception>
        public bool Contains(KeyValuePair<string, object> item)
        {
            if (string.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                return dictionary.Contains(item);
            }
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            else
            {
                return dictionary.ContainsKey(key);
            }
        }

        /// <summary>
        /// Determines whether <see cref="BoundParameter" /> contains a specific value.
        /// </summary>
        /// <param name="value">
        /// Specifies the value to locate in <see cref="BoundParameter" />.
        /// </param>
        /// <returns>
        /// True if <see cref="BoundParameter" /> contains an element with the specified value; otherwise, false.
        /// </returns>
        public bool ContainsValue(object value)
        {
            return dictionary.ContainsValue(value);
        }

        /// <summary>
        /// Copies the elements of the <see cref="Values" /> to an array at the specified <paramref name="array" /><paramref name="index" />.
        /// </summary>
        /// <param name="array">
        /// Specifies the one-dimensional array that is the destination of the elements copied from <see cref="Values" />. The array
        /// must have zero-based indexing.
        /// </param>
        /// <param name="index">
        /// Specifies the zero-based index in <paramref name="array" /> at which copying begins.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="array" /> is multi-dimensional.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="array" /> is null or empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throw if either index is negative or <paramref name="array" /> lacks the capacity to hold <see cref="Values" />.
        /// </exception>
        public void CopyTo(Array array, int index)
        {
            if (array == null || array.Length < 1)
            {
                throw new ArgumentNullException(nameof(array));
            }
            else if (index < 0 || (array.Length - index < Values.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is less than zero, or there is not enough capacity from Array Length minus Array Index for Values.");
            }
            else if (array.Rank > 1)
            {
                throw new ArgumentException("Array cannot be multi-dimensional.", nameof(array));
            }
            else
            {
                this.Values.CopyTo(array.Cast<object>().ToArray(), index);
            }
        }

        /// <summary>
        /// Copies the key/value pairs of <see cref="BoundParameter" /> to an array at the specified <paramref name="array" /><paramref name="index" />.
        /// </summary>
        /// <param name="array">
        /// Specifies the one-dimensional array that is the destination of the elements copied from <see cref="Values" />. The array
        /// must have zero-based indexing.
        /// </param>
        /// <param name="index">
        /// Specifies the zero-based index in <paramref name="array" /> at which copying begins.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="array" /> is null or empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throw if either index is negative or <paramref name="array" /> lacks the capacity to hold <see cref="BoundParameter" />.
        /// </exception>
        public void CopyTo(KeyValuePair<string, object>[] array, int index)
        {
            // shallow copy of Value. Cannot do more without value being cloneable
            var keyValuePairs = dictionary.ToDictionary(entry => entry.Key, entry => entry.Value).ToList();

            if (array == null || array.Length < 1)
            {
                throw new ArgumentNullException(nameof(array));
            }
            else if (array.Length - index < keyValuePairs.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Not enough capacity from Array Length minus Array Index for Key/Value Pairs.");
            }
            else
            {
                for (int i = index, j = 0; i < array.Length && j < keyValuePairs.Count; i++, j++)
                {
                    array[i] = keyValuePairs[j];
                }
            }
        }

        /// <summary>
        /// Copies the elements of the <see cref="Keys" /> to an array at the specified <paramref name="array" /><paramref name="index" />.
        /// </summary>
        /// <param name="array">
        /// Specifies the one-dimensional array that is the destination of the elements copied from <see cref="Keys" />. The array
        /// must have zero-based indexing.
        /// </param>
        /// <param name="index">
        /// Specifies the zero-based index in <paramref name="array" /> at which copying begins.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="array" /> is multi-dimensional.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="array" /> is null or empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throw if either index is negative or <paramref name="array" /> lacks the capacity to hold <see cref="Keys" />.
        /// </exception>
        public void CopyTo(string[] array, int index)
        {
            if (array == null || array.Length < 1)
            {
                throw new ArgumentNullException(nameof(array));
            }
            else if (index < 0 || (array.Length - index < Values.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is less than zero, or there is not enough capacity from Array Length minus Array Index for Values.");
            }
            else
            {
                this.Values.CopyTo(array, index);
            }
        }

        /// <summary>
        /// Ensures that <see cref="BoundParameter" /> can hold up to a specified number of entries without any further expansion of
        /// its backing storage.
        /// </summary>
        /// <param name="capacity">
        /// Specifies the number of entries.
        /// </param>
        /// <returns>
        /// The current capacity of <see cref="BoundParameter" />.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if <paramref name="capacity" /> is negative.
        /// </exception>
        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than or equal to zero.");
            }
            else
            {
                return dictionary.EnsureCapacity(capacity);
            }
        }

        /// <summary>
        /// Gets an instance of a type that can be used to perform operations on <see cref="BoundParameter" /> using
        /// <typeparamref name="TAlternateKey" /> as a key instead of string.
        /// </summary>
        /// <typeparam name="TAlternateKey">
        /// Specifies the alternate key type for performing lookups. This type must not allow null, and must allow structures to be
        /// addressed as references.
        /// </typeparam>
        /// <returns>
        /// The created lookup instance of type <see cref="Dictionary{TKey, TValue}.AlternateLookup{T}" />; otherwise, default which
        /// should not be used.
        /// </returns>
        public Dictionary<string, object>.AlternateLookup<TAlternateKey> GetAlternateLookup<TAlternateKey>() where TAlternateKey : notnull, allows ref struct
        {
            return dictionary.TryGetAlternateLookup(out Dictionary<string, object>.AlternateLookup<TAlternateKey> lookup) ? lookup : default;
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        /// <summary>
        /// Tests whether <see cref="BoundParameter" /> has a valid PowerShell parameter <paramref name="name" />.
        /// </summary>
        /// <param name="name">
        /// Specifies the parameter name to locate.
        /// </param>
        /// <returns>
        /// True if parameter <paramref name="name" /> is present; otherwise, false.
        /// </returns>
        public bool HasParameter(string name)
        {
            return ParameterComparer.ValidateParameterName(name) && this.ContainsKey(name);
        }

        /// <summary>
        /// Tests whether <see cref="BoundParameter" /> has a valid PowerShell parameter <paramref name="name" /> with <paramref name="value" />..
        /// </summary>
        /// <param name="name">
        /// Specifies the parameter name to locate.
        /// </param>
        /// <param name="value">
        /// Specifies the parameter value to locate.
        /// </param>
        /// <returns>
        /// True if parameter <paramref name="name" /> with <paramref name="value" /> is present; otherwise, false.
        /// </returns>
        public bool HasParameter(string name, object value)
        {
            return ParameterComparer.ValidateParameterName(name) && this.Contains(new KeyValuePair<string, object>(name, value));
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            return dictionary.Remove(key);
        }

        /// <summary>
        /// Removes the value with the specified <paramref name="key" />, and copies the element value to the
        /// <paramref name="value" /> parameter.
        /// </summary>
        /// <param name="key">
        /// Specifies the key of the element to remove.
        /// </param>
        /// <param name="value">
        /// Specifies the remove element value.
        /// </param>
        /// <returns>
        /// True if the element is successfully found and remove; otherwise, false.
        /// </returns>
        public bool Remove(string key, out object? value)
        {
            return dictionary.Remove(key, out value);
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<string, object> item)
        {
            if (this.TryGetValue(item.Key, out object? value) && value == item.Value)
            {
                return this.Remove(item.Key);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the capacity of <see cref="BoundParameter" /> to what it would be if it had been originally initialized with all of
        /// its entries.
        /// </summary>
        public void TrimExcess()
        {
            dictionary.TrimExcess();
        }

        /// <summary>
        /// Sets the capacity of <see cref="BoundParameter" /> to hold a specific number of entries without further expansion of its
        /// backing storage.
        /// </summary>
        /// <param name="capacity">
        /// Specifies the new capacity.
        /// </param>
        public void TrimExcess(int capacity)
        {
            dictionary.TrimExcess(capacity);
        }

        /// <summary>
        /// Attempts to add the specified <paramref name="key" /> and <paramref name="value" /> to <see cref="BoundParameter" />.
        /// </summary>
        /// <param name="key">
        /// Specifies the key of the element to add.
        /// </param>
        /// <param name="value">
        /// Specifies the value of the element to add.
        /// </param>
        /// <returns>
        /// True if the key/value pair was successfully added to <see cref="BoundParameter" />; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="key" /> is not a valid PowerShell parameter name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Throws if <paramref name="key" /> is null or empty.
        /// </exception>
        public bool TryAdd(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            else if (ParameterComparer.ValidateParameterName(key))
            {
                throw new ArgumentException("Key is not a valid PowerShell parameter name", nameof(key));
            }
            else
            {
                return dictionary.TryAdd(key, value);
            }
        }

        /// <summary>
        /// Gets an instance of a type that can used to perform operations on <see cref="BoundParameter" /> using
        /// <typeparamref name="TAlternateKey" /> as a key instead of a string.
        /// </summary>
        /// <typeparam name="TAlternateKey">
        /// Specifies the alternate key type for performing lookups.
        /// </typeparam>
        /// <param name="lookup">
        /// Specifies the created lookup instance when the method returns true, or a default instance that should not be used if the
        /// method returns false.
        /// </param>
        /// <returns>
        /// True if a lookup could be created; otherwise, false.
        /// </returns>
        public bool TryGetAlternateLookup<TAlternateKey>(out Dictionary<string, object>.AlternateLookup<TAlternateKey> lookup) where TAlternateKey : notnull, allows ref struct
        {
            return dictionary.TryGetAlternateLookup(out lookup);
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Tries to add <paramref name="key" /> and <paramref name="value" /> to <see cref="BoundParameter" />, and if that fails,
        /// updates the <paramref name="key" /> with <paramref name="value" />.
        /// </summary>
        /// <param name="key">
        /// Specifies the key of the element to add or update.
        /// </param>
        /// <param name="value">
        /// Specifies the value of the element to add or update.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws if <paramref name="key" /> is not a valid PowerShell parameter name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="key" /> is null or empty.
        /// </exception>
        public void Update(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            else if (ParameterComparer.ValidateParameterName(key))
            {
                throw new ArgumentException("Key is not a valid PowerShell parameter name", nameof(key));
            }
            else if (!dictionary.TryAdd(key, value))
            {
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// Tries to add Key/Value pair <paramref name="item" /> to <see cref="BoundParameter" />, and if that fails, updates
        /// Key/Value pair in <see cref="BoundParameter" />.
        /// </summary>
        /// <param name="item">
        /// Specifies the Key/Value pair to add or update.
        /// </param>
        public void Update(KeyValuePair<string, object> item)
        {
            Update(item.Key, item.Value);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Public Methods

        #region Private Fields

        /// <summary>
        /// Backing storage for <see cref="BoundParameter" />.
        /// </summary>
        private readonly Dictionary<string, object> dictionary;

        #endregion Private Fields
    }
}
