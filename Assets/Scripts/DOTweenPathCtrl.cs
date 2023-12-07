using DG.Tweening;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public sealed class MyFloatParameter : VolumeParameter<float>
{
    public MyFloatParameter(float value, bool overrideState = false)
        : base(value, overrideState) { }

    public sealed override void Interp(float from, float to, float t)
    {
        m_Value = from + (to - from) * t;
    }
}

public class DOTweenPathCtrl : MonoBehaviour
{
    private DOTweenPath _doTweenPath;
    [SerializeField] Vector3[] _wayPoints;

    [SerializeField] Vector3 _nextWayPoint;

    [SerializeField] VolumeProfile _volumeProfile;

    Vignette _vignette;

    float _time = 0f;

    MyFloatParameter _param;

    private WaitForEndOfFrame _delayFrame = new WaitForEndOfFrame();
    void Start()
    {
        // 1에서 0.7로
        // 0.7에서 1로
        // 1에서 0으로
        // 이를 3초 동안 진행
        if (_volumeProfile.TryGet(out _vignette))
        {
            StartCoroutine(StartVignetteCo());
        }
    }
    IEnumerator StartVignetteCo()
    {
        float value = 0f;
        while (_time <= 3f)
        {
            if (_time <= 1f)
            {
                value = Mathf.Lerp(1f, 0.7f, _time);
            }
            else if (_time <= 2f)
            {
                value = Mathf.Lerp(0.7f, 1.0f, (_time - 1f));
            }
            else if (_time <= 3f)
            {
                value = Mathf.Lerp(1f, 0f, (_time - 2f));
            }

            _param = new MyFloatParameter(value);
            _vignette.intensity.SetValue(_param);
            _time += 0.02f;
            yield return _delayFrame;
        }
    }
    private void OnDestroy()
    {
        _vignette.intensity.SetValue(new MyFloatParameter(1f));
    }
}
