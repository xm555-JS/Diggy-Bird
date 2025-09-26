using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * [목적]
 * - 게임의 모든 오디오를 관리하는 싱글톤 매니저 클래스
 */

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    // BGM 식별용 Enum. 순서는 bgmClips와 일치해야 함.
    // bgmClips 추가 시 Enum값 추가
    public enum Bgm { MAIN };

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channellIndex;

    // SFX 식별용 Enum. 순서는 sfxClips와 일치해야 함.
    // sfxClips 추가 시 Enum값 추가
    public enum Sfx { TOUCH, EAT, DIGGY, MAGNETIC, HASTE, POP };

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.Play();
    }

    public void PlayerSfx(Sfx sfx, float volume = 1)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channellIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channellIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].volume = volume;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void StopAllSfxSound()
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channellIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                sfxPlayers[loopIndex].Stop();
                continue;
            }
        }
    }

    public void StartBgm()
    {
        PlayBgm(Bgm.MAIN);
    }

    public void SetBgmVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = volume;
        }
    }
}