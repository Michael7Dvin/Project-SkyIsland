using UI.Animators.OnClickScaler;
using UI.Animators.OnSelectScaler;
using UnityEngine;

namespace UI.Controls.Buttons.Close
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Close Button", fileName = "Close Button")]
    public class CloseButtonConfig : ScriptableObject
    {
        [field: SerializeField] public OnSelectScalerConfig OnSelectScalerConfig { get; private set; }
        [field: SerializeField] public OnClickScalerConfig OnClickScalerConfig { get; private set; }
    }
}