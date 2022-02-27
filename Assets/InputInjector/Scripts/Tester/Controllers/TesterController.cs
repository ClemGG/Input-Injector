using Project.Player.Input;
using Project.Pool;
using Project.StateMachine;
using UnityEngine;

namespace Project.Player.Tester
{
    public class TesterController : Controller<TesterContextData, TesterEndData>
    {
        #region Public Fields

        [Space(20)]

        [SerializeField] protected internal bool _isAI = false;
        [SerializeField] protected internal Transform _playerIndicatorUI;       //The UI set above the Controller currently managed by the Player.
        [SerializeField] protected internal TesterController _otherController;  //Stored to swap between them

        #endregion


        #region Mono

        protected override void Awake()
        {
            base.Awake();
            _stateFactory.Start<GrowState>();
        }

        protected override void Start()
        {
            base.Start();

            //Set Player UI on the object controlled by the Player
            if (!_isAI)
            {
                _playerIndicatorUI.SetParent(ControllerData.T);
                _playerIndicatorUI.localPosition = Vector3.up * 2f;
            }
        }

        protected override void Update()
        {
            base.Update();

            //Changes the Controllers' Injectors when we press space
            if (InputData.HasPressedSwap)
            {
                ControllerData.IsAI = !ControllerData.IsAI;
                _otherController.ControllerData.IsAI = !_otherController.ControllerData.IsAI;

                if (ControllerData.IsAI)
                {
                    SetInjector<AITesterInjector>();
                    _otherController.SetInjector<TesterInjector>();
                }
                else
                {
                    SetInjector<TesterInjector>();
                    _otherController.SetInjector<AITesterInjector>();

                }
            }

            if (!ControllerData.IsAI)
            {
                if(_playerIndicatorUI.parent != ControllerData.T)
                {
                    //When we swap the Injector, we display the "Player" cursor above the Controller used by the Player
                    _playerIndicatorUI.SetParent(ControllerData.T);
                    _playerIndicatorUI.localPosition = Vector3.up * 2f;
                    _playerIndicatorUI.localScale = Vector3.one * .02f;
                }
            }
        }

        


        /* We use the Controller for the MonoBehaviour related instructions,
         * like physics.
         * 
         * This will make the AI Injector change directions once when it collides with a wall.
         */
        private void OnCollisionEnter(Collision collision)
        {
            InputData.IsColliding = true;
        }
        private void OnCollisionExit(Collision collision)
        {
            InputData.IsColliding = false;
        }


        #endregion


        #region Controller Overrides

        protected override void SetupPoolers()
        {
            _injectorPooler = new ClassPooler<InputInjector>
                (
                    new Pool<InputInjector>(nameof(TesterInjector), 1, () => new TesterInjector()),
                    new Pool<InputInjector>(nameof(AITesterInjector), 1, () => new AITesterInjector())
                );

            _statesPooler = new ClassPooler<State>
                (
                    new Pool<State>(nameof(GrowState), 1, () => new GrowState()),
                    new Pool<State>(nameof(IdleState), 1, () => new IdleState()),
                    new Pool<State>(nameof(MoveState), 1, () => new MoveState())
                );

            if (_isAI)
            {
                SetInjector<AITesterInjector>();
            }
            else
            {
                SetInjector<TesterInjector>();
            }
        }

        #endregion
    }
}