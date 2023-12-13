using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI _name; // NPC이름
    public TextMeshProUGUI _content; // 대화 내용
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
    void SetDialogueData(DialogueData data) // 매니저로 부터 NPC이름과 대사를 전달받는다.
    {
        if (data == null || data._isStart) return; // 가져온 데이터가 없거나 이미 시작한 대화라면 리턴

        _data = data;
        _data._isStart = true;

        _name.text = _data._name; // NPC 이름 등록
        _nowText = _data._dialogueLines[_data._index++];

        if (_nowText == "CamEvt")
        {
            ActivePanel(false);
            DialogueManager._instance.CamEvent();
        }
        else if (_nowText == "MonsterSpawn")
        {
            ActivePanel(false);
            DialogueManager._instance.MonsterSpawnEvent();
        }
        else
        {
            ActivePanel(true);
            StartCoroutine(TypingCo());
        }
    }
    void NextDialogue()
    {
        
        if (_data._index >= _data._dialogueLines.Count) // 마지막 대사가 끝났다면
        {
            //_fromDOT.DORestart();
            ActivePanel(false);

            _data._isFinish = true;
            _data = null;

            DialogueManager._instance.EndDialogue();
            CameraManager._instance.ChangeCam(CameraType.PlayerCam);

            //gameObject.SetActive(false);
            //transform.DOLocalMove(new Vector3(0, -400, 0), 0.2f);
        }
        else
        {
            _nowText = _data._dialogueLines[_data._index++]; // _lineCount 대사를 가져온다.

            if(_nowText == "CamEvt") // 카메라 이벤트라면
            {
                ActivePanel(false);
                DialogueManager._instance.CamEvent();
            }
            else if(_nowText == "MonsterSpawn") // 몬스터 스폰이라면
            {
                ActivePanel(false);
                DialogueManager._instance.MonsterSpawnEvent();
            }
            else
            {
                if (_isTyping)
                    StopAllCoroutines();

                ActivePanel(true);

                StartCoroutine(TypingCo());
            }
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
    public void ActivePanel(bool value)
    {
        if(value)
            transform.DOMove(_toPos, 0);
        else
            transform.DOMove(_fromPos, 0);
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
