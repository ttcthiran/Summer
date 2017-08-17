using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour {

    [SerializeField]
    private float yOffset = 0f;

    // 位置座標
    private Vector3 position;

    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    // 初期座標
    private Vector3 oriPosition;

    // Use this for initialization
    void Start()
    {
        oriPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdatePosition()
    {
        // Vector3でマウス位置座標を取得する
        this.position = Input.mousePosition;
        // Z軸修正
        this.position.z = this.oriPosition.z;

        // マウス位置座標をスクリーン座標からワールド座標に変換する
        this.screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

        // Y少し修正
        this.screenToWorldPointPosition.y += yOffset;

        // ワールド座標に変換されたマウス座標を代入
        this.gameObject.transform.position = screenToWorldPointPosition;
    }
}
