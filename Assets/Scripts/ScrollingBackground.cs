using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Bryan
public class ScrollingBackground : MonoBehaviour {
   public GameObject backgroundPrefab;
   private GameObject[,] tiles = new GameObject[3,3];
   public GameObject ship;
   private float x;
   private float y;
   private Vector3 initialPos = new Vector3(0,0,0);
   
   void initRow(int rowNum, float yPos)
   {
      Vector3 pos = new Vector3(initialPos.x - x, initialPos.y + yPos, 0);
      //Left 
      tiles[rowNum,0] = (GameObject)Instantiate(backgroundPrefab, pos, Quaternion.identity);
      //Middle 
      pos.x = initialPos.x;
      tiles[rowNum,1] = (GameObject)Instantiate(backgroundPrefab, pos, Quaternion.identity);
      //Right 
      pos.x = initialPos.x + x;
      tiles[rowNum,2] = (GameObject)Instantiate(backgroundPrefab, pos, Quaternion.identity);
   }
   void initializeTiles()
   {
      //0,0 = top left
      initRow(0, y);
      initRow(1, 0);
      initRow(2, -y);
   }
	// Use this for initialization
	void Start () 
   {
      x = backgroundPrefab.GetComponent<Renderer>().bounds.size.x;
      y = backgroundPrefab.GetComponent<Renderer>().bounds.size.y;
		initializeTiles();
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
   //dir = 0 means everything shifts up, dir = 1 means everything shifts down
   void rotateCol(int colNum, int dir)
   {
      GameObject temp;
      if (dir == 0)
      {
         temp = tiles[0,colNum];
         tiles[0,colNum] = tiles[1,colNum];
         tiles[1,colNum] = tiles[2,colNum];
         tiles[2,colNum] = temp;
         Vector3 tf = tiles[1,colNum].transform.position;
         tf.y -= y;
         tiles[2,colNum].transform.position = tf;
      }
      else
      {
         temp = tiles[2,colNum];
         tiles[2,colNum] = tiles[1,colNum];
         tiles[1,colNum] = tiles[0,colNum];
         tiles[0,colNum] = temp;
         Vector3 tf = tiles[1,colNum].transform.position;
         tf.y += y;
         tiles[0,colNum].transform.position = tf;
      }
   }
	// Update is called once per frame
	void Update () 
   {
      //Scroll right
		if (ship.transform.position.x > tiles[1,1].transform.position.x + (x / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateRow(i,0);
      }
      //Scroll up
      if (ship.transform.position.y > tiles[1,1].transform.position.y + (y / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateCol(i,1);
      }
      //Scroll left
		if (ship.transform.position.x < tiles[1,1].transform.position.x - (x / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateRow(i,1);
      }
      //Scroll down
      if (ship.transform.position.y < tiles[1,1].transform.position.y - (y / 2))
      {
         for (int i = 0; i < 3; i++)
            rotateCol(i,0);
      }
	}
}