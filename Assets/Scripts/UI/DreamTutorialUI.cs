using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamTutorialUI : MonoBehaviour
{
    enum Buttons
    {
        None = -1,
        Prev,
        Next,
        Close,
    }
    public List<GameObject> _pageLst; // ������ ����Ʈ
    public List<GameObject> _btnLst; // ��ư ����Ʈ [����, ����]

    int _nowPage = 0;

    void Start()
    {
        SetPage();
    }

    void SetPage() // ������ ����
    {
        for(int i = 0; i < _pageLst.Count; i++)
        {
            if (_nowPage == i)
                _pageLst[i].SetActive(true);
            else
            {
                _pageLst[i].SetActive(false);
            }
        }
    }

    void SetButton()
    {
        _btnLst[(int)Buttons.Prev].SetActive(_nowPage > 0); // _nowPage�� 0���� ũ��, ������ư�� Ȱ��ȭ
        _btnLst[(int)Buttons.Next].SetActive(_nowPage < _pageLst.Count - 1); // _nowPage�� �ִ������� ���� ������ ������ư Ȱ��ȭ
    }
    public void ClickBtn(int idx)
    {
        switch(idx)
        {
            case 0: // ����
                _nowPage--;
                SetPage();
                SetButton();
                break;
            case 1: // ����
                _nowPage++;
                SetPage();
                SetButton();
                break;
            case 2: // �ݱ� => PlayScene���� ��ȯ
                gameObject.SetActive(false); // ���� ��
                UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play); // Play�� ��ȯ
                break;
        }
    }
}
