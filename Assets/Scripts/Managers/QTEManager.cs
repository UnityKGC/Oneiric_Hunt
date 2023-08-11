using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public static QTEManager _instance; // �ܺο����� ����� �� �ְ� �̱���ȭ
    public float _slowTime = 0.2f; // TimeScale�� ����� ��
    public bool CheckQTEStart { get { return _isStart; } }
    public bool CheckQTESuccess { get { return _isSuccess; } } // �ܺο��� QTE�̺�Ʈ�� �����ߴ��� �����ߴ��� �˷��ִµ� �ʿ��� ����
    public bool CheckQTEEnd { get { return _isEnd; } } // �ܺο��� QTE�̺�Ʈ�� �������� �˷��ִµ� �ʿ��� ����

    private QTEEvent _eventData; // �̺�Ʈ ������
    private List<QTEKeys> _keys; // ������ �Ѵ� Ű ����Ʈ

    private float _evtTime; // ������ �� ���� �ð�

    private bool _isSuccess; // ���� Ȯ�� ����.
    private bool _isStart; // �̺�Ʈ ���� Ȯ�� ����
    private bool _isFail; // ���� Ȯ�� ����
    private bool _isEnd; // �� Ȯ�� ����
    
    private void Awake()
    {
        _instance = this; // �̱���ȭ => �ڱ��ڽ��� ���
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!_isStart) return; // �̺�Ʈ�� �������� �ʾ����� �ٷ� ����
        if(_keys.Count == 0 || _isFail) // ������ �� key�� ���̻� �������� �ʰų�, �����Ͽ�����, QTE�̺�Ʈ �����Լ� ȣ��
        {
            QTEEnd();
        }
        else // ������ �� Key�� ���� �����Ѵٸ�
        {
            for(int i = 0; i < _eventData._keys.Count; i++) // for���� ����, �÷��̾ �ش� key�� �������� �Ǵ��ϴ� CheckKey�Լ� ȣ��
            {
                CheckKey(_eventData._keys[i]);
            }
        }
    }
    public void StartEvent(QTEEvent evt) // �̺�Ʈ ����
    {
        _eventData = evt; // �Ŵ����� ���ڷ� ���޹��� �̺�Ʈ ���

        _isSuccess = false; // ������ �ʱ�ȭ
        _isFail = false; // ������ �ʱ�ȭ
        _isEnd = false; // ������ �ʱ�ȭ

        _keys = new List<QTEKeys>(_eventData._keys); // ������ �� Ű ����Ʈ ����

        Time.timeScale = _slowTime; // �ð��� ������
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // �� �� �ε巴�� => �� �� ���� �ʿ�

        _evtTime = evt._time; // ���޹��� �̺�Ʈ�� ���ѽð��� ����

        UIManager._instacne.SetQTEPosEvt(_eventData._pos); // UI���� QTE�� ��ġ�Ǿ� �� ��ġ�� �˷���

        CheckKeyUI(); // ���� Ű�� ������ �ϴ��� �˷��ִ� �Լ� => ����� Debug�� �˷��� => �Ŀ� UI�� ���� ����
        StartCoroutine(StartCountDown()); // ī��Ʈ �ٿ� �ڷ�ƾ ����
    }
    IEnumerator StartCountDown() // ī��Ʈ �ٿ�
    {
        _isStart = true; // ����������, ���� ����

        while(_isStart && _evtTime > 0) // �����߰�, ������ �� �ð��� 0�ʺ��� ũ�� �ݺ�
        {
            Debug.Log(_evtTime);
            _evtTime--; // ���� �ð� �پ��
            yield return new WaitForSecondsRealtime(1f); // 1�ʸ��� �پ��
        }

        if(_isEnd == false) // ������ ��, _isEnd�� false��� ����!
        {
            _isFail = true; // ���� Ȯ�� ������ true��
            QTEEnd(); // �̺�Ʈ ���� �Լ� ȣ��
        }
    }
    void QTEEnd() // �̺�Ʈ ���� ��
    {
        if(_keys.Count == 0) // ��� �����ٸ�
        {
            _isSuccess = true; // ���� Ȯ�� ������ true��
        }

        _isEnd = true; // ���� ����
        _isStart = false; // ���� ����

        Time.timeScale = 1f; // �ð� �ʱ�ȭ
        Time.fixedDeltaTime = 0.02f; // �ð� �ʱ�ȭ

        if (_isFail) // ���а� true���
        {
            Debug.Log("����");
        }
        else if(_isSuccess) // ������ true���
        {
            Debug.Log("����");
        }
        _eventData = null; // �̺�Ʈ ����
    }
    void CheckKey(QTEKeys key) // ���ڷ� ���޹��� key�� �������� Ȯ��
    {
        #region InputSystem�� ����� ��
        /*
        Keyboard inputKey = Keyboard.current; // Ű������ ���¸� �����´�
        if (inputKey != null) // Ű���尡 ����Ǿ� ������,
        {
            if (inputKey[key._key].wasPressedThisFrame) // �׸��� �� ���� Ű��, �̺�Ʈ���� ������ �ϴ� Ű�� �����ϴٸ�,
            {
                _keys.Remove(key);
            }
        }*/
        #endregion
        if (Input.GetKeyDown(key._key)) // �ش� Key�� �����ٸ�,
        {
            _keys.Remove(key); // �� key ����Ʈ���� �ش� key�� �����Ѵ�.
        }
    }
    void CheckKeyUI() // ������ �ϴ� Ű Ȯ��
    {
        string text = "";
        _keys.ForEach(key => text += key._key.ToString()); // ���ٽ�, key ����Ʈ�� key�� ������, �����Ǿ� �ִ� key�� string���� ����� �ش�. => ToString�� ���� ������, Keycode�� Enum�� �������� ����
        Debug.Log("������ �� ��ư : " + text);
    }

}
