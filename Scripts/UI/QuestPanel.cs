using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    public TextMeshProUGUI _questTitle;
    public TextMeshProUGUI _questContent;
    public string _questContentDefault;
    public string _questContentCount;

    void Start()
    {
        UIManager._instacne._questDataEvt -= GetQuestData;
        UIManager._instacne._questDataEvt += GetQuestData;
        UIManager._instacne._questContentEvt -= UpdateContent;
        UIManager._instacne._questContentEvt += UpdateContent;
    }

    void GetQuestData(List<string> lst) // 플레이어가 퀘스트를 받으면 UI매니저에게 퀘스트 받았다고 전달 => UI매니저를 퀘스트 매니저에게 정보달라고 하고 나에게 그 정보를 전달
    {
        // 0번째 : 제목
        // 1번째 : 내용 텍스트
        // 2번째 : 내용 수치
        _questTitle.text = lst[0];
        _questContentDefault = lst[1];
        _questContentCount = lst[2];

        _questContent.text = _questContentDefault + _questContentCount;
    }
    void UpdateContent(List<string> lst)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < lst.Count; i++)
        {
            stringBuilder.AppendLine(lst[i]);
        }
        _questContent.text = _questContentDefault + stringBuilder.ToString();
    }
    private void OnDestroy()
    {
        UIManager._instacne._questDataEvt -= GetQuestData;
    }
}
