using System;
using System.Collections.Generic;
using UnityEngine;

namespace DuckovController.SceneEdit.Other
{
    public class SelectionGroup<T> : SelectionGroupBase<T>
    {
        public SelectionGroup(
            Func<T[]> getSelection,
            Action<T, int> onSelected = null,
            Func<IReadOnlyList<T>, int> selectorIndex = null,
            bool loop = true) : base(getSelection, onSelected, selectorIndex, loop) { }

        public bool SelectNext()
        {
            var cur = defaultSelectorIndex.Invoke(selections);
            if (isLoop)
            {
                cur = cur + 1 >= GroupLength ? 0 : cur + 1;
            }
            else
            {
                if (cur + 1 >= GroupLength)
                {
#if DEBUG
                    Debug.Log("Trying to select next but already at the end");
#endif
                    return false;
                }
                cur = cur + 1;
            }
#if DEBUG
            Debug.Log($"Trying to select prev {defaultSelectorIndex.Invoke(selections)} => {cur}");
#endif
            return Select(cur);
        }

        public bool SelectPrev()
        {
            var cur = defaultSelectorIndex.Invoke(selections);
            if (isLoop)
            {
                cur = cur - 1 < 0 ? GroupLength - 1 : cur - 1;
            }
            else
            {
                if (cur - 1 < 0)
                {
#if DEBUG
                    Debug.Log("Trying to select prev but already at the start");
#endif
                    return false;
                }
                cur = cur - 1;
            }
#if DEBUG
            Debug.Log($"Trying to select prev {defaultSelectorIndex.Invoke(selections)} => {cur}");
#endif
            return Select(cur);
        }
    }
}
