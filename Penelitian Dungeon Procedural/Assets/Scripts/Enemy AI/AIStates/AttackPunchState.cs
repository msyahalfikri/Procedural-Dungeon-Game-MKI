using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI.FSM
{
    public class AttackPunchState : EnemyStateBase
    {
        public AttackPunchState(bool needsExitTime, Enemy enemy, Transform target) : base(needsExitTime, enemy)
        { }
        public override void OnEnter()
        {
            agent.isStopped = true;
            base.OnEnter();
            //animator.Play("Attack");
        }

        public override void OnLogic()
        {

        }
    }

}
