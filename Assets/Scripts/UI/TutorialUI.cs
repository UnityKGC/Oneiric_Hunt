using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    // 현재 Scene에 따라서, 해당하는 튜토리얼 UI를 활성화 시키자.

    enum SceneType
    {
        None = -1,
        Real,
        Dream,
        MiniGame,
        Chase,
    }

    [SerializeField] SceneType _sceneType = SceneType.None;

    public List<GameObject> _tutorialUILst;
    
    void Start()
    {
        SetTutorial();
    }
    void OnEnable() // 켜질때 세팅함
    {
        
    }

    void SetTutorial()
    {
        for(int i = 0; i < _tutorialUILst.Count; i++)
        {
            if(i == (int)_sceneType)
                _tutorialUILst[i].SetActive(true);
            else
                _tutorialUILst[i].SetActive(false);
        }
    }
}
