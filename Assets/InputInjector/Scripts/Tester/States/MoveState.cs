using Project.Player.Tester;
using UnityEngine;

namespace Project.StateMachine
{
    public class MoveState : BaseState<TesterContextData, TesterEndData>
    {

        #region State Methods

        protected override void Enter()
        {
            _ctx.T.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        protected override void FixedUpdate() 
        {
            _ctx.T.Translate(_ctx.MoveSpeed * _input.Horizontal * Time.deltaTime * Vector3.right);
        }

        protected override void CheckSwitchStates()
        {
            if (Mathf.Approximately(_input.Horizontal, 0f))
            {
                SwitchState<IdleState>();
            }
        }

        #endregion
    }
}
