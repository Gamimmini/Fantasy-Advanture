using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance;

    [Header("Skill Image")]
    public Image skillImage1;
    public Image skillImage2;
    public Image skillImage3;

    [Header("Skill Cooldown")]
    public Image cooldownImage;
    public Image usingImage;

    private void Awake()
    {
        Instance = this;
        cooldownImage.gameObject.SetActive(false);
        usingImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        Skills.Instance.OnProcessedItemsChanged += UpdateSkillImages;
        UpdateSkillImages();
    }

    public void UpdateSkillImages()
    {
        if (Skills.Instance != null)
        {
            if (Skills.Instance.defaultItems.Count > 0)
            {
                if (skillImage1 != null && Skills.Instance.defaultItems.Count >= 1)
                {
                    skillImage1.sprite = Skills.Instance.defaultItems[0].itemImage;
                    Color imageColor = skillImage1.color;
                    imageColor.a = 1f;
                    skillImage1.color = imageColor;
                }
            }

            if (Skills.Instance.weaponItems.Count > 0)
            {
                if (skillImage2 != null && Skills.Instance.weaponItems.Count >= 1)
                {
                    skillImage2.sprite = Skills.Instance.weaponItems[0].itemImage;
                    Color imageColor = skillImage2.color;
                    imageColor.a = 1f;
                    skillImage2.color = imageColor;
                }
            }

            if (Skills.Instance.skillItems.Count > 0)
            {
                if (skillImage3 != null && Skills.Instance.skillItems.Count >= 1)
                {
                    skillImage3.sprite = Skills.Instance.skillItems[0].itemImage;
                    Color imageColor = skillImage3.color;
                    imageColor.a = 1f;
                    skillImage3.color = imageColor;

                    if (PlayerHealth.Instance.cooldownTimer > 0 && !PlayerHealth.Instance.isInvulnerable)
                    {
                        cooldownImage.gameObject.SetActive(true);
                        float fillAmount = PlayerHealth.Instance.cooldownTimer / PlayerHealth.Instance.cooldownDuration;
                        cooldownImage.fillAmount = fillAmount;
                    }
                    else if (Berserker.Instance.berserkerCooldownTimer > 0 && !Berserker.Instance.isBerserkerActive)
                    {
                        cooldownImage.gameObject.SetActive(true);
                        float fillAmount = Berserker.Instance.berserkerCooldownTimer / Berserker.Instance.berserkerCooldown;
                        cooldownImage.fillAmount = fillAmount;
                    }
                    else if (Priests.Instance.cooldownTimer > 0)
                    {
                        cooldownImage.gameObject.SetActive(true);
                        float fillAmount = Priests.Instance.cooldownTimer / Priests.Instance.cooldownDuration;
                        cooldownImage.fillAmount = fillAmount;
                    }
                    else
                    {
                        cooldownImage.gameObject.SetActive(false);
                    }

                    if (PlayerHealth.Instance.isInvulnerable)
                    {
                        usingImage.gameObject.SetActive(true);
                    }
                    else if (Berserker.Instance.isBerserkerActive)
                    {
                        usingImage.gameObject.SetActive(true);
                    }
                    else if (Priests.Instance.priestsActive && Priests.Instance.cooldownTimer <= 0)
                    {
                        usingImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        usingImage.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
