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

    [SerializeField] AudioSource[] _skillSoundPool; // ��ų ���� Ǯ��

    [SerializeField] private AudioSource _bgmAudio;
    [SerializeField] private AudioSource _attackAudio;
    [SerializeField] private AudioSource _moveAudio;
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
    public void PlayMoveSound(MoveEffectSound type, bool isWalk = false)
    {
        if (type == MoveEffectSound.None) // Ÿ���� None�̶��, Idle������.
        {
            _moveAudio.Stop();
            return;
        }

        _moveAudio.clip = _moveClips[(int)type];
        if (isWalk) // �Ȱ� �ִٸ�,
        {
            _moveAudio.pitch = 1f;
            _moveAudio.Play(); // �׳� �ٷ� ����
        }
        else // �޸��� �ִٸ�,
        {
            _moveAudio.pitch = 1.25f; // �� �� ������ �����,
            _moveAudio.Play(); // ����
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

        temp.gameObject.SetActive(true); // ���� ������Ʈ Ȱ��ȭ.

        temp.loop = isLoop; // �ݺ����� => ��ġ��ų�� ���

        temp.volume = volume; // �Ҹ� ����

        temp.pitch = pitch;

        if (startTime > 0)
            temp.time = startTime;

        temp.Play(); // ����

        yield return new WaitForSeconds(durationTime); // ��ų ���ӽð� ��ŭ �Ҹ��� �����Ѵ�

        temp.Stop(); // ������ ����� ��ž

        temp.gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
    }
}
