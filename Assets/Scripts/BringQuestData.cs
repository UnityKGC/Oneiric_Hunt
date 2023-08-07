using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class ObjectData
{
    public int _objID; // �����;� �ϴ� ������Ʈ id;

    public int _totalCount; // �����;� �� ���� ����
    public int _nowCount; // �÷��̾ ���� ���ϰ� �ִ� �����;� �� ������ ����

    public bool _isFull; // �÷��̾ �� ������Ʈ�� �� ��������
}
[System.Serializable]
public class BringQuestData : QuestData
{
    public List<ObjectData> _objLst; // �����;� �ϴ� ������Ʈ ����Ʈ

}
