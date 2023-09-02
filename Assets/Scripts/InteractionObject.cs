using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private QuestMarkUI _questMarkUI; // 퀘스트 이펙트

    public int _objID; // 본인의 ID

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
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space")) // Sapce를 누를때
            {
                QuestManager._instance.QuestTrigger(_objID); // 플레이어의 퀘스트에 본인이 포함되어 있으면
            }
        }
    }

    void SetEffect(int id) // 오브젝트의 id를 인자로 받아 같으면, 해당하는 UI 호출
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
