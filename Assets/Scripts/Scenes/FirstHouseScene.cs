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

    public void StartFirstScene() // �ó׸ӽ� ������ �ش� �޼ҵ� ȣ�� => DOTween�� OnComplete�� ���� ��
    {
        StartCoroutine(StartCo());
        
    }
    IEnumerator StartCo() // 1�� ��� ��, ī�޶� ��ȯ
    {
        yield return new WaitForSeconds(1f);

        PlayerManager._instance._nowPlayer.SetActive(true);

        CameraManager._instance.ChangeCam(CameraType.PlayerCam);

        yield return new WaitForSeconds(2f); // 2��(�� Cam���� PlayerCam���� ��ȯ�ð�)��, Ʃ�丮�� Ȱ��ȭ �� ��ȭ ����

        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Tutorial);

        DialogueManager._instance.GetQuestDialogue(_data, _data._dialogueData[0]);
    }
    void Update()
    {

        // �ش� Scene�� ���� ����Ʈ�� ������, portal�� Ȱ��ȭ ��Ű�� �ϱ�.
    }
}
