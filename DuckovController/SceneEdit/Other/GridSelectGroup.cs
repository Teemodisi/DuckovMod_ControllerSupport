using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.Other
{
    public class GridSelectGroup<T> : SelectionGroupBase<T>
    {
        private readonly GridLayoutGroup _gridLayoutGroup;

        private int _xCount;

        private int _yCount;

        public GridSelectGroup(
            GridLayoutGroup gridLayoutGroup,
            Func<T[]> getSelection,
            Action<T, int> onSelected = null,
            Func<IReadOnlyList<T>, int> selectorIndex = null)
            : base(
                getSelection,
                onSelected,
                selectorIndex, false)
        {
            _gridLayoutGroup = gridLayoutGroup;
        }

        public event Action onUpEdge;

        public event Action onDownEdge;

        public event Action onLeftEdge;

        public event Action onRightEdge;

        public bool SelectUp()  => SelectVer(-1);

        public bool SelectDown() => SelectVer(1);

        public bool SelectLeft() => SelectHor(-1);

        public bool SelectRight() => SelectHor(1);

        private bool SelectHor(int offset)
        {
            var cur = defaultSelectorIndex.Invoke(selections);
            if (cur < 0)
            {
                Select(0);
                return true;
            }
            var target = cur + offset;
            var curY = Mathf.FloorToInt((float)cur / _xCount);
            var targetY = Mathf.FloorToInt((float)target / _xCount);
            if (targetY != curY || target < 0 || target >= GroupLength)
            {
                if (offset < 0)
                {
#if DEBUG
                    Debug.Log("到达左边边界");
#endif
                    onLeftEdge?.Invoke();
                }
                else
                {
#if DEBUG
                    Debug.Log("到达右边边界");
#endif
                    onRightEdge?.Invoke();
                }
                return false;
            }
            return Select(target);
        }     
        private bool SelectVer(int offset)
        {
            var cur = defaultSelectorIndex.Invoke(selections);
            if (cur < 0)
            {
                Select(0);
                return true;
            }
            var target = cur + offset * _xCount;
            // var curY = Mathf.FloorToInt((float)cur / _xCount);
            // var targetY = Mathf.FloorToInt((float)target / _xCount);
            if (target < 0 || target >= GroupLength)
            {
                if (offset < 0)
                {
#if DEBUG
                    Debug.Log("到达顶边边界");
#endif
                    onUpEdge?.Invoke();
                }
                else
                {
#if DEBUG
                    Debug.Log("到达底边边界");
#endif
                    onDownEdge?.Invoke();
                }
                return false;
            }
            return Select(target);
        }

        public override void UpdateSelections()
        {
            base.UpdateSelections();
            UpdateEdgeSize();
        }

        public override void UpdateSelections(T[] newSelections)
        {
            base.UpdateSelections(newSelections);
            UpdateEdgeSize();
        }

        private void UpdateEdgeSize()
        {
            var w = _gridLayoutGroup.gameObject.GetComponent<RectTransform>().rect.width;
            var spaceX = _gridLayoutGroup.spacing.x;
            _xCount = Mathf.FloorToInt((w - spaceX) / (_gridLayoutGroup.cellSize.x + spaceX));
            var h = _gridLayoutGroup.gameObject.GetComponent<RectTransform>().rect.height;
            var spaceY = _gridLayoutGroup.spacing.y;
            _yCount = Mathf.FloorToInt((h - spaceY) / (_gridLayoutGroup.cellSize.y + spaceY));
        }
    }
}
