using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{

    public class SceneManager : MonoBehaviour
    {
        public static SceneManager instance = null;

        [SerializeField]
        private SceneView baseScene = null;

        [SerializeField]
        private SceneView tempListSceneView = null;

        private void Awake()
        {
            if (instance != null)
                DestroyImmediate(this.gameObject);
            else
                instance = this;

            if(baseScene!= null)
;               LoadAsync(baseScene, LoadSceneMode.Additive);
            if (tempListSceneView != null)
                LoadAsync(tempListSceneView, LoadSceneMode.Additive);
        }

        private void LoadAsync(SceneView scene, LoadSceneMode loadSceneMode)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.name, loadSceneMode);
        }
    }

}
