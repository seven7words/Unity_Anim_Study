using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
    private int isHoldLogId = Animator.StringToHash("IsHoldLog");
    private CharacterController characterController;
    private Vector3 matchTarget = Vector2.zero;
    public GameObject unityLog = null;
    public Transform LeftHand;
    public Transform RightHand;
    public PlayableDirector director;
    private int sliderId = Animator.StringToHash("Slider");
    #endregion
    #region 事件
    #endregion
    #region 方法

        private void ProcessSlider(){
            bool isSlider = false;
             if(animator.GetFloat(speedZId)>3&&animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")){
                  RaycastHit hit;
                 if(Physics.Raycast(transform.position+Vector3.up*1.5f,transform.forward,out hit,2)){
                    if(hit.collider.tag == "Obstacle"){
                        if(hit.distance>1)
                        {
                            Vector3 point = hit.point;
                            point.y = 0;
                            matchTarget =   point+transform.forward*2;
                            isSlider = true;
                        }          
                    }
                 }
             }
             animator.SetBool(sliderId,isSlider);
              if(animator.GetCurrentAnimatorStateInfo(0).IsName("Slider")&&animator.IsInTransition(0)==false){
               animator.MatchTarget(matchTarget,Quaternion.identity,AvatarTarget.LeftHand,new MatchTargetWeightMask(Vector3.one,0),0.32f,0.4f);

            }
        }
        private void ProcessVault(){
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
               animator.MatchTarget(matchTarget,Quaternion.identity,AvatarTarget.Root,new MatchTargetWeightMask(new Vector3(1,0,1),0),0.17f,0.67f);

            }
            if(animator.GetFloat(colliderId)>0.5f){
                characterController.enabled = false;
            }else{
                characterController.enabled = true;
            }
        }
    #endregion
    #region Unity回调
        // Use this for initialization
        void Start () {
            animator = gameObject.GetComponent<Animator>();
            characterController = gameObject.GetComponent<CharacterController>();
            //unityLog = transform.Find("Log").gameObject;
        }
        
        // Update is called once per frame
        void Update () {
            animator.SetFloat(speedZId,Input.GetAxis("Vertical")*4.1f);
            animator.SetFloat(speedRotateId,Input.GetAxis("Horizontal")*126f);
            ProcessVault();
            ProcessSlider();
            // animator.SetFloat(speedID,Input.GetAxisRaw("Vertical")*4.1f);
            // animator.SetFloat(horizontalId,Input.GetAxis("Horizontal"));
            // if(Input.GetKeyDown(KeyCode.LeftShift)){
            //     animator.SetBool(IsSpeedUpId,true);
            // }
            // if(Input.GetKeyUp(KeyCode.LeftShift)){
            //     animator.SetBool(IsSpeedUpId,false);
            // }
           

        }
        void OnTriggerEnter(Collider other){
            if(other.tag == "Log"){
                Destroy(other.gameObject);
                CarryWood();
            }
            if(other.tag =="Playable"){
                Debug.Log("????");
                director.Play();
            }
        }
        void CarryWood(){
            unityLog.SetActive(true);
            Debug.Log(unityLog);
            animator.SetBool(isHoldLogId,true);
        }
        private void OnAnimatorIK(int layerIndex) {
            if(layerIndex==1){
                int weight = animator.GetBool(isHoldLogId)?1:0;
                //当前是被HoldLog这一层调用的
                animator.SetIKPosition(AvatarIKGoal.LeftHand,LeftHand.transform.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand,LeftHand.transform.rotation);
                //aniamtor.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,weight);
                animator.SetIKPosition(AvatarIKGoal.RightHand,RightHand.transform.position);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,weight);
            }
        }
    #endregion
    #region  事件回调
    #endregion
    #region 帮助方法
    #endregion
	
}
