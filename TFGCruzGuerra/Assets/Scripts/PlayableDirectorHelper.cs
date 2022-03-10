using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableDirectorHelper : MonoBehaviour
{
    [SerializeField] PlayableDirector _dir;
    public void Play()
    {
        _dir.Stop();
        _dir.time = 0;
        _dir.Evaluate();
        _dir.Play();
    }
}
