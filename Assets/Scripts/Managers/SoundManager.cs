using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    None = -1,
    Title,
    PlayerHouse,
    PlayerOffice,
    TutorialDream,
}
public enum WeaponSound
{
    None = -1,
    SwordAtk,
    SpearAtk,
    AxeAtk,
}
public enum MoveEffectSound
{
    None = -1,
    Grass = 13,
    Wood = 14,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [SerializeField] AudioClip[] _attackClips;
    [SerializeField] AudioClip[] _bgmClips;
    [SerializeField] AudioClip[] _moveClips;

    [SerializeField] AudioSource[] _skillSoundPool; // 스킬 사운드 풀링

    [SerializeField] private AudioSource _bgmAudio;
    [SerializeField] private AudioSource _attackAudio;
    [SerializeField] private AudioSource _moveAudio;
    //[SerializeField] private AudioSource _skillAudio; // 스킬들을 스킬의 자식으로 오디오를 주고 실행시키게 한다.

    private void Awake()
    {
        _instance = this;
    }


    public void PlayBGM(BGM type)
    {
        AudioClip clip = _bgmClips[(int)type];

        if(_bgmAudio.isPlaying)
            _bgmAudio.Stop();

        _bgmAudio.clip = clip;
        _bgmAudio.Play();
    }
    public void PlayMoveSound(MoveEffectSound type, bool isWalk = false)
    {
        if (type == MoveEffectSound.None) // 타입이 None이라면, Idle상태임.
        {
            _moveAudio.Stop();
            return;
        }

        _moveAudio.clip = _moveClips[(int)type];
        if (isWalk) // 걷고 있다면,
        {
            _moveAudio.pitch = 1f;
            _moveAudio.Play(); // 그냥 바로 실행
        }
        else // 달리고 있다면,
        {
            _moveAudio.pitch = 1.25f; // 좀 더 빠르게 만들고,
            _moveAudio.Play(); // 실행
        }
    }
    public void PlayAttackSound(WeaponSound type)
    {
        AudioClip clip = _attackClips[(int)type];
        _attackAudio.PlayOneShot(clip);
    }
    public void PlaySkillSound(Skills type, float durationTime, float volume = 1.0f, float pitch = 1.0f, float startTime = 0f, bool isLoop = false)
    {
        StartCoroutine(SkillSoundCo(type, durationTime, volume, pitch, startTime, isLoop));
    }
    IEnumerator SkillSoundCo(Skills type, float durationTime, float volume = 1.0f, float pitch = 1.0f, float startTime = 0f, bool isLoop = false)
    {
        AudioSource temp = _skillSoundPool[(int)type];

        temp.gameObject.SetActive(true); // 사운드 오브젝트 활성화.

        temp.loop = isLoop; // 반복여부 => 배치스킬인 경우

        temp.volume = volume; // 소리 조절

        temp.pitch = pitch;

        if (startTime > 0)
            temp.time = startTime;

        temp.Play(); // 실행

        yield return new WaitForSeconds(durationTime); // 스킬 지속시간 만큼 소리도 지속한다

        temp.Stop(); // 끝나면 오디오 스탑

        temp.gameObject.SetActive(false); // 사운드 오브젝트 비활성화
    }
}
