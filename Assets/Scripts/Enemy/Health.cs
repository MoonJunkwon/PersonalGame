using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFX;


    public Slider healthSlider;

    private int currentHealth;
    Knockback knockback;
    private Flash flash;

    private float currentSliderValue; // 현재 슬라이더 값
    private float targetSliderValue; // 목표 슬라이더 값
    private float lerpSpeed = 2f; // Lerp 속도 조절


    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

  

    private void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = startingHealth;
        currentSliderValue = startingHealth; // 시작 시 슬라이더 값 초기화
        targetSliderValue = startingHealth;  // 목표 슬라이더 값 초기화
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);
        knockback.GetKnockBack(PlayerControll.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());

        targetSliderValue = currentHealth;

        if (currentHealth <= 0)
        {
            Death();

        }
    }
    private void Update()
    {
        // Mathf.Lerp를 사용하여 슬라이더 값을 부드럽게 업데이트
        currentSliderValue = Mathf.Lerp(currentSliderValue, targetSliderValue, Time.deltaTime * lerpSpeed);
        healthSlider.value = currentSliderValue;
    }

    private void Death()
    {
        if(currentHealth <= 0)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
