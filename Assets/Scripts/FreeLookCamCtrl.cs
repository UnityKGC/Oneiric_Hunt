using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SimpleInputNamespace;
public class FreeLookCamCtrl : MonoBehaviour
{
    CinemachineFreeLook _freeLook;
    [SerializeField] Joystick _joyStick;
    [SerializeField] bool _isSetting = false;

    [SerializeField] float _saveXAxis;
    [SerializeField] float _saveYAxis;
    private void Start()
    {
        _freeLook = GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {
        if (_joyStick._isUsing)
        {
            if (!_isSetting)
            {
                _saveXAxis = _freeLook.m_XAxis.Value;
                _saveYAxis = _freeLook.m_YAxis.Value;

                _isSetting = true;
            }
            
            _freeLook.m_XAxis.Value = _saveXAxis;
            _freeLook.m_YAxis.Value = _saveYAxis;
        }
        else
        {
            _isSetting = false;
        }
    }
}
