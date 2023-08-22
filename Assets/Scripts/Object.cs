using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public enum ObjectType
    {
        None = -1,
        NPC,
        Clue,
        Object,
    }
    public ObjectType ObjType { get { return _objType; } set { _objType = value; } }
    [SerializeField] private ObjectType _objType = ObjectType.None;

    public GameObject _selectOutLine; // 외곽선
    public ObjectInteractUI _interactionUI; // 상호작용 UI
    private Transform _trans;

    private static Object _closeObj; // 가장 가까운 오브젝트 => static으로 만들어, Object.cs 파일을 지니고 있는 모든 객체는 _closeObj 변수를 공유하여, 누가 _closeObj인지 판단하도록 만든다.
    private static float _closeDist = float.MaxValue; // 가장 가까운 오브젝트의 거리

    public int _objID = 10000; // 본인의 ID

    private Transform _camTrans;
    private void Awake()
    {
        _trans = GetComponent<Transform>();
    }
    void Start()
    {
        _camTrans = Camera.main.transform;
        _interactionUI.Init(SetUIText());
        _interactionUI.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    private string SetUIText()
    {
        string text = "";
        switch(ObjType)
        {
            case ObjectType.NPC:
                text = "대화하기";
                break;
            case ObjectType.Clue:
                text = "확인하기";
                break;
            case ObjectType.Object:
                text = "상호작용";
                break;
        }
        return text;
    }

    private void OnTriggerEnter(Collider other) // 1. 처음 들어오면 판단 => 선택된 아이템이 없으면 선택, 이미 존재해도 본인이 가장 가깝지 않을 것 이므로 넘김
    {
        if(other.gameObject.tag == "Player")
        {
            _selectOutLine.SetActive(true);
            _interactionUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other) // 2. 들어오고 있을 때
    {
        if (other.gameObject.tag == "Player")
        {
            float distance = Vector3.Distance(_trans.position, other.transform.position); // 거리를 계산

            if (_closeObj == this) // 가장 가까운 대상이 자기자신 이라면, 거리 갱신 및 선택되었으므로, 로직 실행
            {
                _closeDist = distance;
                _selectOutLine.SetActive(true);

                _interactionUI.gameObject.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.Space)) // Sapce를 누를때
                {

                    QuestManager._instance.QuestTrigger(_objID);
                }
                
            }
            else // 가까운 대상이 아니라면, 외곽 해제 
            {
                _selectOutLine.SetActive(false);
                _interactionUI.gameObject.SetActive(false);
            }

            if (_closeObj == null || distance < _closeDist) // 선택된 대상이 없거나, 자신과 플레이어의 거리가 선택된 아이템(가장 가까운 아이템)보다 더 가까이 있으면, => 선택된 대상 갱신 로직
            {
                _closeObj = this;
                _closeDist = distance;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _selectOutLine.SetActive(false);
            _interactionUI.gameObject.SetActive(false);

            if (_closeObj == this)
            {
                _closeObj = null;
                _closeDist = float.MaxValue;
            }
        }
    }
}
