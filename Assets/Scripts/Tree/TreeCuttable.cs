using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.9f;
    [SerializeField] int hitCount = 0;

    public override void Hit()
    {
        Debug.Log("🌳 Hit chamado na árvore");

        if (pickUpDrop == null)
        {
            Debug.LogError("❌ pickUpDrop está NULL!");
            return;
        }

        FindObjectOfType<SoundManager>().Play("Cut");
        hitCount++;

        if (hitCount >= 3)
        {
            Debug.Log("🌳 Árvore destruída, dropando logs");

            MoneyController.money += 30;

            while (dropCount > 0)
            {
                dropCount -= 1;

                Vector3 position = transform.position;
                position.x -= spread * UnityEngine.Random.value - spread / 2;
                position.y -= spread * UnityEngine.Random.value - spread / 2;
                Debug.Log("🔨 Tentando instanciar " + pickUpDrop.name);

                Debug.Log("📦 Instanciando  Log na posição " + position);

                GameObject log = Instantiate(pickUpDrop);
                log.transform.position = position;
            }

            Destroy(gameObject);
        }
    }

}
