using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Prizes;
using Assets.Scripts.UICoillection;
using Assets.Scripts.UICoillection.Drawer;
using DG.Tweening;
using EasyUI.PickerWheelUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    [SerializeField]
    private PickerWheel _wheel;

    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    [SerializeField]
    private UICollection _uiCollection;
    [SerializeField]
    private Vector2 _position;
    [SerializeField]
    private List<PrizeModel> _ulElemets = new List<PrizeModel>();
    [SerializeField]
    private List<UiDrawer> _uiDrawers = new List<UiDrawer>();

    private void Start()
    {
        onElementComplete += OnComplete;
        _wheel.OnSpinEnd(SpinEnd);

        _ulElemets.Add(new PrizeModel("1"));
        _ulElemets.Add(new PrizeModel("2"));
        _ulElemets.Add(new PrizeModel("3"));
        _ulElemets.Add(new PrizeModel("4"));
        _ulElemets.Add(new PrizeModel("5"));
        _ulElemets.Add(new PrizeModel("6"));
        //_ulElemets.AddRange(new PrizeModel[coinsAmount]);
    }

    private void SpinEnd(WheelPiece arg0)
    {
        Debug.Log("END!");

        Test1();
    }

    private void SpinEnd()
    {
        Debug.Log("SPINEND");
        Test1();
    }


    public void Test1()
    {
        _uiDrawers.Clear();
        if (_uiCollection) 
        {
            _uiDrawers = _uiCollection.Set(_ulElemets);
                if (_uiDrawers != null)
                    InitPosition(_uiDrawers);
        }
    }
  

    private void InitPosition(List<UiDrawer> objects)
    {
        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < objects.Count; i++)
        {
            var obj = objects[i];
            var rect = obj.GetComponent<RectTransform>();

            initialPos[i] = rect.anchoredPosition;
            initialRotation[i] = rect.rotation;
        }

        CountCoins();
    }

    [SerializeField] 
    private Vector3 _randomPosition = new Vector3(100, 100, 100);
    private Vector3 RandomVector(Vector3 max) => new Vector3(Random.RandomRange(0, max.x), Random.RandomRange(0, max.y),0);
    public void CountCoins()
    {
        var delay = 0f;

        for (int i = 0; i < _uiDrawers.Count; i++)
        {
            var elements = _uiDrawers[i];

            elements.transform.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            elements.transform.localPosition += RandomVector(_randomPosition);

            elements.rectTransform.DOAnchorPos(_position, 0.8f).SetDelay(delay + 0.5f).SetEase(Ease.InBack).OnComplete((
                () =>
                {
                    onElementComplete.Invoke();
                }));

            elements.transform.DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            elements.transform.DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;

            //counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
        }

        StartCoroutine(CountDollars());
    }
    private UnityAction onElementComplete ;
    private void OnComplete()
    {
        Debug.Log("OnComplete" );
    }

    IEnumerator CountDollars()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        counter.text = PlayerPrefs.GetInt("CountDollar").ToString();
    }
}