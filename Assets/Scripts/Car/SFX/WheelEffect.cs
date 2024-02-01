using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class WheelEffect : MonoBehaviour, IDependency<Pauser>
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private ParticleSystem[] wheelsSmoke;

    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sidewaySlipLimit;

    [SerializeField] private GameObject skidPrfab;

    [SerializeField] private float offsetY;

    [SerializeField] private new AudioSource audio;

    private WheelHit wheelHit;
    private Transform[] skidTrail;

    private Pauser pauser;
    public void Construct(Pauser pauser) => this.pauser = pauser;
    private void OnPauseStateChanged(bool pause) => audio.enabled = !pause;
    
    private void Start()
    {
        skidTrail = new Transform[wheels.Length];

        pauser.PauseStateChange += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChanged;
    }
    
    private void Update()
    {
        bool isSlip = false;
        
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetGroundHit(out wheelHit);

            if (wheels[i].isGrounded == true)
            {
                wheelHit.forwardSlip = Mathf.Abs(wheelHit.forwardSlip);
                wheelHit.sidewaysSlip = Mathf.Abs(wheelHit.sidewaysSlip);

                if (wheelHit.forwardSlip >  forwardSlipLimit || wheelHit.sidewaysSlip > sidewaySlipLimit)
                {
                    if (skidTrail[i] == null)
                        skidTrail[i] = Instantiate(skidPrfab).transform;

                    if (audio.isPlaying == false && audio.enabled == true)
                        audio.Play();

                    if (skidTrail[i] != null)
                    {
                        //skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * wheels[i].radius; // 1й вариант.
                        //skidTrail[i].position = wheelHit.point; // 2й вариант.
                        
                        skidTrail[i].position = wheels[i].transform.position - (wheelHit.normal * (wheels[i].radius + offsetY)) ;
                        skidTrail[i].forward = -wheelHit.normal;

                        wheelsSmoke[i].transform.position = skidTrail[i].position;
                        wheelsSmoke[i].Emit(10);
                    }

                    isSlip = true;

                    continue;
                }
            }

            skidTrail[i] = null;
            wheelsSmoke[i].Stop();
        }

        if (isSlip == false)
            audio.Stop();
    }
}
