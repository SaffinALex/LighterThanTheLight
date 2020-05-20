using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

public class RessourcesLoader
{
    private List<GameObject> listeWeapons;

    private List<GameObject> listeUpgrades;

    private List<GameObject> listeShipUpgrades;
    private List<GameObject> listeDashUpgrades;
    private List<GameObject> listeOndeUpgrades;
    private List<GameObject> listeWeaponUpgrades;
    private List<GameObject> listeWeaponRewards;

    public RessourcesLoader(Transform parent, string wepRep, string shipUpRep, string dashUpRep, string ondeUpRep, string wepUpRep, string wepRewardRep)
    {
        listeWeapons = new List<GameObject>();
        listeShipUpgrades = new List<GameObject>();
        listeDashUpgrades = new List<GameObject>();
        listeOndeUpgrades = new List<GameObject>();
        listeWeaponUpgrades = new List<GameObject>();
        listeWeaponRewards = new List<GameObject>();

        List<GameObject> tempListeWeapons = new List<GameObject>(Resources.LoadAll(wepRep, typeof(GameObject)).Cast<GameObject>().ToArray());

        List<GameObject> tempListeShipUpgrades = new List<GameObject>(Resources.LoadAll(shipUpRep,typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeDashUpgrades = new List<GameObject>(Resources.LoadAll(dashUpRep, typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeOndeUpgrades = new List<GameObject>(Resources.LoadAll(ondeUpRep, typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeWeaponUpgrades = new List<GameObject>(Resources.LoadAll(wepUpRep, typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeWeaponRewards = new List<GameObject>(Resources.LoadAll(wepRewardRep, typeof(GameObject)).Cast<GameObject>().ToArray());

        Vector3 pos = new Vector3(10000, 10000, 10000);
        foreach (GameObject go in tempListeWeapons)
            if(!(go.name.Contains("Enemy") || go.name.Contains("enemy")))
            {
                GameObject o = GameObject.Instantiate(go);
                o.transform.SetParent(parent);
                o.transform.position = pos;
                GameObject.DontDestroyOnLoad(o);
                listeWeapons.Add(o);
            }

        foreach (GameObject go in tempListeShipUpgrades)
        {
            GameObject o = GameObject.Instantiate(go);
            o.transform.SetParent(parent);
            o.transform.position = pos;
            GameObject.DontDestroyOnLoad(o);
            listeShipUpgrades.Add(o);
        }

        foreach (GameObject go in tempListeDashUpgrades)
        {
            GameObject o = GameObject.Instantiate(go);
            o.transform.SetParent(parent);
            o.transform.position = pos;
            GameObject.DontDestroyOnLoad(o);
            listeDashUpgrades.Add(o);
        }

        foreach (GameObject go in tempListeOndeUpgrades)
        {
            GameObject o = GameObject.Instantiate(go);
            o.transform.SetParent(parent);
            o.transform.position = pos;
            GameObject.DontDestroyOnLoad(o);
            listeOndeUpgrades.Add(o);
        }

        foreach (GameObject go in tempListeWeaponUpgrades)
        {
            GameObject o = GameObject.Instantiate(go);
            o.transform.SetParent(parent);
            o.transform.position = pos;
            GameObject.DontDestroyOnLoad(o);
            listeWeaponUpgrades.Add(o);
        }

        foreach (GameObject go in tempListeWeaponRewards)
        {
            GameObject o = GameObject.Instantiate(go);
            o.transform.SetParent(parent);
            o.transform.position = pos;
            GameObject.DontDestroyOnLoad(o);
            listeWeapons.Add(o);
        }

        listeUpgrades = new List<GameObject>();
        listeUpgrades.AddRange(listeShipUpgrades);
        listeUpgrades.AddRange(listeDashUpgrades);
        listeUpgrades.AddRange(listeOndeUpgrades);
        listeUpgrades.AddRange(listeWeaponUpgrades);

        //On casse les références de maniére un peu overkill
        //printAll();
    }

    public ReadOnlyCollection<GameObject> getWeapons() => this.listeWeapons.AsReadOnly();
    public ReadOnlyCollection<GameObject> getShipUpgrades() => this.listeShipUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getDashUpgrades() => this.listeDashUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getOndeUpgrades() => this.listeOndeUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getWeaponUpgrades() => this.listeWeaponUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getWeaponRewards() => this.listeWeaponRewards.AsReadOnly();
     
    public ReadOnlyCollection<GameObject> getUpgrades() => this.listeUpgrades.AsReadOnly();

    private void printAll()
    {
        Debug.Log("Weapons (" + listeWeapons .Count+ ")");
        foreach (GameObject go in listeWeapons)
            Debug.Log(go);

        Debug.Log("ShipUpgrades" + listeShipUpgrades.Count + ")");
        foreach (GameObject go in listeShipUpgrades)
            Debug.Log(go);

        Debug.Log("DashUpgrades" + listeDashUpgrades.Count + ")");
        foreach (GameObject go in listeDashUpgrades)
            Debug.Log(go);

        Debug.Log("OndeUpgrades" + listeOndeUpgrades.Count + ")");
        foreach (GameObject go in listeOndeUpgrades)
            Debug.Log(go);

        Debug.Log("WeaponUpgrades" + listeWeaponUpgrades.Count + ")");
        foreach (GameObject go in listeWeaponUpgrades)
            Debug.Log(go);
    }
}
