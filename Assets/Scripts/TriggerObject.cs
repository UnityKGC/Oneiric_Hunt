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
            if(QuestManager._instance.QuestTrigger(_objID)) // 본인이 포함되는 퀘스트가 있다면,
            {
                gameObject.SetActive(false); // 비활성화 시킨다.
            }
        }
    }
}
