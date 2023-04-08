using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : PlayerAction
{
    public override void Execute(GameObject playerObject)
    {
       Rigidbody playerRigidBody= playerObject.GetComponent<Rigidbody>();
        playerRigidBody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }
}
