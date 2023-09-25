using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _anim;

    private AudioSource _audio;
    private bool _isOpen = false;
    private bool _isActive = false;

    [SerializeField] private AudioClip[] _clips;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space"))
            {
                if (!_isOpen)
                {
                    _anim.CrossFade("OpenDoor", 0.1f);
                    _audio.PlayOneShot(_clips[0]);
                    _isOpen = true;
                }
                else
                {
                    _anim.CrossFade("CloseDoor", 0.1f);
                    _audio.PlayOneShot(_clips[1]);
                    _isOpen = false;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActive = false;
        }
    }
}
