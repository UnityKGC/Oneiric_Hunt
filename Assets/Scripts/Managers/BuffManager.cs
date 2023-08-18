using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
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
    public void StartAtkBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        UIManager._instacne._buffEvt.Invoke(time);
        buff.StartAtkBuff(target, value, time);
    }
    public void StartDefBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        UIManager._instacne._buffEvt.Invoke(time);
        buff.StartDefBuff(target, value, time);

    }
    public void StartMovSpdBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_buff, target.transform);

        Buff buff = obj.GetComponent<Buff>();

        UIManager._instacne._buffEvt.Invoke(time);
        buff.StartMovSpdBuff(target, value, time);
    }

    public void StartAtkDeBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        UIManager._instacne._buffEvt.Invoke(time);
        debuff.StartAtkDeBuff(target, value, time);
    }
    
    public void StartDefDeBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        UIManager._instacne._buffEvt.Invoke(time);
        debuff.StartDefDeBuff(target, value, time);
    }
    
    public void StartMovSpdDeBuff(GameObject target, float value, float time)
    {
        GameObject obj = Instantiate(_deBuff, target.transform);

        DeBuff debuff = obj.GetComponent<DeBuff>();

        UIManager._instacne._buffEvt.Invoke(time);
        debuff.StartMovSpdDeBuff(target, value, time);
    }
}
