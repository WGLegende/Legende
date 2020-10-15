using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enum_manager : MonoBehaviour
{

    public enum game_pad_attribution{
        player,
        inventory,
        kart,
        dialogue,
        actionDisplay,
        actionDisplayKart,
        startConversationNavy,
        actionNavy,
        nothing
    }

    public enum typeAttack{
        Cac,
        distance,
        Cac_et_distance 
    }

    public enum typeOuverture{
        classique,
        coulissant,
        slideUp,
        chute     
    }

    public enum ressource{
        aucun,
        composite,
        textile,
        plante_ou_champignon,
        liquide,
        metal,
        pierre,
        bois
    };

    public enum type_object{ 
        aucun,
        equipement,
        consommable,
        ressource,
        plan,
        carte,
        quete,
        savoir,
        relique
    };

    public enum equipement{
        aucun,
        arme_CaC,
        arme_Distance,
        arme_Projectile,
        bouclier,
        armure_Tete,
        armure_Corps,
        armure_Mains,
        armure_Pieds
    };


    public enum type_caracteristiques{ 
        aucun, 
        tous,
        attack,
        armure,
        vie,
        other_value
    };

    public enum type_effets{ 
        aucun, 
        life,
        brut_force,
        hot,
        cold,
        ondes,
        tous
    };
    public enum Enemy_type{
        none,
        all,
        human,
        monster,
        spirit,
        mechanic,
        beast,
        vegetal,
        flying
    }
    public enum Weapon_type{
        none,
        all,
        epee,
        hache,
        masse,
        hast,
        arc,
        arbalete,
        explosif,
        mine,
        piege
    }
    public enum Attack_type{
        none,
        all,
        corpsACorps,
        distance,
        piege
    }

    public enum Comparateur{
        lower,
        lowerOrEqual,
        equal,
        higherOrEquals,
        higher
    }

   public enum Direction{
        up,
        right,
        down,       
        left       
    }
}
