namespace Project.Player.Input
{
    /// <summary>
    /// The class used to change the InputContainr's values
    /// depending on the player's inputs or an AI's behaviour.
    /// </summary>
    /// <typeparam name="TContextData">The data shared by the Controller driving what type of input to inject.</typeparam>
    /// <typeparam name="TEndData">The data to manipulate.</typeparam>
    public abstract class InputInjector<TContextData, TEndData> : InputInjector 
        where TContextData : InputContextData
        where TEndData : InputEndData
    {
        protected TContextData _contextData { get; private set; }
        protected TEndData _endData { get; private set; }


        /// <summary>
        /// When we want to swap injectors, we can just pass the same container 
        /// from one injector to the other.
        /// </summary>
        public virtual void SetContextAndEndData(TContextData contextData, TEndData endData)
        {
            _contextData = contextData;
            _endData = endData;
        }
    }

    public abstract class InputInjector
    {
        public virtual void GetDelegatedInputs() { }    //Called once at startup to assign commands to delegates if needed
        public virtual void GetContinuousInputs() { }   //Called every Update
    }
}