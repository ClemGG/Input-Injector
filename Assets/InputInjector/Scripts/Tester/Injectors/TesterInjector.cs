using Project.Player.Input;
using UnityEngine.InputSystem;

namespace Project.Player.Tester
{
    /// <summary>
    /// This Injector is the "brain" powered by the user's input.
    /// </summary>
    public class TesterInjector : InputControlsInjector<TesterContextData, TesterEndData>
    {
        public override void OnStart()
        {
            _endData.ShouldGrow = false;    //clean-up from last swap
            _controls.Player.Grow.started += EnableGrow;
            _controls.Player.Grow.canceled += EnableGrow;
        }

        public override void OnUpdate()
        {
            _endData.Horizontal = _controls.Player.Move.ReadValue<float>();
            _endData.HasPressedSwap = _controls.Player.Swap.triggered;
        }

        protected override void Unsubscribe()
        {
            _controls.Player.Grow.started -= EnableGrow;
            _controls.Player.Grow.canceled -= EnableGrow;
        }

        private void EnableGrow(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                _endData.ShouldGrow = true;
            }
            else if (ctx.canceled)
            {
                _endData.ShouldGrow = false;
            }
        }
    }
}