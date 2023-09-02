using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private int _objID;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(QuestManager._instance.QuestTrigger(_objID)) // ������ ���ԵǴ� ����Ʈ�� �ִٸ�,
            {
                gameObject.SetActive(false); // ��Ȱ��ȭ ��Ų��.
            }
        }
    }
}
