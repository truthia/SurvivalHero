using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    Stitcher _stitcher;
    GameObject shirt;
    GameObject hair;
    GameObject pant;
    GameObject arm;
    private void Awake()
    {
        _stitcher = new Stitcher();
    }
    public void ChangeArm(GameObject skin, GameObject body)
    {
        if (arm != null) Destroy(arm);
        // transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
     
        this.arm = _stitcher.Stitch(skin, body);

    }
}
