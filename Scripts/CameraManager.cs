using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance;
    [SerializeField] CinemachineBrain _brain;

    [SerializeField] CameraBlendCtrl _ctrl;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        
    }
    public void SetFreeLookCam()
    {
        _ctrl.SetFreeLookCam();
    }
    public void SetVirtualCam()
    {
        _ctrl.SetVirtualCam();
    }
}
