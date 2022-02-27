using Project.Pool;
using Project.StateMachine;
using UnityEngine;

namespace Project.Player.Input
{
    /// <summary>
    /// Inherited by all classes necessitating an automatic or manual input stream
    /// (ex: Character controllers, UI navigation, etc.)
    /// </summary>
    /// <typeparam name="TContextData">The data shared by all states of the Controller and sent to the Injector.</typeparam>
    /// <typeparam name="TEndData">The data modified by the Injector and read by the Controller to change its state.</typeparam>
    public abstract class Controller<TContextData, TEndData> : Controller 
        where TContextData : InputContextData, new() 
        where TEndData : InputEndData, new()
    {
        #region Fields

        [field: SerializeField] public TContextData ControllerData { get; private set; }
        [field: SerializeField] public TEndData InputData { get; private set; }


        // Creates a State Machine to recycle the Controller's states.
        protected StateMachine<TContextData, TEndData> _stateFactory { get; set; }
        protected ClassPooler<State> _statesPooler;

        #endregion


        #region Mono

        private void OnEnable()
        {
            if (Injector is InputControlsInjector<TContextData, TEndData> controlsInjector)
            {
                controlsInjector.Enable();
            }
        }
        private void OnDisable()
        {
            if (Injector is InputControlsInjector<TContextData, TEndData> controlsInjector)
            {
                controlsInjector.Disable();
            }
        }

        protected override void Awake()
        {
            SetupContextData();
            base.Awake();
            _stateFactory = new(ControllerData, InputData, _statesPooler);
        }

        protected override void Update()
        {
            base.Update();
            _stateFactory.Update();
        }

        private void FixedUpdate()
        {
            _stateFactory.FixedUpdate();
        }

        #endregion


        #region Setup

        /// <summary>
        /// Swaps between control schemes (Player, AI, etc.)
        /// </summary>
        public void SetInjector<TInjector>() where TInjector : InputInjector<TContextData, TEndData>
        {
            if (Injector is not null) 
            {
                //We disable the InputSystem script currently in use if any.
                OnDisable();
                _injectorPooler.ReturnToPool(Injector);
            }
            
            TInjector newInjector = _injectorPooler.GetFromPool<TInjector>();
            newInjector.SetContextAndEndData(ControllerData, InputData);
            Injector = newInjector;
            Injector.OnStart();
        }


        /// <summary>
        /// Allows to assign all references from the Inspector to the ContextData.
        /// </summary>
        private void SetupContextData()
        {
            ControllerData = new TContextData();
            InputData = new TEndData();
            ControllerData.SetupContextData(this);
        }

        #endregion
    }


    public abstract class Controller : MonoBehaviour
    {
        #region Fields

        internal InputInjector Injector { get; private protected set; }
        protected ClassPooler<InputInjector> _injectorPooler;

        #endregion


        #region Mono

       

        protected virtual void Awake() => SetupPoolers();
        protected virtual void Start() => Injector.OnStart();
        protected virtual void Update() => Injector.OnUpdate();

        #endregion


        #region Setup

        /// <summary>
        /// Lets the subclasses handle which states and injectors to use.
        /// The _statePooler will be automatically referenced by the State Machine.
        /// </summary>
        protected abstract void SetupPoolers();



        #endregion
    }
}