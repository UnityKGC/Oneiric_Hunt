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

        // UI 체력 이벤트 등록.
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

        //_canvas.SetHPAmount(HP / MaxHp);

        UIManager._instacne.BossHPUIEvt(HP / MaxHp); // 현재 체력 비율을 이벤트로 전달 => BossHPUI가 받음.

        if (HP <= 0)
        {
            _isDead = true;

            //Debug.Log("사망");

            QuestManager._instance.QuestTrigger(_id);
            SpawnManager._instance.KilledMonster();

            Destroy(gameObject);
        }
    }
}
