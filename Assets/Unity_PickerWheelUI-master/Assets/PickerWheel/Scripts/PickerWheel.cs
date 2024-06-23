using Assets.Scripts.Prizes;
using Assets.Scripts.Wheel;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace EasyUI.PickerWheelUI
{

    public enum WheelState
    {
        Cooldowm,
        Active,
        DrawPrizes
    }
    public class PickerWheel : MonoBehaviour
    {
        [SerializeField]
        private WheelStateUI _currentState;

        [SerializeField]
        private Button _buttonSpin;

        [SerializeField] private Transform PickerWheelTransform;
        [SerializeField] private Transform wheelCircle;
        [SerializeField] private RubinPrize wheelPiecePrefab;
        [SerializeField] private Transform wheelPiecesParent;
        [SerializeField] private Image _mainItems;
        [SerializeField] private int _cooldown;
        public int _cooldownNumber;
        [SerializeField] private int spinDuration = 8;


        [SerializeField]
        private List<WheelPiece> wheelPieces = new List<WheelPiece>();

        [SerializeField]
        private List<Sprite> _icons;

        private UnityAction onSpinStartEvent;
        public UnityAction<WheelPiece> onSpinEndEvent;
        [SerializeField]
        private bool _isSpinning = false;

        public bool IsSpinning { get { return _isSpinning; } }


        private Vector2 pieceMinSize = new Vector2(81f, 146f);
        private Vector2 pieceMaxSize = new Vector2(144f, 213f);
        private int piecesMin = 2;
        private int piecesMax = 12;

        private float pieceAngle;
        private float halfPieceAngle;
        private float halfPieceAngleWithPaddings;


        private double accumulatedWeight;

        private void Start()
        {
            wheelPieces.Clear();
            wheelPieces.AddRange(new WheelPiece[piecesMax]);

            pieceAngle = 360 / wheelPieces.Count;
            halfPieceAngle = pieceAngle / 2f;
            halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 4f);
            _cooldownNumber = _cooldown;

            if (_currentState)
            {
                _currentState.onChangeValue += OnSwitchState;
                _currentState.Init(WheelState.Cooldowm);               
            }

            Generate();

            _buttonSpin.onClick.AddListener(Spin);
        }

        public void OnSwitchState(WheelState state)
        {
            Debug.Log("STATE" + state);
            switch (state)
            {
                case WheelState.Active:
                    WheelActive();
                    break;
                case WheelState.Cooldowm:
                    Cooldown();
                    break;
                case WheelState.DrawPrizes:
                    DrawPrizes();
                    break;
            }
        }

        private void DrawPrizes()
        {
            _isSpinning = false;
            onSpinEndEvent.Invoke(_piece);
        }

        [ContextMenu("DRAW")]
        public async void Cooldown()
        {
            Debug.Log("Cooldoown");
            await Task.Delay(1000);
            DrawNumber();

            if (_cooldownNumber > 0)
            {
                Debug.Log("Cooldoo - 1");
                _cooldownNumber -= 1;
                Cooldown();
            }
            else
            {
                Debug.Log("ELSE");
                _cooldownNumber = _cooldown;
                _currentState.SetState(WheelState.Active);
            }
        }

        private void WheelActive()
        {
            Debug.Log("WheelActive");
            //Spin();
        }

        private void DrawNumber()
        {
            Debug.Log(wheelPieces.Count + " wheelPieces");
            for (int i = 0; i < _prizes.Count; i++)
            {
                var pieces = _prizes[i];               
                pieces.amount = UnityEngine.Random.Range(0, 100);
                _prizes[i].Draw(pieces.amount.ToString());
            }
            if (_mainItems)
                _mainItems.sprite = _icons[UnityEngine.Random.Range(0,_icons.Count - 1)];
        }

        private async void Generate()
        {
            for (int i = 0; i < wheelPieces.Count; i++)
                DrawPiece(i);

            await Task.Delay(500);
            DrawNumber();
        }

        [SerializeField]
        private List<RubinPrize> _prizes = new List<RubinPrize>();
        private void DrawPiece(int index)
        {
            RubinPrize pieceTrns = InstantiatePiece();

            if (pieceTrns)
            {
                pieceTrns.transform.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index);
                _prizes.Add(pieceTrns);
            }
        }

        private RubinPrize InstantiatePiece()
        {
            return Instantiate(wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent);
        }

        WheelPiece _piece;

        public void Spin()
        {
            Debug.Log(_currentState.GetState().ToString());
            if (!_isSpinning && _currentState.GetState() == WheelState.Active)
            {
                _isSpinning = true;

                if (onSpinStartEvent != null)
                    onSpinStartEvent.Invoke();

                int index = GetRandomPieceIndex();
                _piece  = wheelPieces[index];

                index = UnityEngine.Random.Range(0, wheelPieces.Count);
                _piece = wheelPieces[index];


                float angle = -(pieceAngle * index);

                float rightOffset = (angle - halfPieceAngleWithPaddings) % 360;
                float leftOffset = (angle + halfPieceAngleWithPaddings) % 360;

                float randomAngle = UnityEngine.Random.Range(leftOffset, rightOffset);

                Vector3 targetRotation = Vector3.back * (randomAngle + 2 * 360 * spinDuration);

                //float prevAngle = wheelCircle.eulerAngles.z + halfPieceAngle ;
                float prevAngle, currentAngle;
                prevAngle = currentAngle = wheelCircle.eulerAngles.z;

                bool isIndicatorOnTheLine = false;

                wheelCircle
                .DORotate(targetRotation, spinDuration, RotateMode.Fast)
                .SetEase(Ease.InOutQuart)
                .OnUpdate(() => {
                    float diff = Mathf.Abs(prevAngle - currentAngle);
                    if (diff >= halfPieceAngle)
                    {
                        if (isIndicatorOnTheLine)
                        {
                            // audioSource.PlayOneShot (audioSource.clip) ;
                        }
                        prevAngle = currentAngle;
                        isIndicatorOnTheLine = !isIndicatorOnTheLine;
                    }
                    currentAngle = wheelCircle.eulerAngles.z;
                })
                .OnComplete(() => {
                    _currentState.SetState(WheelState.DrawPrizes);
                });

            }
        }
        public void OnSpinStart(UnityAction action)
        {
            onSpinStartEvent = action;
        }

        public void OnSpinEnd(UnityAction<WheelPiece> action)
        {
            onSpinEndEvent += action;
        }


        private int GetRandomPieceIndex()
        {
            int v = UnityEngine.Random.RandomRange(0, piecesMax - 1);
            return v;

            //double r = rand.NextDouble();

            //for (int i = 0; i < wheelPieces.Count; i++)
                //if (wheelPieces[i]._weight >= r)
                    //return i;

        }

        private void CalculateWeightsAndIndices()
        {
            for (int i = 0; i < wheelPieces.Count; i++)
            {
                WheelPiece piece = wheelPieces[i];

                //add weights:
                //'piece._weight = accumulatedWeight;

                //add index :
                //piece.Index = i;

                //save non zero chance indices:
            }
        }        
    }
}