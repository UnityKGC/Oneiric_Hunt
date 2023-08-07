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

    void GetQuestData(List<string> lst) // �÷��̾ ����Ʈ�� ������ UI�Ŵ������� ����Ʈ �޾Ҵٰ� ���� => UI�Ŵ����� ����Ʈ �Ŵ������� �����޶�� �ϰ� ������ �� ������ ����
    {
        // 0��° : ����
        // 1��° : ���� �ؽ�Ʈ
        // 2��° : ���� ��ġ
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
