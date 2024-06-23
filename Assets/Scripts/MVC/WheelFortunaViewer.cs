using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC
{
    public class WheelFortunaViewer : BaseViewer<WheelFortunaController>
    {
        private void Awake()
        {
            SetController(new WheelFortunaController());
        }

        public override void OnShow()
        {

        }
    }
}