using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;

    public DialogueData _nowData; // ���� ��ȭ ������

    public Action<DialogueData> _dialogueEvt = null;
    public Action _npcEvt = null; // ��ȭ���� NPC���� �˷��ش�.
    public Action _clickNext = null; // ���� ��� ���
    public Action _camEvt = null; // �ó׸ӽ� �̺�Ʈ

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
        if (_nowData == null) return; // ���� ��ϵ� ��ȭ�� ������ �ٷ� ����

        if(_clickNext != null && _nowData._isStart)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_nowData._isFinish)
            {
                SoundManager._instance.PlayDialogueSound();
                _clickNext.Invoke();
            }
        }
    }
    
    public void GetQuestDialogue(QuestData quest, DialogueData data) // ����Ʈ�� �ް�, �� ����Ʈ�� ���¿� ���� �ٸ� ��縦 �޴´�.
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
                yield return new WaitForSeconds(5f); // ī�޶� �̵� �ð�
                break;
            case CameraType.Event_1_Cam:
                CameraManager._instance.StartEvent_1_Cam();
                yield return new WaitForSeconds(2f); // ī�޶� �̵� �ð�
                break;
            case CameraType.Event_2_Cam:
                CameraManager._instance.StartEvent_2_Cam();
                yield return new WaitForSeconds(2.5f); // ī�޶� �̵� �ð�
                break;
            case CameraType.Event_3_Cam:
                CameraManager._instance.StartEvent_3_Cam();
                yield return new WaitForSeconds(3.0f); // ī�޶� �̵� �ð�
                break;
            case CameraType.Event_4_Cam:
                CameraManager._instance.StartEvent_4_Cam();
                yield return new WaitForSeconds(7f); // ī�޶� �̵� �ð�
                break;
            case CameraType.Boss_Cam:
                CameraManager._instance.StartBossCam();
                yield return new WaitForSeconds(5f); // ī�޶� �̵� �ð�
                break;
        }
        
        dialoguePanel.gameObject.SetActive(true); // ���̾�α� ����

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

        TutorialManager._instance.EnableObject(_tutorialType); // Ʃ�丮�� => ���� ����

        yield return null; //new WaitForSeconds(1f); // ���� Ȯ�� �ð�

        dialoguePanel.gameObject.SetActive(true); // ���̾�α� ����
        _clickNext.Invoke();
    }

    public void EndDialogue() // ������ ��簡 ��µǰ� ��ȭ Ÿ�Կ� ���� ����Ʈ �������� �������� ������.
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
        _npcEvt?.Invoke(); // NPC���� ��ȭ�� �����ٰ� �˸�.
    }
}
