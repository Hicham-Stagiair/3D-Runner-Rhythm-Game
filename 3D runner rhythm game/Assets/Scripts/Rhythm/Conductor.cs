using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Singleton instance
    private static Conductor instance;

    // Public property to access the instance
    public static Conductor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Conductor>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("Conductor");
                    instance = obj.AddComponent<Conductor>();
                }
            }
            return instance;
        }
    }

    // Song beats per minute
    public float songBpm;
    
    //keep all the position-in-beats of notes in the song
    public Notes[] notes;

    // The number of seconds for each song beat
    public float secPerBeat;
    
    // Current song position, in seconds
    [HideInInspector] public float songPosition;

    // Current song position, in beats
    [HideInInspector] public float songPositionInBeats;

    // How many seconds have passed since the song started
    [HideInInspector] public float dspSongTime;

    // The offset to the first beat of the song in seconds
    public float firstBeatOffset;
    
    //how many beats in advance should the notes be shown
    public float beatsShownInAdvance;

    // An AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;
    public GameObject obstacleSpawnPoint;
    public Transform endPos;
    
    //the index of the next note to be spawned
    int nextIndex = 0;

    void Awake()
    {
        // Ensure only one instance of the Conductor exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        // Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        // Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        // Start the music
        musicSource.Play();
    }

    void Update()
    {
        // Determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        // Determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        
        if (nextIndex < notes.Length && notes[nextIndex].beat < songPositionInBeats + beatsShownInAdvance)
        {
            GameObject obstacle = Instantiate( notes[nextIndex].obstacle, obstacleSpawnPoint.transform.position, Quaternion.identity);

            //initialize the fields of the music note
            obstacle.GetComponent<ObstacleMovement>().beatOfThisNote = notes[nextIndex].beat;
            obstacle.GetComponent<ObstacleMovement>().songPosInBeats = songPositionInBeats;
            obstacle.GetComponent<ObstacleMovement>().BeatsShownInAdvance = beatsShownInAdvance;
            //obstacle.GetComponent<ObstacleMovement>().startPos = obstacleSpawnPoint.transform.position;
            //obstacle.GetComponent<ObstacleMovement>().endPos = endPos.position;
            nextIndex++;
        }
    }
}

[System.Serializable]
public class Notes
{
    public float beat;
    public GameObject obstacle;
}