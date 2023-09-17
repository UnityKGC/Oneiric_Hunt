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
    [SerializeField] AudioClip[] _skillClips; // �迭�� ������ ��ų Enum�� ������ ����.

    [SerializeField] private AudioSource _bgmAudio;
    [SerializeField] private AudioSource _effectAudio;
    [SerializeField] private AudioSource _skillAudio; // ��ų���� ��ų�� �ڽ����� ������� �ְ� �����Ű�� �Ѵ�.

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
}
