namespace Project.Player.Input
{
    /// Processes the players' inputs from Unity's Input System
    public class InputControlsInjector<TContextData, TEndData> : InputInjector<TContextData, TEndData> 
        where TContextData : InputContextData
        where TEndData : InputEndData
    {
        protected InputControls _controls { get; private set; }

        public override void SetContextAndEndData(TContextData contextData, TEndData endData)
        {
            base.SetContextAndEndData(contextData, endData);

            _controls = new InputControls();
            Enable();
        }

        public void Enable()
        {
            _controls.Enable();
        }

        // Don't forget to unsubscribe to all events
        public virtual void Disable()
        {
            _controls.Disable();
        }

    }
}