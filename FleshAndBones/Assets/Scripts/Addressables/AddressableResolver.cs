using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace App.Addressable
{
    public class AddressableResolver : MonoBehaviour
    {

        [SerializeField]
        private AssetReference christmasTreeAssetReference;

        private GameObject christmasTree;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("DLCEnabled", 0) == 1)
            {
                Addressables.InitializeAsync().Completed += (handle) =>
                {
                    christmasTreeAssetReference.InstantiateAsync().Completed += (go) =>
                    {
                        christmasTree = go.Result;
                        christmasTree.transform.position = new Vector3(0, 25, 0);
                    };

                };
            }

        }
        private void OnDestroy()
        {
            if (christmasTree != null)
            {
                Addressables.ReleaseInstance(christmasTree);
            }

        }
    }

}
