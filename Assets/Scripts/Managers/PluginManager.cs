using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinformPlugin;
public class PluginManager : MonoBehaviour
{
    public static PluginManager _instance;

    private AndroidJavaObject m_AndroidJavaObject = null;
    private AndroidJavaObject m_ActivityInstance = null;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                m_ActivityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            m_AndroidJavaObject = new AndroidJavaObject("com.example.kgcandroidplugin.Plugin");
            Debug.LogWarning("PluginManager.AndroidJavaObject : " + m_AndroidJavaObject);

            m_AndroidJavaObject.Call("SetAcivity", m_ActivityInstance);
        }
    }
    public void GetToastMessage(string msg = null)
    {
        if (msg == null) return;

        if(m_ActivityInstance != null && m_AndroidJavaObject != null)
        {
            m_ActivityInstance.Call("runOnUiThread", new AndroidJavaRunnable(() =>  // 이 runOnUiThread가 중요함. UiThread는 안드로이드에서 관리하므로, 실행시켜 줘야 한다
            {
                m_AndroidJavaObject.Call("GetToastMessage", msg);
            }));
        }
    }
    public void GetExitBox()
    {
        m_AndroidJavaObject.Call("GetExitMessage");
    }
    public void GetExitWinMessageBox() // 외부에서 이 함수를 호출하면, 윈도우 메시지 박스가 호출된다.
    {
        Plugin.MessageMsg("정말로 종료하시겠습니까?", "종료 메시지", Application.Quit); // 확인을 누르면, 0번째 action이 Invoke되고, 닫기를 누르면, 1번째가 Invoke된다.
    }

}
