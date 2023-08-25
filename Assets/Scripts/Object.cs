using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None = -1,
    NPC,
    Clue,
    Object,
}

public class Object : MonoBehaviour
{
    public int _objID; // 본인의 ID

    private void Awake()
    {
        
    }
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
            QuestManager._instance.QuestTrigger(_objID);
        }
    }
    private void OnTriggerStay(Collider other) // 2. 들어오고 있을 때
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Sapce를 누를때
            {
                QuestManager._instance.QuestTrigger(_objID);
            }
        }
    }
}
