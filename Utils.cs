using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DuckovController
{
    public class Utils
    {
        public void ShowAllComponents(Transform transform)
        {
            var stack = new Stack<Transform>();
            stack.Push(transform);
            var level = 0;
            var sb = new StringBuilder();
            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                if (cur != null)
                {
                    sb.Clear();
                    for (var i = 0; i < level; i++)
                    {
                        sb.Append("    ");
                    }
                    // sb.Append()
                    // Debug.Log($"{string.}");

                    level++;
                    var count = cur.childCount;
                    for (var i = 0; i < count; i++)
                    {
                        var obj = cur.GetChild(i);
                        // stack.Push();
                    }
                }
            }
        }
    }
}
