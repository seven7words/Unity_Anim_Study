using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region 常量
    #endregion
    #region  属性
    #endregion
    #region 字段
    private Animator animator;
    private int speedID = Animator.StringToHash("Speed");
    private int IsSpeedUpId = Animator.StringToHash("IsSpeedUp");
    private int horizontalId = Animator.StringToHash("Horizontal");
    private int speedRotateId = Animator.StringToHash("SpeedRotate");
    private int speedZId = Animator.StringToHash("SpeedZ");
    private int vaultId = Animator.StringToHash("Vault");
    private int colliderId = Animator.StringToHash("Collider");
    private CharacterController characterController;
    private Vector3 matchTarget = Vector2.zero;
    #endregion
    #region 事件
    #endregion
    #region 方法
    #endregion
    #region Unity回调
        // Use this for initialization
        void Start () {
            animator = gameObject.GetComponent<Animator>();
            characterController = gameObject.GetComponent<CharacterController>();

        }
        
        // Update is called once per frame
        void Update () {
            animator.SetFloat(speedZId,Input.GetAxis("Vertical")*4.1f);
            animator.SetFloat(speedRotateId,Input.GetAxis("Horizontal")*126f);
            // animator.SetFloat(speedID,Input.GetAxisRaw("Vertical")*4.1f);
            // animator.SetFloat(horizontalId,Input.GetAxis("Horizontal"));
            // if(Input.GetKeyDown(KeyCode.LeftShift)){
            //     animator.SetBool(IsSpeedUpId,true);
            // }
            // if(Input.GetKeyUp(KeyCode.LeftShift)){
            //     animator.SetBool(IsSpeedUpId,false);
            // }
            bool isVault =false;
            if(animator.GetFloat(speedZId)>3&&animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")){
                RaycastHit hit;
                if(Physics.Raycast(transform.position+Vector3.up*0.3f,transform.forward,out hit,4)){
                    if(hit.collider.tag == "Obstacle"){
                        if(hit.distance>3)
                        {
                            Vector3 point = hit.point;
                            point.y = hit.collider.transform.position.y+hit.collider.bounds.size.y+0.07f;
                            matchTarget = point;
                            isVault = true;
                        }          
                    }
                }
                

            }
            animator.SetBool(vaultId,isVault);
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Vault")&&animator.IsInTransition(0)==false){
               animator.MatchTarget(matchTarget,Quaternion.identity,AvatarTarget.LeftHand,new MatchTargetWeightMask(Vector3.one,0),0.32f,0.4f);

            }
            if(animator.GetFloat(colliderId)>0.5f){
                characterController.enabled = false;
            }else{
                characterController.enabled = true;
            }

        }
    #endregion
    #region  事件回调
    #endregion
    #region 帮助方法
    #endregion
	
}
