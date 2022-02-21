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

        protected override void Update()
        {
            base.Update();
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

        protected override void SetupInjectorAndStatePoolers()
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