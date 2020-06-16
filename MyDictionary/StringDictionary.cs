using System;

namespace MyDictionary
{
    public class StringDictionary : IDictionary
    {
        private struct Entry
        {
            public int next;
            public uint hashCode;
            public string key;           
            public string value;         
        }

        private int[]? _buckets;
        private Entry[]? _entries;

        private int _count;
        private int _freeList;
        private int _freeCount;

        private const int StartOfFreeList = -3;


        public StringDictionary()
        {
            var size = 64;

            _freeList = -1;
            _buckets = new int[size];
            _entries = new Entry[size];
        }

        public int Count => _count - _freeCount;


        public string this[string key]
        {
            get
            {
                var i = FindEntry(key);

                if (i >= 0) 
                    return _entries![i].value;
                
                return null;
            }

            set => Add(key, value);
        }

        public void Add(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            var entries = _entries;

            var hashCode = (uint)key.GetHashCode();

          
            ref var bucket = ref _buckets[hashCode % _buckets.Length];

            var i = bucket - 1;

            while (true)
            {
                if ((uint)i >= (uint)entries.Length)
                {
                    break;
                }

                if (entries[i].hashCode == hashCode && entries[i].key == key)
                {
                    entries[i].value = value;
                    return;
                }

                i = entries[i].next;
            } 

            var updateFreeList = false;
            int index;
            if (_freeCount > 0)
            {
                index = _freeList;
                updateFreeList = true;
                _freeCount--;
            }
            else
            {
                var count = _count;
                if (count == entries.Length)
                {
                    Resize();
                    bucket = ref _buckets[hashCode % (uint)_buckets.Length];
                }
                index = count;
                _count = count + 1;
                entries = _entries;
            }

            ref var entry = ref entries![index];

            if (updateFreeList)
            {
                _freeList = StartOfFreeList - entries[_freeList].next;
            }

            entry.hashCode = hashCode;
            entry.next = bucket - 1;
            entry.key = key;
            entry.value = value;
            bucket = index + 1;
        }

        public bool ContainsKey(string key)
            => FindEntry(key) >= 0;

        private int FindEntry(string key)
        {
            if (key == null)
            {
                throw new NullReferenceException();
            }

            var i = -1;
            var buckets = _buckets;
            var entries = _entries;
            
            if (buckets != null)
            {
                var hashCode = (uint)key.GetHashCode();
                
                i = buckets[hashCode % buckets.Length] - 1;
                
                while (true)
                {
                    if ((uint)i >= entries.Length || (entries[i].hashCode == hashCode && entries[i].key == key))
                    {
                        break;
                    }

                    i = entries[i].next;
                }
            }

            return i;
        }

        private void Resize() => Resize(_count * 3);

        private void Resize(int newSize)
        {
            var buckets = new int[newSize];
            var entries = new Entry[newSize];
            Array.Copy(_entries, 0, entries, 0, _count);

            for (var i = 0; i < _count; i++)
            {
                if (entries[i].next >= -1)
                {
                    var bucket = entries[i].hashCode % (uint)newSize; 
                    entries[i].next = buckets[bucket] - 1;
                    
                    buckets[bucket] = i + 1;
                }
            }

            _buckets = buckets;
            _entries = entries;
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            var buckets = _buckets;
            var entries = _entries;

            if (buckets != null)
            {
                var hashCode = (uint)key.GetHashCode();
                var bucket = hashCode % (uint)buckets.Length;
                var last = -1;
                
                var i = buckets[bucket] - 1;
                while (i >= 0)
                {
                    ref var entry = ref entries[i];

                    if (entry.hashCode == hashCode && entry.key == key)
                    {
                        if (last < 0)
                        {
                            buckets[bucket] = entry.next + 1;
                        }
                        else
                        {
                            entries[last].next = entry.next;
                        }

                        entry.next = StartOfFreeList - _freeList;

                        _freeList = i;
                        _freeCount++;
                        return;
                    }

                    last = i;
                    i = entry.next;
                }
            }
        }
    }
}
