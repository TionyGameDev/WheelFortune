using Assets.Scripts.Prizes;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC
{
    public class WheelFortunaController : BaseController<WheelFortunaViewer>
    {

        [ContextMenu("TEST")]
        public void Test ()
        {
            if (viewer != null)
            {
                viewer.OnShow();
            }
        }

        private RubinPrize InstantiatePiece()
        {
            return null;
            //return Instantiate(wheelPiecePrefab, wheelPiecesParent.position, Vector3.back, pieceAngle * index);
        }
    }
}