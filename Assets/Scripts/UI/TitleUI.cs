using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public enum TitleState
    {
        None = -1,
        GameStart, // 저장된 데이터 경고
        Option, // 옵션
        End, // 경고창
    }
    public TitleState _stateUI;

    [SerializeField] List<GameObject> _popupUI;

    void Start()
    {

    }

    void Update()
    {

    }
    public void ClickBtn(int idx) // 버튼을 누르면 idx를 받는데, idx에 해당하는 PopupUI를 열어준다. 
    {
        UIManager._instacne.AllClosePopupUI(); // 메뉴버튼의 아무버튼(게임 재개, 옵션, 도감 등)을 누르면, 열려있던 모든 PopupUI는 닫아준다. => 열려고 하는 팝업UI만 열도록 만듬
        SoundManager._instance.PlayUISound();

        for (int i = 0; i < _popupUI.Count; i++)
        {
            _stateUI = (TitleState)i;
            if (idx == i)
            {
                if(_stateUI == TitleState.GameStart)
                {
                    // 대충 저장된 데이터 있는지 판단하고, 있으면 경고 UI 호출 => 현재는 있는것으로 판단한다. KGC
                }
                _popupUI[i].SetActive(true);
                _popupUI[i].transform.DOScale(0.85f, 0.3f).SetUpdate(true); // 닷트윈 실행 => 스케일을 0.85로 만듬
                UIManager._instacne.SetPopupUI(_popupUI[i]); // UIManager에게 연 PopupUI를 전달해준다.
            }
            else
            {
                _popupUI[i].SetActive(false); // i번째가 아닌 PopupUI는 전부 비활성화 해준다.
            }
        }
    }
}
