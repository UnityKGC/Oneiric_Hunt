using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI _name; // NPC이름
    public TextMeshProUGUI _content; // 대화 내용
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
    void SetDialogueData(DialogueData data) // 매니저로 부터 NPC이름과 대사를 전달받는다.
    {
        if (data == null || data._isStart) return; // 가져온 데이터가 없거나 이미 시작한 대화라면 리턴

        _data = data;
        _data._isStart = true;

        _name.text = _data._name; // NPC 이름 등록
        _content.text = _data._dialogueLines[_data._index++]; // _lineCount 대사 출력.

        StartDialogue(); // 대화 시작
    }
    void StartDialogue() // 대화 시작
    {
        gameObject.SetActive(true); // UI 활성화
    }
    void NextDialogue()
    {
        if (_data._index >= _data._dialogueLines.Count) // 마지막 대사가 끝났다면
        {
            _data._isFinish = true;
            _data = null;

            DialogueManager._instance.EndDialogue();

            gameObject.SetActive(false);
        }
        else
        {
            _content.text = _data._dialogueLines[_data._index++]; // _lineCount 대사 출력.
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
            NextDialogue();
        }
    }
}
