using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager _instance;
    // 전투가 시작될 때, 사용하는 기능들
    // 1. 플레이어의 상태를 전투시작으로 변경
    // 2. 전투가 끝나면, 다시 기본상태로 변경

    public bool _isBattle = false;
    private bool _isLastBattle = false;
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
    public void StartBattle(bool isLastBattle = false) // Scene으로 부터 전투 시작 메시지를 받음
    {
        // 플레이어 상태 변경 등등
        _isBattle = true;

        _isLastBattle = isLastBattle;

        GameManager._instance.Playstate = GameManager.PlayState.Dream_Battle;

        Debug.Log("전투 시작");
    }
    public void EndBattle() // 몬스터를 다 해치워 전투가 끝날 시
    {
        _isBattle = false;

        SkillManager._instance.EndBattle(); // 스킬이 남아있으면 스킬 파괴 및 스킬 변수 초기화
        UIManager._instacne.EndBattle(); // 스킬 UI 초기화

        GameManager._instance.Playstate = GameManager.PlayState.Dream_Normal;

        if(_isLastBattle)
        {
            // 포탈 활성화 => 포탈은 누가 관리하고 있지? => Scene이, 그럼 Scene에게 너가 지니고 있는 포탈을 활성화 시켜라! 라고 전달해야 하는데, 그걸 어떻게 구현할까?
            // 사실, BattleManager에 Scene을 지니고 있으면 간단하게 되는 일이지만, 좀... 그렇다...!
            // 그냥 SceneManager나 만들어 주자. 차피 포탈은 Scene마다 존재하는 오브젝트니까...!
            SceneManagerEX._instance.EnablePotal(SceneManagerEX.SceneType.FirstDreamScene, SceneManagerEX.PortalType.ExitPortal);
        }
        Debug.Log("전투 끝");
    }
    private void OnDestroy()
    {
        _instance = null;
        _isLastBattle = false;
        _isBattle = false;
    }
}
