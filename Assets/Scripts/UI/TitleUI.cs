using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public enum TitleState
    {
        None = -1,
        GameStart, // ����� ������ ���
        Option, // �ɼ�
        End, // ���â
    }
    public TitleState _stateUI;

    [SerializeField] List<GameObject> _popupUI;

    void Start()
    {

    }

    void Update()
    {

    }
    public void ClickBtn(int idx) // ��ư�� ������ idx�� �޴µ�, idx�� �ش��ϴ� PopupUI�� �����ش�. 
    {
        UIManager._instacne.AllClosePopupUI(); // �޴���ư�� �ƹ���ư(���� �簳, �ɼ�, ���� ��)�� ������, �����ִ� ��� PopupUI�� �ݾ��ش�. => ������ �ϴ� �˾�UI�� ������ ����
        SoundManager._instance.PlayUISound();

        for (int i = 0; i < _popupUI.Count; i++)
        {
            _stateUI = (TitleState)i;
            if (idx == i)
            {
                if(_stateUI == TitleState.GameStart)
                {
                    // ���� ����� ������ �ִ��� �Ǵ��ϰ�, ������ ��� UI ȣ�� => ����� �ִ°����� �Ǵ��Ѵ�. KGC
                }
                _popupUI[i].SetActive(true);
                _popupUI[i].transform.DOScale(0.85f, 0.3f).SetUpdate(true); // ��Ʈ�� ���� => �������� 0.85�� ����
                UIManager._instacne.SetPopupUI(_popupUI[i]); // UIManager���� �� PopupUI�� �������ش�.
            }
            else
            {
                _popupUI[i].SetActive(false); // i��°�� �ƴ� PopupUI�� ���� ��Ȱ��ȭ ���ش�.
            }
        }
    }
}
