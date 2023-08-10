using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
    public RectTransform _pausePanel; // DOTween 효과를 주기위해 사용
    public enum PauseMenuState
    {
        None = -1,
        Ency, // 도감
        Option, // 옵션
        Lobby, // 경고창
        End, // 경고창
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

    public void ClickResume() // 게임 재개는 혼자 Popup을 여는게 아니므로 따로 제작
    {
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play);
    }
    public void ClickBtn(int idx) // 버튼을 누르면 idx를 받는데, idx에 해당하는 PopupUI를 열어준다. 
    {
        UIManager._instacne.AllClosePopupUI(); // 메뉴버튼의 아무버튼(게임 재개, 옵션, 도감 등)을 누르면, 열려있던 모든 PopupUI는 닫아준다. => 열려고 하는 팝업UI만 열도록 만듬

        for (int i = 0; i < _popupUI.Count; i++)
        {
            if(idx == i)
            {
                _popupUI[i].SetActive(true);
                _popupUI[i].transform.DOScale(1, 0.3f).SetUpdate(true); // 닷트윈 실행 => 스케일을 1로 만듬
                UIManager._instacne.SetPopupUI(_popupUI[i]); // UIManager에게 연 PopupUI를 전달해준다.
            }  
            else
            {
                _popupUI[i].SetActive(false); // i번째가 아닌 PopupUI는 전부 비활성화 해준다.
                _popupUI[i].transform.DOScale(0, 0f).SetUpdate(true); // 닷트윈 실행 => 스케일을 1로 만듬
            }
        }
    }
    public void ClickEncy()
    {
        // 도감 UI 호출

    }
    public void ClickOption()
    {
        // 옵션 UI 호출
    }
    public void ClickLobby()
    {
        // 정말 로비로 돌아가시겠습니까? UI 호출
    }
    public void ClickEnd()
    {
        // 정말 게임을 종료하시겠습니까? UI 호출
    }
}
