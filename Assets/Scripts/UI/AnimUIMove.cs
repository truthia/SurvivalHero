using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class AnimUIMove : MonoBehaviour
{
    [SerializeField] Vector2 posFrom;
    [SerializeField] Vector2 posTo;
    [SerializeField] float timeMove;
    [SerializeField] Ease ease = Ease.OutBack;
    [SerializeField] Ease easeClose=Ease.InBack;
    RectTransform rect;
    [SerializeField] bool isNotAuto;
    public UnityEvent startAnim;
    public UnityEvent completeAnim;
    public GameObject parent;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (!isNotAuto)
        {
           // SoundManager.instance.PlaySound("Open");
            startAnim?.Invoke();
            DOTween.Kill(rect);
            rect.anchoredPosition = posFrom;
            rect.DOAnchorPos(posTo, timeMove).SetEase(ease).SetUpdate(true).OnComplete(delegate {
                completeAnim?.Invoke();
            });
        }
    }
    public void Open()
    {
        DOTween.Kill(rect);
        rect.anchoredPosition = posFrom;
        rect.DOAnchorPos(posTo, timeMove).SetEase(ease).SetUpdate(true);
    }
    public void Close()
    {
        DOTween.Kill(rect);
        rect.anchoredPosition = posTo;
        rect.DOAnchorPos(posFrom, timeMove).SetEase(easeClose).SetUpdate(true).OnComplete(()=> {
            parent.SetActive(false);
        });
      //  SoundManager.instance.PlaySound("Close");
    }
}
