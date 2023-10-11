using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : Stat
{
    public EnemyCanvas _canvas;

    private bool _isDead;

    [SerializeField] private int _id;
    void Start()
    {
        Type = TypeEnum.Enemy;

        // UI ü�� �̺�Ʈ ���.
    }

    void Update()
    {
        
    }
    public override float GetDamage() // ���������� �������� �� ��
    {
        float dmg = Random.Range(MinAtk, MaxAtk);
        return dmg;
    }
    public override void SetDamage(float value)
    {
        if (_isDead) return; // ���� �� ��� 2�� �׾ ���� �߰��Ͽ� ���� ����

        float dmg = value - Defense;

        HP -= dmg;

        //_canvas.SetHPAmount(HP / MaxHp);

        UIManager._instacne.BossHPUIEvt(HP / MaxHp); // ���� ü�� ������ �̺�Ʈ�� ���� => BossHPUI�� ����.

        if (HP <= 0)
        {
            _isDead = true;

            //Debug.Log("���");

            QuestManager._instance.QuestTrigger(_id);
            SpawnManager._instance.KilledMonster();

            Destroy(gameObject);
        }
    }
}
