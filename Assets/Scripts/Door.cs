using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private Animator _anim;

    private AudioSource _audio;
    private bool _isOpen = false;
    private bool _isActive = false;

    [SerializeField] private Button _interactUI;
    [SerializeField] private AudioClip[] _clips;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _interactUI.onClick.AddListener(DoorTrigger);
    }
    void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("Space"))
            {
                DoorTrigger();
            }
        }
    }
    void DoorTrigger()
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
