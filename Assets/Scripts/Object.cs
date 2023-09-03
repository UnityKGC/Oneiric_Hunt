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
    [SerializeField] private QuestMarkUI _questMarkUI; // ����Ʈ ����Ʈ
    [SerializeField] private TargetUIObj _targetObj;
    public int _objID; // ������ ID
    
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
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space")) // Sapce�� ������
            {
                if(QuestManager._instance.QuestTrigger(_objID)) // �÷��̾��� ����Ʈ�� ������ ���ԵǾ� ������
                {
                    gameObject.SetActive(false); // ��Ȱ��ȭ ��Ų��.
                }
            }
        }
    }

    void SetQuestMark(QuestMark mark, int id)
    {
        if (id != _objID) return; // ������ ID�� �ٸ��� ����

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
