using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTutorialUI : MonoBehaviour
{
    enum Buttons
    {
        None = -1,
        Prev,
        Next,
        Close,
    }
    public List<GameObject> _pageLst; // 페이지 리스트
    public List<GameObject> _btnLst; // 버튼 리스트 [이전, 다음]

    int _nowPage = 0;

    void Start()
    {
        UISetting();
    }
    
    void UISetting()
    {
        SetPage();
        SetButton();
    }
    void SetPage() // 페이지 세팅
    {
        for (int i = 0; i < _pageLst.Count; i++)
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
        _btnLst[(int)Buttons.Prev].SetActive(_nowPage > 0); // _nowPage가 0보다 크면, 이전버튼은 활성화
        _btnLst[(int)Buttons.Next].SetActive(_nowPage < _pageLst.Count - 1); // _nowPage가 최대페이지 보다 작으면 다음버튼 활성화
    }

    public void ClickBtn(int idx)
    {
        SoundManager._instance.PlayUISound();
        switch (idx)
        {
            case 0: // 이전
                _nowPage--;
                UISetting();
                break;
            case 1: // 다음
                _nowPage++;
                UISetting();
                break;
            case 2: // 닫기 => PlayScene으로 전환
                gameObject.SetActive(false); // 닫은 후
                UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play); // Play로 전환
                break;
        }
    }
}
