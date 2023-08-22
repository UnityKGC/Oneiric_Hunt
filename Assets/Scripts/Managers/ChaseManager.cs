using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    public static ChaseManager _instance;

    public enum ChaseState
    {
        None = -1,
        Normal,
        QTE,
        Catch,
    }
    public ChaseState Chasestate
    {
        get { return _chaseState; }
        set
        {
            _chaseState = value;
            UIManager._instacne.SetChaseState(_chaseState); // UI���� ���ӻ��� ��ȭ�ߴٰ� �˸�.
        }

    }

    public bool ChasePlayerDie { get { return _isChasePlayerDie; } set { _isChasePlayerDie = value; } }

    private bool _isChasePlayerDie = false;

    private ChaseState _chaseState;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        _isChasePlayerDie = false;
    }
}
