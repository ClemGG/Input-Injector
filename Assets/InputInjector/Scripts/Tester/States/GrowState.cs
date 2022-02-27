using Project.Player.Tester;
using UnityEngine;

namespace Project.StateMachine
{
    public class GrowState : BaseState<TesterContextData, TesterEndData>
    {
        #region Fields

        private float _growthSize;
        private float _globalTime = 1f;

        #endregion


        #region Constructor & IPooled

        public GrowState()
        {
            _isRootState = true;
        }

        #endregion


        #region State Methods


        protected override void Enter()
        {
            base.Enter(); 
            
        }

        protected override void Update()
        {
            if (_input.ShouldGrow)
            {
                _globalTime += _ctx.GrowSpeed * Time.deltaTime;
            }

            _growthSize = Mathf.PingPong(_globalTime, 3f);
        }
        protected override void FixedUpdate() 
        {
            _ctx.T.localScale = Vector3.one * _growthSize;
        }
        protected override void InitSubState()
        {
            SetSubState<IdleState>();
        }

        #endregion
    }
}
