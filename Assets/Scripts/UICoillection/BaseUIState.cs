using EasyUI.PickerWheelUI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Wheel
{
    public abstract class BaseUIState<T> : MonoBehaviour, IBaseUIState<T> where T : Enum
    {
        public T currentValue;
        public Action<T> onChangeValue;

        public abstract void Init(T state);

        public abstract void SetState(T state);
        
        public abstract T GetState();
            
        

    }    
}