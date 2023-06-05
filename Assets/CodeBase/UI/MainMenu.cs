using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private IMediator _mediator;
        
        [Inject]
        private void Cosntruct(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public void OnPlayButtonClick()
        {
            _mediator.StartGame();
        }

        public void OnOptionsButtonClick()
        {
            
        }
        
        public void OnQuitButtonClick()
        {
            _mediator.QuitGame();
        }
    }
}