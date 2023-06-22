using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Menu
{
    [Serializable]
    public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int id;
        public bool unlocked;
        public bool purchased;

        [SerializeField] int unlockCost;

        [SerializeField] List<Skill> requiredSkills;
        List<Skill> requiredFor;

        [SerializeField] Color lockedColour;
        [SerializeField] Color unlockedColour;
        [SerializeField] Color purchasedColour;
        [SerializeField] Button button;

        [SerializeField] Image tile;
        [SerializeField] Sprite image;

        [SerializeField] SkillTree skillTree;

        public Item itemToGivePlayer;

        [SerializeField] Image icon;
        Nexbit nexbitManager;
        Tooltip tooltip;

        void OnEnable()
        {
            tile = GetComponent<Image>();
            CheckIfUnlockable();
            icon.sprite = itemToGivePlayer.IconSprite;
            nexbitManager = FindObjectOfType<Nexbit>();
            tooltip = FindObjectOfType<Tooltip>();
        }

        private void Update()
        {

        }

        public void CheckIfUnlockable()
        {
            unlocked = true;
            if (!purchased)
            {
                if (requiredSkills != null)
                {
                    foreach (Skill s in requiredSkills)
                    {
                        if (s.purchased == false)
                        {
                            unlocked = false;
                        }
                    }
                }
                if(tile)
                {
                    tile.color = unlocked ? unlockedColour : lockedColour;
                    tile.color = purchased ? purchasedColour : tile.color;
                }
            }
            button.enabled = unlocked;
        }

        public void Purchase()
        {
            if(!purchased && (nexbitManager.nexbits - unlockCost) > 0)
            {
                purchased = true;
                nexbitManager.RemoveNexbits(unlockCost);
                skillTree.RefreshSkills();
                tile.color = purchasedColour;
                skillTree.AddItem(itemToGivePlayer, this);
            }
        }

        public void FreePurchase()
        {
            unlocked = true;
            purchased = true;
            if (tile)
            {
                tile.color = purchasedColour;
            }
            button.enabled = false;
            skillTree.RefreshSkills();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.ShowTooltip(itemToGivePlayer.name + " : " + "<font=\"StandardGalacticAlphabet SDF\">q</font>" + unlockCost);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.HideTooltip();
        }
    }
}

