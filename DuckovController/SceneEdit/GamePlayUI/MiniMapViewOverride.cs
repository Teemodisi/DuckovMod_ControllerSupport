using System.Collections.Generic;
using Duckov.MiniMaps;
using Duckov.MiniMaps.UI;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class MiniMapViewOverride : MonoBehaviour
    {
        private const float cursor_speed = 1000f;

        private const float zoom_speed = 2f;

        private Vector2 _cursorPosInput;

        private Vector2 _cursorPosition;

        private Vector2 _moveInput;

        private float _zoomInput;

        private RectTransform _miniMapDisplay;

        private ScrollRect _scrollRect;

        // 气笑了这里两个选择组在更新选择的时候会被MapMarkerSettingsPanel重新绑定响应的Index
        // 在调用前需要手动刷新一遍选择项列表
        private HorizontalLayoutGroup _iconGroup;

        private HorizontalLayoutGroup _colorGroup;

        private SelectionGroup<Button> _iconGroupSelectionGroup;

        private SelectionGroup<Button> _colorGroupSelectionGroup;

        private SelectionGroup<SelectionGroup<Button>> _toolSelectionGroup;

        private MapMarkerSettingsPanel _mapMarkerSettingsPanel;

        private readonly List<RaycastResult> _raycastResults = new List<RaycastResult>();

        private void Awake()
        {
            _miniMapDisplay = GetComponentInChildren<MiniMapDisplay>().GetComponent<RectTransform>();
            _scrollRect = transform.Find("Content/Scroll View")?.GetComponent<ScrollRect>();
            if (_scrollRect == null)
            {
                Debug.LogError("Scroll View not found");
            }
            _mapMarkerSettingsPanel = GetComponentInChildren<MapMarkerSettingsPanel>();
            if (_mapMarkerSettingsPanel == null)
            {
                Debug.LogError("Map Marker Settings Panel not found");
            }
            _iconGroup = _mapMarkerSettingsPanel.transform.Find("Icons").GetComponent<HorizontalLayoutGroup>();
            if (_iconGroup == null)
            {
                Debug.LogError("Icon Group not found");
            }
            //狗屎啊 每次选中 MapMarkerSettingsPanel 全回收对象池重新生成一边
            //我说patch进去的数字怎么每按一次就反转排序一次
            //原来是进了对象池又出来了一遍重新赋值了icon
            //没法用SelectionGroup<T>
            _iconGroupSelectionGroup = new SelectionGroup<Button>(
                _iconGroup.GetComponentsInChildren<Button>(),
                (button, index) => button.onClick.Invoke(),
                buttons => MapMarkerManager.SelectedIconIndex);
            _colorGroup = _mapMarkerSettingsPanel.transform.Find("Colors").GetComponent<HorizontalLayoutGroup>();
            if (_colorGroup == null)
            {
                Debug.LogError("Color Group not found");
            }
            _colorGroupSelectionGroup = new SelectionGroup<Button>(
                _colorGroup.GetComponentsInChildren<Button>(),
                (button, index) => { button.onClick.Invoke(); }
            );
            _toolSelectionGroup = new SelectionGroup<SelectionGroup<Button>>(
                new[] { _iconGroupSelectionGroup, _colorGroupSelectionGroup }
                , loop: false
            );
            Patch();
            InitInput();
            UpdateToolSelected();
        }

        private void LateUpdate()
        {
            if (_moveInput.sqrMagnitude > 0.0001f)
            {
                //操控速度 2次幂
                _miniMapDisplay.anchoredPosition +=
                    -_moveInput * (_moveInput.sqrMagnitude * cursor_speed * Time.deltaTime);
            }
            if (_cursorPosInput.sqrMagnitude > 0.0001f)
            {
                _cursorPosition = Mouse.current.position.ReadValue();
                //操控速度 2次幂
                _cursorPosition += _cursorPosInput * (_cursorPosInput.sqrMagnitude * cursor_speed * Time.deltaTime);
                Mouse.current.WarpCursorPosition(_cursorPosition);
            }
            if (Mathf.Abs(_zoomInput) > 0.1f)
            {
                RefZoom += _zoomInput * zoom_speed * Time.deltaTime;
            }
        }

        private void OnEnable()
        {
            _inputActionMap?.Enable();
        }

        private void OnDisable()
        {
            _inputActionMap?.Disable();
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _moveInput = context.ReadValue<Vector2>();
            }
            if (context.canceled)
            {
                _moveInput = Vector2.zero;
            }
        }

        private void OnMoveMouseInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _cursorPosition = Mouse.current.position.ReadValue();
            }
            if (context.performed)
            {
                _cursorPosInput = context.ReadValue<Vector2>();
            }
            if (context.canceled)
            {
                _cursorPosInput = Vector2.zero;
            }
        }

        private void OnZoomInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _zoomInput = context.ReadValue<float>();
            }
            if (context.canceled)
            {
                _zoomInput = 0;
            }
        }

        private void OnPinInput(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            // 模拟事件点击
            var pos = Mouse.current.position.ReadValue();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, pos, null, out _);
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = pos,
                button = PointerEventData.InputButton.Right
            };
            EventSystem.current.RaycastAll(pointerEventData, _raycastResults);
            if (_raycastResults.Count > 0)
            {
                var obj = _raycastResults[0].gameObject;
                var handle = ExecuteEvents.Execute(obj, pointerEventData, ExecuteEvents.pointerClickHandler);
                if (!handle)
                {
                    ExecuteEvents.ExecuteHierarchy(obj, pointerEventData, ExecuteEvents.pointerClickHandler);
                }
            }
        }

        private void OnSelectColorAndIconInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var dir = context.ReadValue<Vector2>();
                if (dir.x > 0.1f)
                {
                    UpdateSelections();
                    _toolSelectionGroup.CurrentSelection.SelectNext();
                }
                else if (dir.x < -0.1f)
                {
                    UpdateSelections();
                    _toolSelectionGroup.CurrentSelection.SelectPrev();
                }
                else if (dir.y > 0.1f)
                {
                    _toolSelectionGroup.SelectPrev();
                    UpdateToolSelected();
                }
                else if (dir.y < -0.1f)
                {
                    _toolSelectionGroup.SelectNext();
                    UpdateToolSelected();
                }
            }
        }

        private void OnCenterPlayerInput(InputAction.CallbackContext context)
        {
            MiniMapView.Instance.CeneterPlayer();
        }

        private void UpdateSelections()
        {
            if (_toolSelectionGroup.CurrentSelection == _iconGroupSelectionGroup)
            {
                _iconGroupSelectionGroup.UpdateSelections(_iconGroup.GetComponentsInChildren<Button>());
            }
            else if (_toolSelectionGroup.CurrentSelection == _colorGroupSelectionGroup)
            {
                _colorGroupSelectionGroup.UpdateSelections(_colorGroup.GetComponentsInChildren<Button>());
            }
        }

        private void UpdateToolSelected()
        {
            if (_toolSelectionGroup.CurrentSelection == _iconGroupSelectionGroup)
            {
                _iconGroupOnSelectedImage.gameObject.SetActive(true);
                _colorGroupOnSelectedImage.gameObject.SetActive(false);
            }
            else if (_toolSelectionGroup.CurrentSelection == _colorGroupSelectionGroup)
            {
                _iconGroupOnSelectedImage.gameObject.SetActive(false);
                _colorGroupOnSelectedImage.gameObject.SetActive(true);
            }
        }
    }
}
