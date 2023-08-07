using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public int _monsterID; // �׿��� �ϴ� ������ ID

    public int _totalCount; // �׿��� �ϴ� ������ ��
    public int _nowCount; // �÷��̾ ���� ���� ������ ��

    public bool _isFull; // �÷��̾ ���͸� �� �׿�����
}

[System.Serializable]
public class KilledMonsterQuestData : QuestData
{
    public List<MonsterData> _monsterLst; // �����;� �ϴ� ������Ʈ ����Ʈ
}
