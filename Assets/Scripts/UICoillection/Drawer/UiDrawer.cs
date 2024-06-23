using UnityEngine;

namespace Assets.Scripts.UICoillection.Drawer
{
    public abstract class UiDrawer<T> : UiDrawer, IUIDrawer<T> where T : class
    {
        void IUIDrawer<T>.Set(T target)
        {
            Set(target);
        }

        sealed protected override void Draw(object target)
        {
            if (target is T cast)
                DrawModel(cast);
        }

        protected abstract void DrawModel(T target);
    }
}