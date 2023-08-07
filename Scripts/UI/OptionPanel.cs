using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public List<GameObject> _optionTabLst; // 옵션 탭(버튼) 리스트

    public List<GameObject> _optionPanelLst; // 옵션 파넬(사운드, 그래픽 등) 리스트

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
                _optionTabLst[i].transform.SetAsLastSibling(); // 버튼을 가장 아래쪽으로 보내, 그 버튼만 앞으로 튀어나오게 만든다 => 선택한 것 처럼 보이도록
                _optionPanelLst[i].SetActive(true);
            }
            else
            {
                _optionPanelLst[i].SetActive(false);
                _optionTabLst[i].transform.SetAsFirstSibling(); // 선택되지 않은 버튼은 위쪽으로 보냄
            }
        }
    }
}
