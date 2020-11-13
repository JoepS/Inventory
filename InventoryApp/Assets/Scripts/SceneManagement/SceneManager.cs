using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace SceneManagement
{

    public class SceneManager : MonoBehaviour
    {
        public static SceneManager instance = null;

        [SerializeField]
        private SceneView baseScene = null;

        [SerializeField]
        private SceneView loadingScene = null;

        [SerializeField]
        private List<SceneView> scenes = null;

        private void Awake()
        {
            if (instance != null)
                DestroyImmediate(this.gameObject);
            else
                instance = this;

            if(baseScene!= null)
               LoadAsync(baseScene, UnitySceneManager.GetActiveScene().name != loadingScene.name ? LoadSceneMode.Additive : LoadSceneMode.Single);

            if (scenes.Count > 0)
                LoadAsync(scenes[0]);
        }

        private void LoadAsync(SceneView scene, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            if (loadSceneMode == LoadSceneMode.Additive)
            {
                int scenesLoaded = UnitySceneManager.sceneCount;
                for (int i = 0; i < scenesLoaded; i++)
                {
                    if (UnitySceneManager.GetSceneAt(i).name.Equals(scene.name))
                    {
                        return;
                    }
                }
            }
            UnitySceneManager.LoadSceneAsync(scene.name, loadSceneMode);
        }
    }

}
