using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClose : MonoBehaviour
{
  public GameObject Exit;
  public GameObject Panel;

  public void ClosePanel(){
      if(Exit != null){
          Panel.SetActive(false);
      }
  }

}
