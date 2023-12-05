using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialType
{
    None = -1,
    Event_1,
    Event_2,
    Event_3,
    BossEvt,
}
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager _instance;

    [SerializeField]
    List<GameObject> _tutorialList = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
    }
    public void EnableObject(TutorialType type)
    {
        _tutorialList[(int)type].SetActive(true);
    }
}
