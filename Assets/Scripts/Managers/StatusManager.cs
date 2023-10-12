using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager _instance;

    public GameObject _confuse; // 데미지를 주지 않는 상태이상
    public GameObject _poison;

    private Stat _stat;
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
    public void StartConfusion(GameObject target, float time) // 이동반전
    {
        GameObject obj = Instantiate(_confuse, target.transform);
        obj.GetComponent<Confusion>().SetValues(time);

        _stat = obj.GetComponentInParent<Stat>(); // 자신이 속한 객체의 Stat을 찾는다.

        switch (_stat.Type) // 타입에 따라서 생성하는 UI가 다르다.
        {
            case Stat.TypeEnum.Player:
                UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffEffect.Confusion, time);
                break;
        }
    }
    public void StartPoison(GameObject target, float dmg, float time) // 독뎀
    {
        GameObject obj = Instantiate(_poison, target.transform);

        obj.GetComponent<Poison>().SetValues(target, dmg, time);

        _stat = obj.GetComponentInParent<Stat>(); // 자신이 속한 객체의 Stat을 찾는다.

        switch (_stat.Type) // 타입에 따라서 생성하는 UI가 다르다.
        {
            case Stat.TypeEnum.Player:
                UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffEffect.Poison, time);
                break;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
