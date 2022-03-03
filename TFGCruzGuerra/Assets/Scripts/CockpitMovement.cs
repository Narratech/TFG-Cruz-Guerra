using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfg
{
    public class CockpitMovement : MonoBehaviour
    {
        [SerializeField] private float maxRotation = 5f;
        [SerializeField] private float timeFreq = 1f, randomTimeFreq = 0.1f;

        float timeLeftX, timeLeftY, timeLeftZ;

        float toX, toY, toZ;

        void Start()
        {
            timeLeftX = timeLeftY = timeLeftZ = 0;
            toX = toY = toZ = 0;
        }

        void Update()
        {
            timeLeftX -= Time.deltaTime;
            timeLeftY -= Time.deltaTime;
            timeLeftZ -= Time.deltaTime;

            if (timeLeftX < 0)
            {
                timeLeftX = timeFreq + Random.Range(-randomTimeFreq, randomTimeFreq);
                toX = Random.Range(-maxRotation, maxRotation);
            }
            if (timeLeftY < 0)
            {
                timeLeftY = timeFreq + Random.Range(-randomTimeFreq, randomTimeFreq);
                toY = Random.Range(-maxRotation, maxRotation);
            }
            if (timeLeftZ < 0)
            {
                timeLeftZ = timeFreq + Random.Range(-randomTimeFreq, randomTimeFreq);
                toZ = Random.Range(-maxRotation, maxRotation);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(toX, toY, toZ), Time.deltaTime);
        }
    }
}