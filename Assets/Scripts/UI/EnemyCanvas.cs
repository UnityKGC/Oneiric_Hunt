using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Transform _buffGridLayout;
    public Image _frontImg; // HP front�κ�

    public List<GameObject> _buffList; // ���� Prefab ����Ʈ => ������ �����Ŵ����� ���� Ÿ�� enum ����.

    private Transform _camTrans;
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
    public void SetHPAmount(float value) // ���� HP ����
    {
        _frontImg.fillAmount = value;
    }
    void StartBuffUI(Transform obj, BuffManager.BuffEffect type, float time) // ���� ���� �ð��� ���ڷ� �޾�, UI ����.
    {
        if (obj != transform.parent) return; // �������� ����� ������ �ƴ϶�� ����

        BuffUIDuration ui = Instantiate(_buffList[(int)type], _buffGridLayout).GetComponent<BuffUIDuration>(); // BuffUI ��ũ��Ʈ�� ���� ������ GridLayOutGroup�� ���ϰ� �ֱ⿡ ������ �θ�� ����

        ui.Init(time);
    }
    private void OnDestroy()
    {
        UIManager._instacne._enemyBuffEvt -= StartBuffUI;
    }
}
