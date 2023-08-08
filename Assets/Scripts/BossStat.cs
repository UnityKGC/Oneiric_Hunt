using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : MonoBehaviour
{
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField]
    private float _maxHp;
    public float HP { get { return _hp; } set { _hp = value; if (_hp >= MaxHp) { _hp = MaxHp; } } }
    [SerializeField]
    private float _hp;

    public float MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    [SerializeField]
    private float _maxMp;

    public float MP { get { return _mp; } set { _mp = value; } }
    [SerializeField]
    private float _mp;

    public float MinAtk { get { return _minAtk; } set { _minAtk = value; } }
    [SerializeField]
    private float _minAtk;

    public float MaxAtk { get { return _maxAtk; } set { _maxAtk = value; } }
    [SerializeField]
    private float _maxAtk;

    public float Defense { get { return _defense; } set { _defense = value; } }
    [SerializeField]
    private float _defense;

    public float MoveSpd { get { return _moveSpd; } set { _moveSpd = value; } }
    [SerializeField]
    private float _moveSpd;

    [SerializeField] private int _id;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public float GetDamage() // 누군가에게 데미지를 줄 때
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
