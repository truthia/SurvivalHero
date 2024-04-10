using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float time = .5f;


    private void OnEnable()
    {
        Invoke(nameof(Delay), time);
    }
    private void Delay()
    {
        System.Action onComplete = Complete;
        onComplete.Invoke();
    }

    public void Complete()
    {
        if (this.gameObject.activeSelf)
            gameObject.SetActive(false);
    }

}
