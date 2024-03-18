using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CustomInputCursor : MonoBehaviour
{
  // The slider in Unity only goes up to 5,
  // this script overrides the slider and makes the caret
  // bigger, to better fit the style
  public int caretSize = 20;
  private TMP_InputField inputField;
  private void Awake()
  {
    PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
    inputField = GetComponent<TMP_InputField>();
    inputField.caretWidth = caretSize;
  }
}
