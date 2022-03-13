using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace tfg
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField] 
        AudioClip[] audioAlarms;

        private Dictionary<string, AudioClip> audios;

        public float volume {
            get { return audioSource.volume; }
            set { audioSource.volume = value; }
        }

        private void Awake()
        {
            audios = new Dictionary<string, AudioClip>();

            foreach (AudioClip sound in audioAlarms)
                audios.Add(sound.name, sound);

            audioSource.volume = GameManager.Instance.volume;
        }

        public void Play(string audioName, bool loop = false)
        {
            AudioClip clip;
            audios.TryGetValue(audioName, out clip);
            if(clip == null)
            {
                Debug.Log("non existing audio:" + audioName);
                return;
            }
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }

        public void Stop(string soundName)
        {
            if(audioSource.clip.name == soundName)
                audioSource.Stop();
        }
    }
}