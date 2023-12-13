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
        StartCoroutine(DamageUI(1 - value));
    }
    void StartBuffUI(Transform obj, BuffManager.BuffEffect type, float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {
        if (obj != transform.parent) return; // �������� ����� ������ �ƴ϶�� ����

        BuffUIDuration ui = Instantiate(_buffList[(int)type], _buffGridLayout).GetComponent<BuffUIDuration>(); // BuffUI ��ũ��Ʈ�� ���� ������ GridLayOutGroup�� ���ϰ� �ֱ⿡ ������ �θ�� ����

        ui.Init(time);
    }
    IEnumerator DamageUI(float value) // ������ ���� �κ�(���)�� ���ҽ�Ű�� �ڷ�ƾ
    {
        yield return new WaitForSeconds(0.5f); // ó���� �������� �ް�, 0.5�� ���

        while(value > 0) // ���� ������ŭ UI�� �����Ѵ�. value�� damagedSpd��ŭ ���ҽ�Ű�� �����, 0���� �۰ų� �������� �����.
        {
            _midImg.fillAmount -= _damageSpd;
            value -= _damageSpd;

            yield return _waitFrame;
        }
        
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
    }
}
