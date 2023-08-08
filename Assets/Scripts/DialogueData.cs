using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType // ��� Ÿ��
{
    None = -1,
    Normal,
    QuestStart,
    QuestEnd,

}
[System.Serializable]
public class DialogueData
{
    // 1. ��ȭ�����ϴ� NPC����Ʈ 2. ��� ����Ʈ 3. NPC���� ���ϴ� ��� index 4. �������������?

    public string _name; // NPC �̸�

    public DialogueType _dialogueType; // ��� Ÿ��

    public List<string> _dialogueLines; // ��� ����Ʈ

    public int _index; // �� ��° �������

    public bool _isStart; // ��ȭ�� �����ߴ���
    public bool _isLast; // ������ �������
    public bool _isFinish; // ��ȭ�� ���´���
}
