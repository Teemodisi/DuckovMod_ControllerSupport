using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.Other
{
    public interface IGridSelectGroup
    {
        public int XCount { get; }

        public int YCount { get; }

        public int GroupLength { get; }

        public event Action<Vector2Int> onUpEdge;

        public event Action<Vector2Int> onDownEdge;

        public event Action<Vector2Int> onLeftEdge;

        public event Action<Vector2Int> onRightEdge;

        public bool Select(int index);

        public bool SelectUp();

        public bool SelectDown();

        public bool SelectLeft();

        public bool SelectRight();

        public void UpdateSelections();
    }

    public class GridSelectGroup<T> : SelectionGroupBase<T>, IGridSelectGroup
    {
        private readonly GridLayoutGroup _gridLayoutGroup;

        public GridSelectGroup(
            GridLayoutGroup gridLayoutGroup,
            Func<T[]> getSelection,
            Action<T, int> onSelected = null,
            Action<T, int> onDeselected = null,
            Func<IReadOnlyList<T>, int> selectorIndex = null)
            : base(
                getSelection,
                onSelected,
                onDeselected,
                selectorIndex, false)
        {
            _gridLayoutGroup = gridLayoutGroup;
        }

        public int XCount { get; private set; }

        public int YCount { get; private set; }

        public event Action<Vector2Int> onUpEdge;

        public event Action<Vector2Int> onDownEdge;

        public event Action<Vector2Int> onLeftEdge;

        public event Action<Vector2Int> onRightEdge;

        public bool SelectUp()
        {
            return SelectVer(-1);
        }

        public bool SelectDown()
        {
            return SelectVer(1);
        }

        public bool SelectLeft()
        {
            return SelectHor(-1);
        }

        public bool SelectRight()
        {
            return SelectHor(1);
        }

        public override void UpdateSelections()
        {
            base.UpdateSelections();
            UpdateEdgeSize();
        }

        private bool SelectHor(int offset)
        {
            var cur = defaultSelectorIndex.Invoke(selections);
            if (cur < 0)
            {
                Select(0);
                return true;
            }
            var target = cur + offset;
            var curY = Mathf.FloorToInt((float)cur / XCount);
            var curX = cur - curY * XCount;
            var targetY = Mathf.FloorToInt((float)target / XCount);
            if (targetY != curY || target < 0 || target >= GroupLength)
            {
                if (offset < 0)
                {
#if DEBUG
                    Debug.Log($"到达左边边界 x:{curX} y:{curY}");
#endif
                    onLeftEdge?.Invoke(new Vector2Int(curX, curY));
                }
                else
                {
#if DEBUG
                    Debug.Log($"到达右边边界 x:{curX} y:{curY}");
#endif
                    onRightEdge?.Invoke(new Vector2Int(curX, curY));
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
            var target = cur + offset * XCount;
            var curY = Mathf.FloorToInt((float)cur / XCount);
            var curX = cur - curY * XCount;
            if (target < 0 || target >= GroupLength)
            {
                if (offset < 0)
                {
#if DEBUG
                    Debug.Log($"到达顶边边界 x:{curX} y:{curY}");
#endif
                    onUpEdge?.Invoke(new Vector2Int(curX, curY));
                }
                else
                {
#if DEBUG
                    Debug.Log($"到达底边边界 x:{curX} y:{curY}");
#endif
                    onDownEdge?.Invoke(new Vector2Int(curX, curY));
                }
                return false;
            }
            return Select(target);
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
            XCount = Mathf.FloorToInt((w - spaceX) / (_gridLayoutGroup.cellSize.x + spaceX));
            var h = _gridLayoutGroup.gameObject.GetComponent<RectTransform>().rect.height;
            var spaceY = _gridLayoutGroup.spacing.y;
            YCount = Mathf.FloorToInt((h - spaceY) / (_gridLayoutGroup.cellSize.y + spaceY));
        }
    }
}
