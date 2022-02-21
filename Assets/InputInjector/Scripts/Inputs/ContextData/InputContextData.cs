namespace Project.Player.Input
{
    /// <summary>
    /// Contains the values shared by all states of the Controller's state machine.
    /// These are sent to the Injector to modify the Controller's end data to change its state.
    /// 
    /// Use SetupContextData in the subclasses to explicitely state which attributes are shared between
    /// the Controller and the Injector.
    /// </summary>
    public abstract class InputContextData
    {
        public virtual void SetupContextData(Controller controller) { }
    }
}