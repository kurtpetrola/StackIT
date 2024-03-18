using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimator : MonoBehaviour
{
  public List<Animator> animators = new List<Animator>();

  public bool callAppearOnStart;

  void Start()
  {
    if (callAppearOnStart)
    {
      CallAppearOnAllAnimators();
    }
  }

  public void CallAppearOnAllAnimators()
  {
    foreach (Animator anim in animators)
    {
      anim.ResetTrigger("Hide");
      anim.SetTrigger("Show");
    }
  }
  // can pass an animators name to skip it 
  public void CallDisappearOnAllAnimators(string skip = "")
  {
    foreach (Animator anim in animators)
    {
      if (anim.name == skip)
      {
        continue;
      }
      anim.ResetTrigger("Show");
      anim.SetTrigger("Hide");
    }
  }
}
