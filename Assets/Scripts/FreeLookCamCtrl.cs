using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SimpleInputNamespace;
using UnityEngine.UI;

public class FreeLookCamCtrl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] CinemachineFreeLook _freeLook;

    [SerializeField] Joystick _joyStick;

    [SerializeField] Image _area; // 카메라 회전을 위해, 화면 전체를 덮는 이미지 UI => 이것 때문에 월드 UI가 조작이 안됨.
    [SerializeField] Rect _rect;
    [SerializeField] Vector2 _mousePos;

    [SerializeField] string _xString, _yString;

    [SerializeField] Vector2 _startPos;

    [SerializeField] LayerMask _mask = 1 << 15;

    [SerializeField] Ray ray;

    [SerializeField] RaycastHit[] hits;
    private void Start()
    {
        _xString = "Mouse X";
        _yString = "Mouse Y";

        _freeLook = CameraManager._instance._playerCam;
        _rect = new Rect(0, 0, Screen.width, Screen.height);
    }
    private void Update()
    {
        _mousePos = Input.mousePosition;

        if (!_rect.Contains(_mousePos))
        {
            _freeLook.m_XAxis.m_InputAxisName = _xString;
            _freeLook.m_YAxis.m_InputAxisName = _yString;

        }
        else if(_rect.Contains(_mousePos))
        {
            _freeLook.m_XAxis.m_InputAxisName = ""; // For example, disable X-axis rotation
            _freeLook.m_YAxis.m_InputAxisName = "";

            _freeLook.m_XAxis.m_InputAxisValue = 0f;
            _freeLook.m_YAxis.m_InputAxisValue = 0f;
        }

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 offset = currentMousePos - _startPos;

            _freeLook.m_XAxis.Value += offset.x * 1.5f * Time.deltaTime;
            _freeLook.m_YAxis.Value -= offset.y * 0.01f * Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            _startPos = Input.mousePosition;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            _freeLook.m_XAxis.m_InputAxisName = ""; // For example, disable X-axis rotation
            _freeLook.m_YAxis.m_InputAxisName = "";

            _startPos = Vector2.zero;
        }
    }
}
