using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class VideoStreamer : MonoBehaviour
{
    [Header("Playback image on canvas")]
    public RawImage image ;
    public string nextScene;
    public bool startCutsceneImmidietly;

    VideoPlayer videoPlayer;
    AudioSource source;

    void Start()
    {
        if( image == null)
        {
            GameObject.FindGameObjectWithTag("SkiftNyckel");
        }
        videoPlayer = GetComponent<VideoPlayer>();
        source = GetComponent<AudioSource>();

        if (startCutsceneImmidietly)
            PrepareVideo();
    }

    public void PrepareVideo()
    {
        videoPlayer.Prepare();
        Invoke("PlayVideo", 1);
    }

    void PlayVideo()
    {
        image.texture = videoPlayer.texture;
        image.gameObject.SetActive(true);

        videoPlayer.Play();
        source.Play();

        Invoke("LoadNextScene", GetVideoLength());
    }

    public void LoadNextScene()
    {
        FindObjectOfType<SceneTransitioner>().LoadScene(nextScene);
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
