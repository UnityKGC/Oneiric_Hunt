using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStartObject : MonoBehaviour
{
    [SerializeField] QuestData _data;

    [SerializeField] GameObject _city, _japan;

    [SerializeField] Cinemachine.CinemachineVirtualCamera _viewCam;
    private Cinemachine.LensSettings _playerCamSetting;

    private Cinemachine.LensSettings _viewCamSetting;
    private float _time;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DialogueManager._instance.GetQuestDialogue(_data, _data._dialogueData[0]);
            if (gameObject.CompareTag("Tutorial_1"))
            {
                GameManager._instance.FirstTuto = true;
            }
            else if (gameObject.CompareTag("Tutorial_2"))
            {
                GameManager._instance.SecondTuto = true;
            }

            if(gameObject.CompareTag("ClearFog"))
            {
                GameManager._instance._isLastQuest = true;
                RenderSettings.fog = false;
                _city.SetActive(false);
                _japan.SetActive(true);

                _playerCamSetting = CameraManager._instance._playerCam.m_Lens;
                _viewCamSetting = _viewCam.m_Lens;
                StartCoroutine(StartFarClipPlaneCo());
            }
            else
                gameObject.SetActive(false);
        }
    }
    IEnumerator StartFarClipPlaneCo()
    {
        float t = 0f;
        while (t < 1f)
        {
            _time += Time.deltaTime;
            t = _time / 5f;

            _viewCamSetting.FarClipPlane = Mathf.Lerp(10f, 500f, t);

            yield return new WaitForEndOfFrame();
        }
        _playerCamSetting.FarClipPlane = 1000f;
        gameObject.SetActive(false);
    }
}
