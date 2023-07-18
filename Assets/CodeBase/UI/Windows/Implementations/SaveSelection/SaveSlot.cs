using Infrastructure.Progress;
using Infrastructure.Services.SaveLoadService;
using TMPro;
using UI.Controls.Buttons.Close;
using UI.Controls.Buttons.SaveSlots;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSlot : MonoBehaviour
    {
        private  SaveSlotID _id;

        [SerializeField] private TextMeshProUGUI _centralNameTextField;
        [SerializeField] private TextMeshProUGUI _heroHealthTextField;
        [SerializeField] private TextMeshProUGUI _lastSaveDateTextField;

        [SerializeField] private SaveSlotButton _saveSlotButton;
        [SerializeField] private CloseButton _deleteSaveButton;

        public void Construct(SaveSlotID id, SaveSlotButtonConfig saveSlotButtonConfig, CloseButtonConfig deleteSaveButtonConfig)
        {
            _id = id;
            _saveSlotButton.Construct(saveSlotButtonConfig);
            _deleteSaveButton.Construct(deleteSaveButtonConfig);
        }

        private string SaveSlotName => _id.ToString();
        private string EmptySlotText => "New Game";
        private string LastSaveText => "Last Save:";
        private string HeroHealthText => "Hero Health:";
        
        public SaveSlotButton SaveSlotButton => _saveSlotButton;
        public CloseButton DeleteSaveButton => _deleteSaveButton;
        
        public void DrawWithProgress(AllProgress progress)
        {
            _deleteSaveButton.gameObject.SetActive(true);

            _centralNameTextField.text = SaveSlotName;
            _heroHealthTextField.text = $"{HeroHealthText} {progress.HeroProgress.CurrentHealth}";
            _lastSaveDateTextField.text = $"{LastSaveText} {progress.LastSaveDateTime}";
        }

        public void DrawEmpty()
        {
            _deleteSaveButton.gameObject.SetActive(false);
            
            _centralNameTextField.text = EmptySlotText;
            _heroHealthTextField.text = "";
            _lastSaveDateTextField.text = "";
        }
    }
}