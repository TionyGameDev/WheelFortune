using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Prizes;
using Assets.Scripts.UICoillection;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    [SerializeField]
    private UICollection _uiCollection;
    [SerializeField]
    private Vector2 _position;
    [SerializeField]
    private List<UIElement> _ulElemets = new List<UIElement>();
    public void Test1()
    {
        RubinPrize[] prizes = new RubinPrize[coinsAmount];

        _ulElemets = _uiCollection?.Draw(prizes);
        if (_ulElemets != null)
            InitPosition(_ulElemets);
    }
  

    private void InitPosition(List<UIElement> objects)
    {
        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < objects.Count; i++)
        {
            var obj = objects[i];

            initialPos[i] = obj.rectTransform.anchoredPosition;
            initialRotation[i] = obj.rectTransform.rotation;
        }

        CountCoins();
    }

    private Vector3 RandomVector(Vector3 max) => new Vector3(Random.RandomRange(0, max.x), Random.RandomRange(0, max.y),0);
    public void CountCoins()
    {
        var delay = 0f;

        for (int i = 0; i < _ulElemets.Count; i++)
        {
            var elements = _ulElemets[i];

            elements.transform.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            elements.transform.localPosition += RandomVector(new Vector3(100, 100,100));

            elements.rectTransform.DOAnchorPos(_position, 0.8f).SetDelay(delay + 0.5f).SetEase(Ease.InBack);

            elements.transform.DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            elements.transform.DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;

            counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
        }

        StartCoroutine(CountDollars());
    }

    IEnumerator CountDollars()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerPrefs.SetInt("CountDollar", PlayerPrefs.GetInt("CountDollar") + 50 + PlayerPrefs.GetInt("BPrize"));
        counter.text = PlayerPrefs.GetInt("CountDollar").ToString();
        PlayerPrefs.SetInt("BPrize", 0);
    }
}