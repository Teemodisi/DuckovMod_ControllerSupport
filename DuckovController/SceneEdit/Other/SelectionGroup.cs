using System;
using UnityEngine;

namespace DuckovController.SceneEdit.Other
{
    public class SelectionGroup<T>
    {
        private readonly T[] _controls;

        private readonly Func<int> _getSelectorIndex;

        private readonly bool _isLoop;

        private readonly Action<T, int> _onSelected;

        private int _currentIndexCache;

        public SelectionGroup(
            T[] controls,
            Action<T, int> onSelected,
            Func<int> getSelectorIndex = null,
            bool loop = true)
        {
            _controls = controls;
            _getSelectorIndex = getSelectorIndex;
            _onSelected = onSelected;
            if (_getSelectorIndex == null)
            {
                _getSelectorIndex = DefaultGetIndex;
            }
            _currentIndexCache = _getSelectorIndex.Invoke();
            _isLoop = loop;
        }

        public int GroupLength => _controls.Length;

        private int DefaultGetIndex()
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
            _onSelected?.Invoke(_controls[index], index);
            _currentIndexCache = index;
        }

        public void SelectNext()
        {
            var cur = _getSelectorIndex.Invoke();
            if (_isLoop)
            {
                cur = cur + 1 >= GroupLength ? 0 : cur + 1;
            }
            else
            {
                cur = cur + 1 >= GroupLength ? GroupLength - 1 : cur + 1;
            }
            Select(cur);
        }

        public void SelectPrev()
        {
            var cur = _getSelectorIndex.Invoke();
            if (_isLoop)
            {
                cur = cur - 1 < 0 ? GroupLength - 1 : cur - 1;
            }
            else
            {
                cur = cur - 1 < 0 ? 0 : cur - 1;
            }
            Select(cur);
        }
    }
}
