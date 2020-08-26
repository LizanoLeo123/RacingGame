using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Target : MonoBehaviour
{
    public Transform[] Marks;

    private int _currentMark = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentMark == Marks.Length) 
            _currentMark = 0;

        transform.position = Marks[_currentMark].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "AI_Car")
        {
            if (_currentMark == 11)
            {
                Rigidbody _rb = other.gameObject.GetComponent<Rigidbody>();
                _rb.velocity = _rb.velocity / 2;
            }
            _currentMark++;
        }
        
        //BoxCollider _bc = GetComponent<BoxCollider>();
        //_bc.enabled = false;
        //yield return new WaitForSeconds(0.2f);
        //_bc.enabled = true;
    }
}
