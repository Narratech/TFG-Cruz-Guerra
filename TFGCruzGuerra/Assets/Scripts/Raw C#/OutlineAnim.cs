using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAnim : MonoBehaviour
{
    EdgeDetect _detect;
    [SerializeField] float _animSeconds;
    int _dir;
    [SerializeField] Color _color1;
    [SerializeField] Color _color2;
    float _secondsPassed;
    const float deadZone = .07f;
    private void Awake()
    {
        _dir = 1;
        _detect = GetComponent<EdgeDetect>();
        _secondsPassed = .02f;
    }
    void Update()
    {

        if (Mathf.Abs(_secondsPassed + _dir * Time.deltaTime) >= _animSeconds || Mathf.Abs(_secondsPassed + _dir * Time.deltaTime) < deadZone)
        {
            _dir = -_dir;
        }
        _secondsPassed += Time.deltaTime * _dir;
        _detect.outlineColor = Color.Lerp(_color1, _color2, _secondsPassed / _animSeconds);
    }
}
