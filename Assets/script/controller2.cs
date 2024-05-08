using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller2 : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField] private driveType drive;
    public float radius = 6;
    private inputManager2 iM2;
    public GameObject[] wheelMesh = new GameObject[4];
    public WheelCollider[] wheels = new WheelCollider[4];
    private Rigidbody rigidbody;
    public float KPH;
    public float torque = 1000;
    public float DownForceValue = 50;
    public float steringMax = 30;
    private GameObject centerOfMass;
    public float brakPower;

    // Start is called before the first frame update
    void Start()
    {
        getObjects();
    }

    public void FixedUpdate()
    {
        addDownForce();
        animetaWheels();
        moveVehicle();
        steerVehicle();
        getFriction();
    }
    private void moveVehicle()
    {

        float totalPower;
        if (drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = iM2.vertical * (torque / 4);
            }
        }
        else if (drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = iM2.vertical * (torque / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length - 2; i++)
            {
                wheels[i].motorTorque = iM2.vertical * (torque / 4);
            }
        }
        KPH = rigidbody.velocity.magnitude * 3.6f;

        if (iM2.handbrake)
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = brakPower;
        }
        else
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = 0;
        }



    }
    private void steerVehicle()
    {
        if (iM2.horizontal > 0)
        {
            //rear tracks size is set to 1.5f       wheel base has been set to 2.55f
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * iM2.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * iM2.horizontal;
        }
        else if (iM2.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * iM2.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * iM2.horizontal;
            //transform.Rotate(Vector3.up * steerHelping);

        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }

    }
    void animetaWheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;
        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }
    private void getObjects()
    {
        iM2 = GetComponent<inputManager2>();
        rigidbody = GetComponent<Rigidbody>();
        centerOfMass = GameObject.Find("mass");
        rigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }
    private void addDownForce()
    {
        rigidbody.AddForce(-transform.up * DownForceValue * rigidbody.velocity.magnitude);

    }
    private void getFriction()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            WheelHit wheelHit;
            wheels[i].GetGroundHit(out wheelHit);
        }
    }
}