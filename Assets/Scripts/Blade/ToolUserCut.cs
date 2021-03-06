﻿using UnityEngine;
using System.Collections;

public class ToolUserCut : MonoBehaviour
{

    [SerializeField]
    private Material capMaterial;

    [SerializeField]
    private float bladeDistance = 0f;
    public float BladeDistance
    {
        get { return this.bladeDistance; }
        set { this.bladeDistance = value; }
    }

    // Use this for initialization
    void Start()
    {


    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, this.bladeDistance, LayerMask.GetMask(new string[] { "Slice" })))
        {
            /// 一度切ってたら処理しない
            Watermelon wmelon = hit.collider.gameObject.GetComponent<Watermelon>();
            if (wmelon.IsCutted){
                return;
            }

            /// 今切った
            wmelon.IsCutted = true;

            GameObject victim = hit.collider.gameObject;
            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            Rigidbody rigidLeft = pieces[0].GetComponent<Rigidbody>();
            Rigidbody rigidRight = pieces[1].GetComponent<Rigidbody>();
            if (!pieces[1].GetComponent<Rigidbody>())
            {
                rigidRight = pieces[1].AddComponent(typeof(Rigidbody)) as Rigidbody;
                rigidRight.mass = rigidLeft.mass;
                rigidRight.interpolation = rigidLeft.interpolation;
            }

            Vector3 velocity = victim.GetComponent<Rigidbody>().velocity;

            rigidLeft.velocity = Quaternion.Euler(0f,  45, 0f) * velocity * 0.6f;
            rigidRight.velocity = Quaternion.Euler(0f, -45, 0f) * velocity * 0.6f;

            /// 試しにバレットタイム
            GameManager.Instance.StartBulletTime();

            /// 分割したオブジェクトは時間経過で消す
            Destroy(pieces[0].gameObject, 2);
            Destroy(pieces[1].gameObject, 2);

            /// 親も含めて刀身は消える
            Destroy(this.transform.parent.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * this.bladeDistance);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * this.bladeDistance);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * this.bladeDistance);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);

    }

}
