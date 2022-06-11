using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BrakeMod
{
    class AssetManager
    {
        private GameObject _menuGameObject;
        public GameObject instantiatedMenu;
        public MenuBuilder menuBuilder;

        public void Init()
        {
            AssetBundle modDataBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetFullPath("."), "UserData/brakemod.bundle"));
            if (modDataBundle)
            {
                //MelonLogger.Msg("Asset bundle loaded.");
                var menuAsset = modDataBundle.LoadAsset("BrakeMod_Canvas");
                _menuGameObject = menuAsset.Cast<GameObject>();
                ModLogger.Log("MenuObject loaded.");
            }
        }

        public void CreateMenu()
        {
            instantiatedMenu = GameObject.Instantiate(_menuGameObject);
            menuBuilder = instantiatedMenu.AddComponent<MenuBuilder>();
            UnityEngine.Object.DontDestroyOnLoad(instantiatedMenu);
            instantiatedMenu.SetActive(false);
        }
    }
}
