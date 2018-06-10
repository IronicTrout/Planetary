using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

	public int num_suns, num_planets;

	// Use this for initialization
	void Start () {

        //Determine the number of suns to put in the solar system. Max 2 for now.
		num_suns = (int)Random.Range (1, 2);

		num_planets = (int)Random.Range (5, 12);

		this.name = NameGenerator.generate (true);

        GameObject sun_container = new GameObject();
        Star sun = sun_container.AddComponent<Star>();
        sun.Initialize();
        sun.moveStar(-500 - sun.getDiameter()/2, 0, 0);

		for (int i = 0; i < num_planets; i++) {

			GameObject planet_container = new GameObject ();
			planet_container.name = "planet_container";
			Planet planet = planet_container.AddComponent<Planet> (); 
            planet.Initialize (false, sun.getScale());
            float x_position = i * (500f);
			planet.movePlanet(x_position, 0f, 0f);
			planet.setSystem (this.name);

			if (planet.num_moons > 0) {
			
				for (int j = 0; j < planet.num_moons; j++) {

					Planet moon = planet_container.AddComponent<Planet> (); 
					moon.Initialize (true, planet.scale);
					moon.movePlanet(x_position, 0f, j * (500f + (2 * planet.scale)));
					moon.setSystem (this.name);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
