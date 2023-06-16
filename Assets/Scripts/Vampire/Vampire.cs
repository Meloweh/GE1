using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D;
using UnityEngine;

public class Vampire : EntityHostile
{

    protected override void OnDie() {
        GetAnimator().SetTrigger("isDying");
        Invoke(nameof(DestroySelf), 0);
        //SpawnParticles();
        //SpawnBat();
    }

}
