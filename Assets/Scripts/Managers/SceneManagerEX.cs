using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagerEX : MonoBehaviour
{
    public static SceneManagerEX _instance;

    [SerializeField] GameObject[] _images; // �ε� �̹��� �迭
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
        foreach(GameObject obj in _images) // �ε��̹��� ���� ��Ȱ��ȭ.
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
    IEnumerator LoadSceneAsync(SceneType scene) // sceneID�� SceneType���� ��ȯ �� �޴´�.
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)scene);

        int idx = Random.Range(0, 3);

        GameObject loadImg = _images[idx];

        loadImg.SetActive(true); // �ε�ȭ���� Ȱ��ȭ ��Ų��.
        Image fillimg = loadImg.transform.GetChild(0).GetComponent<Image>(); // 0��°�� Fillimg

        while (!operation.isDone) // ���μ����� �Ϸ���� ������ �ݺ�
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f); // ���ప(0.dd)�� 0.9�� ����, ������¸� Value�� �ִ´�
            fillimg.fillAmount = progressValue; // �� value�� fill�� �����Ѵ�.
            yield return null; // null ����
        }
    }
}
