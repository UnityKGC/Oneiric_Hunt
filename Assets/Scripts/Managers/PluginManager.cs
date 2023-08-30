using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginManager : MonoBehaviour
{
    public static PluginManager _instance;
#if UNITY_ANDROID
    private AndroidJavaObject m_AndroidJavaObject = null;
    private AndroidJavaObject m_ActivityInstance = null;
#elif UNITY_IOS
#endif
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                m_ActivityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            m_AndroidJavaObject = new AndroidJavaObject("com.sbsgame.unityandroidplugin.Plugin");
            Debug.LogWarning("PluginManager.AndroidJavaObject : " + m_AndroidJavaObject);

            m_AndroidJavaObject.Call("SetAcivity", m_ActivityInstance);
        }
    }
    public void GetExitBox()
    {
        m_AndroidJavaObject.Call("GetExitMessage");
    }
}
