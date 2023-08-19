using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public enum BuffEffect
    {
        None = -1,

        AtkUp,
        AtkDown,

        DefUp,
        DefDown,

        MovSpdUp,
        MovSpdDown,
    }
    public static BuffManager _instance;

    public GameObject _buff;
    public GameObject _deBuff;
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
    public void StartBuff(BuffEffect type, GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        buff.StartBuff(type, value, time);
    }
    public void StartDeBuff(BuffEffect type, GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        debuff.StartDeBuff(type, value, time);
    }
    /*
    public void StartAtkBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        buff.StartAtkBuff(target, value, time);
    }
    public void StartDefBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        buff.StartDefBuff(target, value, time);

    }
    public void StartMovSpdBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        buff.StartMovSpdBuff(target, value, time);
    }

    public void StartAtkDeBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        debuff.StartAtkDeBuff(target, value, time);
    }
    
    public void StartDefDeBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        debuff.StartDefDeBuff(target, value, time);
    }
    
    public void StartMovSpdDeBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        debuff.StartMovSpdDeBuff(target, value, time);
    }*/
}
