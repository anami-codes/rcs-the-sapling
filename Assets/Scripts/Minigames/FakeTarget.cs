using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames
{
    public class FakeTarget : MinigameTarget
    {
        public PlantingCollider[] otherColliders;
        public PlantingGoal goal;
        public Collider2D plant;

        public override void Initialize(Minigame minigame)
        {
            base.Initialize(minigame);
            interactable = new TapZone(gameObject, null, "");
        }
        
        void Update()
        {
            if(!goalIsEnabled && (otherColliders[0].isReady) && (otherColliders[1].isReady))
            {
                goal.GetComponent<Collider2D>().enabled = true;
                goal.hint.StartHint();
                goalIsEnabled = true;
            }

            if(!plantIsEnabled && (goalIsEnabled && goal.CanPlant()))
            {
                plant.enabled = true;
                plantIsEnabled = true;
            }

            if(plantIsEnabled && waitToLift > 0.0f)
            {
                waitToLift -= Time.deltaTime;
                if (waitToLift <= 0.0f && liftTimes <= 0) SetAsReady();
            }
        }

        public void Lift()
        {
            if (liftTimes > 0 && waitToLift <= 0.0f)
            {
                GetComponent<Animator>().SetTrigger("Lift");
                liftTimes -= 1;
                waitToLift = 1.5f;
            }
        }

        private bool goalIsEnabled = false;
        private bool plantIsEnabled = false;
        private int liftTimes = 2;
        private float waitToLift = 0.0f;
    }
}