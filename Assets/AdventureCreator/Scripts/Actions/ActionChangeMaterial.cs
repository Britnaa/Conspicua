﻿/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2014
 *	
 *	"ActionChangeMaterial.cs"
 * 
 *	This Action allows you to change an object's material.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{
	
	[System.Serializable]
	public class ActionChangeMaterial : Action
	{

		public int constantID = 0;
		public int parameterID = -1;

		public bool isPlayer;
		public GameObject obToAffect;
		public int materialIndex;
		public Material newMaterial;
		
		
		public ActionChangeMaterial ()
		{
			this.isDisplayed = true;
			title = "Object: Change material";
		}


		override public void AssignValues (List<ActionParameter> parameters)
		{
			if (isPlayer)
			{
				obToAffect = KickStarter.player.gameObject;

				if (KickStarter.player && KickStarter.player.spriteChild && KickStarter.player.spriteChild.renderer)
				{
				    obToAffect = KickStarter.player.spriteChild.gameObject;
				}
			}
			else
			{
				obToAffect = AssignFile (parameters, parameterID, constantID, obToAffect);
			}
		}

		
		override public float Run ()
		{
			if (obToAffect && obToAffect.renderer && newMaterial)
			{
				Material[] mats = obToAffect.renderer.materials;
				mats[materialIndex] = newMaterial;
				obToAffect.renderer.materials = mats;
			}
			return 0f;
		}
		
		
		#if UNITY_EDITOR
		
		override public void ShowGUI (List<ActionParameter> parameters)
		{
			isPlayer = EditorGUILayout.Toggle ("Affect player?", isPlayer);
			if (!isPlayer)
			{
				parameterID = Action.ChooseParameterGUI ("Object to affect:", parameters, parameterID, ParameterType.GameObject);
				if (parameterID >= 0)
				{
					constantID = 0;
					obToAffect = null;
				}
				else
				{
					obToAffect = (GameObject) EditorGUILayout.ObjectField ("Mesh renderer:", obToAffect, typeof (GameObject), true);
					
					constantID = FieldToID (obToAffect, constantID);
					obToAffect = IDToField (obToAffect, constantID, true);
				}
			}

			materialIndex = EditorGUILayout.IntSlider ("Material index:", materialIndex, 0, 10);
			newMaterial = (Material) EditorGUILayout.ObjectField ("New material:", newMaterial, typeof (Material), false);

			AfterRunningOption ();
		}
		
		
		public override string SetLabel ()
		{
			if (obToAffect)
			{
				string labelAdd = " (" + obToAffect.gameObject.name;
				if (newMaterial)
				{
					labelAdd += " - " + newMaterial;
				}
				labelAdd += ")";
				return labelAdd;
			}
			return "";
		}
		
		#endif
		
	}
	
}