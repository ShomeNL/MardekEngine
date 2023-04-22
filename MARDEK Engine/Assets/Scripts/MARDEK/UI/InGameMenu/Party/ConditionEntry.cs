using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class ConditionEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Image portraitImage;
        [SerializeField] Image elementImage;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] TextMeshProUGUI classText;

        [SerializeField] ConditionBar hpBar;
        [SerializeField] ConditionBar mpBar;
        [SerializeField] Image xpProgressBar;
        [SerializeField] TextMeshProUGUI xpText;

        [SerializeField] IntegerStat levelStat;
        [SerializeField] IntegerStat currentHpStat;
        [SerializeField] IntegerStat maxHpStat;
        [SerializeField] IntegerStat currentMpStat;
        [SerializeField] IntegerStat maxMpStat;

        public void SetCharacter(Character character)
        {
            if (character == null || character.Profile == null)
                return;
            // TODO Portrait
            // TODO Element
            nameText.text = character.Profile.displayName;
            levelText.text = "Lv " + character.GetStat(levelStat);
            // TODO Class
            hpBar.SetValues(character.GetStat(currentHpStat), character.GetStat(maxHpStat));
            mpBar.SetValues(character.GetStat(currentMpStat), character.GetStat(maxMpStat));
            // TODO Update XP bar
        }
    }
}
