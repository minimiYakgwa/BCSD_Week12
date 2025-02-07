using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int count;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private SphereCollider col;

    [SerializeField]
    private GameObject go_rock;
    [SerializeField]
    private GameObject go_debris;
    [SerializeField]
    private GameObject go_effect_prefabs;
    [SerializeField]
    private GameObject go_rock_item_prefabs;

    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;

    

    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);
        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        
        Destroy(clone, 3f);
        hp--;
        if (hp <= 0)
        {
            Destruction();
        }
    }
    
    private void Destruction()
    {
        SoundManager.instance.PlaySE(destroy_Sound);

        col.enabled = false;
        Destroy(go_rock);

        for (int i = 0; i < count; i++)
            Instantiate(go_rock_item_prefabs, go_rock.transform.position, Quaternion.identity);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
