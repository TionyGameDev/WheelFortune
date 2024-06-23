using EasyUI.PickerWheelUI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Wheel
{
    public class WheelStateUI : BaseUIState<WheelState>
    {
        public override WheelState GetState()
        {
            return currentValue;
        }

        public override void Init(WheelState state)
        {
            currentValue = state;
            onChangeValue?.Invoke(currentValue);
        }

        public override void SetState(WheelState state)
        {
            if (currentValue == state)
                return;

            Debug.Log(currentValue.ToString());
            currentValue = state;

            onChangeValue?.Invoke(currentValue);
        }
    }
}