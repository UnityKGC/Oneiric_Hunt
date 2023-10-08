using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SimpleInputNamespace;
using UnityEngine.UI;

public enum TestEnum
{
    None = -1,
    Assign,
    TargetWorldUp,
    NoRoll,
    Target,
    WorldSpace,
    FollowWorldUp,

}
public class FreeLookCamCtrl : MonoBehaviour//, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] CinemachineFreeLook _freeLook;

    [SerializeField] Joystick _joyStick;

    [SerializeField] Image _area;
    [SerializeField] Rect _rect;
    [SerializeField] Vector2 _mousePos;
    [SerializeField] Image _joyStickArea;

    [SerializeField] float _saveXAxis;
    [SerializeField] float _saveYAxis;

    [SerializeField] TestEnum _type;

    [SerializeField] string _xString, _yString;

    Vector2 _startPos;
    bool _isDrag;
    Vector2 _lastDragPos;
    private void Start()
    {
        _xString = "Mouse X";
        _yString = "Mouse Y";

        RectTransform parentRectTransform = _joyStickArea.GetComponentInParent<RectTransform>();


        // Create a Rect using the position and size of the RectTransform
        _rect = parentRectTransform.rect;
        /*
        _rect = new Rect(parentRectTransform.position.x, parentRectTransform.position.y,
                             parentRectTransform.rect.width, parentRectTransform.rect.height);*/
    }
    private void Update()
    {
        Debug.Log("Xvalue : " + _freeLook.m_XAxis.Value);
        // Check if the mouse is inside the disableCameraArea
        if (!_rect.Contains(Input.mousePosition) && Joystick._isUsing)
        {
            Debug.Log("움직이지 않아");
            // Disable the FreeLook camera components (Or set the enabled property of the components you want to disable)
            _freeLook.m_XAxis.m_InputAxisValue = Input.GetAxis("Horizontal");
            _freeLook.m_YAxis.m_InputAxisValue = Input.GetAxis("Vertical");
        }
        else if(_rect.Contains(Input.mousePosition) && Joystick._isUsing)
        {
            // Enable the FreeLook camera components
            _freeLook.m_XAxis.m_InputAxisValue = 0; // For example, disable X-axis rotation
            _freeLook.m_YAxis.m_InputAxisValue = 0;
        }
    }
    /*
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            if(_isDrag)
            {
                Debug.Log("Drag");
                Vector2 _lastDragPos = _startPos - posOut;

                _freeLook.m_XAxis.Value += _lastDragPos.x * 0.01f;
                _freeLook.m_YAxis.Value += _lastDragPos.y * 1f;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //_freeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetOnAssign;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            Debug.Log("Down");
            _isDrag = true;
            _startPos = posOut;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        _isDrag = false;
        _freeLook.m_XAxis.m_MaxSpeed = 2f;
        _freeLook.m_YAxis.m_MaxSpeed = 0.01f;
        
    }
    */
    private void OnGUI()
    {
        //GUI.Box(_rect, "응애");
        /*
        if (GUI.Button(new Rect(0, 0, 300, 100), "Assign"))
        {
            _freeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetOnAssign;
        }
        if (GUI.Button(new Rect(0, 100, 300, 100), "TargetWorldUp"))
        {
            _freeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
        }
        if (GUI.Button(new Rect(0, 200, 300, 100), "NoRoll"))
        {
            _freeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetNoRoll;
        }
        if (GUI.Button(new Rect(0, 300, 300, 100), "Target"))
        {
            _freeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
        }
        if (GUI.Button(new Rect(0, 400, 300, 100), "WorldSpace"))
        {
            _freeLook.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        }
        if (GUI.Button(new Rect(0, 500, 300, 100), "FollowWorldUp"))
        {
            _freeLook.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        }*/
    }
}
