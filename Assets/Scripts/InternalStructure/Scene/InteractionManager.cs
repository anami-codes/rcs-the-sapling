using System.Collections;
using System.Collections.Generic;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class InteractionManager
    {
        public InteractionManager(Page page)
        {
            this.page = page;
            m_interactables = page.manager.GetComponentsInChildren<InteractableObject>();
            ActivateAll(false);
        }

        public void GameUpdate(float delta)
        {
            foreach (InteractableObject obj in m_interactables)
            {
                obj.interactable?.GameUpdate(delta);
            }
        }

        public InteractableObject GetInteractable(string objName)
        {
            for (int i = 0; i < m_interactables.Length; i++)
            {
                if (m_interactables[i].gameObject.name == objName)
                    return m_interactables[i];
            }
            return null;
        }

        public void ActivateAll(bool activate)
        {
            foreach (InteractableObject obj in m_interactables)
            {
                if (obj.interactable == null)
                    obj.Initialize();
                obj.Activate(activate);
            }
        }

        public void ActivateHint(string code, bool isActive)
        {
            HintControl hint = GetInteractable(code).hint;
            if (hint)
            {
                if (isActive)
                    hint.StartHint();
                else
                    hint.StopHint();
            }
        }

        public void Clear()
        {
            foreach (InteractableObject obj in m_interactables)
            {
                obj.interactable.InterruptInteraction();
            }
        }

        private Page page;
        private InteractableObject[] m_interactables;

    }
}