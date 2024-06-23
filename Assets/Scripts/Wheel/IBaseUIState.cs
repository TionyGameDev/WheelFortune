using System;

namespace Assets.Scripts.Wheel
{
    public interface IBaseUIState<T>
    {
        void Init(T state);
        void SetState(T state);
    }
}