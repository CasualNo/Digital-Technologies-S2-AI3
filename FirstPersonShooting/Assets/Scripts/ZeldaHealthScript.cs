using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
To all in this group, a few things about health.
Each heart = 1 health. In typical Zelda style, feel free to use increments of .25 when changing health. You can use other increments, but it would be weird.

In any script:
    To heal, use the command ZeldaHealthScript.instance.AddHearts(value);
    To do damage, use the command ZeldaHealthScript.instance.RemoveHearts(value);
    To increase max hearts, use the command ZeldaHealthScript.instance.AddContainer(); (For extra, just do the command multiple times)
    To set the health to a specific number, use the command ZeldaHealthScript.instance.SetCurrentHearts (value); (Try not to go over the max, I don't know what would happen)  
    To change the hearts Zelda starts with, check the public int in this script called public int MaxHealth
 */



public class ZeldaHealthScript : MonoBehaviour
{
    public static ZeldaHealthScript instance; //Creates an instance of this script

    [SerializeField] GameObject heartContainerPrefab; //Gets a prefab of our heart container

    [SerializeField] List<GameObject> heartContainers; //Gets a list of our heart containers

    int totalHearts; //Guess
    float currentHearts; //You can figure it out, I believe in you

    public int MaxHealth;

    HeartContainer currentContainer; //A reference to the current container we are working with

    private void Start()
    {
        instance = this; //Makes the instance this script?
        heartContainers = new List<GameObject>(); //Makes a list of game objects
        SetupHearts(MaxHealth);
    }

    public void SetupHearts(int heartsIn) //Setting up hearts, believe it or not
    {
        //Destroys each object that is a child of heartContainers (All the heart containers)
        heartContainers.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        totalHearts = heartsIn; // sets the total hearts to the value given to the void
        currentHearts = (float)totalHearts; //Sets health to full, casting total hearts as a float

        for (int i = 0; i < totalHearts; i++) //Creates all the hearts
        {
            GameObject newHeart = Instantiate(heartContainerPrefab, transform); //Makes a new heart
            heartContainers.Add(newHeart); //Adds heart to list

            //Making our single link list (I have no idea what this does, I'm just going with it)
            if (currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartContainer>();
            }
            currentContainer = newHeart.GetComponent<HeartContainer>();
        }
        currentContainer = heartContainers[0].GetComponent<HeartContainer>(); //Makes the last heart the first one in our list
    }

    public void SetCurrentHealth(float health) //This is for setting the player health to a specific value.
    {
        currentHearts = health;
        currentContainer.SetHeart(currentHearts);
    }
    public void AddContainer() //Used for adding new heart containers / increasing max health
    {
        //Same as in setup, makes a new heart
        GameObject newHeart = Instantiate(heartContainerPrefab, transform);
        currentContainer = heartContainers[heartContainers.Count - 1].GetComponent<HeartContainer>();
        heartContainers.Add(newHeart);

        //I still don't understand this bit
        if (currentContainer != null)
        {
            currentContainer.next = newHeart.GetComponent<HeartContainer>();
        }

        currentContainer = heartContainers[0].GetComponent<HeartContainer>(); //Sets the new container to current container

        totalHearts++; //Increases total health to match
        currentHearts = totalHearts; //Sets health to full
        SetCurrentHealth(currentHearts);
    }
}
