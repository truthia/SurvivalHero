using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  
    [System.Serializable]
   public struct CameraFollow
    {
        public CameraType type;
        public GameObject cameraGO;
    }
    public CameraFollow[] cameraFollows;
    public void ChangeCamera(CameraType _type)
    {
        foreach(CameraFollow cam in cameraFollows)
        {
            cam.cameraGO.SetActive(false);
            if (cam.type == _type) cam.cameraGO.SetActive(true);
        }
    }
}
[System.Serializable]
public enum CameraType
{
    MainCamera,Practise1
}