using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UICoillection
{
    public class UIContainer : BaseUICollection<UIElement> { }
    public abstract class BaseUICollection<T> : MonoBehaviour where T : UIElement
    {
        [SerializeField] private Transform _container;
        [SerializeField] private T _itemPrefab;
        public T prefab { get { return _itemPrefab; } }

        private List<T> _elements = new List<T>();
        public T[] elements { get { return _elements.ToArray(); } }


       

        protected virtual void AddElement(T element)
        {
            if (!element) return;

            element.transform.SetParent(_container ? _container : transform);
            element.transform.localPosition = Vector3.zero;
            element.transform.localScale = Vector3.one;
            element.transform.localRotation = Quaternion.identity;

            _elements.Add(element);
        }

        protected void RemoveElement(T element)
        {
            if (!element) return;

            _elements.Remove(element);

            Destroy(element.gameObject);
        }

        public virtual void Clear()
        {
            foreach (var element in _elements)
                if (element != null && element.gameObject != null)
                    Destroy(element.gameObject);

            _elements.Clear();

            if (_container)
            {
                var elements = _container.GetComponentsInChildren<T>();

                foreach (var element in elements)
                    if (element != null && element.gameObject != null && element.gameObject != gameObject)
                        Destroy(element.gameObject);
            }
        }

        protected bool HasElementPrefab()
        {
            return _itemPrefab;
        }

        public virtual T NewElement()
        {
            var obj = _itemPrefab ? Instantiate(_itemPrefab) : null;
            Debug.Log(obj);

            if (obj != null)
                obj.gameObject.SetActive(true);

            return obj;
        }



    }
}