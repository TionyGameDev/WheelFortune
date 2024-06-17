using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UICoillection
{
    public class UICollection<T> : BaseUICollection<T>
    {
        public Transform parent;
        public GameObject prefab;
        public void Drawer(IEnumerable<T> values)
        {
           foreach (var item in values)
            {
                
            }
        }
        
    }
}