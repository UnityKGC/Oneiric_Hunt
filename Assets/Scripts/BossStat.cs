using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : Stat
{
    [SerializeField] private int _id;
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
        float dmg = value - Defense;
        HP -= dmg;
        if (HP <= 0)
        {
            QuestManager._instance.QuestTrigger(_id);
            SpawnManager._instance.KilledMonster();
            Destroy(gameObject);
        }
    }
}
