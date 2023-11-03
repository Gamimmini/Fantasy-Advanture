using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Generic Ability", fileName = "New Generic Ability")]
public class GenericAbility : ScriptableObject
{
    public float magicCost;
    public float duration;

    public FloatValue playerMagic;
    public SignalSender usePlayerMagic;
   public virtual void Ability(Vector2 playerPositon, Vector2 playerFacingDirection, 
       Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
   {

   }      
}
