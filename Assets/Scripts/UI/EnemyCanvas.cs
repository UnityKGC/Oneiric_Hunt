using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Transform _buffGridLayout;
    public Image _midImg; // HP ������ ���� ��, ���̴� ����κ�
    public Image _frontImg; // HP front�κ�

    public List<GameObject> _buffList; // ���� Prefab ����Ʈ => ������ �����Ŵ����� ���� Ÿ�� enum ����.

    private Transform _camTrans;

    private WaitForEndOfFrame _waitFrame = new WaitForEndOfFrame();

    private float _damageSpd = 0.02f;
    [SerializeField] private float _remainDmg; // ���� ������ �����ϴ� ����

    [SerializeField] private float _nowHP = 1f; // ���� ���� ü�� => �ִ�ü�� ����;
    [SerializeField] private float _damage; // ���� ������
    void Start()
    {
        _camTrans = Camera.main.transform;

        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
        UIManager._instacne._enemyBuffEvt += StartBuffUI;
    }
    
    void Update()
    {
        transform.LookAt(_camTrans);
    }
    public void SetHPAmount(float value) // ���� HP ����, value�� (����ü�� / �ִ�ü��)�� ���̴�.
    {
        _frontImg.fillAmount = value;

        StopAllCoroutines();

        _damage = _nowHP - value;
        _nowHP -= _damage;

        StartCoroutine(DamageUI());
    }
    void StartBuffUI(Transform obj, BuffManager.BuffEffect type, float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {
        if (obj != transform.parent) return; // �������� ����� ������ �ƴ϶�� ����

        BuffUIDuration ui = Instantiate(_buffList[(int)type], _buffGridLayout).GetComponent<BuffUIDuration>(); // BuffUI ��ũ��Ʈ�� ���� ������ GridLayOutGroup�� ���ϰ� �ֱ⿡ ������ �θ�� ����

        ui.Init(time);
    }
    IEnumerator DamageUI() // ������ ���� �κ�(���)�� ���ҽ�Ű�� �ڷ�ƾ
    {
        while(_damage > 0) // ���� ������ŭ UI�� �����Ѵ�. value�� damagedSpd��ŭ ���ҽ�Ű�� �����, 0���� �۰ų� �������� �����.
        {
            _midImg.fillAmount -= _damageSpd;

            _damage -= _damageSpd;

            yield return _waitFrame;
        }
        
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
    }
}
