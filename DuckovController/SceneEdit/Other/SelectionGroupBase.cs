using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace DuckovController.SceneEdit.Other
{
    public class SelectionGroupBase<T>
    {
        protected readonly Func<IReadOnlyList<T>, int> defaultSelectorIndex;

        protected readonly Func<T[]> getSelection;

        protected readonly bool isLoop;

        protected readonly Action<T, int> onSelected;

        protected int currentIndexCache = -1;

        protected T[] selections;

        public SelectionGroupBase(
            Func<T[]> getSelection,
            Action<T, int> onSelected = null,
            Func<IReadOnlyList<T>, int> selectorIndex = null,
            bool loop = true)
        {
            this.getSelection = getSelection;
            defaultSelectorIndex = selectorIndex;
            this.onSelected = onSelected;
            if (defaultSelectorIndex == null)
            {
                defaultSelectorIndex = DefaultGetIndex;
            }
            isLoop = loop;
            selections = this.getSelection?.Invoke();
        }

        public int GroupLength => selections.Length;

        public IReadOnlyList<T> Selections => selections;

        [CanBeNull]
        public T CurrentSelection
        {
            get
            {
                var index = defaultSelectorIndex.Invoke(selections);
                if (index < 0 || index >= GroupLength)
                {
                    return default;
                }
                return selections[index];
            }
        }

        public bool Select(int index)
        {
            if (index < 0 || index >= GroupLength)
            {
#if DEBUG
                Debug.LogWarning("Invalid selection group index");
#endif
                return false;
            }
#if DEBUG
            Debug.Log("Trying to select " + index);
#endif
            onSelected?.Invoke(selections[index], index);
            currentIndexCache = index;
            return true;
        }

        private int DefaultGetIndex(IReadOnlyList<T> controls)
        {
            return currentIndexCache;
        }

        public virtual void UpdateSelections()
        {
            selections = getSelection?.Invoke();
        }

        public virtual void UpdateSelections(T[] newSelection)
        {
            selections = newSelection;
        }
    }
}
