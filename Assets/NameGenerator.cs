using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour {

	private static string generated_name = "";

	private static bool has_suffix, is_numbered, is_numeral, add_letters;

	private static string [] prefixes = new string[]{
		"Mer",
		"Ven",
		"Ear",
		"M",
		"Jup",
		"Sat",
		"Ur",
		"Nep",
		"Hal"
	};

	private static string [] middles = new string[]{
		"cur",
		"it",
		"an",
		"t",
		"cy"
	};

	private static string [] suffixes = new string[]{
		"on",
		"o",
		"ury",
		"us",
		"th",
		"ars",
		"er",
		"urn",
		"une",
		"ine",
		"ane"
	};

	private static string[] alphabet = new string[]{ 
		"A", "B", "C", "D", "E", "F",
		"G", "H", "I", "J", "K", "L",
		"M", "N", "O", "P", "Q", "R",
		"S", "T", "U", "V", "W", "X",
		"Y", "Z"
	};

	public static string generate(bool is_system = false){

		generated_name = "";

		int num_syllables = (int)Random.Range (2, 4);

		for (int i = 0; i < num_syllables; i++) {
		
			if (i == 0) {

				generated_name += prefixes [(int)Random.Range (0, prefixes.Length - 1)];
			} else if (num_syllables > 2 && i < num_syllables - 1) {

				generated_name += middles [(int)Random.Range (0, middles.Length - 1)];
			} else {

				generated_name += suffixes [(int)Random.Range (0, suffixes.Length - 1)];
			}
		}

		//50% of the time add a suffix onto the planet and attach it
		if (Random.Range (0, 100) < 50) {
		
			is_numbered = true;
		} else {

			is_numbered = false;
		}

		if (is_system) {

			is_numbered = false;

			generated_name += " System";
		}

		if (is_numbered) {
		
			generated_name += " ";
			int number = (int)Random.Range (1, 3000);
			string string_number = "" + number + "";

			//75% of the time convert the suffix to a Roman numeral
			if (Random.Range (0, 100) < 25) {

				is_numeral = true;
				add_letters = false;
			} else {

				is_numeral= false;
				//10% of the time add letters on to the number for the planet
				if (Random.Range (0, 100) < 10) {

					add_letters = true;
				} else {
					add_letters = false;
				}
			}

			if (is_numeral) {

				string_number = NameGenerator.ToRoman (number);
			} else if (add_letters) {
				
				int num_letters = (int)Random.Range (1, 3);
				for (int i = 0; i < num_letters; i++) {

					string_number += alphabet [(int)Random.Range(0, 25)];
				}
			}

			generated_name += string_number;
		}

		return generated_name;
	}

	private static string ToRoman(int number) {
		
		if (number < 1) return string.Empty;            
		if (number >= 1000) return "M" + ToRoman(number - 1000);
		if (number >= 900) return "CM" + ToRoman(number - 900); 
		if (number >= 500) return "D" + ToRoman(number - 500);
		if (number >= 400) return "CD" + ToRoman(number - 400);
		if (number >= 100) return "C" + ToRoman(number - 100);            
		if (number >= 90) return "XC" + ToRoman(number - 90);
		if (number >= 50) return "L" + ToRoman(number - 50);
		if (number >= 40) return "XL" + ToRoman(number - 40);
		if (number >= 10) return "X" + ToRoman(number - 10);
		if (number >= 9) return "IX" + ToRoman(number - 9);
		if (number >= 5) return "V" + ToRoman(number - 5);
		if (number >= 4) return "IV" + ToRoman(number - 4);
		if (number >= 1) return "I" + ToRoman (number - 1);
		else return string.Empty;
	}
}
