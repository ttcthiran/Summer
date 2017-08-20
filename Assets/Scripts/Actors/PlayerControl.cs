using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject touchParticleInstance;

    [SerializeField]
    private GameObject baldeInstance;

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
            beganPosition = AppUtil.GetTouchWorldPosition(Camera.main, Camera.main.nearClipPlane + 1.0f);
        }
        else if (AppUtil.GetTouch() == TouchInfo.Ended)
        {
            Vector3 endPosition = AppUtil.GetTouchWorldPosition(Camera.main, Camera.main.nearClipPlane + 1.0f);

            /// オブジェクト生成
            GameObject obj = Instantiate(baldeInstance, beganPosition, Quaternion.identity);

            /// 視点、終点をもとにブレードの向きを設定
            obj.transform.up = (endPosition - this.beganPosition).normalized;

            /// ブレードの長さ
            obj.GetComponentInChildren<ToolUserCut>().BladeDistance = (endPosition - this.beganPosition).magnitude;

            Destroy(obj, 2f);

        }
        else if (AppUtil.GetTouch() == TouchInfo.Moved)
        {
            gameObject.transform.position = AppUtil.GetTouchWorldPosition(Camera.main, Camera.main.nearClipPlane + 1.0f);

            GameObject obj = Instantiate(touchParticleInstance, gameObject.transform.position, Quaternion.identity, this.transform);

            // 1秒後に消す
            Destroy(obj, 1f);
        }
    }
}
