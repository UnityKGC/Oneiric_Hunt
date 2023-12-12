using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectType
{
    None,
    NPC,
    Door,
    Object,
}

public class Object : MonoBehaviour
{
    public Action<bool> _checkPlayerEvt = null;

    [SerializeField] private QuestMarkUI _questMarkUI; // ����Ʈ ����Ʈ
    [SerializeField] private TargetUIObj _targetObj;
    [SerializeField] private OutlineScript _outline;


    //[SerializeField] private Button _interactUI;
    public int _objID; // ������ ID
    
    bool _isActive = false;
    private void Awake()
    {
        
    }
    void Start()
    {
        QuestManager._instance._questMarkEvt -= SetQuestMark;
        QuestManager._instance._questMarkEvt += SetQuestMark;

        //_interactUI.onClick.AddListener(CheckObj);

        _targetObj.Init(_objID);
    }

    void Update()
    {
        if(_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space)|| SimpleInput.GetButtonDown("Space")) // Sapce�� ������
            {
                CheckObj();
            }
        }
    }
    void CheckObj()
    {
        if (QuestManager._instance.QuestTrigger(_objID)) // �÷��̾��� ����Ʈ�� ������ ���ԵǾ� ������
        {
            gameObject.SetActive(false); // ��Ȱ��ȭ ��Ų��.
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
            _checkPlayerEvt?.Invoke(true); // �÷��̾ ��������, true�� ����
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = false;
            _checkPlayerEvt?.Invoke(false); // �÷��̾ ��������, false�� ����
        }
    }
    private void OnDestroy()
    {
        QuestManager._instance._questMarkEvt -= SetQuestMark;
    }
}
