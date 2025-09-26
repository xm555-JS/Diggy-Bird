using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * [����]
 * - ������ ��� ������� �����ϴ� �̱��� �Ŵ��� Ŭ����
 */

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    // BGM �ĺ��� Enum. ������ bgmClips�� ��ġ�ؾ� ��.
    // bgmClips �߰� �� Enum�� �߰�
    public enum Bgm { MAIN };

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channellIndex;

    // SFX �ĺ��� Enum. ������ sfxClips�� ��ġ�ؾ� ��.
    // sfxClips �߰� �� Enum�� �߰�
    public enum Sfx { TOUCH, EAT, DIGGY, MAGNETIC, HASTE, POP };

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // ȿ���� �÷��̾� �ʱ�ȭ
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