﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Statiq.Common
{
    public class FilteredMetadata : IMetadata
    {
        private readonly IMetadata _metadata;
        private readonly HashSet<string> _keys;

        public FilteredMetadata(IMetadata metadata, params string[] keys)
        {
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            _keys = keys == null
                ? new HashSet<string>()
                : new HashSet<string>(keys.Where(x => metadata.ContainsKey(x)), StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public object this[string key]
        {
            get
            {
                _ = key ?? throw new ArgumentNullException(nameof(key));
                if (!_keys.Contains(key))
                {
                    throw new KeyNotFoundException();
                }
                return _metadata[key];
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> Keys => _keys;

        /// <inheritdoc/>
        public IEnumerable<object> Values => _keys.Select(x => _metadata[x]);

        /// <inheritdoc/>
        public int Count => _keys.Count;

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));
            return _keys.Contains(key);
        }

        /// <inheritdoc/>
        public bool TryGetRaw(string key, out object value)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));
            value = default;
            return _keys.Contains(key) && _metadata.TryGetRaw(key, out value);
        }

        /// <inheritdoc/>
        public bool TryGetValue<TValue>(string key, out TValue value)
        {
            if (TryGetRaw(key, out object rawValue))
            {
                return TypeHelper.TryExpandAndConvert(rawValue, this, out value);
            }
            value = default;
            return false;
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out object value)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));
            value = default;
            return _keys.Contains(key) && _metadata.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            _keys.Select(x => KeyValuePair.Create(x, _metadata[x])).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetRawEnumerator() =>
            _keys.Select(x => KeyValuePair.Create(x, _metadata.GetRaw(x))).GetEnumerator();
    }
}
