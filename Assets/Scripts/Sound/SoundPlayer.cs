using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : SingletonBase<SoundPlayer>
{
    [SerializeField] private SoundsAsset m_Sounds;
    [SerializeField] private AudioClip[] m_BGM;

    private AudioSource m_AudioSource;

    private int randomIndex;
    [SerializeField] private bool inRandomOrder;

    private new void Awake()
    {
        base.Awake();
        m_AudioSource = GetComponent<AudioSource>();

        if (inRandomOrder == true)
        {
            randomIndex = Random.Range(0, m_BGM.Length);
        }
        else randomIndex = 0;

        Instance.m_AudioSource.clip = m_BGM[randomIndex];
        
        for (int i = 0; i < m_BGM.Length; i++)
        {
            Instance.m_AudioSource.Play();
        }
    }

    public void Play(Sound sound)
    {
        m_AudioSource.PlayOneShot(m_Sounds[sound]);
    }

    public void PlayRandom(AudioClip m_AudioClip)
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
    }
}

