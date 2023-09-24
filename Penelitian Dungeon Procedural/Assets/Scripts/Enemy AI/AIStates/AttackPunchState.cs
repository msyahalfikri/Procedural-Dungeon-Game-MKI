using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using System;

namespace EnemyAI.FSM
{
    public class AttackPunchState : EnemyStateBase
    {
        public AttackPunchState(
        bool needsExitTime, Enemy enemy,
        Action<State<EnemyState, StateEvent>> onEnter,
        float exitTime = 0.33f) : base(needsExitTime, enemy, exitTime, onEnter)
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
