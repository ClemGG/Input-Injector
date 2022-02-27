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

        [HideInInspector] public Transform T;
        [ReadOnly] public bool IsAI = false;


        #endregion

        public override void SetupContextData(Controller controller)
        {
            TesterController Controller = controller as TesterController;
            T = Controller.transform;
            IsAI = Controller._isAI;
        }

    }
}