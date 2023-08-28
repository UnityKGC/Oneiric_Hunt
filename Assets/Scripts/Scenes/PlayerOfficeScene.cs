using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOfficeScene : MonoBehaviour
{
    [SerializeField]
    QuestData _quest;

    
    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.PlayerOfficeScene;

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play);
        Invoke("StartQuest", 1f);
    }

    void StartQuest()
    {
        QuestManager._instance.StartQuest(_quest);
    }
    void Update()
    {
        
    }
}
