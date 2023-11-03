using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance;
    public Image skillImage1; 
    public Image skillImage2; 
    public Image skillImage3;

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
        // Đăng ký lắng nghe sự kiện thay đổi trong processedItems
        Skills.Instance.OnProcessedItemsChanged += UpdateSkillImages;
        UpdateSkillImages();
    }

    public void UpdateSkillImages()
    {
        if (Skills.Instance != null)
        {
            // Kiểm tra danh sách các mục mặc định (defaultItems)
            if (Skills.Instance.defaultItems.Count > 0)
            {
                // Kiểm tra skillImage1 không rỗng và có ít nhất một mục trong danh sách
                if (skillImage1 != null && Skills.Instance.defaultItems.Count >= 1)
                {
                    // Lấy ảnh của mục đầu tiên trong danh sách defaultItems và gán nó cho skillImage1
                    skillImage1.sprite = Skills.Instance.defaultItems[0].itemImage;
                }
            }

            // Kiểm tra danh sách các mục vũ khí (weaponItems)
            if (Skills.Instance.weaponItems.Count > 0)
            {
                // Kiểm tra skillImage2 không rỗng và có ít nhất một mục trong danh sách
                if (skillImage2 != null && Skills.Instance.weaponItems.Count >= 1)
                {
                    // Lấy ảnh của mục đầu tiên trong danh sách weaponItems và gán nó cho skillImage2
                    skillImage2.sprite = Skills.Instance.weaponItems[0].itemImage;
                }
            }

            // Kiểm tra danh sách các mục kỹ năng (skillItems)
            if (Skills.Instance.skillItems.Count > 0)
            {
                // Kiểm tra skillImage3 không rỗng và có ít nhất một mục trong danh sách
                if (skillImage3 != null && Skills.Instance.skillItems.Count >= 1)
                {
                    // Lấy ảnh của mục đầu tiên trong danh sách skillItems và gán nó cho skillImage3
                    skillImage3.sprite = Skills.Instance.skillItems[0].itemImage;

                    // Kiểm tra xem có cần hiển thị cooldownImage không
                    if (PlayerHealth.Instance.cooldownTimer > 0 && !PlayerHealth.Instance.isInvulnerable)
                    {
                        cooldownImage.gameObject.SetActive(true);

                        // Tính fillAmount cho cooldownImage
                        float fillAmount = PlayerHealth.Instance.cooldownTimer / PlayerHealth.Instance.cooldownDuration;
                        cooldownImage.fillAmount = fillAmount;
                    }
                    else if (Berserker.Instance.berserkerCooldownTimer > 0 && !Berserker.Instance.isBerserkerActive)
                    {
                        cooldownImage.gameObject.SetActive(true);

                        // Tính fillAmount cho cooldownImage
                        float fillAmount = Berserker.Instance.berserkerCooldownTimer / Berserker.Instance.berserkerCooldown;
                        cooldownImage.fillAmount = fillAmount;
                    }
                    else if (Priests.Instance.cooldownTimer > 0)
                    {
                        cooldownImage.gameObject.SetActive(true);

                        // Tính fillAmount cho cooldownImage
                        float fillAmount = Priests.Instance.cooldownTimer / Priests.Instance.cooldownDuration;
                        cooldownImage.fillAmount = fillAmount;
                    }
                    else
                    {
                        // Nếu không cần hiển thị cooldownImage, ẩn nó đi
                        cooldownImage.gameObject.SetActive(false);
                    }

                    //
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
                        // Nếu không cần hiển thị cooldownImage, ẩn nó đi
                        usingImage.gameObject.SetActive(false);
                    }
                }

            }

        }
    }
}
