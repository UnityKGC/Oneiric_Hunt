using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlendCtrl : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _fCam;
    [SerializeField] CinemachineVirtualCamera _vCam;
    
    public void SetFreeLookCam()
    {
        SetPlayerFocus();

        _fCam.MoveToTopOfPrioritySubqueue();
    }
    void SetPlayerFocus()
    {
        _fCam.Follow = CameraManager._instance._playerFocus.transform;
        _fCam.LookAt = CameraManager._instance._playerFocus.transform;
    }
    public void SetVirtualCam()
    {
        _vCam.MoveToTopOfPrioritySubqueue();
    }
}
