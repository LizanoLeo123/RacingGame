using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject needle;

    public Text speedLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNeedleAngle(float speed)
    {
        
        if(speed < 30f)
            needle.transform.eulerAngles = new Vector3(0, 0, -1 * Mathf.Abs(speed) * 8);
    }

    public void UpdateSpeedLabel(float speed)
    {
        float kmh = speed * 3.6f;
        speedLabel.text = kmh.ToString("N2") + " km/h";
    }
}
