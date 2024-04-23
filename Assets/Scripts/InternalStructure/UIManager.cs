using UnityEngine;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class UIManager : MonoBehaviour
    {
        public GameObject[] UIObjs;

        public void ChangeUI(string uiName)
        {
            foreach (GameObject ui in UIObjs)
            {
                ui.SetActive(ui.name == uiName);
            }
        }
    }
}