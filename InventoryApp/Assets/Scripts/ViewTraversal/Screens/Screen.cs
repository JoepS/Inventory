using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViewTraversal.Screens
{

    public class Screen : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup = null;
        [SerializeField]
        private Scriptables.Identifier identifier = null;

        public Scriptables.Identifier Identifier
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
