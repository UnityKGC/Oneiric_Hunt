using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    // ���� Scene�� ����, �ش��ϴ� Ʃ�丮�� UI�� Ȱ��ȭ ��Ű��.

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
    void OnEnable() // ������ ������
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
