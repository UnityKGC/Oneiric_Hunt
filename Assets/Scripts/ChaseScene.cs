using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    // �÷��̾ �׾����� �Ǵ�
    // ���������� �������� �Ǵ�

    void Start()
    {
        SceneManagerEX._instance.NowScene = SceneManagerEX.SceneType.Chase;
        GameManager._instance.Playstate = GameManager.PlayState.Real_Normal;
        UIManager._instacne.SetSceneUI(UIManager.SceneUIState.Tutorial);
    }

    void Update()
    {
           
    }
}
