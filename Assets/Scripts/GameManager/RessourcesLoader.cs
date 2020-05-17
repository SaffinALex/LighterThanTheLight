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

    public RessourcesLoader(string wepRep, string shipUpRep, string dashUpRep, string ondeUpRep, string wepUpRep)
    {
        listeWeapons = new List<GameObject>();
        listeShipUpgrades = new List<GameObject>();
        listeDashUpgrades = new List<GameObject>();
        listeOndeUpgrades = new List<GameObject>();
        listeWeaponUpgrades = new List<GameObject>();

        List<GameObject> tempListeWeapons = new List<GameObject>(Resources.LoadAll(wepRep, typeof(GameObject)).Cast<GameObject>().ToArray());

        List<GameObject> tempListeShipUpgrades = new List<GameObject>(Resources.LoadAll(shipUpRep,typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeDashUpgrades = new List<GameObject>(Resources.LoadAll(dashUpRep, typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeOndeUpgrades = new List<GameObject>(Resources.LoadAll(ondeUpRep, typeof(GameObject)).Cast<GameObject>().ToArray());
        List<GameObject> tempListeWeaponUpgrades = new List<GameObject>(Resources.LoadAll(wepUpRep, typeof(GameObject)).Cast<GameObject>().ToArray());

        foreach (GameObject go in tempListeWeapons)
            if(!(go.name.Contains("Enemy") || go.name.Contains("enemy")))
                listeWeapons.Add(GameObject.Instantiate(go));

        foreach (GameObject go in tempListeShipUpgrades)
            listeShipUpgrades.Add(GameObject.Instantiate(go));

        foreach (GameObject go in tempListeDashUpgrades)
            listeDashUpgrades.Add(GameObject.Instantiate(go));

        foreach (GameObject go in tempListeOndeUpgrades)
            listeOndeUpgrades.Add(GameObject.Instantiate(go));

        foreach (GameObject go in tempListeWeaponUpgrades)
            listeWeaponUpgrades.Add(GameObject.Instantiate(go));

        listeUpgrades = new List<GameObject>();
        listeUpgrades.AddRange(listeShipUpgrades);
        listeUpgrades.AddRange(listeDashUpgrades);
        listeUpgrades.AddRange(listeOndeUpgrades);
        listeUpgrades.AddRange(listeWeaponUpgrades);

        //On casse les références de maniére un peu overkill
        tempListeWeapons.Clear(); tempListeWeapons = null;
        tempListeShipUpgrades.Clear(); tempListeShipUpgrades = null;
        tempListeDashUpgrades.Clear(); tempListeDashUpgrades = null;
        tempListeOndeUpgrades.Clear(); tempListeOndeUpgrades = null;
        tempListeWeaponUpgrades.Clear(); tempListeWeaponUpgrades = null;

        Resources.UnloadUnusedAssets();

        //printAll();
    }

    public ReadOnlyCollection<GameObject> getWeapons() => this.listeWeapons.AsReadOnly();
    public ReadOnlyCollection<GameObject> getShipUpgrades() => this.listeShipUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getDashUpgrades() => this.listeDashUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getOndeUpgrades() => this.listeOndeUpgrades.AsReadOnly();
    public ReadOnlyCollection<GameObject> getWeaponUpgrades() => this.listeWeaponUpgrades.AsReadOnly();
     
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
