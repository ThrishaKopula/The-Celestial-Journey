using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCJ{
public class AnimatorManager : MonoBehaviour
{
    public Animator anim;
    int vertical;
    int horizontal;

    public bool camRotate;

    public void Initialize(){
        anim.GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimator(float VerticalMovement, float HorizontalMovement){
        #region Vertical
        
        float v = 0;

        if(VerticalMovement > 0 && VerticalMovement < 0.55f){
            v = 0.5f;
        }

        else if(VerticalMovement > 0.55f){
            v = 1;
        }
        
        else if(VerticalMovement < 0 && VerticalMovement > -0.55f){
            v = -0.5f;
        }

        else if (VerticalMovement < -0.55f){
            v = -1;
        }
        else{
            v = 0;
        }
        #endregion

        #region Horizontal
        
        float h = 0;

        if(HorizontalMovement > 0 && HorizontalMovement < 0.55f){
            h = 0.5f;
        }

        else if(HorizontalMovement > 0.55f){
            h = 1;
        }
        
        else if(HorizontalMovement < 0 && HorizontalMovement > -0.55f){
            h = -0.5f;
        }

        else if (HorizontalMovement < -0.55f){
            h = -1;
        }
        else{
            h = 0;
        }
        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting){
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting",isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }
    public void canRotate(){
        camRotate = true;
    }

     public void stopRotate(){
        camRotate = false;
    }
}

}