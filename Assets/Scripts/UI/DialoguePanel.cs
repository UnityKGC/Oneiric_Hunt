using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI _name; // NPC�̸�
    public TextMeshProUGUI _content; // ��ȭ ����
    public RectTransform _downArrow;

    DialogueData _data;

    private void Start()
    {
        DialogueManager._instance._dialogueEvt -= SetDialogueData;
        DialogueManager._instance._dialogueEvt += SetDialogueData;

        DialogueManager._instance._clickNext -= NextDialogue;
        DialogueManager._instance._clickNext += NextDialogue;

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        
    }
    void SetDialogueData(DialogueData data) // �Ŵ����� ���� NPC�̸��� ��縦 ���޹޴´�.
    {
        if (data == null || data._isStart) return; // ������ �����Ͱ� ���ų� �̹� ������ ��ȭ��� ����

        _data = data;
        _data._isStart = true;

        _name.text = _data._name; // NPC �̸� ���
        _content.text = _data._dialogueLines[_data._index++]; // _lineCount ��� ���.

        if (_content.text == "CamEvt")
        {
            DialogueManager._instance.CamEvent(gameObject);
        }
        else if (_content.text == "MonsterSpawn")
        {
            DialogueManager._instance.MonsterSpawnEvent(gameObject);
        }
        else
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
            CameraManager._instance.ChangeCam(CameraType.PlayerCam);

            gameObject.SetActive(false);
        }
        else
        {
            _content.text = _data._dialogueLines[_data._index++]; // _lineCount ��� ���.
            if(_content.text == "CamEvt")
            {
                DialogueManager._instance.CamEvent(gameObject);
            }
            else if(_content.text == "MonsterSpawn")
            {
                DialogueManager._instance.MonsterSpawnEvent(gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        DialogueManager._instance._dialogueEvt -= SetDialogueData;
        DialogueManager._instance._clickNext -= NextDialogue;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameObject.activeSelf)
        {
            SoundManager._instance.PlayDialogueSound();
            NextDialogue();
        }
    }
}
