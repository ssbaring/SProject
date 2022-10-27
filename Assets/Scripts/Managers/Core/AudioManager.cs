using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.AudioType.MaxCount];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    private GameObject _rootAudio;

    public GameObject RootAudio { get { return _rootAudio; } }

    public void Init()
    {
        _rootAudio = new GameObject { name = "@AUDIO_ROOT" };
        Object.DontDestroyOnLoad(_rootAudio);

        string[] audioNames = System.Enum.GetNames(typeof(Define.AudioType));
        for (int i = 0; i < audioNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = audioNames[i] };
            _audioSources[i] = go.GetOrAddComponent<AudioSource>();
            go.transform.SetParent(_rootAudio.transform);
        }

        _audioSources[(int)Define.AudioType.Bgm].loop = true;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }

    public void Play(string path, Define.AudioType type = Define.AudioType.Effect, float pitch = 1.0f)
    {
        Play(GetAudioClip(path, type), type, pitch);
    }

    public void Play(AudioClip audioClip, Define.AudioType type = Define.AudioType.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        switch (type)
        {
            case Define.AudioType.Bgm:
                {
                    AudioSource audioSource = _audioSources[(int)Define.AudioType.Effect];
                    if (audioSource.isPlaying)
                        audioSource.Stop();

                    audioSource.pitch = pitch;
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }
                break;
            case Define.AudioType.Effect:
                {
                    AudioSource audioSource = _audioSources[(int)Define.AudioType.Effect];
                    audioSource.pitch = pitch;
                    audioSource.PlayOneShot(audioClip);
                }
                break;
            default:
                break;
        }
    }

    AudioClip GetAudioClip(string path, Define.AudioType type = Define.AudioType.Effect)
    {
        if (path.Contains("Audio/") == false)
            path = $"Audio/{path}";

        AudioClip audioClip;

        if (type == Define.AudioType.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip is missing at {path}");

        return audioClip;
    }
}
