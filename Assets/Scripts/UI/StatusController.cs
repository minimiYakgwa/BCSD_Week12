using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int currentHp;

    [SerializeField]
    private int sp;
    private int currentSp;

    [SerializeField]
    private int spIncreaseSpeed;
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    private bool spUsed;

    [SerializeField]
    private int dp;
    private int currentDp;

    [SerializeField]
    private int hungry;
    private int currentHungry;

    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    [SerializeField]
    private int thirsty;
    private int currentThirsty;

    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    [SerializeField]
    private int satisfy;
    private int currentSatisfy;

    [SerializeField]
    private Image[] images_Gauge;

    private const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;

    private void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentSatisfy = satisfy;
    }

    private void Update()
    {
        Hungry();
        Thirsty();
        GaugeUpdate();
        SPRechargeTime();
        SPRecover();
    }

    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        /*else
            Debug.Log("배고픔 수치 0으로 되었음.");*/
    }

    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        /*else
            Debug.Log("목마름 수치 0으로 되었음.");*/
    }

    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[DP].fillAmount = (float)currentDp / dp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
        images_Gauge[SATISFY].fillAmount = (float)currentSatisfy / satisfy;
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count >= 0)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    public void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
            {
                currentSpRechargeTime++;
            }
            else
                spUsed = false;
        }
    }

    public void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    public int GetCurrentSP()
    {
        return currentSp;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }

    public void DecreaseHp(int _count)
    {
        if (currentDp > 0)
        {
            DecreaseDp(_count);
            return;
        }
        currentHp -= _count;

        if (currentHp < 0)
        {
            Debug.Log("체력이 0이 되었습니다.");
        }
    }

    public void IncreaseDP(int _count)
    {
        if (currentDp + _count < dp)
            currentDp += _count;
        else
            currentDp = dp;
    }

    public void DecreaseDp(int _count)
    {
        currentDp -= _count;

        if (currentDp < 0)
        {
            Debug.Log("체력이 0이 되었습니다.");
        }
    }

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
            currentHungry = 0;
        else
            currentHungry -= _count;
    }

    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }

    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
            currentThirsty = 0;
        else
            currentThirsty -= _count;
    }


}
