using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None = -1,
    NPC,
    Clue,
    Object,
}

public class Object : MonoBehaviour
{
    [SerializeField] private QuestMarkUI _questMarkUI; // 퀘스트 이펙트
    [SerializeField] private TargetUIObj _targetObj;
    public int _objID; // 본인의 ID
    
    bool _isActive = false;
    private void Awake()
    {
        
    }
    void Start()
    {
        QuestManager._instance._questMarkEvt -= SetQuestMark;
        QuestManager._instance._questMarkEvt += SetQuestMark;

        _targetObj.Init(_objID);
    }

    void Update()
    {
        if(_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space")) // Sapce를 누를때
            {
                if(QuestManager._instance.QuestTrigger(_objID)) // 플레이어의 퀘스트에 본인이 포함되어 있으면
                {
                    gameObject.SetActive(false); // 비활성화 시킨다.
                }
            }
        }
    }

    void SetQuestMark(QuestMark mark, int id)
    {
        if (id != _objID) return; // 본인의 ID와 다르면 리턴

        _questMarkUI.SetQuestMark(mark);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = false;
        }
    }
    private void OnDestroy()
    {
        QuestManager._instance._questMarkEvt -= SetQuestMark;
    }
}
