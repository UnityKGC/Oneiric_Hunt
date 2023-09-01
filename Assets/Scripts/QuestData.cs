using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    None = -1,
    BringObject, // ���� ��������
    FindClue, // �ܼ� ã��
    KillMonster, // ���� ��ġ
    KillBossMonster,
    InteractionObject, // ��ȣ�ۿ� �ϱ�.(NPC�� ��ȭ�ϱ�, ������Ʈ ���, �б�, ����)
    GoToPosition, // �ش� ��ġ�� �̵��ϱ�.
    Max,
}

[System.Serializable]
public class TriggerData
{
    public int _objID; // �����ؾ� �� objID

    public string _objName; // ������ �Ŵ��� ���� ������ _objID�� ���� ������Ʈ ������ �������� �ؾ��ϳ�

    public bool _isFinish; // �ش� ������Ʈ�� Trigger�ߴ���,
}
[System.Serializable]
public class ObjectData
{
    public int _objID; // �����ؾ� �� objID

    public string _objName; // ������ �Ŵ��� ���� ������ _objID�� ���� ������Ʈ ������ �������� �ؾ��ϳ�

    public int _totalCount; // �����ؾ� �� ��ü ����
    public int _nowCount; // �÷��̾ ������ ���� ����

    public bool _isFull; // �÷��̾ ��� �� �ߴ���
}

[System.Serializable]
public class QuestData // �Ŀ�, ObjectData�� TriggerData�� QuestData�� ��ӹް� �ϴ°͵� ������ ��?
{
    public int _questID; // ����Ʈ ID

    public QuestType _questType; // ����Ʈ Ÿ��

    public string _questName; // ����Ʈ �̸�

    public string _questContentText;

    public List<DialogueData> _dialogueData; // �� ����Ʈ�� ���̾�α� ���

    public List<ObjectData> _objLst; // �����ؾ� �� ����Ʈ�� ������Ʈ ����Ʈ

    public List<TriggerData> _triggerDatas; // Ư���� ���Ǿ��� Ŭ���� �Ǵ� ����Ʈ ������.

    public QuestPreceds _preced; // ����Ʈ ���� ��, ���� ����.
    public QuestRewards _reward; // ����Ʈ ����

    public bool _isStart; // ����Ʈ�� �����ߴ���,

    public bool _isAchieve; // ����Ʈ �޼� ������ �����ߴ���, 

    public bool _isFinish; // ����Ʈ�� ���´���,

}

[Flags]
public enum RewardType
{
    None = -1,
    Exp = 1 << 0,
    Gold = 1 << 1,
    Object = 1 << 2,
    Collider = 1 << 3,
    Dialogue = 1 << 4,
    PlayType = 1<< 5,
    DisableObj = 1 << 6,
    ChangeScene = 1 << 7,
}

[System.Serializable]
public class QuestRewards
{
    public RewardType _type;
    public List<GameObject> _objLst; // ����Ʈ �Ϸ� �� Ư�� ������Ʈ Ȱ��ȭ
    public List<GameObject> _disAbleObjLst;
    public Collider _coll; // ����Ʈ �Ϸ� �� �ݶ��̴� Ȱ��ȭ => �̰� �ӽ� => �÷��̾� ������ �� ���� �ʿ�
    public DialogueData _dialogue;
    public SceneManagerEX.SceneType _ToScene; // �̵��ϰ��� �ϴ� Scene
    public GameManager.PlayState _playerState;
    public int _gold;
    public float _exp;

}

[Flags]
public enum PrecedType
{
    None = -1,
    Exp = 1 << 0,
    Gold = 1 << 1,
    Object = 1 << 2,
    Collider = 1 << 3,
    Dialogue = 1 << 4,
    PlayType = 1 << 5,
    DisableObj = 1 << 6,
    ChangeScene = 1 << 7,
}

[System.Serializable]
public class QuestPreceds
{
    public PrecedType _type;
    public List<GameObject> _objLst; // ����Ʈ �Ϸ� �� Ư�� ������Ʈ Ȱ��ȭ
    public List<GameObject> _disAbleObjLst;
    public Collider _coll; // ����Ʈ �Ϸ� �� �ݶ��̴� Ȱ��ȭ => �̰� �ӽ� => �÷��̾� ������ �� ���� �ʿ�
    public DialogueData _dialogue; // ����Ʈ ���� ��, ���̾�αװ� �ʿ��� ��
    public SceneManagerEX.SceneType _ToScene; // �̵��ϰ��� �ϴ� Scene
    public GameManager.PlayState _playerState;
    public int _gold;
    public float _exp;
    
}