using Scriptables;
using UnityEngine;

namespace ViewTraversal.Popup
{

    public class Popup : MonoBehaviour
    {

        [SerializeField]
        private CanvasGroup canvasGroup = null;

        [SerializeField]
        private Identifier identifier = null;

        public Identifier Identifier
        {
            get
            {
                return identifier;
            }
        }

        public virtual void Open(params object[] parameters)
        {
            this.canvasGroup.alpha = 1;
            this.canvasGroup.interactable = true;
            this.canvasGroup.blocksRaycasts = true;
        }

        public virtual void Close()
        {
            this.canvasGroup.alpha = 0;
            this.canvasGroup.interactable = false;
            this.canvasGroup.blocksRaycasts = false;
        }

    }

}
