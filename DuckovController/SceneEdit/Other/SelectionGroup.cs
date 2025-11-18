using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace DuckovController.SceneEdit.Other
{
    public class SelectionGroup<T>
    {
        private readonly Func<IReadOnlyList<T>, int> _defaultSelectorIndex;

        private readonly bool _isLoop;

        private readonly Action<T, int> _onSelected;

        private int _currentIndexCache;

        private readonly Func<T[]> _onUpdateSelection;

        private T[] _selections;

        public SelectionGroup(
            Func<T[]> onUpdateSelection,
            Action<T, int> onSelected = null,
            Func<IReadOnlyList<T>, int> selectorIndex = null,
            bool loop = true)
        {
            _onUpdateSelection = onUpdateSelection;
            _selections = onUpdateSelection.Invoke();
            _defaultSelectorIndex = selectorIndex;
            _onSelected = onSelected;
            if (_defaultSelectorIndex == null)
            {
                _defaultSelectorIndex = DefaultGetIndex;
            }
            _isLoop = loop;
        }

        public int GroupLength => _selections.Length;

        [CanBeNull]
        public T CurrentSelection
        {
            get
            {
                var index = _defaultSelectorIndex.Invoke(_selections);
                if (index < 0 || index >= GroupLength)
                {
                    return default;
                }
                return _selections[index];
            }
        }

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
#if DEBUG
                    Debug.Log($"Trying to select next but already at the end");
#endif
                    return;
                }
                cur = cur + 1;
            }
#if DEBUG
            Debug.Log($"Trying to select prev {_defaultSelectorIndex.Invoke(_selections)} => {cur}");
#endif
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
#if DEBUG
                    Debug.Log($"Trying to select prev but already at the start");
#endif
                    return;
                }
                cur = cur - 1;
            }
#if DEBUG
            Debug.Log($"Trying to select prev {_defaultSelectorIndex.Invoke(_selections)} => {cur}");
#endif
            Select(cur);
        }

        public void UpdateSelections()
        {
            _selections = _onUpdateSelection.Invoke();
        }

        public void UpdateSelections(T[] selections)
        {
            _selections = selections;
        }
    }
}
