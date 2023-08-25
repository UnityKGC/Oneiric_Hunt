using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    public TextMeshProUGUI _name; // NPC�̸�
    public TextMeshProUGUI _content; // ��ȭ ����

    DialogueData _data;

    private void Start()
    {
        
        DialogueManager._instance._dialogueEvt -= SetDialogueData;
        DialogueManager._instance._dialogueEvt += SetDialogueData;

        DialogueManager._instance._clickNext -= NextDialogue;
        DialogueManager._instance._clickNext += NextDialogue;

        gameObject.SetActive(false);
    }

    void SetDialogueData(DialogueData data) // �Ŵ����� ���� NPC�̸��� ��縦 ���޹޴´�.
    {
        if (data == null || data._isStart) return; // ������ �����Ͱ� ���ų� �̹� ������ ��ȭ��� ����

        _data = data;
        _data._isStart = true;

        _name.text = _data._name; // NPC �̸� ���
        _content.text = _data._dialogueLines[0]; // _lineCount ��� ���.
        StartDialogue(); // ��ȭ ����
    }
    void StartDialogue() // ��ȭ ����
    {
        gameObject.SetActive(true); // UI Ȱ��ȭ
    }
    void NextDialogue()
    {
        if (_data._index >= _data._dialogueLines.Count) // ������ ��簡 �����ٸ�
        {
            _data._isFinish = true;
            _data = null;

            DialogueManager._instance.EndDialogue();

            gameObject.SetActive(false);
        }
        else
        {
            _content.text = _data._dialogueLines[_data._index++]; // _lineCount ��� ���.
        }
    }
    private void OnDestroy()
    {
        DialogueManager._instance._dialogueEvt -= SetDialogueData;
        DialogueManager._instance._clickNext -= NextDialogue;
    }
}
