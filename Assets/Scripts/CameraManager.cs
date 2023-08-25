using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance;

    public GameObject _sceneCams; // 현재 Scene의 Cam들 [메인 제외]

    [SerializeField] CinemachineBrain _brain;

    [SerializeField] CameraBlendCtrl _ctrl;

    public GameObject _playerFocus;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        
    }
    void FindPlayerFocus()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("CamFocus");
        foreach(GameObject o in obj)
        {
            if(o.activeSelf)
            {
                _playerFocus = o;
                break;
            }
        }
    }
    public void SetFreeLookCam()
    {
        FindPlayerFocus();
        _ctrl.SetFreeLookCam();
    }
    public void SetVirtualCam()
    {
        _ctrl.SetVirtualCam();
    }
    public void AbleCinemachineCams()
    {
        _sceneCams.SetActive(true);
    }
    public void DisableCinemachineCams()
    {
        _sceneCams.SetActive(false);
    }
}
