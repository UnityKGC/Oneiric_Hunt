using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager _instance;
    // ������ ���۵� ��, ����ϴ� ��ɵ�
    // 1. �÷��̾��� ���¸� ������������ ����
    // 2. ������ ������, �ٽ� �⺻���·� ����

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

        Debug.Log("���� ����");
    }
    public void EndBattle() // ���͸� �� ��ġ�� ������ ���� ��
    {
        _isBattle = false;

        SkillManager._instance.EndBattle(); // ��ų�� ���������� ��ų �ı� �� ��ų ���� �ʱ�ȭ
        UIManager._instacne.EndBattle(); // ��ų UI �ʱ�ȭ

        if(_isBossBattle) // ���� �������
        {
            UIManager._instacne.SetBossHP(false); // UI�� ����.
            _isBossBattle = false; // ���� ���� �к� �ʱ�ȭ
        }

        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;
    }
    private void OnDestroy()
    {
        _instance = null;
        _isBattle = false;
    }
}
