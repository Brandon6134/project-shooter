using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Melee melee;
    private bool isMeleeing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(melee!=null && isMeleeing)
        {
            melee.Attack();
        }
        
    }

    void OnMelee()
    {
        isMeleeing = true;
    }

    void OnMeleeRelease()
    {
        isMeleeing = false;
    }
}
