using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {
    #region 常量
    #endregion
    #region  属性
    #endregion
    #region 字段
    public float rotateSpeed = 90;
    #endregion
    #region 事件
    #endregion
    #region 方法
    #endregion
    #region Unity回调
        // Use this for initialization
        void Start () {
            
        }
        
        // Update is called once per frame
        void Update () {
            transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime,Space.World);
        }
    #endregion
    #region  事件回调
    #endregion
    #region 帮助方法
    #endregion
	
}
