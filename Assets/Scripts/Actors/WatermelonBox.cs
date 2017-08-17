using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonBox : MonoBehaviour
{

    [SerializeField]
    private GameObject waterMelonObj;

    [SerializeField]
    private float shootSpeed = 0f;

    [SerializeField]
    private float nextWaitMin = 0f;

    [SerializeField]
    private float nextWaitMax = 0f;

    [SerializeField]
    private float shootAngleMin = 0f;

    [SerializeField]
    private float shootAngleMax = 0f;

    private float timer = 0f;

    // Use this for initialization
    void Start()
    {
        this.timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.timer -= Time.deltaTime;
        if (this.timer <= 0f)
        {
            GameObject obj = Instantiate(this.waterMelonObj, this.transform.position, Quaternion.identity, this.transform);

            float angle = Random.Range(this.shootAngleMin, this.shootAngleMax);

            Vector3 force;
            force.x = 0f;
            force.y = Mathf.Sign(angle * Mathf.Deg2Rad);
            force.z = -Mathf.Cos(angle * Mathf.Deg2Rad);
            force = Vector3.Normalize(force) * this.shootSpeed;
            force = Quaternion.Euler(0f, Random.Range(-5f, 5f), 0f) * force;

            // Rigidbodyに力を加えて発射
            obj.GetComponent<Rigidbody>().AddForce(force);

            // 次の発射までの時間
            this.timer = Random.Range(this.nextWaitMin, this.nextWaitMax);
        }
    }
}
