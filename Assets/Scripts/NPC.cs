using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public enum NPCState
    {
        None = -1,
        Normal,
        Talk,
    }

    public NPCState State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch(_state)
            {
                case NPCState.Normal:
                    _anim.CrossFade("Idle", 0.1f);
                    break;

                case NPCState.Talk:
                    _anim.CrossFade("Talk", 0.1f);
                    break;
            }
        }
    }

    [SerializeField] NPCState _state = NPCState.Normal;

    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // �ش� NPC�� ���ϰ� �ִ� ����Ʈ ��� => 1. �̷��� NPC�� ����Ʈ�� ���ϰ� �ִ°� ������? 2. �ƴϸ�, ����Ʈ ID�� ���ϰ�, ����Ʈ �Ŵ����� ��� ����Ʈ�� ���ϰ� �ִ°� ������? => �ϴ� 1�� �����Ѵ�.
    [SerializeField] private QuestMarkUI _questMarkUI; // ����Ʈ ��ũ
    [SerializeField] private Button _interactionUI; // ��ȣ�ۿ� UI
    [SerializeField] private TargetUIObj _targetObj;
    private Animator _anim;

    QuestData _quest = null; // �÷��̾ �ް� �ִ� ����Ʈ, null�̸� �ް� ���� ����

    GameObject _player = null;
    
    bool _isTalkAble; // �÷��̾ ������ ��ȭ������ �����ߴ���
    bool _activeMark; // ����Ʈ ��ũ�� Ȱ��ȭ �Ǿ� �ִ��� Ȯ��

    [SerializeField] int _npcID;
    [SerializeField] bool _isSpace = false;
    //[SerializeField] bool _simpleSpace = false;
    void Start()
    {
        DialogueManager._instance._npcEvt -= EndDialogue;
        DialogueManager._instance._npcEvt += EndDialogue;

        QuestManager._instance._questMarkEvt -= SetQuestMark;
        QuestManager._instance._questMarkEvt += SetQuestMark;

        _anim = GetComponent<Animator>();

        _interactionUI.onClick.AddListener(StartTalk);

        if (CheckQuest()) // �޾߾� �ϴ� ����Ʈ�� �ִٸ�s
            _questMarkUI.SetQuestMark(QuestMark.Start); // ! ��ũ Ȱ��ȭ

        _targetObj.Init(_npcID);
    }
    void Update()
    {
        switch(State)
        {
            case NPCState.Normal:
                UpdateNormal();
                break;
            case NPCState.Talk:
                UpdateTalk();
                break;
        }
    }
    void UpdateNormal()
    {
        if(!_activeMark) // ����Ʈ ��ũ�� �����ִٸ�,
        {
            if(CheckQuest()) // ����Ʈ�� ���� �����ִٸ�.
            {
                if(_quest == null) // ����Ʈ�� �ް� ���� �ʴٸ�,
                {
                    _questMarkUI.SetQuestMark(QuestMark.Start); // ���� ��ũ Ȱ��ȭ
                    _activeMark = true;
                }
            }
        }

        if (_isTalkAble) // ��ȭ�����ϰ�, ��ȭ���� �ƴ϶��,
        {
            _isSpace = Input.GetKeyDown(KeyCode.Space);
            //_simpleSpace = SimpleInput.GetButtonDown("Space");
            
            if (_isSpace)// || _simpleSpace)
            {
                StartTalk();
            }
        }
    }
    
    void UpdateTalk()
    {
        
    }

    void StartTalk()
    {
        _quest = null;
        DialogueData data = null;

        _quest = GetQuestOrder();

        if (_quest == null) return;

        // ����Ʈ ����, ����, ���ῡ ���� �����ϴ� ����Ʈ ���̾�αװ� �ٸ���.
        // �̰� ����Ʈ �Ŵ����� �Լ��� ����°� ������? ���ϰ��� DialogueData�ΰ���.

        if (!_quest._isStart)
            data = _quest._dialogueData[(int)DialogueType.QuestStart];
        else if (!_quest._isAchieve)
            data = _quest._dialogueData[(int)DialogueType.QuestProgress];
        else if (_quest._isAchieve)
            data = _quest._dialogueData[(int)DialogueType.QuestEnd];
        else
            data = _quest._dialogueData[(int)DialogueType.Normal];

        DialogueManager._instance.GetQuestDialogue(_quest, data);

        SoundManager._instance.PlayDialogueSound();

        transform.LookAt(_player.transform);

        Input.ResetInputAxes();

        State = NPCState.Talk;

        _interactionUI.gameObject.SetActive(false);
        CameraManager._instance.StartTalkCam(_player, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTalkAble = true;
            _player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTalkAble = false;
            _player = null;
        }
    }
    void SetQuestMark(QuestMark mark, int id) // ����Ʈ�Ŵ����κ��� ���޹�����,
    {
        if (_npcID != id) return; // ������ id�� �ٸ��� ����.

        _questMarkUI.SetQuestMark(mark);

        if(mark == QuestMark.None) // None�� ���޹޾����� => ����Ʈ�� �Ϸ�������, ���� ���ϰ� �ִ� quest�� null��, ��ũȰ��ȭ���δ�false�� ��ȯ
        {
            _quest = null;
            _activeMark = false;
        }
    }
    bool CheckQuest()
    {
        foreach (QuestData data in _questList)
        {
            if (!data._isFinish) // ���� �Ϸ����� ���� ����Ʈ�� ������ true
            {
                return true;
            }
        }
        return false; // �� �Ϸ������� false
    }

    QuestData GetQuestOrder() // ���� ����Ʈ�� ���ʸ� Ȯ�� ��, 0���� ������ Ȯ���ؼ�, isFinish�� true�� �ƴ϶��, �� ģ���� ���Ͻ�Ŵ.
    {
        for(int i = 0; i < _questList.Count; i++)
        {
            if (!_questList[i]._isFinish)
                return _questList[i];
        }
        return null; // ���� �Ϸ��� null ����.
    }

    void EndDialogue()
    {
        State = NPCState.Normal;
        _interactionUI.gameObject.SetActive(true);
        
    }
}
