using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class VideoStreamer : MonoBehaviour
{
    [Header("Playback image on canvas")]
    public RawImage image;

    VideoPlayer videoPlayer;
    AudioSource source;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        source = GetComponent<AudioSource>();
    }

    public void PrepareVideo()
    {
        videoPlayer.Prepare();
    }

    public void PlayVideo()
    {
        image.texture = videoPlayer.texture;
        image.gameObject.SetActive(true);

        videoPlayer.Play();
        source.Play();

    }

    public bool IsVideoPrepared()
    {
        return videoPlayer.isPrepared;
    }

    public bool IsVideoPlaying()
    {
        return videoPlayer.isPlaying;
    }

    public float GetVideoLength()
    {

        float length = (float)videoPlayer.clip.length;
        return length;
    }
}
