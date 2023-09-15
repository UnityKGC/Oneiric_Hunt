using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagerEX : MonoBehaviour
{
    public static SceneManagerEX _instance;

    [SerializeField] GameObject[] _images; // 로딩 이미지 배열
    public enum SceneType
    {
        None = -1,
        Title,
        FirstHouseScene,
        PlayerOfficeScene,
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
    private void Start()
    {
        foreach(GameObject obj in _images) // 로딩이미지 전부 비활성화.
        {
            obj.SetActive(false);
        }
    }
    public SceneType NowScene { get { return _nowScene; } set { _nowScene = value; } }

    private SceneType _nowScene = SceneType.None;

    [SerializeField] private FirstDreamScene _F_D_S;

    public void LoadScene(SceneType scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
        //SceneManager.LoadScene((int)scene);
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
    IEnumerator LoadSceneAsync(SceneType scene) // sceneID를 SceneType으로 변환 후 받는다.
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)scene);

        int idx = Random.Range(0, 3);

        GameObject loadImg = _images[idx];

        loadImg.SetActive(true); // 로딩화면을 활성화 시킨다.
        Image fillimg = loadImg.transform.GetChild(0).GetComponent<Image>(); // 0번째가 Fillimg

        while (!operation.isDone) // 프로세스가 완료되지 않으면 반복
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f); // 진행값(0.dd)을 0.9로 나눠, 진행상태를 Value에 넣는다
            fillimg.fillAmount = progressValue; // 그 value를 fill에 기입한다.
            yield return null; // null 리턴
        }
    }
}
