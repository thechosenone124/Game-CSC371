using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 *
 */
public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,0,45) * Time.deltaTime);
    }
}