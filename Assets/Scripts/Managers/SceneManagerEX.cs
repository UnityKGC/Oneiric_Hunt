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
        int idx = Random.Range(0, 3);

        GameObject loadImg = _images[idx];

        loadImg.SetActive(true); // �ε�ȭ���� Ȱ��ȭ ��Ų��.

        Image fillimg = loadImg.transform.GetChild(0).GetComponent<Image>(); // 0��°�� Fillimg

        AsyncOperation operation = SceneManager.LoadSceneAsync((int)scene);

        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!operation.isDone) // ���μ����� �Ϸ���� ������ �ݺ�
        {
            yield return null;

            timer += Time.deltaTime;

            if (operation.progress < 0.9f)
            {
                fillimg.fillAmount = Mathf.Lerp(fillimg.fillAmount, operation.progress, timer);
                if (fillimg.fillAmount >= operation.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                fillimg.fillAmount = Mathf.Lerp(fillimg.fillAmount, 1f, timer);
                if (fillimg.fillAmount == 1.0f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
