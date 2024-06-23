using UnityEngine;

namespace Assets.Scripts.UICoillection
{
    public interface IUIDrawer<T>
    {
        void Set(T target);
    }
    
    public abstract class UiDrawer : UIElement
    {
        public object target { get; protected set; }
        
        public void Set(object target)
        {
            if (this.target == target) return;

            this.target = target;

            if (this.target != null)
                Draw(target);
            
        }               
        protected abstract void Draw(object target);
    }
}