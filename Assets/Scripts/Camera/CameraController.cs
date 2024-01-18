using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera) 
        {
            if (target) 
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, this.transform.position.z);
                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime);            
            }
        }
    }

    public void SetPositionToZero(Transform _transform) 
    {
        Vector3 zeroPosition = _transform.position;
        zeroPosition.z = this.transform.position.z;
        this.transform.position = zeroPosition;
    }

    public void ChangeTarget(Transform _target) 
    {
        target = _target;
    }
}
