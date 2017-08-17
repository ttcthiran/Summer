using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float baseTime = 0f;

    private bool hooldTypeVertical = true;

    private float startTime = 0f;

    private Vector3 eulerAngle = Vector3.zero;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHoldType();

        float diff = Time.timeSinceLevelLoad - this.startTime;
        float rate = diff / baseTime;

        if(diff > baseTime)
        {

        }
        else
        {
            if (this.hooldTypeVertical)
            {
                this.transform.eulerAngles = Vector3.Lerp(this.eulerAngle, new Vector3(0f, 0f, 0f), rate);
            }
            else
            {
                this.transform.eulerAngles = Vector3.Lerp(this.eulerAngle, new Vector3(0f, 0f, 70f), rate);
            }
        }
    }

    private void UpdateHoldType()
    {
        if (AppUtil.GetTouch() == TouchInfo.Began)
        {
            this.hooldTypeVertical = !this.hooldTypeVertical;

            this.startTime = Time.timeSinceLevelLoad;

            this.eulerAngle = this.transform.localRotation.eulerAngles;
        }
    }
}
