using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTestTEst : MonoBehaviour
{
    [SerializeField] Animation _anim;
    [SerializeField] Button _interactUI;
    private AudioSource _audio;
    private bool _isActive = false;
    private GameObject _player;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _interactUI.onClick.AddListener(StartMove);
    }

    private void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartMove();
            }
        }
    }
    void StartMove()
    {
        _player.SetActive(false);
        _interactUI.gameObject.SetActive(false);

        CameraManager._instance.ChangeCam(CameraType.ViewCam);
        _anim.Play();
        _audio.Play();

        _isActive = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
            _isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
            _isActive = false;
        }
    }
}
