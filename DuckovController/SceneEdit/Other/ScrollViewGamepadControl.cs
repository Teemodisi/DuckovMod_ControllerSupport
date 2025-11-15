using UnityEngine;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.Other
{
    public class ScrollViewGamepadControl : MonoBehaviour
    {
        private ScrollRect _scrollRect;

        private float _velocity;

        private float _input;

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
            var target = Mathf.Abs( _input )> 0.1f ? _input : 0;
            _velocity = Mathf.MoveTowards(_velocity, target, 10 * Time.deltaTime);
            _scrollRect.content.anchoredPosition += new Vector2(0, 1500f * _velocity * Time.deltaTime);
        }

        public void Move(Vector2 axisInput)
        {
            _input = axisInput.y;
        }
    }
}
