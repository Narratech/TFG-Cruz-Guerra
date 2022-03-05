using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _secondsToTakeOff = 2;
    [SerializeField] float _angularSpeed = 2;
    [SerializeField] float _endDegrees = 45;
    [SerializeField] float _speed = 2;
    [SerializeField] float _takeOffSpeed = 1.3f;
    [SerializeField] float _flightSpeed = 30;
    [SerializeField][Tooltip("como de rapido sube la velocidad al terminar de girar")] float _speedDelta = 5;
    float _degrees = 0;

    void Start()
    {
        StartCoroutine(takeOff());
    }

    // Update is called once per frame
    void Update()
    {
        //El modelo esta rotado asi que el forward cambia
        transform.Translate(Vector3.back * _speed * Time.deltaTime,Space.Self);
    }
    IEnumerator takeOff()
    {
        yield return new WaitForSeconds(_secondsToTakeOff);
        while (_degrees< _endDegrees)
        {
            _degrees += Time.deltaTime * _angularSpeed;
            transform.Rotate(Vector3.right * Time.deltaTime * _angularSpeed);
            transform.Translate(Vector3.up * Time.deltaTime *_takeOffSpeed);
            yield return new WaitForEndOfFrame();
        }
        while(_speed< _flightSpeed)
        {
            _speed += _speedDelta * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _speed = _flightSpeed;
    }
}
