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

        gameObject.SetActive(false); // �ڱ��ڽ��� ��Ȱ��ȭ ��
    }

    void GetQuestData(QuestData data) // �÷��̾ ����Ʈ�� ������ UI�Ŵ������� ����Ʈ �޾Ҵٰ� ���� => UI�Ŵ����� ����Ʈ �Ŵ������� �����޶�� �ϰ� ������ �� ������ ����
                                            // �� �����͸� �޳�? �Ŵ����� ������ ���� ��, ���� �����ϰ� �� ������ ���⿡ �ָ� ���� �ʳ�? => string�� ������ ������µ�, List<string>�� �޾ƾ� �ؼ�... �׳� ������ ��ü�� �޵��� ����
    {
        gameObject.SetActive(true); // �ڱ��ڽ��� Ȱ��ȭ ��

        _questTitle.text = data._questName;

        _questContentText.text = data._questContentText;

        for(int i = 0; i < data._objLst.Count; i++)
        {
            ObjectData od = data._objLst[i];
            sb.Append(od._objName + " " + od._nowCount + " / " + od._totalCount + "\n");
        }
        _questObjContent.text = sb.ToString();
        sb.Clear(); // stringbuilder �ʱ�ȭ
    }
    void UpdateContent(QuestData data) // ����Ʈ ������Ʈ ���� ����
    {
        for (int i = 0; i < data._objLst.Count; i++)
        {
            ObjectData od = data._objLst[i];
            sb.Append(od._objName + " " + od._nowCount + " / " + od._totalCount + "\n");
        }

        _questObjContent.text = sb.ToString();
        sb.Clear(); // stringbuilder �ʱ�ȭ
    }
    void FinishQuest(QuestData data) // ����Ʈ UI �ʱ�ȭ
    {
        _questTitle.text = "";
        _questContentText.text = "";
        _questObjContent.text = "";

        gameObject.SetActive(false); // �ڱ��ڽ��� ��Ȱ��ȭ ��
    }
    private void OnDestroy()
    {
        UIManager._instacne._questDataEvt -= GetQuestData;
        UIManager._instacne._questContentEvt -= GetQuestData;
        UIManager._instacne._questFinishEvt -= GetQuestData;
    }
}
