using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInteractUI : MonoBehaviour
{
    Transform _cam;
    [SerializeField] TextMeshProUGUI _text;
    public void Init(string text)
    {
        _cam = Camera.main.transform;

        _text.text = text;
    }

    void Update()
    {
        transform.LookAt(_cam);
    }
}
