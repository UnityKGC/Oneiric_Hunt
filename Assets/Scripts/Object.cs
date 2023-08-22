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

    public GameObject _selectOutLine; // �ܰ���
    public ObjectInteractUI _interactionUI; // ��ȣ�ۿ� UI
    private Transform _trans;

    private static Object _closeObj; // ���� ����� ������Ʈ => static���� �����, Object.cs ������ ���ϰ� �ִ� ��� ��ü�� _closeObj ������ �����Ͽ�, ���� _closeObj���� �Ǵ��ϵ��� �����.
    private static float _closeDist = float.MaxValue; // ���� ����� ������Ʈ�� �Ÿ�

    public int _objID = 10000; // ������ ID

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
                text = "��ȭ�ϱ�";
                break;
            case ObjectType.Clue:
                text = "Ȯ���ϱ�";
                break;
            case ObjectType.Object:
                text = "��ȣ�ۿ�";
                break;
        }
        return text;
    }

    private void OnTriggerEnter(Collider other) // 1. ó�� ������ �Ǵ� => ���õ� �������� ������ ����, �̹� �����ص� ������ ���� ������ ���� �� �̹Ƿ� �ѱ�
    {
        if(other.gameObject.tag == "Player")
        {
            _selectOutLine.SetActive(true);
            _interactionUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other) // 2. ������ ���� ��
    {
        if (other.gameObject.tag == "Player")
        {
            float distance = Vector3.Distance(_trans.position, other.transform.position); // �Ÿ��� ���

            if (_closeObj == this) // ���� ����� ����� �ڱ��ڽ� �̶��, �Ÿ� ���� �� ���õǾ����Ƿ�, ���� ����
            {
                _closeDist = distance;
                _selectOutLine.SetActive(true);

                _interactionUI.gameObject.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.Space)) // Sapce�� ������
                {

                    QuestManager._instance.QuestTrigger(_objID);
                }
                
            }
            else // ����� ����� �ƴ϶��, �ܰ� ���� 
            {
                _selectOutLine.SetActive(false);
                _interactionUI.gameObject.SetActive(false);
            }

            if (_closeObj == null || distance < _closeDist) // ���õ� ����� ���ų�, �ڽŰ� �÷��̾��� �Ÿ��� ���õ� ������(���� ����� ������)���� �� ������ ������, => ���õ� ��� ���� ����
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
