using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour
{
    
    //References
    [Header("References")]
    public Transform trans;
    public Transform modelTrans;
    public CharacterController characterController;

    //Movement
    [Header("Movement")]
    [Tooltip("Units moved per second at maximum speed.")]
    public float movespeed = 24;

    [Tooltip("Time in seconds,to reech maximum speed.")]
    public float timeToMaxSpeed = 0.26f;

    private float VelocityGainPerSecond ()
    {
        return movespeed / timeToMaxSpeed / 3.0f;
    } 
    [Tooltip("Time in seconds, to go from maximum speed to stationary.")]
    public float timeToLooseMaxSpeed = 0.2f;

    private float VelocityLossPerSecond ()
    {
        return movespeed / timeToMaxSpeed / 2.0f;
    }

    

    [Tooltip("Multipliyer for momentum when attempting to movein a direction opposite the current traveling direction (e.g. trying to move left when already moving right.")]
    public float reverceMomentumMultiplyer = 2.0f;

    private Vector3 movementVelocity = Vector3.zero;

    void MoveForwardBackward()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (movementVelocity.z >= 0)
            {
                var newZ = movementVelocity.z + VelocityGainPerSecond() * Time.deltaTime;
                movementVelocity.z = Mathf.Min(movespeed, newZ);
            }
            else 
            {
                var newZ = movementVelocity.z + 
                    (VelocityGainPerSecond() * reverceMomentumMultiplyer * Time.deltaTime);
                movementVelocity.z = Mathf.Min(0.0f, newZ);
           }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if( movementVelocity.z > 0)
            {
                var newZ = movementVelocity.z - VelocityGainPerSecond() * reverceMomentumMultiplyer * Time.deltaTime;
                movementVelocity.z = Mathf.Max(0,newZ );
            }
            else
            {
                var newZ = movementVelocity.z - VelocityGainPerSecond() * Time.deltaTime;
                movementVelocity.z = Mathf.Max ( -movespeed, newZ );
            }
        }
        else
        {
           if (movementVelocity.z > 0)
            {
                var newZ = movementVelocity.z - VelocityLossPerSecond() * Time.deltaTime;
                movementVelocity.z = Mathf.Max(0, newZ);
            }
            else
            {
                var newZ = movementVelocity.z + VelocityLossPerSecond() * Time.deltaTime;
                movementVelocity.z = Mathf.Min(0, newZ);
            }
        }
    }

    void MoveLeftRight()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (movementVelocity.x >= 0)
            {
                var newX = movementVelocity.x + VelocityGainPerSecond() * Time.deltaTime;
                movementVelocity.x = Mathf.Min (movespeed, newX);
            }
            else
            {
                var newX = movementVelocity.x + VelocityGainPerSecond() * reverceMomentumMultiplyer * Time.deltaTime;
                movementVelocity.x = Mathf.Min (0, newX);
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (movementVelocity.x > 0)
            {
                var newX = movementVelocity.x - VelocityGainPerSecond() * reverceMomentumMultiplyer * Time.deltaTime;
                movementVelocity.x = Mathf.Max(0, newX);
            }
            else 
            {
                var newX = movementVelocity.x - VelocityGainPerSecond() * Time.deltaTime;
                movementVelocity.x = Mathf.Max(-movespeed, newX);
            }
        }
        else 
        {
            if (movementVelocity.x > 0)
            {
                var newX = movementVelocity.x - VelocityLossPerSecond() * Time.deltaTime;
                movementVelocity.x = Mathf.Max(0, newX);
            }
            else
            {
                var newX = movementVelocity.x + VelocityLossPerSecond() * Time.deltaTime;
                movementVelocity.x = Mathf.Min(0, newX);
            }
        }

    }
    void Moove(){
        MoveForwardBackward();
        MoveLeftRight();

        if (movementVelocity.x != 0 || movementVelocity.z != 0){
            characterController.Move(movementVelocity * Time.deltaTime);
            modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation, Quaternion.LookRotation(movementVelocity), 0.18F);
        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        Moove();
    }
}
