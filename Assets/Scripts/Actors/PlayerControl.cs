using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject touchParticleInstance;

    [SerializeField]
    private GameObject baldeInstance;

    [SerializeField]
    private float bladeLineMinDistance = 0f;

    private Vector3 beganPosition;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (AppUtil.GetTouch() == TouchInfo.Began)
        {
            this.beganPosition = AppUtil.GetTouchWorldPosition(Camera.main, Camera.main.nearClipPlane + 1.0f);
        }
        else if (AppUtil.GetTouch() == TouchInfo.Ended)
        {
            Vector3 endPosition = AppUtil.GetTouchWorldPosition(Camera.main, Camera.main.nearClipPlane + 1.0f);
            float magnitude = (endPosition - beganPosition).magnitude;

            if (magnitude >= this.bladeLineMinDistance)
            {
                /// オブジェクト生成
                GameObject obj = Instantiate(this.baldeInstance, this.beganPosition, Quaternion.identity);

                /// 視点、終点をもとにブレードの向きを設定
                obj.transform.up = (endPosition - this.beganPosition).normalized;

                /// ブレードの長さ
                obj.GetComponentInChildren<ToolUserCut>().BladeDistance = (endPosition - this.beganPosition).magnitude;

                /// 何も切らなくても指定秒数後に消える
                Destroy(obj, 2f);
            }
        }
        else if (AppUtil.GetTouch() == TouchInfo.Moved)
        {
            gameObject.transform.position = AppUtil.GetTouchWorldPosition(Camera.main, Camera.main.nearClipPlane + 1.0f);

            GameObject obj = Instantiate(this.touchParticleInstance, gameObject.transform.position, Quaternion.identity, this.transform);

            // 1秒後に消す
            Destroy(obj, 1f);
        }
    }
}
