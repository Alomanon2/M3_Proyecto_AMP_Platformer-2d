using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    [SerializeField] float smoothFactor;
    [SerializeField] Vector3 minLimit, maxLimit;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
        if(playerTransform.position.y>4.2f)
        {
            offset.y = 0.5f;
        }
        else
        {
            offset.y = -5f;
        }
    }


    void Follow()
    {



        // Smooth position hace que haya un pequeño lag cuando sigue al personaje, efecto de jugabilidad
        Vector3 targetPosition = playerTransform.position + offset;
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x),
                                            Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y),
                                            Mathf.Clamp(targetPosition.x, minLimit.z, maxLimit.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.deltaTime);

        transform.position = smoothPosition;
    }
}
