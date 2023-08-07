using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public GameObject _selectOutLine; // �ܰ���
    private Transform _trans;

    private static Object _closeObj; // ���� ����� ������Ʈ => static���� �����, Object.cs ������ ���ϰ� �ִ� ��� ��ü�� _closeObj ������ �����Ͽ�, ���� _closeObj���� �Ǵ��ϵ��� �����.
    private static float _closeDist = float.MaxValue; // ���� ����� ������Ʈ�� �Ÿ�

    public int _objID = 10000; // ������ ID
    private void Awake()
    {
        _trans = GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) // 1. ó�� ������ �Ǵ� => ���õ� �������� ������ ����, �̹� �����ص� ������ ���� ������ ���� �� �̹Ƿ� �ѱ�
    {
        if(other.gameObject.tag == "Player")
        {
            _selectOutLine.SetActive(true);
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
                
                if(Input.GetKeyDown(KeyCode.Space)) // Sapce�� ������
                {
                    QuestManager._instance.BringQuestTrigger(_objID);
                }
            }
            else // ����� ����� �ƴ϶��, �ܰ� ���� 
            {
                _selectOutLine.SetActive(false);
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
            if(_closeObj == this)
            {
                _closeObj = null;
                _closeDist = float.MaxValue;
            }
        }
    }
}
