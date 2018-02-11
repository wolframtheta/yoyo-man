using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardGenerator : MonoBehaviour {

    public GameObject papa;
    public GameObject player;
    public GameObject[] bigRooms;
    public GameObject[] smallRooms;
    private bool start = true;
    public int maxBigRooms = 2;
    private int qttyBigRooms;
    

    private void doChildren(int i, GameObject doors)
    {
        for (; i < doors.transform.childCount; ++i)
        {
            if (qttyBigRooms <= 1)
            {
                Transform childPos = doors.transform.GetChild(i);
                int element = Random.Range(0, smallRooms.Length);
                GameObject child = smallRooms[element];
                addRoom(child, childPos);
            }
            else
            {
                --qttyBigRooms;
                Transform childPos = doors.transform.GetChild(i);
                int element = Random.Range(0, bigRooms.Length);
                GameObject child = bigRooms[element];
                addRoom(child, childPos);
            }
        }
    }

    private void addRoom(GameObject father, Transform posLastDoor){
        GameObject doors = father.transform.Find("Doors").gameObject;
        Transform childPos = doors.transform.GetChild(0);
        Quaternion aux = Quaternion.Euler(posLastDoor.rotation.eulerAngles - childPos.rotation.eulerAngles + new Vector3(0,0,180));
        GameObject instance = Instantiate(father, new Vector3(0,0,0), aux);
        doors = instance.transform.Find("Doors").gameObject;
        childPos = doors.transform.GetChild(0);
        instance.transform.position = posLastDoor.position - childPos.position;
        instance.transform.SetParent(papa.transform);

        doChildren(1,doors);
    }

    void Start () {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || start)
        {
            start = false;
            qttyBigRooms = maxBigRooms;
            for (int i = 0; i <papa.transform.childCount; ++i)
            {
                Destroy(papa.transform.GetChild(i).gameObject);
            }

            int element = Random.Range(0, bigRooms.Length);
            GameObject father = bigRooms[element];
            GameObject instance = Instantiate(father, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(papa.transform);
            GameObject doors = father.transform.Find("Doors").gameObject;

            doChildren(0,doors);
            GameObject p1 = Instantiate(player, doors.transform.GetChild(0).transform.position, Quaternion.identity);
            p1.transform.SetParent(papa.transform);
        }
    }
}