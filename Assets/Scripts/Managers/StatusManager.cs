using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager _instance;

    public GameObject _confuse; // �������� ���� �ʴ� �����̻�
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
    public void StartConfusion(GameObject target, float time) // �̵�����
    {
        GameObject obj = Instantiate(_confuse, target.transform);
        obj.GetComponent<Confusion>().SetValues(time);

        _stat = obj.GetComponentInParent<Stat>(); // �ڽ��� ���� ��ü�� Stat�� ã�´�.

        switch (_stat.Type) // Ÿ�Կ� ���� �����ϴ� UI�� �ٸ���.
        {
            case Stat.TypeEnum.Player:
                UIManager._instacne.StartPlayerBuffUI(BuffManager.BuffEffect.Confusion, time);
                break;
        }
    }
    public void StartPoison(GameObject target, float dmg, float time) // ����
    {
        GameObject obj = Instantiate(_poison, target.transform);

        obj.GetComponent<Poison>().SetValues(target, dmg, time);

        _stat = obj.GetComponentInParent<Stat>(); // �ڽ��� ���� ��ü�� Stat�� ã�´�.

        switch (_stat.Type) // Ÿ�Կ� ���� �����ϴ� UI�� �ٸ���.
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
