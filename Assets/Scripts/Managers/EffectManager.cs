using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public GameObject mohoi;
    public GameObject khoiChan;
    public GameObject gemPrefab;
    public Transform player;
     GameObject effectUI;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        effectUI = GameObject.FindGameObjectWithTag("effectUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ParabolicMovement(GameObject go, Vector3 start, Vector3 targetPosition, float duration, float height, TweenCallback OnComplete)
    {
        Vector3[] path = new Vector3[3];
        path[0] = start/*+Vector3.up*100*/;
        Vector3 tempPos = (Vector2)start + UnityEngine.Random.insideUnitCircle * 150;
        //path[1] = (targetPosition + start) / 2 + new Vector3(height, 0, 0);
        path[1] = tempPos;
        path[2] = targetPosition;

        go.GetComponent<RectTransform>().DOPath(path, duration, PathType.CatmullRom, PathMode.Sidescroller2D, 10, Color.red)
            .SetEase(Ease.InQuad)
            .OnComplete(OnComplete);

    }
    public void MoveFromWorkSpaceToUI(Transform startPos, RectTransform targetUI, GameObject obj, TweenCallback complete)
    {


        Vector3 startScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, startPos.position + Vector3.up*3 * PlayerController.Instance.transform.localScale.x);

        Vector3 targetScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetUI.position);
        float travelDistance = Vector3.Distance(startScreenPos, targetScreenPos);
        obj.transform.position = new Vector3(startScreenPos.x, startScreenPos.y, 0);
        ParabolicMovement(obj, startScreenPos, targetUI.position, UnityEngine.Random.Range(1, 1.5f), UnityEngine.Random.Range(-200f, 200f), complete);
        // obj.GetComponent<RectTransform>().DOMove(targetUI.position, travelDistance/speed).SetEase(Ease.InQuad).OnComplete(()=>complete());
    }
   
    public void ScaleBig(GameObject go, float amount, float duration)
    {
        Vector3 originalScale = go.transform.localScale;
        go.transform.DOScale(Vector3.one * amount, duration).SetEase(Ease.OutQuad).OnComplete(() => { go.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutQuad); });
    }

  
    public GameObject TextEffect(string prefabName,string text,Vector3 pos)
    {
        GameObject go = ObjectPooler.instance.SpawnFromPool(prefabName, effectUI.transform);
        TextMeshProUGUI textmesh = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textmesh.text = text;

        Color color = textmesh.color;
        color.a = 1;
        textmesh.color = color;

        // UIPopOut(go.transform);
        Vector2 uiPos = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        go.transform.DOMoveY(uiPos.y + 50, 0.5f).SetEase(Ease.OutQuad);
        textmesh.DOColor(color, 0.4f).SetEase(Ease.OutQuad).OnComplete(() => {
            color.a = 0;
            textmesh.DOColor(color, 0.1f).SetEase(Ease.InQuad);
        });
        go.transform.DOScale(PlayerController.Instance.transform.localScale * 30f, 0.2f).SetEase(Ease.OutQuad).OnComplete(() => {

            go.transform.DOScale(PlayerController.Instance.transform.localScale, 0.5f).SetEase(Ease.OutQuad);
        });
        /* go.transform.localPosition = Vector3.zero;
         UIPopOut(go.transform);
         go.transform.DOLocalMoveY(300, 2);*/
        return go;
    } public void TextWorldEffect(string text,Vector3 pos)
    {
      //  pos = new Vector3(pos.x * Random.Range(-0.5f, 0.5f), pos.y * Random.Range(-0.5f, 0.5f), pos.z * Random.Range(-0.5f, 0.5f));
        GameObject go = ObjectPooler.instance.SpawnFromPool("TextWorld",pos);
        go.transform.localScale = PlayerController.Instance.transform.localScale*0.5f;
        TextMeshPro textmesh = go.transform.GetChild(0).GetComponent<TextMeshPro>();
        textmesh.text = text;
        Color color = textmesh.color;
        color.a = 1;
        textmesh.color = color;
       
       // UIPopOut(go.transform);
        go.transform.DOMoveY(pos.y+1, 0.5f).SetEase(Ease.OutQuad);
        textmesh.DOColor(color, 0.4f).SetEase(Ease.OutQuad).OnComplete(()=> {
            color.a = 0;
            textmesh.DOColor(color, 0.1f).SetEase(Ease.InQuad);
        });
        go.transform.DOScale(PlayerController.Instance.transform.localScale * 1.5f, 0.2f).SetEase(Ease.OutQuad).OnComplete(() => {
            
              go.transform.DOScale(PlayerController.Instance.transform.localScale, 0.5f).SetEase(Ease.OutQuad);
        });
    }
    public void Sweating(Vector3 pos,float scale)
    {
        //Instantiate(mohoi, pos, mohoi.transform.rotation).transform.localScale*=scale;
       // ObjectPooler.instance.SpawnFromPool("Sweat", pos, mohoi.transform.rotation).transform.localScale = scale*Vector3.one;
    }
    public void UIPopOut(Transform tr, float delay = 0)
    {
        tr.transform.localScale = Vector3.zero;
        tr.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).SetDelay(delay);
    }

   
}
