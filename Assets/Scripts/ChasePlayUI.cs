using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayUI : MonoBehaviour
{
    enum ChasePlayState
    {
        None = -1,
        Normal,
        QTE,
        Catch,
    }

    ChasePlayState _state = ChasePlayState.None;

    public List<GameObject> _chasePlayUILst;

    void Start()
    {
        _state = ChasePlayState.Normal;

        UIManager._instacne._playStateEvt -= SetChaseUIState;
        UIManager._instacne._playStateEvt += SetChaseUIState;
    }

    void SetChaseUIState(GameManager.PlayState playState)
    {
        switch (playState)
        {
            case GameManager.PlayState.Real_Normal:
                _state = ChasePlayState.Normal;
                break;
            case GameManager.PlayState.Real_QTE:
                _state = ChasePlayState.QTE;
                break;
            case GameManager.PlayState.Real_Catch:
                _state = ChasePlayState.Catch;
                break;
        }

        ActiveUI();
    }
    void ActiveUI()
    {
        for (int i = 0; i < _chasePlayUILst.Count; i++)
        {
            if (i == (int)_state)
                _chasePlayUILst[i].SetActive(true);
            else
                _chasePlayUILst[i].SetActive(false);
        }
    }
    private void OnDestroy()
    {
        UIManager._instacne._playStateEvt -= SetChaseUIState;
    }
}
