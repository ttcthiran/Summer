using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

    private Text timerText;

    private string timerStr;

	// Use this for initialization
	void Start ()
    {
        this.timerText = GetComponent<Text>();

        this.timerStr = this.timerText.text;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.timerText.text = this.timerStr + GameManager.Instance.GamePlayTime.ToString("0.0");
    }
}
