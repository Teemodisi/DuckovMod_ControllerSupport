using System;
using System.Collections.Generic;
using UnityEngine;

namespace DuckovController.SceneEdit.Other
{
    public class SelectionGroup<T>
    {
        private readonly Func<IReadOnlyList<T>, int> _defaultSelectorIndex;

        private readonly bool _isLoop;

        private readonly Action<T, int> _onSelected;

        private T[] _selections;

        private int _currentIndexCache;

        public SelectionGroup(
            T[] selections,
            Action<T, int> onSelected = null,
            Func<IReadOnlyList<T>, int> selectorIndex = null,
            bool loop = true)
        {
            _selections = selections;
            _defaultSelectorIndex = selectorIndex;
            _onSelected = onSelected;
            if (_defaultSelectorIndex == null)
            {
                _defaultSelectorIndex = DefaultGetIndex;
            }
            _isLoop = loop;
        }

        public int GroupLength => _selections.Length;

        public T CurrentSelection => _selections[_defaultSelectorIndex.Invoke(_selections)];

        private int DefaultGetIndex(IReadOnlyList<T> controls)
        {
            return _currentIndexCache;
        }

        public void Select(int index)
        {
            if (index < 0 || index >= GroupLength)
            {
                Debug.LogError("Invalid selection group index");
                return;
            }
            Debug.Log("Trying to select " + index);
            _onSelected?.Invoke(_selections[index], index);
            _currentIndexCache = index;
        }

        public void SelectNext()
        {
            var cur = _defaultSelectorIndex.Invoke(_selections);
            if (_isLoop)
            {
                cur = cur + 1 >= GroupLength ? 0 : cur + 1;
            }
            else
            {
                if (cur + 1 >= GroupLength)
                {
                    return;
                }
                cur = cur + 1;
            }
            Select(cur);
        }

        public void SelectPrev()
        {
            var cur = _defaultSelectorIndex.Invoke(_selections);
            if (_isLoop)
            {
                cur = cur - 1 < 0 ? GroupLength - 1 : cur - 1;
            }
            else
            {
                if (cur - 1 < 0)
                {
                    return;
                }
                cur = cur - 1;
            }
            Select(cur);
        }

        public void UpdateSelections(T[] selections)
        {
            this._selections = selections;
        }
    }
}
