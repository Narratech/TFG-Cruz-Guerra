using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace tfg
{
    public class VideoMenuPlayer : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        public void prepareVideo()
        {
            _videoPlayer?.Prepare();
        }

        public void stop()
        {
            _videoPlayer.Stop();
            _videoPlayer.targetCamera = null;
        }

        public void start(Camera cam)
        {
            if (_videoPlayer == null)
                return;

            _videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
            _videoPlayer.targetCamera = cam;
            _videoPlayer.Play();
        }
    }
}