using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    // �÷��̾ �׾����� �Ǵ�
    // ���������� �������� �Ǵ�

    void Start()
    {
        GameManager._instance.Playstate = GameManager.PlayState.Real_Chase;
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.Chase;
    }

    void Update()
    {
           
    }
}
