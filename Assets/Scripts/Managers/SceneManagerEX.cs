using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    public static SceneManagerEX _instance;

    public enum SceneType
    {
        None = -1,
        Title,
        FirstHouseScene,
        FirstDreamScene,
        MiniGame,
        Chase,
    }

    public enum PortalType
    {
        ExitPortal,
    }

    private void Awake()
    {
        _instance = this;
    }

    public SceneType NowScene { get { return _nowScene; } set { _nowScene = value; } }

    private SceneType _nowScene = SceneType.None;

    [SerializeField] private FirstDreamScene _F_D_S;

    public void LoadScene(SceneType scene)
    {
        SceneManager.LoadScene((int)scene);
    }
    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }
    public void EnablePotal(SceneType sceneType, PortalType portalType)
    {
        _nowScene = sceneType;
        switch(_nowScene)
        {
            case SceneType.FirstDreamScene:
                _F_D_S.EnablePortal(portalType);
                break;
        }
    }
}
