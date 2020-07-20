using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
        Pourquoi ?

        - La forge permet d'ajouter quelques petits gamePlays intéressant, notamment lorsque le jeu sera en VR.
        - La forge permet d'introduire le besoin d'accumulation et de recherche de ressources importante pour le joueur.
        - Ces ressources peuvent être utilisés des lors qu'il faut récompenser le joueur.

        Comment ça marche ?

        - Le joueur peut créer, transformer ou améliorer (on dira "CRAFTER") des armes, armures, pièces de reliques ou objets spéciaux.
        - Pour la création d'un nouvel objet, le joueur doit posséder un plan de cet objet.
        - Les parties améliorables ou transformables sont précisés dans le plan d'un objet : il peut ne pas y en avoir.

        - Pour crafter un objet, le joueur doit posséder des ressources, qu'il loot pendant le jeu.
            - Ces ressources se divisent en deux types :
                - Ressources basique,
                - Ressources composites
            
            - Les ressources composites sont des mélanges de ressources basiques et/ou de ressources composites.
            - Pour mélanger plusieurs ressources et en obtenir une nouvelle, il faut savoir les DOSER correctement.
            - Lorsque le joueur tente de mélanger deux ressources pour en obtenir une nouvelle, il peut :
                - L'obtenir en bonne qualité,
                - L'obtenir en qualité moyenne,
                - L'obtenir en mauvaise qualité,
                - Obtenir un matériau inutilisable (gachis)
            - Les bons dosages sont en pourcentage, que le joueur ne connais pas.
            EXEMPLE : Mélanger du FER BLEU avec du FER ROUGE pour obtenir un matériau composite FER VIOLET :
                - les pourcentages de métal mélangés sont affiché :
                    - Le joueur teste 70% FER ROUGE et 30% FER BLEU : il obtient un matériau composite de mauvaise qualité.
                    - Le joueur teste 60% FER ROUGE et 40% FER BLEU : il obtient un matériau composite inutile. Il comprend alors que son premier essai était plus proche d'un bon mélange avec 70% de FER ROUGE.
                    - Le joueur teste 80% FER ROUGE et 20% FER BLEU : il obtient un matériau composite de bonne qualité.
                - Le joueur comprendra donc qu'il faut 80% de FER ROUGE et 20% de FER BLEU pour obtenir un bon FER VIOLET.
                - L'information des meilleurs dosage peut être obtenue via de multiples essais, mais aussi dans certains livres ou dans des discussions avec des PNJ.

            - La qualité des matériaux compte lors du CRAFT : plus elle est bonne, plus l'objet crafté sera bon.

            - Les matériaux peuvent posséder plusieurs états.
                - Par exemple, pour les métaux : 
                            - MINERAI (de ...) BRUT
                            - BARRE (de ...)
                            - BARRE (de ...) CONDENSE
                            - ... etc

            - Certains objets sont destructible, et permettent de récupérer certains de leurs matériaux (mais avec une perte par rapport à la composition initiale)

            
    La FORGE permet de réaliser trois actions :
        - Fonte de minerais - création de ressources composites
        - Forge de nouveaux objets
        - Forge d'amélioration d'objets

    En entrant dans une FORGE, le joueur peut faire ces actions (si toutefois le feu est actif...)



    */


public class forge_manager : MonoBehaviour
{
    public static forge_manager instance;

    





    void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }

    public void OpenForge()
    {
        


    }

    public void CloseForge()
    {
        


    }
    public void OpenFonte()
    {
        


    }

    public void CloseFonte()
    {
        


    }




}
