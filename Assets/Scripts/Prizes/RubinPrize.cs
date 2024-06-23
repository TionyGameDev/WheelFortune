using Assets.Scripts.UICoillection.Drawer;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Prizes
{
    public class RubinPrize : UiDrawer<PrizeModel>
    {
        public int amount;
        [SerializeField]
        private TextMeshProUGUI _text;
        protected override void DrawModel(PrizeModel target)
        {
            if (_text)
                _text.text = target.name;
            Debug.Log(target.name);
        }

        public void Draw(string count)
        {
            if (_text)
                _text.text = amount.ToString();
        }
    }
}