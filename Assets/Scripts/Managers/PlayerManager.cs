using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerOrder
{
    None = -1,
    PlayerDN,
    PlayerDB,
}
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

    GameObject _nowPlayer;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _nowPlayer = GameManager._instance.Player;
    }

    void Update()
    {
        
    }
    public void ChangePlayer(GameManager.PlayState playState, Vector3 pos, Quaternion rot) // ������ ���¿� ���� ��ȯ�Ǵ� �÷��̾ ���� �ٸ���.
    {
        //Instantiate(_playerLst[(int)playState], pos, rot);
        if (_nowPlayer == null) return; // ó������ null�̴ϱ� ����

        _nowPlayer.SetActive(false);
        GameObject player = null;
        switch (playState)
        {
            case GameManager.PlayState.Dream_Normal:
                player = _playerLst[(int)PlayerOrder.PlayerDN];
                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                
                break;

            case GameManager.PlayState.Dream_Battle:
                player = _playerLst[(int)PlayerOrder.PlayerDB];
                player.transform.position = pos;
                player.transform.rotation = rot;
                player.SetActive(true);
                GameManager._instance.Player = _nowPlayer = player;
                break;

            case GameManager.PlayState.Real_Normal:

                break;
            case GameManager.PlayState.Real_Event:

                break;
            case GameManager.PlayState.MiniGame:

                break;
        }

        CameraManager._instance.SetFreeLookCam(); // �ٲ� �÷��̾�� ī�޶� ��Ŀ��
    }
}
