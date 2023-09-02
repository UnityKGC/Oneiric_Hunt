using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private QuestMarkUI _questMarkUI; // ����Ʈ ����Ʈ

    public int _objID; // ������ ID

    bool _isActive = false;

    private void Awake()
    {

    }
    void Start()
    {
        QuestManager._instance._objEffectEvt -= SetEffect;
        QuestManager._instance._objEffectEvt += SetEffect;
    }

    void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space")) // Sapce�� ������
            {
                QuestManager._instance.QuestTrigger(_objID); // �÷��̾��� ����Ʈ�� ������ ���ԵǾ� ������
            }
        }
    }

    void SetEffect(int id) // ������Ʈ�� id�� ���ڷ� �޾� ������, �ش��ϴ� UI ȣ��
    {
        if (id == _objID)
        {
            _questMarkUI.SetQuestMark(QuestMark.Finish);
        }
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
}
