using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Private input variables
    private float _horizontalInput;
    private float _verticalInput;
    private float _steeringAngle;
    private Rigidbody _rb;

    //Control variables
    public float maxSteeringAngle = 45f;
    public float motorForce = 50f;

    //Wheels public elements
    public WheelCollider frontLeftW, frontRightW;
    public WheelCollider backLeftW, backRightW;
    public Transform frontLeftT, frontRightT;
    public Transform backLeftT, backRightT;

    public void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        _steeringAngle = maxSteeringAngle * _horizontalInput;
        frontLeftW.steerAngle = _steeringAngle;
        frontRightW.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        frontRightW.motorTorque = _verticalInput * motorForce;
        frontLeftW.motorTorque = _verticalInput * motorForce;
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //    _rb.AddForce(Vector3.forward * 100, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * 10 * Time.deltaTime * _verticalInput);
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(backLeftW, backLeftT);
        UpdateWheelPose(backRightW, backRightT);
    }

    private void UpdateWheelPose(WheelCollider _wheelC, Transform _wheelT)
    {
        Vector3 _position = _wheelT.position;
        Quaternion _quat = _wheelT.rotation;

        _wheelC.GetWorldPose(out _position, out _quat);

        _wheelT.position = _position;
        _wheelT.rotation = _quat;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
