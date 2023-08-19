using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    public EnemyCanvas _canvas;

    private bool _isDead;

    [SerializeField] int _id; // 몬스터 id

    void Start()
    {
        Type = TypeEnum.Enemy;
    }

    void Update()
    {
        
    }

    public override float GetDamage() // 누군가에게 데미지를 줄 때
    {
        float dmg = Random.Range(MinAtk, MaxAtk);
        return dmg;
    }
    public override void SetDamage(float value)
    {
        if (_isDead) return; // 죽을 때 계속 2번 죽어서 조건 추가하여 버그 방지

        float dmg = value - Defense;

        HP -= dmg;

        _canvas.SetHPAmount(HP / MaxHp);

        if (HP <= 0)
        {
            _isDead = true;

            Debug.Log("사망");

            QuestManager._instance.QuestTrigger(_id);
            SpawnManager._instance.KilledMonster();

            Destroy(gameObject);
        }
    }
}
