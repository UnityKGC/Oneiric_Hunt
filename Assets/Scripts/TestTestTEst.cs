using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTestTEst : MonoBehaviour
{
    [SerializeField] Animation _anim;
    [SerializeField] GameObject _interactUI;
    private bool _isActive = false;
    private GameObject _player;
    void Start()
    {
        
    }

    private void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space"))
            {
                _player.SetActive(false);
                _interactUI.SetActive(false);

                CameraManager._instance.ChangeCam(CameraType.ViewCam);
                _anim.Play();

                _isActive = false;
            }
        }
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
