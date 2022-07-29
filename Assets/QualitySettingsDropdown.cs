using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
public class QualitySettingsDropdown : MonoBehaviour
{
    [SerializeField] RenderPipelineAsset[] renderPiplines;
    [SerializeField] TMP_Dropdown UIdropDown;
    private void Start()
    {
        UIdropDown.value = QualitySettings.GetQualityLevel();
        UIdropDown = GetComponent<TMP_Dropdown>();
    }
    public void ChangeLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        QualitySettings.renderPipeline = renderPiplines[qualityIndex];
    }
}
