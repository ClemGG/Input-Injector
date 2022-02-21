using Project.Player.Tester;
using UnityEngine;

namespace Project.StateMachine
{
    public class IdleState : BaseState<TesterContextData, TesterEndData>
    {


        #region State Methods

        protected override void Enter()
        {
            _ctx.T.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        protected override void CheckSwitchStates()
        {
            if(!Mathf.Approximately(_input.Horizontal, 0f))
            {
                SwitchState<MoveState>();
            }
        }

        #endregion
    }
}
