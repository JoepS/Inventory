using Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace ViewTraversal.Popup
{

    public class Popup : MonoBehaviour
    {

        [SerializeField]
        private CanvasGroup canvasGroup = null;

        [SerializeField]
        private Identifier identifier = null;

        [SerializeField]
        private Button backButton = null;

        public Identifier Identifier
        {
            get
            {
                return identifier;
            }
        }

		private void Awake()
		{
            backButton.onClick.AddListener(delegate { this.Close(); });
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
