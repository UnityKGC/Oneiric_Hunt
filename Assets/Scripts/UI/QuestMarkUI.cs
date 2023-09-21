using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestMark
{
    None = -1,
    Start,
    Preced,
    Finish,
}
public class QuestMarkUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _markImages;

    private Transform _cam = null;

    private void Start()
    {
        _cam = Camera.main.transform;
    }
    public void SetQuestMark(QuestMark mark)
    {
        for(int i = 0; i < _markImages.Count; i++)
        {
            if(i == (int)mark)
                _markImages[i].SetActive(true);
            else
                _markImages[i].SetActive(false);
        }
    }
    void LateUpdate()
    {
        //Debug.Log("일반 Vector3.up : " + Vector3.up);
        //Debug.Log("카메라 회전 * Vector3.up : " + _cam.rotation * Vector3.up);

        transform.LookAt(transform.position + _cam.rotation * Vector3.forward, _cam.rotation * Vector3.up);
        // 우선, 오브젝트의 현재위치와 카메라 방향의 정면을 더하여, ???벡터를 얻을 수 있다.
        // 그 후, 그 벡터를 카메라의 rotation에 Vector.up을 곱하여 회전기준축을 정한다.
    }
}
