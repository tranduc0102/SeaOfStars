using System;
using System.Collections;
using System.Collections.Generic;
using CoreActor;
using UnityEngine;

public class CollisionActor : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      if (other.transform.TryGetComponent(out Actor actor))
      {
         actor.Animator.SetTrigger("Hit");
      }
   }
}
