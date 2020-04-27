using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory_show_details : MonoBehaviour
{

    public bool showDifference = true;

    public RectTransform RTArmes;
    public RectTransform RTArmures;

    public RectTransform Action_utiliser;
    public RectTransform Action_Equiper;
    public RectTransform Action_Jeter;


    public Text TX_NomObjet;
    public Text TX_DescriptionObjet;
    public Image IMG_Objet;
    public Text TX_TypeObjet;
    public Text TX_Quantite;

    // in RTArmesCAC
    public Text[] Degats;
    public Text[] DegatsSecondaires;
    public Text[] pourcentageCritiques;
    public Text[] Recul;
    public Text[] Portee;
    public Text Degats_DIFF;
    public Text DegatsSecondaires_DIFF;
    public Text pourcentageCritiques_DIFF;
    public Text Recul_DIFF;
    public Text Portee_DIFF;

    // in RTArmures
    public Text[] montantProtection;
    public Text[] montantProtectionSecondaire;
    public Text montantProtection_DIFF;
    public Text montantProtectionSecondaire_DIFF;

    void Start(){
        if(!showDifference){
            Degats_DIFF.gameObject.SetActive(false);
            DegatsSecondaires_DIFF.gameObject.SetActive(false);
            pourcentageCritiques_DIFF.gameObject.SetActive(false);
            Recul_DIFF.gameObject.SetActive(false);
            Portee_DIFF.gameObject.SetActive(false);
            montantProtection_DIFF.gameObject.SetActive(false);
            montantProtectionSecondaire_DIFF.gameObject.SetActive(false);
        }
    }



    public void Show_Object_Detail(inventory_object obj)
    {
        gameObject.SetActive(obj != null);
        if(obj == null){
            return;
        }

        bool object_is_arme = obj._type_object == inventory_main.type_object.arme_CaC || obj._type_object == inventory_main.type_object.arme_Distance || obj._type_object == inventory_main.type_object.arme_Projectile;
        bool object_is_armure = obj._type_object == inventory_main.type_object.armure_Corps || obj._type_object == inventory_main.type_object.armure_Mains || obj._type_object == inventory_main.type_object.armure_Pieds|| obj._type_object == inventory_main.type_object.armure_Tete;
        bool object_is_consommable = obj._type_object == inventory_main.type_object.consommable_player;
        bool object_is_ressource = obj._type_object == inventory_main.type_object.consommable_ressources;
        bool object_is_relique = obj._type_object == inventory_main.type_object.relique_relique;
        bool object_is_relique_composant = obj._type_object == inventory_main.type_object.relique_composant;



        // todo must evolve pour pouvoir verifier plus de types, ressources etc
        bool has_secondary_effect = obj._type_effets_secondaire != inventory_main.type_effets.aucun;

        DegatsSecondaires[0].gameObject.SetActive(has_secondary_effect);
        DegatsSecondaires[1].gameObject.SetActive(has_secondary_effect);
        DegatsSecondaires_DIFF.gameObject.SetActive(has_secondary_effect);
        montantProtectionSecondaire[0].gameObject.SetActive(has_secondary_effect);
        montantProtectionSecondaire[1].gameObject.SetActive(has_secondary_effect);
        montantProtectionSecondaire_DIFF.gameObject.SetActive(has_secondary_effect);

        RTArmes.gameObject.SetActive(object_is_arme);
        RTArmures.gameObject.SetActive(object_is_armure);

        TX_TypeObjet.text = obj._type_object.ToString();
        TX_DescriptionObjet.text = obj.description;
        TX_NomObjet.text = obj.nom;
        IMG_Objet.sprite = Sprite.Create(obj.image, new Rect(0.0f, 0.0f, obj.image.width, obj.image.height), new Vector2(0.5f, 0.5f), 100.0f);
        
        TX_Quantite.text = "x" + obj.quantite;
        TX_Quantite.gameObject.SetActive(obj.quantite > 1);


        Action_utiliser.gameObject.SetActive(object_is_consommable);
        Action_Equiper.gameObject.SetActive(object_is_arme || object_is_armure || object_is_relique_composant);
        Action_Jeter.gameObject.SetActive(object_is_arme || object_is_armure || object_is_consommable || object_is_ressource);

        if(object_is_arme){
            Degats[1].text = obj.degatsInfligesMin +  " -> " + obj.degatsInfligesMax;
            DegatsSecondaires[0].text = " + Dégats " + obj._type_effets_secondaire.ToString();
            DegatsSecondaires[1].text = obj.degatsSecondairesInfligesMin +  " -> " + obj.degatsSecondairesInfligesMax;

            pourcentageCritiques[1].text = obj.pourcentageCritique + "%";
            Recul[1].text = obj.puissanceDeRecul.ToString();

            if(obj._type_object == inventory_main.type_object.arme_Distance || obj._type_object == inventory_main.type_object.arme_Projectile){
                Portee[0].gameObject.SetActive(true);
                Portee[1].gameObject.SetActive(true);
                Portee[1].text = obj.portee.ToString();
            }else{
                Portee[0].gameObject.SetActive(false);
                Portee[1].gameObject.SetActive(false);
            }
        }else if (object_is_armure){
            montantProtection[1].text = obj.montantArmure_min +  " -> " + obj.montantArmure_max;
            montantProtectionSecondaire[1].text = obj.armureSecondaireMin +  " -> " + obj.armureSecondaireMax;
        }
    }
    
}
