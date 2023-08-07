using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public enum PauseMenuState
    {
        None = -1,
        Ency, // ����
        Option, // �ɼ�
        Lobby, // ���â
        End, // ���â
    }

    [SerializeField] List<GameObject> _popupUI;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickResume() // ���� �簳�� ȥ�� Popup�� ���°� �ƴϹǷ� ���� ����
    {
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play);
    }
    public void ClickBtn(int idx) // ��ư�� ������ idx�� �޴µ�, idx�� �ش��ϴ� PopupUI�� �����ش�. 
    {
        UIManager._instacne.AllClosePopupUI(); // �޴���ư�� �ƹ���ư(���� �簳, �ɼ�, ���� ��)�� ������, �����ִ� ��� PopupUI�� �ݾ��ش�. => ������ �ϴ� �˾�UI�� ������ ����

        for (int i = 0; i < _popupUI.Count; i++)
        {
            if(idx == i)
            {
                _popupUI[i].SetActive(true);
                UIManager._instacne.SetPopupUI(_popupUI[i]); // UIManager���� �� PopupUI�� �������ش�.
            }  
            else
            {
                _popupUI[i].SetActive(false); // i��°�� �ƴ� PopupUI�� ���� ��Ȱ��ȭ ���ش�.
            }
        }
    }
    public void ClickEncy()
    {
        // ���� UI ȣ��

    }
    public void ClickOption()
    {
        // �ɼ� UI ȣ��
    }
    public void ClickLobby()
    {
        // ���� �κ�� ���ư��ðڽ��ϱ�? UI ȣ��
    }
    public void ClickEnd()
    {
        // ���� ������ �����Ͻðڽ��ϱ�? UI ȣ��
    }
}
