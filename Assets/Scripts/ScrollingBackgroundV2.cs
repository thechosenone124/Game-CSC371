using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=======================================
 * Original Code By: Bryan Tan
 * Modified By: Davin Johnson
 * ======================================
 */

public class ScrollingBackgroundV2 : MonoBehaviour {

   //Davin Code=================================
   public GameObject bg;
   [Header("Transition Distance is the distance from origin where Bg will begin to fade."), Space(-10), Header("Bg will be fully transparent at Transition Distance x 2.")]
   public float transitionDistance = 500;
   private float dist = 0;
   public bool doTransition = true;
   //===========================================

   private GameObject[,] tiles = new GameObject[3,3];
   public GameObject ship;
   private float x;
   private float y;
   private Vector3 initialPos = new Vector3(0,0,0);

	// Use this for initialization
	void Start () 
   {
      x = bg.GetComponent<Renderer>().bounds.size.x;
      y = bg.GetComponent<Renderer>().bounds.size.y;
		initializeTiles();

      //Davin Code=================================
      bg.GetComponent<SpriteRenderer>().sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
      //===========================================
   }

   //dir = 0 means everything shifts left, dir = 1 means everything shifts right
   void rotateRow(int rowNum, int dir)
   {
      GameObject temp;
      if (dir == 0)
      {
         temp = tiles[rowNum,0];
         tiles[rowNum,0] = tiles[rowNum,1];
         tiles[rowNum,1] = tiles[rowNum,2];
         tiles[rowNum,2] = temp;
         Vector3 tf = tiles[rowNum,1].transform.position;
         tf.x += x;
         tiles[rowNum,2].transform.position = tf;
      }
      else
      {
         temp = tiles[rowNum,2];
         tiles[rowNum,2] = tiles[rowNum,1];
         tiles[rowNum,1] = tiles[rowNum,0];
         tiles[rowNum,0] = temp;
         Vector3 tf = tiles[rowNum,1].transform.position;
         tf.x -= x;
         tiles[rowNum,0].transform.position = tf;
      }
   }

	// Update is called once per frame
	void Update ()
   {
      //Davin Code=================================
      float bgAlpha = 1;

      dist = ship.transform.position.magnitude;

      if (doTransition && dist >= transitionDistance)
      {
         bgAlpha = ((transitionDistance * 2) - dist) / transitionDistance;
         bgAlpha = Mathf.Clamp01(bgAlpha);
         bg.GetComponent<SpriteRenderer>().sharedMaterial.color = new Color(1f, 1f, 1f, bgAlpha);
      }

      Debug.Log(dist + ", " + bgAlpha);
      //===========================================

      //Scroll right
      if (ship.transform.position.x > tiles[1, 1].transform.position.x + (x / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateRow(i, 0);
      }
      //Scroll up
      if (ship.transform.position.y > tiles[1, 1].transform.position.y + (y / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateCol(i, 1);
      }
      //Scroll left
      if (ship.transform.position.x < tiles[1, 1].transform.position.x - (x / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateRow(i, 1);
      }
      //Scroll down
      if (ship.transform.position.y < tiles[1, 1].transform.position.y - (y / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateCol(i, 0);
      }

   }

   //dir = 0 means everything shifts up, dir = 1 means everything shifts down
   void rotateCol(int colNum, int dir)
   {
      GameObject temp;
      if (dir == 0)
      {
         temp = tiles[0, colNum];
         tiles[0, colNum] = tiles[1, colNum];
         tiles[1, colNum] = tiles[2, colNum];
         tiles[2, colNum] = temp;
         Vector3 tf = tiles[1, colNum].transform.position;
         tf.y -= y;
         tiles[2, colNum].transform.position = tf;
      }
      else
      {
         temp = tiles[2, colNum];
         tiles[2, colNum] = tiles[1, colNum];
         tiles[1, colNum] = tiles[0, colNum];
         tiles[0, colNum] = temp;
         Vector3 tf = tiles[1, colNum].transform.position;
         tf.y += y;
         tiles[0, colNum].transform.position = tf;
      }
   }
   
   void initRow(int rowNum, float yPos)
   {
      Vector3 pos = new Vector3(initialPos.x - x, initialPos.y + yPos, 0);
      //Left 
      tiles[rowNum, 0] = (GameObject)Instantiate(bg, pos, Quaternion.identity);
      //Middle 
      pos.x = initialPos.x;
      tiles[rowNum, 1] = (GameObject)Instantiate(bg, pos, Quaternion.identity);
      //Right 
      pos.x = initialPos.x + x;
      tiles[rowNum, 2] = (GameObject)Instantiate(bg, pos, Quaternion.identity);
   }

   void initializeTiles()
   {
      //0,0 = top left
      initRow(0, y);
      initRow(1, 0);
      initRow(2, -y);
   }

}