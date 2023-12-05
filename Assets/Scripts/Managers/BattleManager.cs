using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager _instance;
    // ������ ���۵� ��, ����ϴ� ��ɵ�
    // 1. �÷��̾��� ���¸� ������������ ����
    // 2. ������ ������, �ٽ� �⺻���·� ����

    public Action<bool> _battleEvt = null; // ������ ���� => �ش� �̺�Ʈ�� ����ϰ� �ִ� ��ü�� ���͵�, ��, ���ʹ� ó���� Idle���¿��ٰ�, �� �̺�Ʈ�� ������ ������ ��ȯ
    public bool _isBattle = false;
    [SerializeField] private bool _isBossBattle = false;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
    public void StartBattle(bool isBossBattle = false) // Scene���� ���� ���� ���� �޽����� ����
    {
        // �÷��̾� ���� ���� ���
        _isBattle = true;
        _isBossBattle = isBossBattle;
        if (_isBossBattle) // ���� �������, 
        {
            UIManager._instacne.SetBossHP(true);// UI�� Ų��.
        }
        GameManager._instance.Playstate = GameManager.PlayState.Dream_Battle;
        _battleEvt?.Invoke(true); // �̺�Ʈ�� ������ ��ü�� �����Ѵٸ� Invoke
        //Debug.Log("���� ����");
    }
    public void EndBattle(bool isBossBattle = false) // ���͸� �� ��ġ�� ������ ���� ��
    {
        _isBattle = false;

        SkillManager._instance.EndBattle(); // ��ų�� ���������� ��ų �ı� �� ��ų ���� �ʱ�ȭ
        UIManager._instacne.EndBattle(); // ��ų UI �ʱ�ȭ
        CameraManager._instance.EndEffectAction();

        if(_isBossBattle) // ���� �������
        {
            UIManager._instacne.SetBossHP(false); // UI�� ����.
            UIManager._instacne.GameEnd();
            _isBossBattle = false; // ���� ���� �к� �ʱ�ȭ
        }

        _battleEvt?.Invoke(false); // �̺�Ʈ�� ������ ��ü�� �����Ѵٸ� Invoke
        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;
    }
    private void OnDestroy()
    {
        _instance = null;
        _isBattle = false;
    }
}
