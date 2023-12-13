using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPhaseShader : MonoBehaviour
{
    [SerializeField] Material _normalMat;
    [SerializeField] Material _phaseMat;

    [SerializeField] bool _isChanged = false;

    [SerializeField] float _phaseValue = 0f; // 0 ~ 3.5±îÁö
    private float _phaseMaxValue = 3.5f;
    Renderer _rend;
    void Start()
    {
        _rend = GetComponent<Renderer>();

        StartCoroutine(PhaseCo());
    }
    IEnumerator PhaseCo()
    {
        _rend.material = _phaseMat;

        while (_phaseValue < _phaseMaxValue)
        {
            _rend.material.SetFloat("_SplitValue", _phaseValue);
            _phaseValue += 0.02f;
            yield return new WaitForEndOfFrame();
        }

        _rend.material = _rend.material = _normalMat;
    }
}
