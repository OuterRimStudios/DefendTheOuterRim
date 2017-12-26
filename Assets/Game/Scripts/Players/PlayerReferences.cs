using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReferences : MonoBehaviour
{
    [Header("Player Specific References")]
    public Image playerArrow;
    public Text playerNumberTag;
    public GameObject playerCursor;
    public Image playerReticle;
    public ObjectPooling projectilePool;
}
