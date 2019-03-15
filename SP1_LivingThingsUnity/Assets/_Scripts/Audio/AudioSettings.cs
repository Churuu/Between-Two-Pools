using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//Joakim
public class AudioSettings : MonoBehaviour
{
    public AudioMixer sound;
    public Slider soundSlider;
    public string volymeToChangeName = "Ljud";
    

    private float soundSliderValue;
    private float soundSliderValue2;

    private void Update()
    {
        soundSliderValue = soundSlider.value;
        float soundLevel = soundSliderValue;
        sound.SetFloat(volymeToChangeName, soundLevel);
    }
}