using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] List<GameObject> _dataLst; // �ι� ������ ����Ʈ

    [SerializeField] List<GameObject> _buttons;

    int _nowCount = 0; // ���� dataList���� �� ��° data�� ���� �ִ���
    void OnEnable()
    {
        ChangeData();
    }

    public void ClickBtn(int value)
    {
        _nowCount += value;
        ChangeData();
    }
    public void ClickCloseBtn()
    {
        UIManager._instacne.ClosePopupUI();
    }
    void ChangeData()
    {
        for (int i = 0; i < _dataLst.Count; i++)
        {
            if (i == _nowCount)
                _dataLst[_nowCount].SetActive(true);
            else
                _dataLst[i].SetActive(false);
        }

        CheckCount();
    }
    void CheckCount()
    {
        if (_nowCount > 0 && _nowCount < _dataLst.Count - 1)
        {
            _buttons[0].SetActive(true);
            _buttons[1].SetActive(true);
        }
        else if(_nowCount <= 0)
        {
            _buttons[0].SetActive(false);
            _buttons[1].SetActive(true);
        }
        else if(_nowCount >= _dataLst.Count - 1)
        {
            _buttons[0].SetActive(true);
            _buttons[1].SetActive(false);
        }
    }
    private void OnDestroy()
    {
        
    }
}
