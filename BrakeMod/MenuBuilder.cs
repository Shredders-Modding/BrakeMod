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

        private GameObject courseBrakeParent;
        private Slider courseBrakeSlider;
        private TMP_InputField courseBrakeInputField;
        private Button courseBrakeResetButton;

        private GameObject powderBrakeParent;
        private Slider powderBrakeSlider;
        private TMP_InputField powderBrakeInputField;
        private Button powderBrakeResetButton;

        private void Start()
        {
            //COURSE
            courseBrakeParent = gameObject.transform.Find("CourseBrake_Parent").gameObject;
            courseBrakeSlider = courseBrakeParent.transform.Find("CourseBrake_Slider").GetComponent<Slider>();
            courseBrakeInputField = courseBrakeParent.transform.Find("CourseBrake_InputField").GetComponent<TMP_InputField>();
            courseBrakeResetButton = courseBrakeParent.transform.Find("CourseBrake_ResetButton").gameObject.GetComponent<Button>();

            courseBrakeSlider.value = ModManager.courseBrakeValue;
            courseBrakeInputField.text = ModManager.courseBrakeValue.ToString("F2");

            courseBrakeSlider.onValueChanged.AddListener(new Action<float>(OnCourseBrakeChange));
            courseBrakeInputField.onSubmit.AddListener(new Action<string>(OnCourseBrakeSubmit));
            courseBrakeResetButton.onClick.AddListener(new Action(OnCourseBrakeReset));

            //POWDER
            powderBrakeParent = gameObject.transform.Find("PowderBrake_Parent").gameObject;
            powderBrakeSlider = powderBrakeParent.transform.Find("PowderBrake_Slider").GetComponent<Slider>();
            powderBrakeInputField = powderBrakeParent.transform.Find("PowderBrake_InputField").GetComponent<TMP_InputField>();
            powderBrakeResetButton = powderBrakeParent.transform.Find("PowderBrake_ResetButton").gameObject.GetComponent<Button>();

            powderBrakeSlider.value = ModManager.powderBrakeValue;
            powderBrakeInputField.text = ModManager.powderBrakeValue.ToString("F2");

            powderBrakeSlider.onValueChanged.AddListener(new Action<float>(OnPowderBrakeChange));
            powderBrakeInputField.onSubmit.AddListener(new Action<string>(OnPowderBrakeSubmit));
            powderBrakeResetButton.onClick.AddListener(new Action(OnPowderBrakeReset));

            ModLogger.Log("Menu builder initalized");
        }

        private void OnCourseBrakeReset()
        {
            courseBrakeSlider.value = ModManager.initCourseBrakeValue.y;
            courseBrakeInputField.text = ModManager.initCourseBrakeValue.y.ToString();
            //Change board size here
            ModManager.SetBrakeValue(ModManager.initCourseBrakeValue.y, ModManager.powderBrakeValue);
        }

        private void OnCourseBrakeSubmit(string text)
        {
            courseBrakeSlider.value = float.Parse(text);
            ModManager.SetBrakeValue(courseBrakeSlider.value, ModManager.powderBrakeValue);
        }

        private void OnCourseBrakeChange(float value)
        {
            courseBrakeInputField.text = value.ToString("F2");
            ModManager.SetBrakeValue(value, ModManager.powderBrakeValue);
        }

        private void OnPowderBrakeReset()
        {
            powderBrakeSlider.value = ModManager.initPowderBrakeValue.y;
            powderBrakeInputField.text = ModManager.initPowderBrakeValue.y.ToString();
            //Change board size here
            ModManager.SetBrakeValue(ModManager.courseBrakeValue, ModManager.initPowderBrakeValue.y);
        }

        private void OnPowderBrakeSubmit(string text)
        {
            powderBrakeSlider.value = float.Parse(text);
            ModManager.SetBrakeValue(ModManager.courseBrakeValue, powderBrakeSlider.value);
        }

        private void OnPowderBrakeChange(float value)
        {
            powderBrakeInputField.text = value.ToString("F2");
            ModManager.SetBrakeValue(ModManager.courseBrakeValue, value);
        }
    }
}
