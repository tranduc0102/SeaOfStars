using System.Collections;
using System.Collections.Generic;
using CoreActor;
using DesignPattern;
using UnityEngine;
using UnityEngine.Events;

public class ObseverAnimationEvent : Singleton<ObseverAnimationEvent>
{
   public Actor objectTarget;
   public UnityAction actionEnter;
   public UnityAction actionExit;
}
