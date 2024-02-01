using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTestRace : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform OffTrack;  
    [SerializeField] private bool isEnabled;
    
    public bool isOutOfTrack;
    public bool inRandomOrder;
    public int raceIndex;

    private void Awake()
    {     
        if (isEnabled)
        {
            if (isOutOfTrack)
            {
                inRandomOrder = false;
                car.transform.position = OffTrack.transform.position;
                car.transform.rotation = OffTrack.transform.rotation;
            }
            else
            {
                if (inRandomOrder)
                {
                    isOutOfTrack = false;
                    int random = Random.Range(0, startPoints.Length);
                    raceIndex = random;
                }

                car.transform.position = startPoints[raceIndex].transform.position;
                car.transform.rotation = startPoints[raceIndex].transform.rotation;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8) == true)      
            SceneManager.LoadScene("TestScene");      
    }
}
