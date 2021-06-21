using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public int skinNb;
    public RuntimeAnimatorController defaultAnimator;
    public AnimatorOverrideController blueAnimator;
    public AnimatorOverrideController greenAnimator;
    public AnimatorOverrideController pinkAnimator;
    public AnimatorOverrideController redAnimator;
    public Animator playerAnimator;

    void Start()
    {
        skinNb = PlayerPrefs.GetInt("Skin");
    }
    void Update()
    {
        if (skinNb == 0)
            playerAnimator.runtimeAnimatorController = defaultAnimator as RuntimeAnimatorController;
        if (skinNb == 1)
            playerAnimator.runtimeAnimatorController = blueAnimator as RuntimeAnimatorController;
        if (skinNb == 2)
            playerAnimator.runtimeAnimatorController = greenAnimator as RuntimeAnimatorController;
        if (skinNb == 3)
            playerAnimator.runtimeAnimatorController = pinkAnimator as RuntimeAnimatorController;
        if (skinNb == 4)
            playerAnimator.runtimeAnimatorController = redAnimator as RuntimeAnimatorController;
        // if (PlayerPrefs.GetInt("Skin") != skinNb)
        // {
        //     PlayerPrefs.SetInt("Skin", skinNb);
        // }
    }
    public void SetSkinNb(int nb)
    {
        skinNb = nb;
        PlayerPrefs.SetInt("Skin", skinNb);
    }
}
