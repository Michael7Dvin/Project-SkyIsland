﻿using UI.Elements.Buttons.Close;
using UI.Elements.Buttons.Selectable;
using UnityEngine;

namespace UI.Windows.Implementations.PauseWindow
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/Pause", fileName = "Pause")]
    public class PauseWindowConfig : ScriptableObject
    {
        [field: SerializeField] public CloseButtonConfig CloseButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig OptionsButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig SaveButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig MainMenuButtonConfig { get; private set; }
    }
}