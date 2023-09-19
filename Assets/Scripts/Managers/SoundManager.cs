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
    public void PlaySkillSound(Skills type, float volume = 1.0f, float pitch = 1.0f, bool isLoop = false, Transform parent = null)
    {
        AudioSource temp = Instantiate(_skillAudio, parent); // 사운드를 스킬의 자식으로 생성.

        temp.loop = isLoop; // 반복여부 => 배치스킬인 경우

        temp.clip = _skillClips[(int)type]; // 스킬에 맞는 사운드를 clip으로 저장한 후,
        
        temp.volume = volume; // 소리 조절

        temp.pitch = pitch;

        temp.Play(); // 실행
    }
}
