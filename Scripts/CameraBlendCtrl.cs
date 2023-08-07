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
        _fCam.MoveToTopOfPrioritySubqueue();
    }
    public void SetVirtualCam()
    {
        _vCam.MoveToTopOfPrioritySubqueue();
    }
}
