using System;
using System.Collections;
using System.Collections.Generic;

namespace FifoGroup
{
    public class ExcuteToolParamEnumerator : IEnumerator<ExcuteToolParam>
    {
        private ExcuteToolParams _params;
        private int curIndex;
        private ExcuteToolParam curParam;
        private List<string> KeyList;

        public ExcuteToolParamEnumerator(ExcuteToolParams Params)
        {
            _params = Params;
            curIndex = -1;
            curParam = default(ExcuteToolParam);
            KeyList = Params.GetKeyList();
        }

        public bool MoveNext()
        {
            if (++curIndex >= _params.Count)
            {
                return false;
            }
            else
            {
                curParam = _params[KeyList[curIndex]];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }
        void IDisposable.Dispose() { }
        public ExcuteToolParam Current
        {
            get { return curParam; }
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }

    }
}