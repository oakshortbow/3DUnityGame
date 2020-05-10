using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DissolveClones : MonoBehaviour
{
    [SerializeField]
    private int resetValue = 3;

    private Renderer[] renders;
    private List<Material> materialList = new List<Material>();
    // Start is called before the first frame update
    private void Start()
    {
        renders = this.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renders) {
            foreach(Material m in r.materials) 
            {
                materialList.Add(m);
            }
          materialList.Add(r.material);
        }

        foreach(Material m in materialList) 
        {
            m.DOFloat(0.0F, "_Fade", 0.5f).OnComplete(()=> Destroy(gameObject));
        }

    }
}
