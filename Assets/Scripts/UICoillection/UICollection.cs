using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UICoillection
{
    public class UICollection : UIContainer
    {
        public List<UIElement> Draw(IEnumerable<object> objects)
        {
            Clear();

            if (objects == null) return null;

            var arr = new List<object>(objects).ToArray();

            if (objects == null || arr.Length == 0 || !HasElementPrefab()) return null;

            List<UIElement> elements = new List<UIElement>();

            for (var i = 0; i < arr.Length; i++)
            {
                //if (!prefab.CanDraw(arr[i])) continue;

                var element = NewElement();
                if (element)
                {
                    AddElement(element);
                    Debug.Log(arr[i]);
                    element.Draw(arr[i]);
                }
                elements.Add(element);
            }

            return elements;
;
        }

        bool CanDraw(object @object)
        {
            return @object != null && @object is IEnumerable<object>;
        }



    }
}