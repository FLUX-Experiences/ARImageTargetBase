using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private MeshRenderer videoPlane;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlane = GetComponent<MeshRenderer>();
    }

    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
    public void Play()
    {
        videoPlayer.Play();
        videoPlane.enabled = true;
    }
    public void Stop()
    {
        videoPlayer.Stop();
        videoPlane.enabled = false;
    }
    private void Update()
    {
        /**
         * when reaching the end, stop the video
         */
        if (videoPlayer.isPlaying 
            && (int)videoPlayer.frame >= (int)videoPlayer.frameCount-1)
        {
            Stop();
        }
    }

    public void Play1FromDevice(string filename="")
    {
        if (filename != "")
        {
            Handheld.PlayFullScreenMovie(filename, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.Fill);
        }
    }
   
}