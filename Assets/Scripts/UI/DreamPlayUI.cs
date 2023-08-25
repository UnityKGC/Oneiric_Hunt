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
        Dialogue,
        Max,
    }

    PlayStateUI _state = PlayStateUI.None;

    public List<GameObject> _sceneUILst;

    void Start()
    {
        UIManager._instacne._playStateEvt -= SetPlayState;
        UIManager._instacne._playStateEvt += SetPlayState;
    }
    void SetPlayState(GameManager.PlayState playState)
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
        UIManager._instacne._playStateEvt -= SetPlayState;
    }

    
}
