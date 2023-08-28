using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isOpen = false;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space"))
            {
                if(!_isOpen)
                {
                    _anim.CrossFade("OpenDoor", 0.1f);
                    _isOpen = true;
                }
                else
                {
                    _anim.CrossFade("CloseDoor", 0.1f);
                    _isOpen = false;
                }
            }
        }
    }
}
