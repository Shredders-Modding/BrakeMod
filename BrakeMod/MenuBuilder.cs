using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BrakeMod
{
    class MenuBuilder : MonoBehaviour
    {
        public MenuBuilder(IntPtr ptr) : base(ptr) { }

        private GameObject parent;
        private Slider slider;
        private TMP_InputField inputFieldTMPro;
        private Button resetButton;

        private void Start()
        {
            parent = gameObject.transform.Find("Brake_Parent").gameObject;
            slider = parent.transform.Find("Brake_Slider").GetComponent<Slider>();
            inputFieldTMPro = parent.transform.Find("Brake_InputField").GetComponent<TMP_InputField>();
            resetButton = parent.transform.Find("Brake_ResetButton").gameObject.GetComponent<Button>();

            slider.value = ModManager.brakeValue;
            inputFieldTMPro.text = ModManager.brakeValue.ToString("F2");

            slider.onValueChanged.AddListener(new Action<float>(OnBrakeChange));
            inputFieldTMPro.onSubmit.AddListener(new Action<string>(OnBrakeSubmit));
            resetButton.onClick.AddListener(new Action(OnBrakeReset));

            ModLogger.Log("Menu builder initalized");
        }

        private void OnBrakeReset()
        {
            slider.value = 0.15f;
            inputFieldTMPro.text = "0.15";
            //Change board size here
            ModManager.SetBrakeValue(0.15f);
        }

        private void OnBrakeSubmit(string text)
        {
            slider.value = float.Parse(text);
            ModManager.SetBrakeValue(slider.value);
        }

        private void OnBrakeChange(float value)
        {
            inputFieldTMPro.text = value.ToString("F2");
            ModManager.SetBrakeValue(value);
        }
    }
}
