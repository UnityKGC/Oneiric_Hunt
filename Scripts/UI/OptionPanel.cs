using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public List<GameObject> _optionTabLst; // �ɼ� ��(��ư) ����Ʈ

    public List<GameObject> _optionPanelLst; // �ɼ� �ĳ�(����, �׷��� ��) ����Ʈ

    void Start()
    {
        _optionTabLst[0].transform.SetAsLastSibling();
        _optionPanelLst[0].SetActive(true);
    }

    void Update()
    {
        
    }
    public void ClickBtn(int idx)
    {
        for(int i = 0; i < _optionTabLst.Count; i++)
        {
            if (i == idx)
            {
                _optionTabLst[i].transform.SetAsLastSibling(); // ��ư�� ���� �Ʒ������� ����, �� ��ư�� ������ Ƣ����� ����� => ������ �� ó�� ���̵���
                _optionPanelLst[i].SetActive(true);
            }
            else
            {
                _optionPanelLst[i].SetActive(false);
                _optionTabLst[i].transform.SetAsFirstSibling(); // ���õ��� ���� ��ư�� �������� ����
            }
        }
    }
}
