using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTestTEst : MonoBehaviour
{
    [SerializeField] Animation _anim;
    [SerializeField] GameObject _interactUI;
    private bool _isActive = false;
    void Start()
    {
        
    }

    private void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space"))
            {
                GameManager._instance.Player.SetActive(false);
                _interactUI.SetActive(false);

                CameraManager._instance.SetVirtualCam();
                _anim.Play();
            }
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
