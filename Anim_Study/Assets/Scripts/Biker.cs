using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : MonoBehaviour {
    #region 常量
    #endregion
    #region  属性
    #endregion
    #region 字段
    private Animator anim ;
    #endregion
    #region 事件
    #endregion
    #region 方法
    #endregion
    #region Unity回调
        // Use this for initialization
        void Start () {
        anim = gameObject.GetComponent<Animator>();
        }
        
        // Update is called once per frame
        void Update () {
          float v =  Input.GetAxisRaw("Vertical");
          anim.SetInteger("Vertical",(int)v);
         // transform.Translate(Vector3.forward*v*Time.deltaTime);

        }
    #endregion
    #region  事件回调
    #endregion
    #region 帮助方法
    #endregion
	
}
