using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPanel : MonoBehaviour
{
    public List<TMP_InputField> _normalFieldLst;

    public List<TMP_InputField> _skillFieldLst;

    public TMP_InputField _mouseSenseField;

    public RectTransform _mouseScnseeBar; // ���콺 ���� ��

    TMP_InputField _nowFocuseField; // ���� �������� �ʵ�

    float _maxWidth = 800f;
    public void SetField(string value)
    {
        if (_nowFocuseField != null)
            _nowFocuseField.text = value;
        else
            FindFieldFocuse();
    }
    
    public void SetMouseField(string value)
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value�� ���� ���ڰ� �ƴ϶�� �����Ѵ�.
            return;

        if (tempValue >= 100)
            tempValue = 100;

        if (_nowFocuseField != null && _mouseScnseeBar != null)
        {
            _nowFocuseField.text = tempValue.ToString();

            Vector2 tempVec = _mouseScnseeBar.sizeDelta;
            float v = tempValue / 100f;
            tempVec.x = _maxWidth * v;
            _mouseScnseeBar.sizeDelta = tempVec;
        }
        else
            FindFieldFocuse();
    }
    public void EndEdit(string value) // ���� �Է� ����
    {
        _nowFocuseField.text = value;

        _nowFocuseField = null;
    }
    public void EndMouseEdit(string value) // ���콺 �Է� ����
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value�� ���� ���ڰ� �ƴ϶�� �����Ѵ�.
            return;

        if (tempValue >= 100)
            tempValue = 100;

        if (_nowFocuseField != null && _mouseScnseeBar != null)
        {
            _nowFocuseField.text = tempValue.ToString();

            Vector2 tempVec = _mouseScnseeBar.sizeDelta;
            float v = tempValue / 100f;
            tempVec.x = _maxWidth * v;
            _mouseScnseeBar.sizeDelta = tempVec;
        }

        _nowFocuseField = null;
    }
    void FindFieldFocuse()
    {
        for (int i = 0; i < _normalFieldLst.Count; i++)
        {
            if (_normalFieldLst[i].isFocused)
            {
                _nowFocuseField = _normalFieldLst[i];
                return;
            }
        }

        for(int i = 0; i < _skillFieldLst.Count; i++)
        {
            if (_skillFieldLst[i].isFocused)
            {
                _nowFocuseField = _skillFieldLst[i];
                return;
            }
        }
        // ���� 2���� for���� ���Ҵµ� ������ ���� �ʾҴٸ�, ���콺 Input�� ��Ŀ�� �� ��
        _nowFocuseField = _mouseSenseField;
    }

}
