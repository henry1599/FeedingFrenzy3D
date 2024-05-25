using System.Collections;
using HenryDev.Events;
using UnityEngine;

namespace HenryDev.Controllers
{
    public class MainSceneController : MonoBehaviour
    {
        [SerializeField] GameObject[] hideables;
        void Start()
        {
            LoadMainMenu();
        }
        public void LoadMainMenu()
        {
            StartCoroutine(Cor_LoadMainScene());
        }
        IEnumerator Cor_LoadMainScene()
        {
            UIEvents.TRANSITION_IN?.Invoke();
            yield return new WaitForSeconds(UIConstants.TRANSITION_DURATION);
            LoadSceneEvents.LOAD_SCENE?.Invoke(
                eSceneType.MainMenu,
                () => 
                {
                    HideAllHideables();
                    LoadSceneEvents.SET_ACTIVE_SCENE?.Invoke(eSceneType.MainMenu);
                    UIEvents.TRANSITION_OUT?.Invoke();
                }
            );
        }
        void HideAllHideables()
        {
            if (this.hideables == null)
                return;
            foreach (var hideable in this.hideables)
                hideable.SetActive(false);
        }
    }
}
