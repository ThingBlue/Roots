using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject followTarget;
    public float smoothTime;

    public bool follow = true;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 target = followTarget.transform.position;
        target.z = -10.0f;

        if(follow)
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
