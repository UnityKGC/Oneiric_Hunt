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
    private bool _isLastBattle = false;
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
    public void StartBattle(bool isLastBattle = false) // Scene���� ���� ���� ���� �޽����� ����
    {
        // �÷��̾� ���� ���� ���
        _isBattle = true;

        _isLastBattle = isLastBattle;

        GameManager._instance.Playstate = GameManager.PlayState.Dream_Battle;

        Debug.Log("���� ����");
    }
    public void EndBattle() // ���͸� �� ��ġ�� ������ ���� ��
    {
        _isBattle = false;

        SkillManager._instance.EndBattle(); // ��ų�� ���������� ��ų �ı� �� ��ų ���� �ʱ�ȭ
        UIManager._instacne.EndBattle(); // ��ų UI �ʱ�ȭ

        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;

        if(_isLastBattle)
        {
            // ��Ż Ȱ��ȭ => ��Ż�� ���� �����ϰ� ����? => Scene��, �׷� Scene���� �ʰ� ���ϰ� �ִ� ��Ż�� Ȱ��ȭ ���Ѷ�! ��� �����ؾ� �ϴµ�, �װ� ��� �����ұ�?
            // ���, BattleManager�� Scene�� ���ϰ� ������ �����ϰ� �Ǵ� ��������, ��... �׷���...!
            // �׳� SceneManager�� ����� ����. ���� ��Ż�� Scene���� �����ϴ� ������Ʈ�ϱ�...!
            SceneManagerEX._instance.EnablePotal(SceneManagerEX.SceneType.FirstDreamScene, SceneManagerEX.PortalType.ExitPortal);
        }
        Debug.Log("���� ��");
    }
    private void OnDestroy()
    {
        _instance = null;
        _isLastBattle = false;
        _isBattle = false;
    }
}
