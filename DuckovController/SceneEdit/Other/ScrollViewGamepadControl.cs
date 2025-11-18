using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.Other
{
    public class ScrollViewGamepadControl : MonoBehaviour
    {
        private ScrollRect _scrollRect;

        private float _velocity;

        private float _input;

        private bool _doFocusTween;

        private float _focusTarget;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        private void Update()
        {
            if (_scrollRect == null)
            {
                return;
            }
            if (_doFocusTween)
            {
                var pos = _scrollRect.content.anchoredPosition;
                pos.y = Mathf.SmoothDamp(pos.y, _focusTarget, ref _velocity, 0.15f);
                _scrollRect.content.anchoredPosition = pos;
            }
            else
            {
                var target = Mathf.Abs(_input) > 0.1f ? _input : 0;
                _velocity = Mathf.MoveTowards(_velocity, target, 10 * Time.deltaTime);
                _scrollRect.content.anchoredPosition += new Vector2(0, 1500f * _velocity * Time.deltaTime);
            }
        }

        public void Move(Vector2 axisInput)
        {
            if (_doFocusTween)
            {
                _velocity = 0;
            }
            _doFocusTween = false;
            _input = -axisInput.y;
        }

        //需要传入的RectTransform是Content下的直接子物体
        //不适用于有缩放关系和嵌套关系的情况
        public void TryToFocusContentObject(RectTransform target)
        {
            if (target == null)
            {
                return;
            }
            if (!_doFocusTween)
            {
                _velocity = 0;
            }
            _doFocusTween = true;
            var viewPortHeight = _scrollRect.viewport.rect.height;
            var contentHeight = _scrollRect.content.rect.height;
            var targetInContentPos = target.anchoredPosition;
            _focusTarget = -(targetInContentPos.y + viewPortHeight * 0.5f);
            _focusTarget = Mathf.Clamp(_focusTarget, 0, contentHeight - viewPortHeight);
        }
    }
}
