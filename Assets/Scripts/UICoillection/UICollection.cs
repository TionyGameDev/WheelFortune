using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UICoillection
{
    public class UICollection : BaseUICollection<UiDrawer>
    {
        public List<UiDrawer> Set(IEnumerable<object> objects)
        {
            Clear();


            if (objects == null) return null;

            var arr = new List<object>(objects).ToArray();

            if (objects == null || arr.Length == 0 || !HasElementPrefab()) return null;

            List<UiDrawer> _uiDrawers = new List<UiDrawer>();

            for (var i = 0; i < arr.Length; i++)
            {
                var data = arr[i];

                if (data == null) continue;

                //if (!prefab.CanDraw(arr[i])) continue;

                var element = NewElement();
                if (element)
                {
                    AddElement(element);
                    element.Set(arr[i]);
                    _uiDrawers.Add(element);
                }
            }

            return _uiDrawers;

            //LogWrapper.Log("elements: " + elements.Length);
        }

        bool CanDraw(object @object)
        {
            return @object != null && @object is IEnumerable<object>;
        }



    }
}