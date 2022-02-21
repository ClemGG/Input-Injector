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
            
            if (!_ctx.IsAI) 
            { 
                _ctx.PlayerIndicatorUI.SetParent(_ctx.T);
                _ctx.PlayerIndicatorUI.localPosition = Vector3.up * 2f;
            }
        }

        protected override void Update()
        {
            if (_input.ShouldGrow)
            {
                _globalTime += _ctx.GrowSpeed * Time.deltaTime;
            }

            _growthSize = Mathf.PingPong(_globalTime, 3f);



            //Changes the Controllers' Injectors when we press space
            if (_input.HasPressedSwap)
            {
                _ctx.IsAI = !_ctx.IsAI;
                _ctx.OtherController.ControllerData.IsAI = !_ctx.OtherController.ControllerData.IsAI;

                if (_ctx.IsAI)
                {
                    _ctx.Controller.SetInjector<AITesterInjector>();
                    _ctx.OtherController.SetInjector<TesterInjector>();
                }
                else
                {
                    _ctx.Controller.SetInjector<TesterInjector>();
                    _ctx.OtherController.SetInjector<AITesterInjector>();
                }
            }

            //When we swap the Injector, we display the "Player" cursor above the Controller used by the Player
            if (!_ctx.IsAI)
            {
                if (_ctx.PlayerIndicatorUI.parent != _ctx.T) 
                {
                    _ctx.PlayerIndicatorUI.SetParent(_ctx.T);
                    _ctx.PlayerIndicatorUI.localPosition = Vector3.up * 2f;
                    _ctx.PlayerIndicatorUI.localScale = Vector3.one * .02f;
                }
            }

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
