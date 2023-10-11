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

    //public List<RectTransform> _soundBarLst; // �Ҹ��� Value���븦 ����Ʈ�� ����

    public List<TMP_InputField> _soundValueLst; // �Ҹ��� ��ġ �ʵ带 ����Ʈ�� ����

    public GameObject _warningUI; // ��������� �����ϴµ� �������� �ʰ� ���� �� ȣ���ϴ� UI

    TMP_InputField _nowFocuseField; // ���� �������� �ʵ�
    Slider _nowFocuseSlider;
    //RectTransform _nowFocuseBar; // ���� �������� ��

    [SerializeField] List<SoundFieldData> _fieldData = new List<SoundFieldData>(); 

    private void OnEnable() // ������ ��, ���� ����Ǿ� �ִ� ��ġ���� ���� �����Ѵ�.
    {
        _warningUI.transform.localScale = Vector3.one; // ��, warning�� �ݱ⸦ ������, �ٽ� Ű��, Scale�� 0���� �Ǿ� �־�, �ɼ�UI�� Enable�� ��, �������� 1�� �ٲٵ��� ����.
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
    public void SetValue(string value) // ��ġ�� ���� => InputField���� ��ġ�� �Է��� ��,
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value�� ���� ���ڰ� �ƴ϶�� �����Ѵ�.
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
    
    public void EndEdit(string value) // �Է� ���� => InputField���� ��ġ �Է��� ���� ��,
    {
        float tempValue = 0;

        if (!float.TryParse(value, out tempValue)) // value�� ���� ���ڰ� �ƴ϶�� �����Ѵ�. => �Ŀ� ��� UI ȣ��? KGC
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

    void FindFieldFocuse() // ��Ŀ���� �ʵ尡 �������� Ȯ���ϴ� �Լ�.
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
    public void ClickApplyBtn() // �ۿ� ��ư
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.ClosePopupUI();
    }

    public void ClickCancelBtn() // �ݱ� ��ư
    {
        SoundManager._instance.PlayUISound();
        if (CheckChangeValue()) // ����� ������ �����Ѵٸ�,
        {
            _warningUI.SetActive(true);
            UIManager._instacne.SetPopupUI(_warningUI);
        }
        else // ���ٸ�,
            UIManager._instacne.ClosePopupUI();
    }
    public void ClickWarningAgreeBtn() // ����� ������ �����Ѵ�.
    {
        SoundManager._instance.PlayUISound();
        UIManager._instacne.AllClosePopupUI(); // ��� PopupUI �ݱ�
    }
    public void ClickWarningCancelBtn() // ����� ������ �������� �ʰ�, �ݱ��ư�� ������.
    {
        for (int i = 0; i < _soundValueLst.Count; i++) // ����Ʈ�� ���� ������ �ʱ�ȭ �����ش�.
        {
            _soundValueLst[i].text = _fieldData[i]._prevValue.ToString();
            _soundSliderLst[i].value = _fieldData[i]._prevValue / 100f;
        }

        SoundManager._instance.PlayUISound();

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

        _fieldData.Clear(); // ������ FieldData ����Ʈ�� �ʱ�ȭ �����ش�.
    }
}
