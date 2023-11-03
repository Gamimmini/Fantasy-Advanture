using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Dash Ability", fileName = "Dash Ability")]
public class DashAbility : GenericAbility
{
    public float dashForce;
    public override void Ability(Vector2 playerPositon, Vector2 playerFacingDirection,
       Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {
        // đảm bảo player có đủ magic
        if(playerMagic.RuntimeValue >= magicCost)
        {
            playerMagic.RuntimeValue -= magicCost;
            usePlayerMagic.Raise();
        }    
        else
        {
            return; 
        } 
        if(playerRigidbody)
        {
            Vector3 dashVector = playerRigidbody.transform.position + (Vector3)playerFacingDirection.normalized * dashForce;
            playerRigidbody.DOMove(dashVector, duration);
        }    
    }
    
}
