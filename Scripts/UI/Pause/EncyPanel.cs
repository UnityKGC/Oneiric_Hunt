using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyPanel : MonoBehaviour
{
    public List<GameObject> _encyTabLst; // 옵션 탭(버튼) 리스트

    public List<GameObject> _encyPanelLst; // 옵션 파넬(사운드, 그래픽 등) 리스트

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
                _encyTabLst[i].transform.SetAsLastSibling(); // 버튼을 가장 아래쪽으로 보내, 그 버튼만 앞으로 튀어나오게 만든다 => 선택한 것 처럼 보이도록
                _encyPanelLst[i].SetActive(true);
            }
            else
            {
                _encyPanelLst[i].SetActive(false);
                _encyTabLst[i].transform.SetAsFirstSibling(); // 선택되지 않은 버튼은 위쪽으로 보냄
            }
        }
    }
}
