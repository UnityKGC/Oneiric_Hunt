using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundFieldData
{
    public float _prevValue;
    public float _nextValue;
    public SoundFieldData(float prev, float next)
    {
        _prevValue = prev;
        _nextValue = next;
    }
}
public class SoundOption : MonoBehaviour
{
    public enum FocusSoundField
    {
        None = -1,
        All,
        Background,
        Effect,
    }

    public FocusSoundField _focusField;

    public List<GameObject> _soundUILst;

    public List<RectTransform> _soundBarLst; // �Ҹ��� Value���븦 ����Ʈ�� ����

    public List<TMP_InputField> _soundValueLst; // �Ҹ��� ��ġ �ʵ带 ����Ʈ�� ����

    public GameObject _warningUI; // ��������� �����ϴµ� �������� �ʰ� ���� �� ȣ���ϴ� UI

    TMP_InputField _nowFocuseField; // ���� �������� �ʵ�
    RectTransform _nowFocuseBar; // ���� �������� ��

    List<SoundFieldData> _fieldData = new List<SoundFieldData>(); 

    float _maxWidth = 800f; // ������ �ִ�ũ��

    private void OnEnable() // ������ ��, ���� ����Ǿ� �ִ� ��ġ���� ���� �����Ѵ�.
    {

        for (int i = 0; i < _soundValueLst.Count; i++)
        {
            float value = float.Parse(_soundValueLst[i].text);

            RectTransform temp = _soundBarLst[i];
            Vector2 tempVec = temp.sizeDelta;
            float v = value / 100f;
            tempVec.x = _maxWidth * v;
            _soundBarLst[i].sizeDelta = tempVec;

            _fieldData.Add(new SoundFieldData(value, value));
        }
    }
    public void SetValue(string value) // ��ġ�� ����
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value�� ���� ���ڰ� �ƴ϶�� �����Ѵ�.
            return;

        if (tempValue >= 100)
            tempValue = 100;

        if (_nowFocuseField != null && _nowFocuseBar != null)
        {
            _nowFocuseField.text = tempValue.ToString();

            _fieldData[(int)_focusField]._nextValue = tempValue;

            Vector2 tempVec = _nowFocuseBar.sizeDelta;
            float v = tempValue / 100f;
            tempVec.x = _maxWidth * v;
            _nowFocuseBar.sizeDelta = tempVec;
        }
        else
            FindFieldFocuse();
    }
    
    public void EndEdit(string value) // �Է� ����
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value�� ���� ���ڰ� �ƴ϶�� �����Ѵ�. => �Ŀ� ��� UI ȣ��? KGC
            return;

        if (tempValue >= 100)
            tempValue = 100;

        if (_nowFocuseField != null && _nowFocuseBar != null)
        {
            _nowFocuseField.text = tempValue.ToString();

            _fieldData[(int)_focusField]._nextValue = tempValue;

            Vector2 tempVec = _nowFocuseBar.sizeDelta;
            float v = tempValue / 100f;
            tempVec.x = _maxWidth * v;
            _nowFocuseBar.sizeDelta = tempVec;
        }

        _nowFocuseField = null;
    }

    void FindFieldFocuse() // ��Ŀ���� �ʵ尡 �������� Ȯ���ϴ� �Լ�.
    {
        for (int i = 0; i < _soundValueLst.Count; i++)
        {
            if (_soundValueLst[i].isFocused)
            {
                _focusField = (FocusSoundField)i;
                _nowFocuseField = _soundValueLst[i];
                _nowFocuseBar = _soundBarLst[i];
                break;
            }
        }
    }
    public void ClickApplyBtn() // �ۿ� ��ư
    {
        // �Ҹ� ũ�� ���� => ���� �Ŵ���?
        UIManager._instacne.ClosePopupUI();
    }

    public void ClickCancelBtn() // �ݱ� ��ư
    {
        if(CheckChangeValue()) // ����� ������ �����Ѵٸ�,
        {
            _warningUI.SetActive(true);
            UIManager._instacne.SetPopupUI(_warningUI);
        }
        else // ���ٸ�,
            UIManager._instacne.ClosePopupUI();
    }
    public void ClickWarningAgreeBtn() // ����� ������ �����Ѵ�.
    {

        UIManager._instacne.AllClosePopupUI(); // ��� PopupUI �ݱ�
    }
    public void ClickWarningCancelBtn() // ����� ������ �������� �ʰ�, �ݱ��ư�� ������.
    {
        for (int i = 0; i < _soundValueLst.Count; i++) // ����Ʈ�� ���� ������ �ʱ�ȭ �����ش�.
        {
            _soundValueLst[i].text = _fieldData[i]._prevValue.ToString();
        }

        UIManager._instacne.AllClosePopupUI(); // ��� PopupUI �ݱ�
    }
    bool CheckChangeValue() // ���� ����Ǿ����� �Ǵ��ϴ� �Լ�
    {
        for(int i = 0; i < _soundValueLst.Count; i++)
        {
            SoundFieldData temp = _fieldData[i];
            if (temp._prevValue != temp._nextValue)
                return true;
        }
        return false;
    }
    private void OnDisable()
    {
        _fieldData.Clear(); // ������ FieldData ����Ʈ�� �ʱ�ȭ �����ش�.
    }
}
