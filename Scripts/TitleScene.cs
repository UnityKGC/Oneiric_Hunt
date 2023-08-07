using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // 외부에서 UI를 이용해 로비로 돌아오면, 마우스 커서가 안보일수도 있으므로, 보이도록 만듬
    }

    void Update()
    {
        
    }
}
