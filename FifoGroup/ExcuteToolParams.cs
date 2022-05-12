using System;
using System.Collections;
using System.Collections.Generic;

namespace FifoGroup
{
    public class ExcuteToolParams : ICollection<ExcuteToolParam>
    {
        public IEnumerator<ExcuteToolParam> GetEnumerator()
        {
            return new ExcuteToolParamEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ExcuteToolParamEnumerator(this);
        }

        public List<string> GetKeyList()
        {
            List<string> list = new List<string>();
            foreach(KeyValuePair<string,ExcuteToolParam> param in innerDictionaty)
            {
                list.Add(param.Key);
            }
            return list;
        }

        private Dictionary<string, ExcuteToolParam> innerDictionaty;
        public ExcuteToolParams()
        {
            innerDictionaty = new Dictionary<string, ExcuteToolParam>();
        }
        public ExcuteToolParam this[string Name]
        {
            get { return innerDictionaty[Name]; }
            set { innerDictionaty[Name] = value; }
        }

        public bool Contains(ExcuteToolParam item)
        {
            bool found = false;
            foreach (KeyValuePair<string, ExcuteToolParam> pair in innerDictionaty)
            {
                if (pair.Value.Equals(item))
                {
                    found = true;
                }
            }
            return found;
        }

        public bool ContainKeys(string key)
        {
            bool found = false;
            if (innerDictionaty.ContainsKey(key)) found = true;
            return found;
        }

        public void Add(ExcuteToolParam param)
        {
            if (param == null) throw new ArgumentNullException(nameof(param));
            innerDictionaty.Add(param.Name, param);
        }
        public void Clear()
        {
            innerDictionaty.Clear();
        }
        public void CopyTo(ExcuteToolParam[] array, int index)
        {

        }

        public int Count
        {
            get { return innerDictionaty.Count; }
        }
        public bool Remove(string key)
        {
            if (!innerDictionaty.ContainsKey(key)) return false;
            innerDictionaty.Remove(key);
            return true;
        }
        public bool Remove(ExcuteToolParam param)
        {
            if (param == null) return false;
            string key = param.Name;
            if (!innerDictionaty.ContainsKey(key)) return true;
            innerDictionaty.Remove(key);
            return true;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

    }
}