using Project.Player.Input;

namespace Project.Player.Tester
{
    /// <summary>
    /// This Injector is the "AI" controlling the other InjectorTester.
    /// </summary>
    public class AITesterInjector : InputInjector<TesterContextData, TesterEndData>
    {
        //OnStart() is only needed when we need to wire a behaviour on startup.
        //Since this AI will calculate everything it needs at runtime, we only need OnUpdate().
        //This one will be used only to initialize the values if needed.
        public override void OnStart()
        {
            _endData.Horizontal = 1f;
            _endData.HasPressedSwap = false;    //clean-up from last swap
        }

        public override void OnUpdate()
        {
            _endData.ShouldGrow = true;
            if (_endData.IsColliding)
            {
                _endData.Horizontal *= -1f;
            }
        }
    }
}