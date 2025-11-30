using DuckovController.Helper;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public abstract class LootViewSubSelection : AbstractPatch
    {
        private RectTransform _selectedBorder;

        protected virtual string SelectorPatchSubPath => null;

        public void OnSelect()
        {
            _selectedBorder.gameObject.SetActive(true);
        }

        public void OnDeselect()
        {
            _selectedBorder.gameObject.SetActive(false);
        }

        protected override void Patch()
        {
            var targetObj =SelectorPatchSubPath == null
                ? transform
                : transform.FindWithDebug(SelectorPatchSubPath);
            
            const int width = 5;
            var obj = new GameObject("SelectionMark");
            _selectedBorder = obj.AddComponent<RectTransform>();
            obj.transform.SetParent(targetObj, false);
            var um = obj.AddComponent<UniformModifier>();
            var pi = obj.AddComponent<ProceduralImage>();
            um.Radius = 10 + width * 2;
            pi.BorderWidth = width;
            pi.raycastTarget = false;
            pi.color = UIStyle.s_Light;
            _selectedBorder.pivot = new Vector2(0.5f, 0.5f);
            _selectedBorder.anchorMin = Vector2.zero;
            _selectedBorder.anchorMax = Vector2.one;
            _selectedBorder.anchoredPosition = Vector2.zero;
            _selectedBorder.sizeDelta = Vector2.one * (width * 2);
            obj.AddComponent<LayoutElement>().ignoreLayout = true;

            _selectedBorder.gameObject.SetActive(false);
        }
    }
}
