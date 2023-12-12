using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    private Object obj;

    void Start()
    {
        obj = GetComponent<Object>();

        obj._checkPlayerEvt -= CheckPlayerEvt;
        obj._checkPlayerEvt += CheckPlayerEvt;

        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = true;
    }
    

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlineScript>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }

    // 플레이어가 주변에 들어왔다는 정보를 오브젝트로 부터 전달받으면, true로 전환 후, 활성화.
    void CheckPlayerEvt(bool value)
    {
        outlineRenderer.enabled = value;
    }
}
