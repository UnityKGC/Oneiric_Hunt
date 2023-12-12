using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI _name; // NPC�̸�
    public TextMeshProUGUI _content; // ��ȭ ����
    public RectTransform _downArrow;

    DialogueData _data;

    private string _nowText;

    private WaitForSeconds _typingSpeed;
    private bool _isTyping;

    [SerializeField] Vector3 _fromPos;
    [SerializeField] Vector3 _toPos;
    private void Start()
    {
        DialogueManager._instance._dialogueEvt -= SetDialogueData;
        DialogueManager._instance._dialogueEvt += SetDialogueData;

        DialogueManager._instance._clickNext -= NextDialogue;
        DialogueManager._instance._clickNext += NextDialogue;

        _fromPos = transform.position;
        _toPos = new Vector3(_fromPos.x, 0);
            
        _typingSpeed = new WaitForSeconds(DialogueManager._instance._typingSpeed);

        //gameObject.SetActive(false);
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
        _nowText = _data._dialogueLines[_data._index++];

        if (_content.text == "CamEvt")
        {
            DialogueManager._instance.CamEvent(gameObject);
        }
        else if (_content.text == "MonsterSpawn")
        {
            DialogueManager._instance.MonsterSpawnEvent(gameObject);
        }
        else
        {
            StartDialogue(); // ��ȭ ����
            StartCoroutine(TypingCo());
        }
    }
    public void StartDialogue() // ��ȭ ����
    {
        transform.DOMove(_toPos, 0);

        //_toDOT.DORestart();
        //gameObject.SetActive(true); // UI Ȱ��ȭ
    }
    void NextDialogue()
    {
        if (_data._index >= _data._dialogueLines.Count) // ������ ��簡 �����ٸ�
        {
            //_fromDOT.DORestart();
            transform.DOMove(_fromPos, 0);

            _data._isFinish = true;
            _data = null;

            DialogueManager._instance.EndDialogue();
            CameraManager._instance.ChangeCam(CameraType.PlayerCam);

            //gameObject.SetActive(false);
            //transform.DOLocalMove(new Vector3(0, -400, 0), 0.2f);
        }
        else
        {
            _nowText = _data._dialogueLines[_data._index++]; // _lineCount ��縦 �����´�.

            if(_content.text == "CamEvt") // ī�޶� �̺�Ʈ���
            {
                DialogueManager._instance.CamEvent(gameObject);
            }
            else if(_content.text == "MonsterSpawn") // ���� �����̶��
            {
                DialogueManager._instance.MonsterSpawnEvent(gameObject);
            }

            if (_isTyping)
                StopAllCoroutines();
            StartCoroutine(TypingCo());
            
        }
    }
    IEnumerator TypingCo()
    {
        _content.text = null;
        _isTyping = true;

        for (int i = 0; i < _nowText.Length; i++)
        {
            _content.text += _nowText[i];
            yield return _typingSpeed;
        }

        _isTyping = false;
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
