using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PublicUserData
{

    public List<Pet> pets;
    public int currentPet;
    public string username;

}

public class UserData
{
    
    public PublicUserData publicData;
    public string guid;
    public int kibble;

}
