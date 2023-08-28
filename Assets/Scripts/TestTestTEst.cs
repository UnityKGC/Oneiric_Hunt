using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTestTEst : MonoBehaviour
{
    [SerializeField] Animation _anim;
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButton("Space"))
            {
                CameraManager._instance.SetVirtualCam();
                other.gameObject.SetActive(false);
                _anim.Play();
            }
        }
    }
}
