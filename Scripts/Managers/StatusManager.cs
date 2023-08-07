using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager _instance;

    public GameObject _confuse; // �������� ���� �ʴ� �����̻�
    public GameObject _poison;
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
    public void StartConfusion(GameObject target, float time)
    {
        GameObject obj = Instantiate(_confuse, target.transform);
        obj.GetComponent<Confusion>().SetValues(time);
    }
    public void StartPoison(GameObject target, float dmg, float time)
    {
        GameObject obj = Instantiate(_poison, target.transform);
        obj.GetComponent<Poison>().SetValues(target, dmg, time);
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
