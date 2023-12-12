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

        _light.color = new Color(0.32f, 0.67f, 1f); // ���⼭ 1f�� �ִ� ��, 255�̹Ƿ�, RGB��/255�� �ؾ� �Ѵ�.
        
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
    void StartBattle(bool isLastBattle) // Trigger�� �޾�����, ���� �����̴�! ��� ���޸� ����
    {
        BattleManager._instance.StartBattle(isLastBattle);
    }
}
