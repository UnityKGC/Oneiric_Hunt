using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager _instance;
    // 전투가 시작될 때, 사용하는 기능들
    // 1. 플레이어의 상태를 전투시작으로 변경
    // 2. 전투가 끝나면, 다시 기본상태로 변경

    public Action<bool> _battleEvt = null; // 전투가 시작 => 해당 이벤트를 등록하고 있는 객체는 몬스터들, 즉, 몬스터는 처음엔 Idle상태였다가, 이 이벤트를 받으면 전투로 전환
    public bool _isBattle = false;
    [SerializeField] private bool _isBossBattle = false;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
    public void StartBattle(bool isBossBattle = false) // Scene으로 부터 전투 시작 메시지를 받음
    {
        // 플레이어 상태 변경 등등
        _isBattle = true;
        _isBossBattle = isBossBattle;
        if (_isBossBattle) // 보스 전투라면, 
        {
            UIManager._instacne.SetBossHP(true);// UI를 킨다.
        }
        GameManager._instance.Playstate = GameManager.PlayState.Dream_Battle;
        _battleEvt?.Invoke(true); // 이벤트를 구독한 객체가 존재한다면 Invoke
        //Debug.Log("전투 시작");
    }
    public void EndBattle(bool isBossBattle = false) // 몬스터를 다 해치워 전투가 끝날 시
    {
        _isBattle = false;

        SkillManager._instance.EndBattle(); // 스킬이 남아있으면 스킬 파괴 및 스킬 변수 초기화
        UIManager._instacne.EndBattle(); // 스킬 UI 초기화
        CameraManager._instance.EndEffectAction();

        if(_isBossBattle) // 보스 전투라면
        {
            UIManager._instacne.SetBossHP(false); // UI를 끈다.
            UIManager._instacne.GameEnd();
            _isBossBattle = false; // 보스 전투 분별 초기화
        }

        _battleEvt?.Invoke(false); // 이벤트를 구독한 객체가 존재한다면 Invoke
        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;
    }
    private void OnDestroy()
    {
        _instance = null;
        _isBattle = false;
    }
}
