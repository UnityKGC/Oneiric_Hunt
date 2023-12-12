using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;

    public DialogueData _nowData; // 현재 대화 데이터

    public Action<DialogueData> _dialogueEvt = null;
    public Action _npcEvt = null; // 대화중인 NPC에게 알려준다.
    public Action _clickNext = null; // 다음 대사 출력
    public Action _camEvt = null; // 시네머신 이벤트

    public bool _isTalk = false;

    public float _typingSpeed = 0.05f;

    TutorialType _tutorialType = TutorialType.None;

    QuestData _quest;
    void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        if (_nowData == null) return; // 현재 등록된 대화가 없으면 바로 리턴

        if(_clickNext != null && _nowData._isStart)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_nowData._isFinish)
            {
                SoundManager._instance.PlayDialogueSound();
                _clickNext.Invoke();
            }
        }
    }
    
    public void GetQuestDialogue(QuestData quest, DialogueData data) // 퀘스트를 받고, 그 퀘스트의 상태에 따라 다른 대사를 받는다.
    {
        _isTalk = true;
        _quest = quest;
        _nowData = data;

        _dialogueEvt?.Invoke(data);
    }

    public void CamEvent(GameObject dialoguePanel)
    {
        if (!_nowData._isCamEvt) return;

        dialoguePanel.gameObject.SetActive(false);
        StartCoroutine(CamEventCo(dialoguePanel));
    }

    IEnumerator CamEventCo(GameObject dialoguePanel)
    {
        switch (_nowData._camType)
        {
            case CameraType.Event_0_Cam:
                CameraManager._instance.StartEvent_0_Cam();
                yield return new WaitForSeconds(5f); // 카메라 이동 시간
                break;
            case CameraType.Event_1_Cam:
                CameraManager._instance.StartEvent_1_Cam();
                yield return new WaitForSeconds(2f); // 카메라 이동 시간
                break;
            case CameraType.Event_2_Cam:
                CameraManager._instance.StartEvent_2_Cam();
                yield return new WaitForSeconds(2.5f); // 카메라 이동 시간
                break;
            case CameraType.Event_3_Cam:
                CameraManager._instance.StartEvent_3_Cam();
                yield return new WaitForSeconds(3.0f); // 카메라 이동 시간
                break;
            case CameraType.Event_4_Cam:
                CameraManager._instance.StartEvent_4_Cam();
                yield return new WaitForSeconds(7f); // 카메라 이동 시간
                break;
            case CameraType.Boss_Cam:
                CameraManager._instance.StartBossCam();
                yield return new WaitForSeconds(5f); // 카메라 이동 시간
                break;
        }
        
        dialoguePanel.gameObject.SetActive(true); // 다이얼로그 진행

        _clickNext.Invoke();
    }

    public void MonsterSpawnEvent(GameObject dialoguePanel)
    {
        dialoguePanel.gameObject.SetActive(false);
        StartCoroutine(MonsterSpawnEventCo(dialoguePanel));
    }

    IEnumerator MonsterSpawnEventCo(GameObject dialoguePanel)
    {
        switch (_nowData._tutorialType)
        {
            case TutorialType.Event_1:
                _tutorialType = TutorialType.Event_1;
                break;
            case TutorialType.Event_2:
                _tutorialType = TutorialType.Event_2;
                break;
            case TutorialType.Event_3:
                _tutorialType = TutorialType.Event_3;
                break;
            case TutorialType.BossEvt:
                _tutorialType = TutorialType.BossEvt;
                break;
        }

        TutorialManager._instance.EnableObject(_tutorialType); // 튜토리얼 => 몬스터 생성

        yield return null; //new WaitForSeconds(1f); // 몬스터 확인 시간

        dialoguePanel.gameObject.SetActive(true); // 다이얼로그 진행
        _clickNext.Invoke();
    }

    public void EndDialogue() // 마지막 대사가 출력되고 대화 타입에 따라 퀘스트 시작인지 끝인지를 전달함.
    {
        switch (_nowData._dialogueType)
        {
            case DialogueType.QuestStart:
                QuestManager._instance.StartQuest(_quest);
                break;

            case DialogueType.QuestProgress:
                _nowData._isStart = _nowData._isFinish = false;
                _nowData._index = 0;
                break;

            case DialogueType.QuestEnd:
                _quest._isFinish = true;
                QuestManager._instance.FinishQuest(_quest);
                break;
        }
        _isTalk = false;
        _nowData = null;
        Input.ResetInputAxes();
        CameraManager._instance.ChangeCam(CameraType.PlayerCam);
        _npcEvt?.Invoke(); // NPC에게 대화가 끝났다고 알림.
    }
}
