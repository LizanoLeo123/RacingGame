using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Private input variables
    private float _horizontalInput;
    private float _verticalInput;
    private float _steeringAngle;
    private bool _burst = false;
    private float _carBreak;
    private Rigidbody _rb;

    //UI Manager to update de speedometer
    UI_Manager _uiManager;

    //Control variables
    public float maxSteeringAngle = 50f;
    public float motorForce = 50f;

    //Wheels public elements
    public WheelCollider frontLeftW, frontRightW;
    public WheelCollider backLeftW, backRightW;
    public Transform frontLeftT, frontRightT;
    public Transform backLeftT, backRightT;

    public void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Accelerate");

        //Debug.Log(transform.InverseTransformDirection(_rb.velocity).z);
        //Debug.Log(_rb.GetPointVelocity(transform.position));
        if (Input.GetAxis("Accelerate") > 0 && (transform.InverseTransformDirection(_rb.velocity).z >= -5 && transform.InverseTransformDirection(_rb.velocity).z < 3f))
        {
            _burst = true;
        }
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

        if (_burst)
        { //My car is still and i want to start moving
            if (Input.GetAxis("Accelerate") < 0.5f)
                _rb.AddRelativeForce(Vector3.forward * 2000, ForceMode.Impulse);
            _burst = false;
        }
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
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        _rb.centerOfMass = new Vector3(0, -0.3f, 0);
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
        float speed = transform.InverseTransformDirection(_rb.velocity).z; //Current speed of the rigidBody of the car
        //Debug.Log(speed);
        _uiManager.UpdateNeedleAngle(speed);
        _uiManager.UpdateSpeedLabel(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Ramp")
        {
            BoxCollider _bc = GetComponent<BoxCollider>();
            Debug.Log("Enter");
            _bc.enabled = false;
            StartCoroutine(ReactivateBody());
        }
    }

    private IEnumerator ReactivateBody()
    {
        yield return new WaitForSeconds(1);
        BoxCollider _bc = GetComponent<BoxCollider>();
        //Debug.Log("Exit");
        _bc.enabled = true;
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.tag == "Ramp")
    //    {
    //        BoxCollider _bc = GetComponent<BoxCollider>();
    //        Debug.Log("Exit"); 
    //        _bc.enabled = true;
    //    }
    //}

}
