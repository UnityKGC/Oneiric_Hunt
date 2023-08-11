using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager _instacne;

    public enum SceneUIState
    {
        None = -1,
        Pause,
        Play,
        GameOver,
        Tutorial,
    }


    public Action<QuestData> _questDataEvt = null; // ����Ʈ UI�� ��Ͻ�Ű�� ���� ����ϴ� �ݹ�
    public Action<QuestData> _questContentEvt = null; // ����Ʈ ������ ���Ž�Ű�� ���� ����ϴ� �ݹ�
    public Action<QuestData> _questFinishEvt = null; // ����Ʈ ������ �Ϸ��Ű�� ���� ����ϴ� �ݹ�

    public Action<float> _hpEvt = null; // �÷��̾� HP���� �̺�Ʈ

    public Action<GameManager.PlayState> _playStateEvt = null; // ������ ���� ���� ����

    public Action<SkillScriptable, SkillManager.Skills> _skillEvt = null; // ��ų ��ũ���ͺ��� �̿��Ͽ� ��ų ��Ÿ�� Ȯ��
    public Action<WeaponType> _weaponEvt = null; // ���� ���� �� ȣ�� => ���� ���⸦ �˷���
    public Action _endBattleEvt = null; // ������ ������ ��,

    public Action _qtePosEvt = null; // QTE�� ��ġ�� �����Ѵ�.

    public SceneUIState SceneUI { get { return _sceneUIState; } set { _sceneUIState = value; } }

    [SerializeField] private SceneUIState _sceneUIState = SceneUIState.None; // �̰� Scene�� ������ �� Scene�����ڰ� �������ش�.
    
    public List<GameObject> _sceneUILst;

    public Stack<GameObject> _popupUIStack = new Stack<GameObject>();

    bool _isGameOver = false; // ���ӿ������� �ƴ���
    private void Awake()
    {
        _instacne = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log("Stack ũ�� : "+_popupUIStack.Count);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Popup�޴��� ����������, ���� �ֱٿ� ���� PopupUI ������� ���ش�. STack���� Popup����
            // Ȥ�ó� �ٷ� �������� �����ϸ�, Popup�� ���ִ� ��� ������ �ʱ�ȭ
            // �����ִ� Popup�޴��� ������ PauseUI�� Ű�ų� ���ش�.
            // Title�� 
            if(_popupUIStack.Count > 0)
            {
                ClosePopupUI();
            }
            else if (!_isGameOver && _sceneUIState != SceneUIState.None)
                SetSceneUI(SceneUIState.Pause);
        }
    }

    #region Scene ���÷���
    public void SetSceneUI(SceneUIState state) // SceneState ����
    {
        AllClosePopupUI(); // Ȥ�ó� �����ִ� ��� PopupUI�� ���ش�. => �����ʿ� �ִ��� �ٽ� �Ǵ��ϱ� �ٶ� KGC

        if (state == SceneUIState.None)
            return;

        if (SceneUI == state && _sceneUILst[(int)SceneUI].activeSelf) // ��, �����ִ� �޴����, Play�� ����
            state = SceneUIState.Play;

        switch(state)
        {
            case SceneUIState.Pause: // Pause�޴� ȣ��
            case SceneUIState.Tutorial: // Tutorial�޴� ȣ��
                Time.timeScale = 0f; // �ð� ����!
                break;
            case SceneUIState.Play: // �̴ϸ�, ��� ȣ��
            case SceneUIState.None: // �̴ϸ�, ��� ȣ��
                Time.timeScale = 1f;
                break;
            case SceneUIState.GameOver: // ���� ���� ȣ��
                _isGameOver = true;
                Time.timeScale = 0f;
                break;
        }

        ActiveSceneUI(state);
        SetCursor(state);
        SceneUI = state;
    }

    void ActiveSceneUI(SceneUIState state) // SceneUIȰ��ȭ
    {
        for (int i = 0; i < _sceneUILst.Count; i++)
        {
            if (state == (SceneUIState)i && !_sceneUILst[i].activeSelf) // state��° SceneUI�� �����ִٸ� Ų��.
            {
                _sceneUILst[i].SetActive(true);
            }
            else
                _sceneUILst[i].SetActive(false);
        }
    }

    public void StartQuest(QuestData data) // ����Ʈ �Ŵ����� ���� ������ ����Ʈ�� ������ �޴´�.
    {
        if(_questDataEvt != null)
            _questDataEvt.Invoke(data);
    }
    public void UpdateQuestContent(QuestData data) // ����Ʈ �Ŵ����� ���� ����Ʈ ������ ������ �޴´�.
    {
        if (_questContentEvt != null)
            _questContentEvt.Invoke(data);
    }
    public void FinishQuest(QuestData data) // ����Ʈ �Ŵ����� ���� ����Ʈ ������ ������ �޴´�.
    {
        if (_questFinishEvt != null)
            _questFinishEvt.Invoke(data);
    }
    public void SetPlayState(GameManager.PlayState playState) // ���ӸŴ����� ���� ���ӻ��º�ȭ�� ��ȭ�� ���¸� �޴´�.
    {
        _playStateEvt?.Invoke(playState); // �Ѹ���.
    }

    public void SetPlayerHP(float value) // ���� �Լ��� ���� ������ �����ΰ�? => ���� �� �Լ��� �����ϰ� �ִ��� �ѹ��� Ȯ���� �� �־� ã�ư��� ���ϴ�.
    {
        _hpEvt?.Invoke(value); // ?. ��, _hpEvt�� null�Ͻ� null ����, ���� ������ �� Invoke���� => ���׿����� : A>B ? 10 : 20 => A>B������ true�� 10 �ƴϸ� 20�̶�� ��
    }
    public void SetSkillUI(SkillScriptable scriptable, SkillManager.Skills skill)
    {
        _skillEvt?.Invoke(scriptable, skill);
    }
    public void SetWeapon(WeaponType weapon)
    {
        _weaponEvt?.Invoke(weapon);
    }
    public void EndBattle() // ���� ���� => ��ų UI���� �ʱ�ȭ
    {
        _endBattleEvt?.Invoke();
    }

    public void SetQTEPosEvt() // QTE�̺�Ʈ�� ����� ��, Trigger���� QTEUI�� ��ġ�� ��ġ�� ������.
    {
        _qtePosEvt?.Invoke();
    }
    #endregion

    #region Popup ���÷���
    public void SetPopupUI(GameObject popupUI) // PopupUI�� ������ ��, UIManager�� �ش� PopupUI�� �����ϴ� popupUIStack�� �������
    {
        _popupUIStack.Push(popupUI);
    }
    public void ClosePopupUI() // ���� �ֱٿ� ���� PopupUI�� �ݴ´�.
    {
        if (_popupUIStack.Count <= 0) // popup stack�� �����Ͱ� ������ ����
            return;

        GameObject obj = _popupUIStack.Pop();

        obj.transform.DOScale(0, 0f).SetUpdate(true); // ��Ʈ�� => �������� �۰� ����

        obj.SetActive(false); // �ش� popupUI�� ���� ��, ��Ȱ��ȭ ��Ų��.
                                              // => �Ŀ� Popup UI�� �θ� ����, ClosePopupUI�� ȣ���ϰ� ������ �ڴ�?
                                              // => ��? �ɼǰ��� ����, ��ġ�� �ٲٸ�, ����� ��ġ�� �ֽ��ϴ�. �����Ű�ڽ��ϱ�? ��� ��� UI�� ȣ����Ѿ� �Ѵ�. => ��! �� popupUI���� Close�� �� ȣ���ϴ°� �ٸ���, �������� ���� �Լ��� ����� �ְ�, ���⿡�� ȣ���Ű�� �ؾ� �Ѵ�.
    }
    public void AllClosePopupUI() // ���� ���� ��� PopupUI�� �ݾ��ش�.
    {
        while(_popupUIStack.Count > 0) // popupUI ������ ������ 0�� �ɶ����� ClosePopupUI�� ����
            ClosePopupUI();

        _popupUIStack.Clear(); // popupStack �ʱ�ȭ => Ȥ�ó� �𸣴ϱ�
    }
    #endregion

    void SetCursor(SceneUIState state) // UIScene ���¿� ���� Ŀ�� ����
    {
        switch (state)
        {
            case SceneUIState.None:
            case SceneUIState.Pause:
            case SceneUIState.GameOver:
            case SceneUIState.Tutorial:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case SceneUIState.Play:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }

    }
    private void OnDestroy() // Scene��ȯ�� ��, 
    {
        _sceneUILst.Clear(); // ������ ������Ʈ�� ���� �ı��Ǿ����Ƿ�, �����͸� ����ִ� �ڷᱸ���� Clear�����ش�.
        _popupUIStack.Clear();
        SetSceneUI(SceneUIState.Play); // UI���� ä�� ���� �� �����ϱ�, PlaySceneUI�� ���ش�

        _questDataEvt = null;
        _questContentEvt = null;
        _questFinishEvt = null;
        _hpEvt = null;
        _skillEvt = null;
        _playStateEvt = null;
        _weaponEvt = null;
        _endBattleEvt = null;

    }
}
