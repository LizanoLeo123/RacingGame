using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject needle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateNeedleAngle(float speed)
    {
        needle.transform.eulerAngles = new Vector3(0, 0, -1 * Mathf.Abs(speed * 20));
    }
}
