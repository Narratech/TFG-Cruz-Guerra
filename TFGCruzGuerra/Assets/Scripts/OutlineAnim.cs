using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAnim : MonoBehaviour
{
    EdgeDetect _edge;
    [SerializeField] Color _from;
    [SerializeField] Color _to;
    [SerializeField] float _animSeconds;
    float _secondsPassed;
    int _dir = 1;
    const float deadZone = .07f;
    private void Awake()
    {
        _edge = GetComponent<EdgeDetect>();
    }
    private void Update()
    {
        if (Mathf.Abs(_secondsPassed + Time.deltaTime * _dir) >= _animSeconds || Mathf.Abs(_secondsPassed + Time.deltaTime * _dir) < deadZone)
            _dir = -_dir;
        _secondsPassed += Time.deltaTime * _dir;
        _edge.outlineColor = Color.Lerp(_from, _to, _secondsPassed / _animSeconds);
    }
}
