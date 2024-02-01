using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
{
    private new AudioSource audio;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        pauser.PauseStateChange += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool pause)
    {
        if (pause == true)
            audio.Stop();

        if (pause == false)
            audio.Play();
    }
}
