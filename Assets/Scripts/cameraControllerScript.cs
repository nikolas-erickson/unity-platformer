using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControllerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, 
            new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);
    }

    public void moveToNewRoom(Transform _newRoom)
    {
        currentPositionX = _newRoom.transform.position.x;  
    }
}
