using DuckovController.Utils;
using TMPro;
using UnityEngine;

namespace DuckovController.SceneEdit
{
    public class ResourceDataBase : Singleton<ResourceDataBase>
    {
        public TMP_FontAsset Font { get; private set; } = null!;

        public void Init(RectTransform transform)
        {
            var tmp = transform.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp == null)
            {
                Debug.LogError($"{nameof(ResourceDataBase)}");
                return;
            }
            Font = tmp.font;
        }
    }
}
