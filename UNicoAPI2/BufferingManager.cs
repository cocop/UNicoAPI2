using System;
using System.Collections.Generic;

namespace UNicoAPI2
{
    public class BufferingManager<DataType, KeyType>
    {
        /*====================================*/
        class Buffer
        {
            public KeyType Key { get; set; } = default;
            public DataType Data { get; set; } = default;
            public int GotCount { get; set; } = 0;
        }

        /*====================================*/
        public int BufferCount { get; set; } = 10;

        /*====================================*/
        IDictionary<KeyType, Buffer> map;
        Queue<Buffer> gotHistory = new Queue<Buffer>();

        /*====================================*/
        public BufferingManager(BufferingMapType bufferingMapType = BufferingMapType.HashTable)
        {
            switch (bufferingMapType)
            {
                case BufferingMapType.HashTable: map = new Dictionary<KeyType, Buffer>(); break;
                case BufferingMapType.BinaryTree: map = new SortedDictionary<KeyType, Buffer>(); break;
            }
        }

        /*====================================*/
        public DataType Get(KeyType id, Func<KeyType, DataType> createMethod)
        {
            Buffer result = GetBuffer(id, createMethod);
            Manageing();

            return result.Data;
        }

        public DataType Get(KeyType id, Func<KeyType, DataType> createMethod, Func<DataType, bool> isAlwaysBuffuring)
        {
            Buffer result = GetBuffer(id, createMethod);
            Manageing(isAlwaysBuffuring);

            return result.Data;
        }

        public void Add(KeyType id, DataType manageingData, Func<DataType, bool> isAlwaysBuffuring)
        {
            AddBuffer(id, manageingData);
            Manageing(isAlwaysBuffuring);
        }

        public void Add(KeyType id, DataType manageingData)
        {
            AddBuffer(id, manageingData);
            Manageing();
        }

        public void Clear()
        {
            gotHistory.Clear();
            map.Clear();
        }

        Buffer AddBuffer(KeyType id, DataType manageingData)
        {
            var result = new Buffer()
            {
                Key = id,
                Data = manageingData,
                GotCount = 1,
            };

            map.Add(id, result);
            gotHistory.Enqueue(result);
            return result;
        }

        Buffer GetBuffer(KeyType id, Func<KeyType, DataType> createMethod)
        {
            if (map.TryGetValue(id, out Buffer result))
            {
                ++result.GotCount;
                gotHistory.Enqueue(result);
            }
            else
                result = AddBuffer(id, createMethod(id));

            return result;
        }

        void Manageing()
        {
            while (map.Count > BufferCount)
            {
                var item = gotHistory.Dequeue();

                if (--item.GotCount == 0)
                    map.Remove(item.Key);
            }
        }

        void Manageing(Func<DataType, bool> isAlwaysBuffuring)
        {
            int count = 0;
            while (count < gotHistory.Count && map.Count > BufferCount)
            {
                var item = gotHistory.Dequeue();

                if (--item.GotCount == 0)
                {
                    if (isAlwaysBuffuring(item.Data))
                        gotHistory.Enqueue(item);
                    else
                        map.Remove(item.Key);
                }

                ++count;
            }
        }
    }

    public class BufferingManager<DataType> : BufferingManager<DataType, string> { }
}
