using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class ObjectData
{
    public int _objID; // 가져와야 하는 오브젝트 id;

    public int _totalCount; // 가져와야 할 물건 개수
    public int _nowCount; // 플레이어가 현재 지니고 있는 가져와야 할 물건의 개수

    public bool _isFull; // 플레이어가 이 오브젝트를 다 가졌는지
}
[System.Serializable]
public class BringQuestData : QuestData
{
    public List<ObjectData> _objLst; // 가져와야 하는 오브젝트 리스트

}
