using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    // �÷��̾ �׾����� �Ǵ�
    // ���������� �������� �Ǵ�

    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.Chase;
    }

    void Update()
    {
           
    }
}
