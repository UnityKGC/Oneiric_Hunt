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
        //Debug.Log("�Ϲ� Vector3.up : " + Vector3.up);
        //Debug.Log("ī�޶� ȸ�� * Vector3.up : " + _cam.rotation * Vector3.up);

        transform.LookAt(transform.position + _cam.rotation * Vector3.forward, _cam.rotation * Vector3.up);
        // �켱, ������Ʈ�� ������ġ�� ī�޶� ������ ������ ���Ͽ�, ???���͸� ���� �� �ִ�.
        // �� ��, �� ���͸� ī�޶��� rotation�� Vector.up�� ���Ͽ� ȸ���������� ���Ѵ�.
    }
}
