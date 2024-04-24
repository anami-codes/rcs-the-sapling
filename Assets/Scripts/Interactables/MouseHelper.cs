using UnityEngine;
using UnityEngine.UI;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Interactables
{
    public class MouseHelper : MonoBehaviour
    {
        private void Start()
        {
            m_transform = GetComponent<RectTransform>();
            m_image = GetComponentInChildren<Image>();

        }

        public void ChangeVisibility(bool isVisible)
        {
            m_image.enabled = isVisible;
        }

        private void Update()
        {
            Vector3 pos = Vector3.zero;
            pos.x = Input.mousePosition.x;
            pos.y = Input.mousePosition.y;
            m_transform.position = pos;
        }

        private RectTransform m_transform;
        private Image m_image;

        //Disappears when using interactable
        //Disappears during cinematics?
    }
}