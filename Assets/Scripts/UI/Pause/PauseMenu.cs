using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
    public RectTransform _pausePanel; // DOTween ȿ���� �ֱ����� ���
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

    void OnEnable()
    {
        _pausePanel.DOMoveX(0, 0.3f).SetUpdate(true);
    }
    private void OnDisable()
    {
        _pausePanel.DOMoveX(-500, 0f).SetUpdate(true);
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
                _popupUI[i].transform.DOScale(1, 0.3f).SetUpdate(true); // ��Ʈ�� ���� => �������� 1�� ����
                UIManager._instacne.SetPopupUI(_popupUI[i]); // UIManager���� �� PopupUI�� �������ش�.
            }  
            else
            {
                _popupUI[i].SetActive(false); // i��°�� �ƴ� PopupUI�� ���� ��Ȱ��ȭ ���ش�.
                _popupUI[i].transform.DOScale(0, 0f).SetUpdate(true); // ��Ʈ�� ���� => �������� 1�� ����
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
