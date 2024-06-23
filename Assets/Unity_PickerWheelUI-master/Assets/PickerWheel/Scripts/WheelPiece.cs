using UnityEngine;

namespace EasyUI.PickerWheelUI
{
    [System.Serializable]
    public class WheelPiece
    {
        public string icon;
        private int _amount;
        public int amount
        {
            get { return _amount; }
            set {_amount = value; } 
        }
    }
}
