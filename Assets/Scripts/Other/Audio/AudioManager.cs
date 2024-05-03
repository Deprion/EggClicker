using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource source;
    private int index = 0;
    private int prevIndex = 0;

    private float leftTime = 0;

    public static AudioManager inst;

    private void Awake()
    {
        inst = this;

        DontDestroyOnLoad(this);

        source = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("mute") == 1) Mute();

        StartNewAudio();
    }

    private void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime > 0) return;

        StartNewAudio();
    }

    private void StartNewAudio()
    {
        prevIndex = index;
        RollIndex();

        source.clip = clips[index];
        leftTime = source.clip.length;

        source.Play();
    }

    private void RollIndex()
    {
        index = Random.Range(0, clips.Length);

        if (clips.Length > 1 && index == prevIndex) RollIndex();
    }

    public void Mute()
    { 
        source.mute = !source.mute;
        PlayerPrefs.SetInt("mute", source.mute ? 1 : 0);
    }
}
