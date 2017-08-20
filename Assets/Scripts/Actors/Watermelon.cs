using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour {

    private bool isCutted = false;
    public bool IsCutted
    {
        get { return this.isCutted; }
        set { this.isCutted = value; }
    }

	// Use this for initialization
	void Start ()
    {
        Rigidbody rig = this.GetComponent<Rigidbody>();

        /// 賑やかしで回転させる
        Vector3 torqe = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        rig.AddTorque(torqe);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("Ground"))
        {
            DestroyObject(this.gameObject);
        }
    }
}
