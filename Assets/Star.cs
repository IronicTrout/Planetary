using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
	
    //GameObject for the planet 'container'
    public GameObject star;

    //Floats for determining the rotational speed, orbits speed, and scale of the planet and orbit.
    //NOTE: Scale of the planet is used to determine the orbit size and max capacity that can be generated for the planet.
    public float scale, rotation_speed;

    //Float for determining the axis of tilt for the planet
    public float axis;
    private Quaternion axis_tilt;

    //Strings for the name of the planet and the name of the system that the planet belongs to.
    public string system_name;

    // Update is called once per frame
    void Update() {

    }

    public void Initialize() {

        //Always load the skeleton of the planet
        //star = (GameObject)Object.Instantiate(Resources.Load("star")) as GameObject;
        star = new GameObject();
        star.AddComponent<MeshFilter>();
        star.AddComponent<MeshRenderer>();

        star.GetComponent<MeshFilter>().mesh = Instantiate(Resources.Load("star")) as Mesh;
        star.GetComponent<MeshRenderer>().material = Instantiate(Resources.Load("Star Mat")) as Material;


        //Determine the size of the moon.
        scale = Random.Range(1, 1000);

        //Determine the axial tilt of the moon.
        axis = Random.Range(-25, 25);

        axis_tilt = Quaternion.Euler(0, 0, axis);

        star.transform.localScale += new Vector3(scale, scale, scale);
        star.transform.rotation = axis_tilt;
    }

    public void setSystem(string new_system_name) {

        this.system_name = new_system_name;
    }

    public float getScale() {

        return scale;
    }

    public void moveStar(float x, float y, float z) {

        star.transform.position = new Vector3(x, y, z);
    }
}
