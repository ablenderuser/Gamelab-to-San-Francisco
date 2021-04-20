using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealthSystem : MonoBehaviour
{
    public const int MAX_FRAGMENT_AMOUNT = 4;
    public List<Heart> heartList;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    public HeartHealthSystem(int heartAmount)
    {
        heartList = new List<Heart>();
        for(int i = 0; i < heartAmount / MAX_FRAGMENT_AMOUNT; i++)
        {
            Heart heart = new Heart(MAX_FRAGMENT_AMOUNT);
            heartList.Add(heart);
        }
    }

    public List<Heart> GetHeartList()
    {
        return heartList;
    }
    
    public int GetNumberOfFragments(){
        int c = 0;

        for(int i = heartList.Count - 1; i >= 0 ; i--)
        {
            c += heartList[i].GetFragmentAmount();
        }

        return c;
    }

    public void Damage(int damageAmount)
    {
        for(int i = heartList.Count - 1; i >= 0 ; i--)
        {
            Heart heart = heartList[i];
            if (damageAmount > heart.GetFragmentAmount())
            {
                damageAmount -= heart.GetFragmentAmount();
                heart.Damage(heart.GetFragmentAmount());
            }
            else
            {
                heart.Damage(damageAmount);
                break;
            }
        }

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);

        if (IsDead())
        {
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }
    }

    public int Heal(int healAmount)
    {
        int newHearts = 0; 
        for (int i =  0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            int missingFragments = MAX_FRAGMENT_AMOUNT - heart.GetFragmentAmount();
            if (healAmount > missingFragments)
            {
                healAmount -= missingFragments;
                heart.Heal(missingFragments);
            }
            else
            {
                heart.Heal(healAmount);
                break;
            }
        }
        
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
        return newHearts;
    }

    public bool IsDead()
    {
        return heartList[0].GetFragmentAmount() == 0;
    }
    

    public class Heart {
        private int fragments;

        public Heart(int fragments)
        {
            this.fragments = fragments;
        }

        public void SetFragments(int fragments)
        {
            this.fragments = fragments;
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void Damage(int damageAmount)
        {
            if (damageAmount >= fragments)
            {
                fragments = 0;
            }
            else
            {
                fragments -= damageAmount;
            }
        }

        public void Heal(int healAmount)
        {
            if (fragments + healAmount > MAX_FRAGMENT_AMOUNT)
            {
                fragments = MAX_FRAGMENT_AMOUNT;
                
            }
            else
            {
                fragments += healAmount;
            }
        }
    }
}
