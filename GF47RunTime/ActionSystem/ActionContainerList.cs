namespace GF47RunTime.ActionSystem
{
    using System.Collections.Generic;

    internal class ActionContainerList
    {
        public const int Count = 29;

        private SortedList<TriggerMethod, List<int>> _listByTrigger;
        private SortedList<int, ActionContainer> _listByIndex;

        public ActionContainerList()
        {
            _listByIndex = new SortedList<int, ActionContainer>();
            _listByTrigger = new SortedList<TriggerMethod, List<int>>(Count);
        }
        public List<ActionContainer> this[TriggerMethod triggerMethod]
        {
            get
            {
                for (int i = 0, iMax = _listByTrigger.Count; i < iMax; i++)
                {
                    if ((_listByTrigger.Keys[i] & triggerMethod) != 0)
                    {
                        List<int> indexList = _listByTrigger.Values[i];
                        if (indexList == null) return null;

                        List<ActionContainer> list =new List<ActionContainer>();
                        for (int j = 0; j < indexList.Count; j++)
                        {
                            if (_listByIndex.ContainsKey(indexList[j]))
                            {
                                list.Add(_listByIndex[indexList[j]]);
                            }
                        }
                        return list;
                    }
                }
                return null;
            }
        }
        public ActionContainer this[int index]
        {
            get
            {
                if (_listByIndex.ContainsKey(index)) { return _listByIndex[index]; }
                return null;
            }
            set
            {
                if (_listByIndex.ContainsKey(index)) { _listByIndex[index] = value; }
                else { _listByIndex.Add(index, value); }
            }
        }
        public void AddToListSortedByIndex(int index, ActionContainer container)
        {
            if (_listByIndex.ContainsKey(index)) return;
            _listByIndex.Add(index, container);
        }
        public void RemoveFromListSortedByIndex(int index)
        {
            _listByIndex.Remove(index);
        }
        public void AddToListSortedByTrigger(ActionContainer container)
        {
            if (!_listByIndex.ContainsValue(container)) return;
            int key = _listByIndex.Keys[_listByIndex.IndexOfValue(container)];

            for (int i = 0; i < Count; i++)
            {
                TriggerMethod trigger = (TriggerMethod)(1 << i);
                if ((trigger & container.TriggerMethod) == 0)
                {
                    if (_listByTrigger.ContainsKey(trigger))
                    {
                        _listByTrigger[trigger].Remove(key);
                    }
                }
                else
                {
                    if (_listByTrigger.ContainsKey(trigger))
                    {
                        if (!_listByTrigger[trigger].Contains(key))
                        {
                            _listByTrigger[trigger].Add(key);
                        }
                    }
                    else
                    {
                        List<int> list = new List<int> { key };
                        _listByTrigger.Add(trigger, list);
                    }
                }
            }
        }
        public void RemoveFromListSortedByTrigger(ActionContainer container)
        {
            if (!_listByIndex.ContainsValue(container)) return;
            int index = _listByIndex.IndexOfValue(container);

            for (int i = 0; i < _listByTrigger.Count; i++)
            {
                _listByTrigger.Values[i].Remove(index);
            }
        }
    }
}