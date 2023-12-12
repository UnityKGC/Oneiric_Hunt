using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDreamScene : MonoBehaviour
{
    [SerializeField] QuestData _questData;

    [SerializeField] Material _skybox;
    public Light _light;

    [SerializeField] private GameObject _exitPortal;
    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.FirstDreamScene;

        _light.color = new Color(0.32f, 0.67f, 1f); // 여기서 1f가 최대 즉, 255이므로, RGB값/255를 해야 한다.
        
        RenderSettings.fog = true;

        RenderSettings.skybox = _skybox;
        RenderSettings.customReflection = null; // Reset any custom reflection probes
        DynamicGI.UpdateEnvironment();

        SoundManager._instance.PlayBGM(BGM.TutorialDream);

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Play);

        CameraManager._instance.ChangeCam(CameraType.Event_0_Cam);

        Invoke("StartQuest", 1f);
    }
    
    void StartQuest()
    {
        DialogueManager._instance.GetQuestDialogue(_questData, _questData._dialogueData[0]);
    }
    public void EnablePortal(SceneManagerEX.PortalType portalType)
    {
        switch(portalType)
        {
            case SceneManagerEX.PortalType.ExitPortal:
                _exitPortal.SetActive(true);
                break;
        }
    }
    void StartBattle(bool isLastBattle) // Trigger를 받았으니, 전투 시작이다! 라고 전달만 해줌
    {
        BattleManager._instance.StartBattle(isLastBattle);
    }
}
