using Project.Player.Input;
using UnityEngine;

namespace Project.Player.Tester
{
    [System.Serializable]
    public class TesterContextData : InputContextData
    {

        #region Public Fields

        public float MoveSpeed = 2f;
        public float GrowSpeed = 2f;

        [HideInInspector] public TesterController Controller;
        [HideInInspector] public TesterController OtherController;
        [HideInInspector] public Transform T;
        [HideInInspector] public Transform PlayerIndicatorUI;    //The UI set above the Controller currently managed by the Player.
        [ReadOnly] public bool IsAI = false;


        #endregion

        public override void SetupContextData(Controller controller)
        {
            Controller = controller as TesterController;
            OtherController = Controller._otherController;
            T = Controller.transform;
            IsAI = Controller._isAI;
            PlayerIndicatorUI = Controller._playerIndicatorUI;
        }

    }
}