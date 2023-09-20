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

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [SerializeField] AudioClip[] _effectClips;
    [SerializeField] AudioClip[] _bgmClips;
    [SerializeField] AudioClip[] _skillClips; // 배열의 순서는 스킬 Enum의 순서와 같다.

    [SerializeField] private AudioSource _bgmAudio;
    [SerializeField] private AudioSource _effectAudio;
    [SerializeField] private AudioSource _skillAudio; // 스킬들을 스킬의 자식으로 오디오를 주고 실행시키게 한다.

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
    public void PlayEffectSound(WeaponSound type)
    {
        AudioClip clip = _effectClips[(int)type];
        _effectAudio.PlayOneShot(clip);
    }
    public void PlaySkillSound(Skills type, float volume = 1.0f, float pitch = 1.0f, float time = 0f, bool isLoop = false, Transform parent = null)
    {
        // 혹시 다른 전용공간에 넣고 싶으면, 매니저 안에 그 공간을 만들고, 스킬 사운드도 그 공간에 생성하도록 만들자.
        // 그럼 시간은? SkillManager에서 각 스킬마다 스크립터블의 durationTime을 이용하자.
        // 즉! SkillManager에서 PlaySkillSound를 실행하고 (인자로 _durationTime도 추가), 여기에서는 "코루틴"을 이용하여 오디오소스를 생성하고, _durationTime초 후에 파괴되도록 구현하자.
        
        AudioSource temp = Instantiate(_skillAudio, parent); // 사운드를 스킬의 자식으로 생성.

        temp.loop = isLoop; // 반복여부 => 배치스킬인 경우

        temp.clip = _skillClips[(int)type]; // 스킬에 맞는 사운드를 clip으로 저장한 후,
        
        temp.volume = volume; // 소리 조절

        temp.pitch = pitch;

        if (time > 0)
            temp.time = time;

        temp.Play(); // 실행
    }
}
