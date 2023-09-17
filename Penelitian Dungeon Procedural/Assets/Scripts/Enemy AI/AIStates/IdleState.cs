using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI.FSM
{
    public class IdleState : EnemyStateBase
    {
        public IdleState(bool needsExitTime, Enemy enemy) : base(needsExitTime, enemy) { }

        public override void OnEnter()
        {
            base.OnEnter();
            agent.isStopped = true;
            // animator.Play(stateName: Idle_A);
        }
    }

}
