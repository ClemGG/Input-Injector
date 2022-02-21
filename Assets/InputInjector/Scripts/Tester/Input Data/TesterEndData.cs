using Project.Player.Input;

namespace Project.Player.Tester
{
    [System.Serializable]
    public class TesterEndData : InputEndData
    {

        #region Public Fields

        [ReadOnly] public bool IsColliding;
        [ReadOnly] public bool HasPressedSwap;
        [ReadOnly] public bool ShouldGrow;
        [ReadOnly] public float Horizontal;

        #endregion

    }
}