using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    public EnemyCanvas _canvas;

    private bool _isDead;

    Monster _monster;
    [SerializeField] int _id; // ���� id

    void Start()
    {
        Type = TypeEnum.Enemy;

        _monster = gameObject?.GetComponent<Monster>();
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

        _canvas.SetHPAmount(HP / MaxHp);

        if (_monster != null)
            _monster.State = Monster.MonsterState.Hit;

        if (HP <= 0)
        {
            _isDead = true;

            //Debug.Log("���");

            QuestManager._instance.QuestTrigger(_id);
            SpawnManager._instance.KilledMonster();

            if(_monster != null)
                _monster.State = Monster.MonsterState.Die;
            else
                Destroy(gameObject);
        }
    }
}
