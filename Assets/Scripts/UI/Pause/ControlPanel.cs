using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPanel : MonoBehaviour
{
    public List<TMP_InputField> _normalFieldLst;

    public List<TMP_InputField> _skillFieldLst;

    public TMP_InputField _mouseSenseField;

    public RectTransform _mouseScnseeBar; // 마우스 막대 바

    TMP_InputField _nowFocuseField; // 현재 선택중인 필드

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

        if (!float.TryParse(value, out tempValue)) // value의 값이 숫자가 아니라면 리턴한다.
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
    public void EndEdit(string value) // 조작 입력 끝남
    {
        _nowFocuseField.text = value;

        _nowFocuseField = null;
    }
    public void EndMouseEdit(string value) // 마우스 입력 끝남
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value의 값이 숫자가 아니라면 리턴한다.
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
        // 위의 2개의 for문을 돌았는데 리턴이 되지 않았다면, 마우스 Input이 포커스 된 것
        _nowFocuseField = _mouseSenseField;
    }

}
