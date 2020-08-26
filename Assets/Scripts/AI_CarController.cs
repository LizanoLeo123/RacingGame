using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CarController : MonoBehaviour
{
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _steeringAngle;
    //private bool _burst = false;
    private bool _aligned = false;
    //private float _carBreak;
    private Rigidbody _rb;

    //UI Manager to update de speedometer
    UI_Manager _uiManager;

    //Control variables
    public float maxSteeringAngle = 50f;
    public float motorForce = 50f;

    //Transform to follow
    public Transform target;

    //Wheels public elements
    public WheelCollider frontLeftW, frontRightW;
    public WheelCollider backLeftW, backRightW;
    public Transform frontLeftT, frontRightT;
    public Transform backLeftT, backRightT;

    private void GetDirection() //Equivalent to get input
    {
        if (_horizontalMovement != 0)
            _verticalMovement = 0.7f;
        else
            _verticalMovement = 1f;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + new Vector3(0f,0f,2f), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log(hit.transform.tag);
            //if (hit.transform.tag == "AI_Target")
            //{
            //    _aligned = true;
            //    //_horizontalMovement = 0f;
            //}
            //else
            //    _aligned = false;
                
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            //Debug.Log("Did not Hit");
        }
        Vector3 dir = (target.transform.position - transform.position).normalized;
        //Using dot product to get the magnitude of the vectors 
        float direction = Vector3.Dot(dir, transform.right);


        _horizontalMovement = System.Convert.ToSingle(System.Math.Round(direction, 2));
        Debug.Log(_horizontalMovement);

        //if (!_aligned)
        //{
        //    //Debug.Log("Car X : " + transform.TransformPoint(transform.position).x);
        //   // Debug.Log("Target X : " + transform.TransformPoint(target.position).x);
        //    //Get the direction of the target object
        //    Vector3 dir = (target.transform.position - transform.position).normalized;
        //    //Using dot product to get the magnitude of the vectors 
        //    float direction = Vector3.Dot(dir, transform.right);


        //    _horizontalMovement = System.Convert.ToSingle(System.Math.Round(direction, 2));
        //    Debug.Log(_horizontalMovement);
            
        //    //Debug.Log(System.Math.Round(direction, 2));
        //    //if (System.Math.Round(direction, 2) > 0) //If it's postive the target is right
        //    //{
        //    //    _horizontalMovement = 1f;
        //    //}
        //    //else if (System.Math.Round(direction, 2) < 0)//If it's negative the target is left
        //    //{

        //    //    _horizontalMovement = -1f;
        //    //}
        //    //else
        //    //{
        //    //    _horizontalMovement = 0f;
        //    //}
        //}
        
    }

    private void Steer()
    {
        _steeringAngle = maxSteeringAngle * _horizontalMovement;
        frontLeftW.steerAngle = _steeringAngle;
        frontRightW.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        frontRightW.motorTorque = _verticalMovement * motorForce;
        frontLeftW.motorTorque = _verticalMovement * motorForce;
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //    _rb.AddForce(Vector3.forward * 100, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * 10 * Time.deltaTime * _verticalInput);

        //if (_burst)
        //{ //My car is still and i want to start moving
        //    if (Input.GetAxis("Accelerate") < 0.5f)
        //        _rb.AddRelativeForce(Vector3.forward * 2000, ForceMode.Impulse);
        //    _burst = false;
        //}
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

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = new Vector3(0, -0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Vector3 dir = (target.transform.position - transform.position).normalized;
    //    float direction = Vector3.Dot(dir, transform.right);
    //    _horizontalMovement = direction;
    //    //Debug.Log(direction);
    //}
}
