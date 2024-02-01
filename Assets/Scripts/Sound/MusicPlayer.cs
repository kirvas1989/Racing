using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour, IDependency<Pauser>
{
    [SerializeField] private bool isEnabled;
    [SerializeField] private bool inRandomOrder;
    [SerializeField] private int currentTrackIndex;
    [SerializeField] private AudioClip[] tracks;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;

    private AudioSource audioSource;

    private int currentSceneIndex;

    private void Start()
    {
        pauser.PauseStateChange += OnPauseStateChanged;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[currentTrackIndex];
        audioSource.Play();

        if (inRandomOrder) PlayRandom();
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChanged;
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void OnPauseStateChanged(bool pause)
    {
        if (pause == true)
            TurnOff();

        if (pause == false)
            TurnOn();
    }

    private void SceneManager_activeSceneChanged(Scene previousActiveScene, Scene newActiveScene)
    {
        if (SceneManager.GetActiveScene().buildIndex != currentSceneIndex)
        {
            TurnOn();
            PlayRandom();
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        else return;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "main_menu" ||
            SceneManager.GetActiveScene().name == "start_screen")
            TurnOff();

        if (isEnabled == true)
        {
            if (!audioSource.isPlaying)
                PlayNext();

            if (Input.GetKeyDown(KeyCode.RightShift))
                PlayRandom();

            if (Input.GetKeyDown(KeyCode.LeftShift))
                PlayNext();

            if (Input.GetKeyDown(KeyCode.RightControl))
                TurnOff();
        }
        else if (isEnabled == false)
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
                TurnOn();
        }
    }

    private void Shuffle(AudioClip[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            AudioClip temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    #region Public API
    public void TurnOn()
    {
        isEnabled = true;
        audioSource.Play();
    }

    public void TurnOff()
    {
        isEnabled = false;
        audioSource.Pause();
    }

    public void PlayRandom()
    {
        Shuffle(tracks);
        audioSource.clip = tracks[0];
        audioSource.Play();
    }

    public void PlayNext()
    {
        currentTrackIndex++;
        if (currentTrackIndex >= tracks.Length)
        {
            currentTrackIndex = 0;
        }
        audioSource.clip = tracks[currentTrackIndex];
        audioSource.Play();
    }
    #endregion
}
