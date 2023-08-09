using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamPlayUI : MonoBehaviour
{
    enum PlayStateUI
    {
        None = -1,
        Normal,
        Battle,
        Max,
    }

    PlayStateUI _state = PlayStateUI.None;

    public List<GameObject> _sceneUILst;
    void Start()
    {
        _state = PlayStateUI.Normal;

        UIManager._instacne._playStateEvt -= SetBattleState;
        UIManager._instacne._playStateEvt += SetBattleState;
    }
    void SetBattleState(GameManager.PlayState playState)
    {
        switch(playState)
        {
            case GameManager.PlayState.Dream_Normal:
                _state = PlayStateUI.Normal;
                break;
            case GameManager.PlayState.Dream_Battle:
                _state = PlayStateUI.Battle;
                break;
        }

        ActiveUI();
    }
    void ActiveUI()
    {
        for(int i = 0; i < _sceneUILst.Count; i++)
        {
            if (i == (int)_state)
                _sceneUILst[i].SetActive(true);
            else
                _sceneUILst[i].SetActive(false);
        }
    }
    private void OnDestroy()
    {
        UIManager._instacne._playStateEvt -= SetBattleState;
    }
}
