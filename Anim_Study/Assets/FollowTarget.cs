using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    #region 常量
    #endregion
    #region  属性
    #endregion
    #region 字段
    private Transform player;
    private Vector3 offset;
    private float smoothing = 3;
    #endregion
    #region 事件
    #endregion
    #region 方法
    #endregion
    #region Unity回调
        // Use this for initialization
        void Start () {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            offset = transform.position-player.position;
        }
        
        // Update is called once per frame
        void LateUpdate () {
            Vector3 targetPosition = player.position+player.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothing*Time.deltaTime);
            transform.LookAt(player.position);
        }
    #endregion
    #region  事件回调
    #endregion
    #region 帮助方法
    #endregion
	
}
