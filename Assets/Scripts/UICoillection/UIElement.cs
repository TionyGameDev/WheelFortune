using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UICoillection
{
    [Serializable]
    public class UIElement : MonoBehaviour
    {
        private RectTransform _transformMe;
        public RectTransform rectTransform { get { return _transformMe; } }
        public RectTransform root { get; private set; }

        private void Awake()
        {
            _transformMe = GetComponent<RectTransform>();
        }
        public void SetRoot(RectTransform root)
        {
            this.root = root;
        }

        public void Draw(object obj)
        {
            
        }

    }
}