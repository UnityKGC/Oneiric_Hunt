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
            m_ActivityInstance.Call("runOnUiThread", new AndroidJavaRunnable(() =>  // �� runOnUiThread�� �߿���. UiThread�� �ȵ���̵忡�� �����ϹǷ�, ������� ��� �Ѵ�
            {
                m_AndroidJavaObject.Call("GetToastMessage", msg);
            }));
        }
    }
    public void GetExitBox()
    {
        m_AndroidJavaObject.Call("GetExitMessage");
    }
    public void GetExitWinMessageBox() // �ܺο��� �� �Լ��� ȣ���ϸ�, ������ �޽��� �ڽ��� ȣ��ȴ�.
    {
        Plugin.MessageMsg("������ �����Ͻðڽ��ϱ�?", "���� �޽���", Application.Quit); // Ȯ���� ������, 0��° action�� Invoke�ǰ�, �ݱ⸦ ������, 1��°�� Invoke�ȴ�.
    }

}
