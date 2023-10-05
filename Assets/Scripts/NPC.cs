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

    [SerializeField] List<QuestData> _questList = new List<QuestData>(); // 해당 NPC가 지니고 있는 퀘스트 목록 => 1. 이렇게 NPC가 퀘스트를 지니고 있는게 맞을까? 2. 아니면, 퀘스트 ID만 지니고, 퀘스트 매니저가 모든 퀘스트를 지니고 있는게 맞을가? => 일단 1로 진행한다.
    [SerializeField] private QuestMarkUI _questMarkUI; // 퀘스트 마크
    [SerializeField] private Button _interactionUI; // 상호작용 UI
    [SerializeField] private TargetUIObj _targetObj;
    private Animator _anim;

    QuestData _quest = null; // 플레이어가 받고 있는 퀘스트, null이면 받고 있지 않음

    GameObject _player = null;
    
    bool _isTalkAble; // 플레이어가 본인의 대화범위에 도달했는지
    bool _activeMark; // 퀘스트 마크가 활성화 되어 있는지 확인

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

        if (CheckQuest()) // 받야아 하는 퀘스트가 있다면s
            _questMarkUI.SetQuestMark(QuestMark.Start); // ! 마크 활성화

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
        if(!_activeMark) // 퀘스트 마크가 꺼져있다면,
        {
            if(CheckQuest()) // 퀘스트가 아직 남아있다면.
            {
                if(_quest == null) // 퀘스트를 받고 있지 않다면,
                {
                    _questMarkUI.SetQuestMark(QuestMark.Start); // 시작 마크 활성화
                    _activeMark = true;
                }
            }
        }

        if (_isTalkAble) // 대화가능하고, 대화중이 아니라면,
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

        // 퀘스트 시작, 진행, 종료에 따라 전달하는 퀘스트 다이얼로그가 다르다.
        // 이건 퀘스트 매니저에 함수로 만드는게 좋을듯? 리턴값이 DialogueData인거지.

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
    void SetQuestMark(QuestMark mark, int id) // 퀘스트매니저로부터 전달받으면,
    {
        if (_npcID != id) return; // 본인의 id와 다르면 리턴.

        _questMarkUI.SetQuestMark(mark);

        if(mark == QuestMark.None) // None을 전달받았으면 => 퀘스트를 완료했으면, 현재 지니고 있는 quest는 null로, 마크활성화여부는false로 전환
        {
            _quest = null;
            _activeMark = false;
        }
    }
    bool CheckQuest()
    {
        foreach (QuestData data in _questList)
        {
            if (!data._isFinish) // 아직 완료하지 못한 퀘스트가 있으면 true
            {
                return true;
            }
        }
        return false; // 다 완료했으면 false
    }

    QuestData GetQuestOrder() // 현재 퀘스트의 차례를 확인 즉, 0부터 끝까지 확인해서, isFinish가 true가 아니라면, 그 친구를 리턴시킴.
    {
        for(int i = 0; i < _questList.Count; i++)
        {
            if (!_questList[i]._isFinish)
                return _questList[i];
        }
        return null; // 전부 완료라면 null 리턴.
    }

    void EndDialogue()
    {
        State = NPCState.Normal;
        _interactionUI.gameObject.SetActive(true);
        
    }
}
