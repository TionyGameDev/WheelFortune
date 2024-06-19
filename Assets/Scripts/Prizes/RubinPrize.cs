using Assets.Scripts.UICoillection;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Prizes
{
    public class RubinPrize :  UIElement
    {
        public RubinPrize (string namees)
        {
            transform.name = namees;
            Debug.Log(namees);
        }
    }
}