using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement {

    [CreateAssetMenu(fileName = "SceneView", menuName = "Scenes/SceneView", order = 1)]
    public class SceneView : ScriptableObject
#if UNITY_EDITOR
		, ISerializationCallbackReceiver
#endif
	{
#if UNITY_EDITOR
		public UnityEditor.SceneAsset scene;
#endif
        new public string name;

#if UNITY_EDITOR
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			//ignore if no scene defined
			if (scene != null)
			{
				this.name = this.scene.name;
			}
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize() { }
#endif
	}

}
