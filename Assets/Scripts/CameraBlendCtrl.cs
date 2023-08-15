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
        if (_fCam.Follow == null || _fCam.LookAt == null)
            SetPlayerFocus();

        _fCam.MoveToTopOfPrioritySubqueue();
    }
    void SetPlayerFocus()
    {
        _fCam.Follow = GameManager._instance.PlayerFocus;
        _fCam.LookAt = GameManager._instance.PlayerFocus;
    }
    public void SetVirtualCam()
    {
        _vCam.MoveToTopOfPrioritySubqueue();
    }
}
