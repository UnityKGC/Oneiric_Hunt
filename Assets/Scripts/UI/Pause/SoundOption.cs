using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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

    public List<Slider> _soundSliderLst;

    //public List<RectTransform> _soundBarLst; // 소리의 Value막대를 리스트로 지님

    public List<TMP_InputField> _soundValueLst; // 소리의 수치 필드를 리스트로 지님

    public GameObject _warningUI; // 변경사항이 존재하는데 적용하지 않고 닫을 시 호출하는 UI

    TMP_InputField _nowFocuseField; // 현재 선택중인 필드
    Slider _nowFocuseSlider;
    //RectTransform _nowFocuseBar; // 현재 선택중인 바

    [SerializeField] List<SoundFieldData> _fieldData = new List<SoundFieldData>(); 

    private void OnEnable() // 시작할 때, 현재 저장되어 있는 수치들을 전부 저장한다.
    {
        _warningUI.transform.localScale = Vector3.one; // 그, warning이 닫기를 누르고, 다시 키면, Scale이 0으로 되어 있어, 옵션UI가 Enable할 때, 스케일을 1로 바꾸도록 구현.
        for (int i = 0; i < _soundValueLst.Count; i++)
        {
            
            float value = float.Parse(_soundValueLst[i].text);
            //_soundSliderLst[i].value = value / 100;

            //float value = float.Parse(_soundValueLst[i].text);

            //RectTransform temp = _soundBarLst[i];
            //Vector2 tempVec = temp.sizeDelta;
            //float v = value / 100f;
            //tempVec.x = _maxWidth * v;
            //_soundBarLst[i].sizeDelta = tempVec;

            _fieldData.Add(new SoundFieldData(value, value));
        }
    }
    public void SetValue(string value) // 수치를 조절 => InputField에서 수치를 입력할 때,
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value의 값이 숫자가 아니라면 리턴한다.
            return;

        if (tempValue >= 100)
            tempValue = 100;

        if (_nowFocuseField != null && _nowFocuseSlider != null)
        {
            _nowFocuseField.text = tempValue.ToString();

            _fieldData[(int)_focusField]._nextValue = tempValue;

            _nowFocuseSlider.value = tempValue / 100;

            /*
            Vector2 tempVec = _nowFocuseBar.sizeDelta;
            float v = tempValue / 100f;
            tempVec.x = _maxWidth * v;
            _nowFocuseBar.sizeDelta = tempVec;*/
        }
        else
            FindFieldFocuse();
    }
    
    public void EndEdit(string value) // 입력 끝남 => InputField에서 수치 입력을 끝낼 때,
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value의 값이 숫자가 아니라면 리턴한다. => 후에 경고 UI 호출? KGC
            return;

        if (tempValue >= 100)
            tempValue = 100;

        if (_nowFocuseField != null && _nowFocuseSlider != null)
        {
            _nowFocuseField.text = tempValue.ToString();

            _fieldData[(int)_focusField]._nextValue = tempValue;

            _nowFocuseSlider.value = tempValue / 100;

            /*
            Vector2 tempVec = _nowFocuseBar.sizeDelta;
            float v = tempValue / 100f;
            tempVec.x = _maxWidth * v;
            _nowFocuseBar.sizeDelta = tempVec;*/
        }

        _nowFocuseField = null;
    }

    void FindFieldFocuse() // 포커스된 필드가 무엇인지 확인하는 함수.
    {
        for (int i = 0; i < _soundValueLst.Count; i++)
        {
            if (_soundValueLst[i].isFocused)
            {
                _focusField = (FocusSoundField)i;
                _nowFocuseField = _soundValueLst[i];
                _nowFocuseSlider = _soundSliderLst[i];
                //_nowFocuseBar = _soundBarLst[i];
                break;
            }
        }
    }
    public void ClickApplyBtn() // 작용 버튼
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI();
    }

    public void ClickCancelBtn() // 닫기 버튼
    {
        SoundManager._instance.PlayUISound();
        if (CheckChangeValue()) // 변경된 사항이 존재한다면,
        {
            _warningUI.SetActive(true);
            UIManager._instacne.SetPopupUI(_warningUI);
        }
        else // 없다면,
            UIManager._instacne.ClosePopupUI();
    }
    public void ClickWarningAgreeBtn() // 변경된 사항을 적용한다.
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.AllClosePopupUI(); // 모든 PopupUI 닫기
    }
    public void ClickWarningCancelBtn() // 변경된 사항을 적용하지 않고, 닫기버튼을 누른다.
    {
        for (int i = 0; i < _soundValueLst.Count; i++) // 리스트를 이전 값으로 초기화 시켜준다.
        {
            _soundValueLst[i].text = _fieldData[i]._prevValue.ToString();
            _soundSliderLst[i].value = _fieldData[i]._prevValue / 100f;
        }

        SoundManager._instance.PlayUISound();

        UIManager._instacne.AllClosePopupUI(); // 모든 PopupUI 닫기
    }
    bool CheckChangeValue() // 값이 변경되었는지 판단하는 함수
    {
        for(int i = 0; i < _soundValueLst.Count; i++)
        {
            SoundFieldData temp = _fieldData[i];
            if (temp._prevValue != temp._nextValue)
                return true;
        }
        return false;
    }

    public void AllSoundSliderValue(float value)
    {
        float tempValue = value * 100f;
        _soundValueLst[0].text = ((int)tempValue).ToString();
        _fieldData[0]._nextValue = (int)tempValue;
    }

    public void BackgroundSoundSliderValue(float value)
    {
        float tempValue = value * 100f;
        _soundValueLst[1].text = ((int)tempValue).ToString();
        _fieldData[1]._nextValue = (int)tempValue;
    }

    public void EffectSoundSliderValue(float value)
    {
        float tempValue = value * 100f;
        _soundValueLst[2].text = ((int)tempValue).ToString();
        _fieldData[2]._nextValue = (int)tempValue;
    }

    private void OnDisable()
    {
        AudioListener.volume = float.Parse(_soundValueLst[0].text) / 100f;
        SoundManager._instance.BGMVolume = float.Parse(_soundValueLst[1].text) / 100f;
        SoundManager._instance.EffectVolume = float.Parse(_soundValueLst[2].text) / 100f;

        _fieldData.Clear(); // 꺼질때 FieldData 리스트는 초기화 시켜준다.
    }
}
