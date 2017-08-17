using UnityEngine;
using System.Collections;

public class ToolUserCut : MonoBehaviour
{

    [SerializeField]
    private Material capMaterial;

    [SerializeField]
    private float bladeDistance = 0f;

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

            Rigidbody rigidRight = pieces[0].GetComponent<Rigidbody>();
            Rigidbody rigidLeft = pieces[1].GetComponent<Rigidbody>();
            if (!pieces[1].GetComponent<Rigidbody>())
            {
                rigidLeft = pieces[1].AddComponent(typeof(Rigidbody)) as Rigidbody;
                rigidLeft.mass = rigidRight.mass;
                rigidLeft.interpolation = rigidRight.interpolation;
            }

            Vector3 velocity = victim.GetComponent<Rigidbody>().velocity;

            rigidRight.velocity = Quaternion.Euler(0f,  45, 0f) * velocity * 0.6f;
            rigidLeft.velocity = Quaternion.Euler(0f, -45, 0f) * velocity * 0.6f;

            /// 試しにバレットタイム
            GameManager.Instance.StartBulletTime();

            Destroy(pieces[0].gameObject, 2);
            Destroy(pieces[1].gameObject, 2);
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
