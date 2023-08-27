using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStartObject : MonoBehaviour
{
    [SerializeField] QuestData _data;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DialogueManager._instance.GetQuestDialogue(_data, _data._dialogueData[0]);
            if (gameObject.CompareTag("Tutorial_1"))
            {
                GameManager._instance.FirstTuto = true;
            }
            else if (gameObject.CompareTag("Tutorial_2"))
            {
                GameManager._instance.SecondTuto = true;
            }

            gameObject.SetActive(false);
        }
    }
}
