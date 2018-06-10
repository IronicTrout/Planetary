using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet: MonoBehaviour { 

	//GameObject for the planet 'container'
	public GameObject planet_controller;

	//TODO Remove base from sockets that can be chosen as resources when new assets are created
	private string[] socket_types = new string[]{"base", "geyser"};

	//Create two arrays that determine the sockets for the planets
	//The first array is for the names of the sockets so the right assets can be loaded
	//The second array is so multiple assets are not being loaded for the same socket
	//It is also used when determining which sockets still need to be filled with the base assets at the end of the load
	private string[] sockets = new string[]{"front", "back", "top", "bottom", "left", "right"};
	private bool[] socket_used = new bool[]{false, false, false, false, false, false};

	//Floats for determining the rotational speed, orbits speed, and scale of the planet and orbit.
	//NOTE: Scale of the planet is used to determine the orbit size and max capacity that can be generated for the planet.
	public float scale, orbit_speed, rotation_speed;

	//Float for determining the axis of tilt for the planet
	public float axis;
	private Quaternion axis_tilt;

	//Int for the number of rings that a planet has
	public int num_rings, num_moons;

	//The number of drones that can be deployed to the planet.
	//This value is used when determining the maximum number of sockets that a planet can have as resources.
	public int capacity;

	//Strings for the name of the planet and the name of the system that the planet belongs to.
	public string planet_name, system_name;

	//Different modifiers that a planet can have that determine some properties for the planet.
	//TODO figure out and implement modifiers
	//enum environmental_modifier;

	//The rate at which resources are generated on the planet
	private float resource_rate = 1.0f;
	
	// Update is called once per frame
	void Update () {

		planet_controller.transform.Rotate (0, Time.deltaTime * (65 / scale), 0);
	}

	public void Initialize(bool isMoon = false, float parent_scale = 0) {

		planet_name = NameGenerator.generate ();

		planet_controller = new GameObject ();
		planet_controller.name = "planet_controller";

		//Always load the skeleton of the planet
		GameObject planet_skeleton = (GameObject)Object.Instantiate (Resources.Load ("planet_skeleton")) as GameObject;
		planet_skeleton.transform.SetParent (planet_controller.transform);

        if (parent_scale > 0) {
            
            //Determine the scale of the moon.
            scale = Random.Range(1, parent_scale / 10);
        }else {

            scale = Random.Range(1, 100);
        }

		//Determine if the planet is a moon or not.
		if (!isMoon) {

			//Determine the axial tilt of the planet
			axis = Random.Range (-90, 90);

			//Determine the max capacity of the planet based on the scale of it
			capacity = (int)Random.Range (1, (int)scale / 10);

			//Determine the number of moons the planet has, if any.
			num_moons = (int)Random.Range(0, 5);
		} else {

			//Determine the axial tilt of the moon.
			axis = Random.Range(-25, 25);

			//Determine the maximum capacity of the moon.
			capacity = (int)Random.Range(1, 2);
		}

		axis_tilt = Quaternion.Euler (0, 0, axis);

		//Iterate through the capacity and pick which sockets it will get
		for (int i = 0; i < Mathf.Min (capacity, 6); i++) {

			//Get the type and location of socket to add to the planet
			string socket_type = socket_types [(int)Random.Range (0, socket_types.Length)];

			int socket_position = (int)Random.Range (0, 5);

			//If we didn't already add a socket here go ahead and add it to the planet change that sockets status to being used.
			if (!socket_used [socket_position]) {

				GameObject planet_socket = (GameObject)Object.Instantiate (Resources.Load ("socket_" + sockets [socket_position] + "_" + socket_type)) as GameObject;
				planet_socket.transform.SetParent (planet_controller.transform);
				socket_used [socket_position] = true;
			}
		}

		//Iterate through all of the sockets on a planet and add a base socket to all of the sockets that are not being used.
		for (int i = 0; i < 6; i++) {

			if (!socket_used [i]) {

				GameObject planet_socket = (GameObject)Object.Instantiate (Resources.Load ("socket_" + sockets [i] + "_base")) as GameObject;
				planet_socket.transform.SetParent (planet_controller.transform);
				socket_used [i] = true;
			}
		}

		planet_controller.transform.localScale += new Vector3 (scale, scale, scale);
		planet_controller.transform.rotation = axis_tilt;
	}

	public void movePlanet(float x, float y, float z) {

		planet_controller.transform.position = new Vector3 (x, y, z);
	}

	public void setSystem(string new_system_name) {

		this.system_name = new_system_name;
	}

    public float getRadius() {

        return 0f;
        //return planet_skeleton.GetComponent<MeshFilter>().mesh.bounds.size.x * planet_skeleton.transform.localScale.x;
    }
}
