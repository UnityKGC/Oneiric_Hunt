using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{

    private bool _isDead;

    [SerializeField] int _id; // ���� id

    void Start()
    {
        Type = TypeEnum.Enemy;
    }

    void Update()
    {
        
    }

    public float GetDamage() // ���������� �������� �� ��
    {
        float dmg = Random.Range(MinAtk, MaxAtk);
        return dmg;
    }
    public void SetDamage(float value)
    {
        if (_isDead) return; // ���� �� ��� 2�� �׾ ���� �߰��Ͽ� ���� ����

        float dmg = value - Defense;

        HP -= dmg;

        if(HP <= 0)
        {
            _isDead = true;

            Debug.Log("���");

            QuestManager._instance.QuestTrigger(_id);
            SpawnManager._instance.KilledMonster();

            Destroy(gameObject);
        }
    }
}
