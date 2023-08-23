using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager _instance;

    public bool IsMove { get { return _isMove; } set { _isMove = value; } } // �̵� ���ΰ�

    private bool _isMove;

    public bool IsAttack { get { return _isAttack; } set { _isAttack = value; } } // ���� ���ΰ�
    private bool _isAttack;

    public bool IsSkill { get { return _isSkill; } set { _isSkill = value; } } // ��ų ��� ���ΰ�
    private bool _isSkill;

    [SerializeField] List<GameObject> _playerLst; // �÷��̾� ����Ʈ (������ ���� ���� Enum�� ������ ��ġ)
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
    public void ChangePlayer(GameManager.PlayState playState, Vector3 pos, Quaternion rot) // ������ ���¿� ���� ��ȯ�Ǵ� �÷��̾ ���� �ٸ���.
    {
        //Instantiate(_playerLst[(int)playState], pos, rot);

        switch (playState)
        {
            case GameManager.PlayState.Dream_Normal:
                
                break;
            case GameManager.PlayState.Dream_Battle:

                break;
            case GameManager.PlayState.Real_Normal:

                break;
            case GameManager.PlayState.Real_Event:

                break;
            case GameManager.PlayState.MiniGame:

                break;
        }
    }
}
