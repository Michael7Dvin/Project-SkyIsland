using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Elements.Buttons.Close
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Close Button", fileName = "Close Button")]
    public class CloseButtonConfig : ScriptableObject
    {
        [field: SerializeField] public OnClickScalerConfig OnClickScalerConfig { get; private set; }
    }
}