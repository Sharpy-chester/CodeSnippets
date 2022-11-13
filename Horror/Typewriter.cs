using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{
    [SerializeField] float timeBetweenLetters;
    [TextArea]
    [SerializeField] string transcript;
    Text currentText;
    float currentTime;
    bool playingText = false;
    int letters;
    [Tooltip("If null, this wont play a clip")]
    [SerializeField] AudioClip audioToPlay;
    AudioSource audioSource;

    private void Awake()
    {
        currentText = GetComponent<Text>();
        audioSource = FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        if (playingText)
        {
            currentTime += Time.deltaTime;
            if(currentTime > timeBetweenLetters)
            {
                letters++;
                currentText.text = transcript.Substring(0, letters);
                currentTime = 0;
            }
            if (transcript == currentText.text)
            {
                playingText = false;
            }
        }
    }

    public void StartEffect()
    {
        currentText.text = "";
        playingText = true;
        letters = 0;
        audioSource.clip = audioToPlay;
        audioSource.Play();
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }
}
