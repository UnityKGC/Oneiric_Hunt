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

    public float BGMVolume
    {
        get { return _bgmVolume; } 
        set
        {
            _bgmVolume = value;
            _bgmAudio.volume = _bgmVolume;
        }
    }
    public float EffectVolume
    {
        get { return _effectVolume; }
        set
        {
            _effectVolume = value;
            _stepSound.volume = _effectVolume;
            foreach (AudioSource audio in _skillSoundPool)
                audio.volume = _effectVolume;
            _attackAudio.volume = _effectVolume;
        }
    }
    [SerializeField] AudioClip[] _attackClips;
    [SerializeField] AudioClip[] _bgmClips;
    [SerializeField] AudioClip[] _moveClips;

    [SerializeField] AudioSource[] _skillSoundPool; // ��ų ���� Ǯ��

    [SerializeField] private AudioSource _bgmAudio;
    [SerializeField] private AudioSource _attackAudio;
    public AudioSource _stepSound;

    private float _bgmVolume = 0.7f;
    private float _effectVolume = 0.7f;

    //[SerializeField] private AudioSource _skillAudio; // ��ų���� ��ų�� �ڽ����� ������� �ְ� �����Ű�� �Ѵ�.

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
    public AudioClip GetMoveClip(int idx)
    {
        return _moveClips[idx];
    }
    public void PlayAttackSound(WeaponSound type)
    {
        AudioClip clip = _attackClips[(int)type];
        _attackAudio.time = 0.2f;
        _attackAudio.PlayOneShot(clip);
    }
    public void PlaySkillSound(Skills type, float durationTime, float pitch = 1.0f, float startTime = 0f, bool isLoop = false)
    {
        StartCoroutine(SkillSoundCo(type, durationTime, pitch, startTime, isLoop));
    }
    IEnumerator SkillSoundCo(Skills type, float durationTime, float pitch = 1.0f, float startTime = 0f, bool isLoop = false)
    {
        AudioSource temp = _skillSoundPool[(int)type];

        temp.gameObject.SetActive(true); // ���� ������Ʈ Ȱ��ȭ.

        temp.loop = isLoop; // �ݺ����� => ��ġ��ų�� ���

        temp.pitch = pitch;

        if (startTime > 0)
            temp.time = startTime;

        temp.Play(); // ����

        yield return new WaitForSeconds(durationTime); // ��ų ���ӽð� ��ŭ �Ҹ��� �����Ѵ�

        temp.Stop(); // ������ ����� ��ž

        temp.gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
    }
}
