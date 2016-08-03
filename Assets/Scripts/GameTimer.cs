using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float levelSeconds = 100;

    private Slider timeSlider;
    private AudioSource audioSource;
    private bool isEndOfLevel = false;
    private LevelManager levelManager;
    private GameObject advanceLabel;

    // Use this for initialization
    void Start()
    {
        timeSlider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        FindYouWin();
        advanceLabel.SetActive(false);
    }

    void FindYouWin()
    {
        advanceLabel = GameObject.Find("Advance Button");
        if (!advanceLabel)
        {
            Debug.LogWarning("Please create Advance Object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSlider.value = Time.timeSinceLevelLoad / levelSeconds;
        bool timeIsUp = (Time.timeSinceLevelLoad >= levelSeconds);
        if (timeIsUp && !isEndOfLevel)
        {
            HandleWinCondition();
        }
    }

    void HandleWinCondition()
    {
        DestroyAllTaggedObjects();
        advanceLabel.SetActive(true);
        audioSource.Play();
        Invoke("LoadNextLevel", audioSource.clip.length);
        isEndOfLevel = true;
    }

    //destroys all objects with destroyOnWin tag
    void DestroyAllTaggedObjects()
    {
        GameObject[] destroyArray = GameObject.FindGameObjectsWithTag("destroyOnWin");
        foreach (GameObject destroy in destroyArray)
        {
            Destroy(destroy);
        }
    }

    void LoadNextLevel()
    {
        levelManager.LoadNextLevel();
    }
}
