using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private float cameraZPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraZPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, cameraZPosition);
        }
    }
}
