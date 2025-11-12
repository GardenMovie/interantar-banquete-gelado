using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    public bool soundOn = false;
    public int MusicMute = 0;
    public AudioClip clickIn;
    public AudioClip clickOut;
    public AudioClip finish;

    [Header("Transição")]
    public Animator transition;
    public float transitionTime = 1f;

    public static GameManager Instance;

    [Header("Game Values")]
    public int score;
    public bool debugMode = false;
    public int globeTargetIndex = -1;

    [Header("Player Settings")]
    public int health = 3;
    public int characterChoice = 0;

    // cooldown between AddHealth calls (seconds)
    public float healthCooldown = 1f;
    private float nextHealthTime = 0f;

    [Header("Performance Settings")]
    public int targetFrameRate = 60;
    public int vsyncEnable = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (soundOn == true)
        {
            MusicSource.clip = backgroundMusic;

            MusicSource.Play();
        }

        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = vsyncEnable;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void incrementIndex()
    {
        globeTargetIndex += 1;
    }

    void Start()
    {
        score = 0;
    }

    public void ChangeScene(string nomeCena)
    {
        // SceneManager.LoadScene(nomeCena);
        StartCoroutine(ChangeSceneTransition(nomeCena));
    }

    System.Collections.IEnumerator ChangeSceneTransition(string nomeCena)
    {
        // transition.SetTrigger("ChangeScene");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(nomeCena);
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
    public event Action<int> OnHealthChanged;

    public void AddHealth(int Value)
    {
        // enforce cooldown
        UnityEngine.Debug.Log("Called");
        if (Time.time < nextHealthTime) return;
        nextHealthTime = Time.time + healthCooldown;

        health += Value;
        OnHealthChanged?.Invoke(health);
        if (health <= 0)
        {
            ChangeScene("FailMenu");
        }
    }
    public void AddScore(int Value)
    {
        score += Value;
        if (score >= 10)
        {
            ChangeScene("FinalMenu");
        }
    }

}
