using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyPanel : MonoBehaviour
{
    public List<GameObject> _encyTabLst; // �ɼ� ��(��ư) ����Ʈ

    public List<GameObject> _encyPanelLst; // �ɼ� �ĳ�(����, �׷��� ��) ����Ʈ

    void Start()
    {
        _encyTabLst[0].transform.SetAsLastSibling();
        _encyPanelLst[0].SetActive(true);
    }

    void Update()
    {

    }
    public void ClickBtn(int idx)
    {
        for (int i = 0; i < _encyTabLst.Count; i++)
        {
            if (i == idx)
            {
                _encyTabLst[i].transform.SetAsLastSibling(); // ��ư�� ���� �Ʒ������� ����, �� ��ư�� ������ Ƣ����� ����� => ������ �� ó�� ���̵���
                _encyPanelLst[i].SetActive(true);
            }
            else
            {
                _encyPanelLst[i].SetActive(false);
                _encyTabLst[i].transform.SetAsFirstSibling(); // ���õ��� ���� ��ư�� �������� ����
            }
        }
    }
}
