using UnityEngine;
using System.Collections;
using UnityEngine.Android;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    [SerializeField] float shootLoudnessBorder;

    public delegate void OnMicrophoneInput();
    public event OnMicrophoneInput OnScream;

    public float sensitivity = 100;
    public float loudness = 0;

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 10, 44100);
        audio.loop = true; // Set the AudioClip to loop
        //audio.mute = true; // Mute the sound, we don't want the player to hear it
        while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the recording has started
        audio.Play(); // Play the audio source!
    }

    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;

        if (loudness > shootLoudnessBorder || Input.GetMouseButtonDown(0))
        {
            OnScream();
        }
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}