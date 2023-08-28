using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteractUI : MonoBehaviour
{
    Transform _cam;
    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] GameObject[] _images;
    [SerializeField] private bool _isMobile;
    public void Init(string text)
    {
        _cam = Camera.main.transform;

        _text.text = text;

        /*
        if (_isMobile)
        {
            _images[1].SetActive(true);
        }
        else
        {
            _images[0].SetActive(true);
        }*/
    }

    void Update()
    {
        transform.LookAt(_cam);
    }
}
