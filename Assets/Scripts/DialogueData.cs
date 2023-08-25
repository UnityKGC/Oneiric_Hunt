using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType // 대사 타입
{
    None = -1,
    QuestStart, // 퀘스트 시작 시 대사
    QuestProgress,  // 퀘스트 진행 중 대사
    QuestEnd, // 퀘스트 종료 시 대사.
    ChangeScene, // Scene전환 단 하나를 위해 사용
    Normal, // 기본
}

[System.Serializable]
public class DialogueData
{
    // 1. 대화참여하는 NPC리스트 2. 대사 리스트 3. NPC마다 지니는 대사 index 4. 마지막대사인지?

    // public int _npcID;
    public string _name; // NPC 이름

    public DialogueType _dialogueType; // 대사 타입

    public List<string> _dialogueLines; // 대사 리스트

    public int _index; // 몇 번째 대사인지

    public bool _isStart; // 대화를 시작했는지
    public bool _isLast; // 마지막 대사인지
    public bool _isFinish; // 대화를 끝냈는지
}
