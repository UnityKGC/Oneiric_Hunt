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

    [SerializeField] Image _area; // ī�޶� ȸ���� ����, ȭ�� ��ü�� ���� �̹��� UI => �̰� ������ ���� UI�� ������ �ȵ�.

    [SerializeField] Vector2 _currentMousePos;
    [SerializeField] Vector2 _prevMousePos;
    [SerializeField] Vector2 _nowDir;

    private float _xSpd = 150f;
    private float _ySpd = 1.5f;

    private bool _isDrag = false;
    private void Start()
    {
        _freeLook = CameraManager._instance._playerCam;

        _freeLook.m_XAxis.m_InputAxisName = ""; // ����� ȯ�濡���� FreeLook ī�޶� ��ġ�θ� ������ �� �ְ� Name�� �������� ����.
        _freeLook.m_YAxis.m_InputAxisName = "";
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDrag) return;

        Vector2 currentMousePos = eventData.position;
        Vector2 nowDir = (currentMousePos - _prevMousePos).normalized;

        _freeLook.m_XAxis.Value += nowDir.x * _xSpd * Time.deltaTime;
        _freeLook.m_YAxis.Value -= nowDir.y * _ySpd * Time.deltaTime;

        _prevMousePos = currentMousePos;
        /*
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            _nowDir = ((Vector2)Input.mousePosition - _prevMousePos).normalized;

            _freeLook.m_XAxis.Value += _nowDir.x * _xSpd * Time.deltaTime;
            _freeLook.m_YAxis.Value -= _nowDir.y * _ySpd * Time.deltaTime;

            _prevMousePos = Input.mousePosition;
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _prevMousePos = eventData.position;
        _isDrag = true;

        /*
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            _prevMousePos = Input.mousePosition;
            _isDrag = true;
        }*/
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDrag = false;
        /*
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area.rectTransform, eventData.position, eventData.enterEventCamera, out Vector2 posOut))
        {
            _isDrag = false;
        }*/
    }
}
