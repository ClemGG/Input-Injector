using Project.Player.Input;

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
            _controls.Player.Grow.started += ctx => _endData.ShouldGrow = true;
            _controls.Player.Grow.canceled += ctx => _endData.ShouldGrow = false;
        }

        public override void OnUpdate()
        {
            _endData.Horizontal = _controls.Player.Move.ReadValue<float>();
            _endData.HasPressedSwap = _controls.Player.Swap.triggered;

        }
    }
}