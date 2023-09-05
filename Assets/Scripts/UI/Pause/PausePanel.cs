using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PausePanel : MonoBehaviour
{
    void OnEnable()
    {
        transform.DOMoveX(0, 0.3f).SetUpdate(true);
    }
    private void OnDisable()
    {
        transform.DOMoveX(-500, 0f).SetUpdate(true);
    }

}
