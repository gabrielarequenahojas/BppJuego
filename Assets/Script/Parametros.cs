using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Parametros : MonoBehaviour
{
    private dbfrutas objdb;    
    
    public string[] rutas;
    public string[] anim_gen = {"","",""};
   
    public GameObject carta;
    public GameObject game;
    public GameObject scenecontrol;



    public void Start()
    {
        carta = GameObject.Find("carta");
        game = GameObject.Find("game");
        scenecontrol = GameObject.Find("SceneControl");

        gameObject.transform.SetAsFirstSibling();
        Debug.Log("EN PARAMETROS");
        int index = 0;
        bool rep = true;
        
        objdb = new dbfrutas();
        
        var totalLocal = objdb.sqlite_totalRegistros();
        rutas =new string[totalLocal];

        for (int i = 0;i < rutas.Length; i++)
        {
            string sel = "SELECT * FROM frutas WHERE id = '";            
            rutas[i] = objdb.sqlite_consulta(sel + (i+1) + "'");            
        }
        
        foreach (string i in rutas) {           
            Debug.Log("rutas en parámetros " + i);
        }
        Debug.Log("total antes de" + anim_gen.Length);

        for (int i = 0;i < anim_gen.Length; i++)
        {
        	rep = true;
	        while (rep == true){
	            rep = false;
	            System.Random rand = new System.Random();
	            index = rand.Next(rutas.Length);
	            anim_gen[i] = rutas[index];
	            if(i == 0){	                
	                if (anim_gen[0] == anim_gen[1] || anim_gen[0] == anim_gen[2]){
	                rep = true;  
	                }
	            }
	            if(i == 1){	                
	                if (anim_gen[1] == anim_gen[0] || anim_gen[1] == anim_gen[2]){
	                rep = true;
	                }
	            }
	            if(i == 2){	               
	                if (anim_gen[2] == anim_gen[0] || anim_gen[2] == anim_gen[1]){
	                rep = true; 
	                }
	            }	                   
	        }
    	}

		foreach (string i in anim_gen) {
           
            Debug.Log("FRUTA FINAL Final" + i);            

        }

        Debug.Log("total" + anim_gen.Length);



      
        carta.SetActive(true);
        game.SetActive(true);
        scenecontrol.SetActive(true);
    }

   

    public string[] aleatorios(){
    	return anim_gen;
    }

    

    public void Update()
    {

    }

}
