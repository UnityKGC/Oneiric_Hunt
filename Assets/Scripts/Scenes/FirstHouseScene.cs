using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FirstHouseScene : MonoBehaviour
{
    public Material _skybox;
    [SerializeField] QuestData _data;
    void Start()
    {
        RenderSettings.ambientIntensity = 1.2f;
        RenderSettings.skybox = _skybox;
        RenderSettings.customReflection = null; // Reset any custom reflection probes
        DynamicGI.UpdateEnvironment();

        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.FirstHouseScene;
        CameraManager._instance.ChangeCam(CameraType.ViewCam);

        SoundManager._instance.PlayBGM(BGM.PlayerHouse);

        PlayerManager._instance._nowPlayer.SetActive(false);
    }

    public void StartFirstScene() // 시네머신 끝나면 해당 메소드 호출 => DOTween의 OnComplete에 참조 중
    {
        StartCoroutine(StartCo());
        
    }
    IEnumerator StartCo() // 1초 대기 후, 카메라 전환
    {
        yield return new WaitForSeconds(1f);

        PlayerManager._instance._nowPlayer.SetActive(true);

        CameraManager._instance.ChangeCam(CameraType.PlayerCam);

        yield return new WaitForSeconds(2f); // 2초(뷰 Cam에서 PlayerCam으로 전환시간)후, 튜토리얼 활성화 및 대화 시작

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Tutorial);

        DialogueManager._instance.GetQuestDialogue(_data, _data._dialogueData[0]);
    }
    void Update()
    {

        // 해당 Scene의 메인 퀘스트가 끝나면, portal을 활성화 시키게 하기.
    }
}
