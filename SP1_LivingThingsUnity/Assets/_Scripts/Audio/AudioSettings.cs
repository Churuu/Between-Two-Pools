using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//Joakim
public class AudioSettings : MonoBehaviour
{
    public AudioMixer sound;
    public Slider soundSlider;
    public string volymeToChangeName;
    

    private float soundSliderValue = 50f;
    private float soundSliderValue2 = 50f;

    private void Update()
    {
        soundSliderValue = soundSlider.value;
        float soundLevel = soundSliderValue * 100f;
        sound.SetFloat("Master", soundLevel);
        Debug.Log(sound);


       
    }
}