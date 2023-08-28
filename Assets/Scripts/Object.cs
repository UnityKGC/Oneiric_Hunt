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
    public int _objID; // ������ ID

    private void Awake()
    {
        
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }

    
    private void OnTriggerStay(Collider other) // 2. ������ ���� ��
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space")) // Sapce�� ������
            {
                QuestManager._instance.QuestTrigger(_objID);
            }
        }
    }
}
