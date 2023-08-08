using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    public TextMeshProUGUI _questTitle;
    public TextMeshProUGUI _questContentText;
    public TextMeshProUGUI _questObjContent;

    StringBuilder sb = new StringBuilder();

    void Start()
    {
        UIManager._instacne._questDataEvt -= GetQuestData;
        UIManager._instacne._questDataEvt += GetQuestData;

        UIManager._instacne._questContentEvt -= UpdateContent;
        UIManager._instacne._questContentEvt += UpdateContent;

        UIManager._instacne._questFinishEvt -= FinishQuest;
        UIManager._instacne._questFinishEvt += FinishQuest;

        gameObject.SetActive(false); // 자기자신을 비활성화 함
    }

    void GetQuestData(QuestData data) // 플레이어가 퀘스트를 받으면 UI매니저에게 퀘스트 받았다고 전달 => UI매니저를 퀘스트 매니저에게 정보달라고 하고 나에게 그 정보를 전달
                                            // 왜 데이터를 받나? 매니저가 데이터 받은 후, 정보 세공하고 그 정보만 여기에 주면 되지 않나? => string만 있으면 상관없는데, List<string>도 받아야 해서... 그냥 데이터 자체를 받도록 만듬
    {
        gameObject.SetActive(true); // 자기자신을 활성화 함

        _questTitle.text = data._questName;

        _questContentText.text = data._questContentText;

        for(int i = 0; i < data._objLst.Count; i++)
        {
            ObjectData od = data._objLst[i];
            sb.Append(od._objName + " " + od._nowCount + " / " + od._totalCount + "\n");
        }
        _questObjContent.text = sb.ToString();
        sb.Clear(); // stringbuilder 초기화
    }
    void UpdateContent(QuestData data) // 퀘스트 오브젝트 내용 갱신
    {
        for (int i = 0; i < data._objLst.Count; i++)
        {
            ObjectData od = data._objLst[i];
            sb.Append(od._objName + " " + od._nowCount + " / " + od._totalCount + "\n");
        }

        _questObjContent.text = sb.ToString();
        sb.Clear(); // stringbuilder 초기화
    }
    void FinishQuest(QuestData data) // 퀘스트 UI 초기화
    {
        _questTitle.text = "";
        _questContentText.text = "";
        _questObjContent.text = "";

        gameObject.SetActive(false); // 자기자신을 비활성화 함
    }
    private void OnDestroy()
    {
        UIManager._instacne._questDataEvt -= GetQuestData;
        UIManager._instacne._questContentEvt -= GetQuestData;
        UIManager._instacne._questFinishEvt -= GetQuestData;
    }
}
