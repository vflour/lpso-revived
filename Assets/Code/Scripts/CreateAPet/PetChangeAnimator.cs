using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetChangeAnimator : MonoBehaviour
{
    public string changePetAnimation;
    public CreateAPetStands stands;
    public Animator standsAnimator;

    public void PlayChangePetAnimation()
    {
        stands.PetSprite.GetComponent<Animator>().Play(changePetAnimation, 0, 0);
        standsAnimator.SetTrigger("ShowParticles");
    }

    public void PlayStillAnimation()
    {
        stands.PetSprite.GetComponent<Animator>().Play("Still", 0, 0);
    }

}
