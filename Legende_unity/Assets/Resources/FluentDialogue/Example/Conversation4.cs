using Fluent;

/// <summary>
/// This example explains how to show options to the player.
/// You also need an OptionsPresenter component to configure how the options are presented.
/// Show() shows the options presenter.
/// Branch() specifies that options should be shown to the player at this point in the conversation.
/// Please note o1(), o2() ... o9() are all the same as o(), the numbers have no significance
/// </summary>
public class Conversation4 : MyFluentDialogue
{
    public override FluentNode Create()
    {
        return
            Yell("Je cherche ta maman") *
            Yell("tu l'as vu ?") *
            Show() *
            Options
            (
                Option("Euh...") *
                    Hide() *
                    Yell("stp !!!!") *
                    End() *

                Option("Veux tu que je la cherche ?") *
                    Hide() *
                    Yell("OUi oui oui!") *
                    Yell("tu es adorable") *
                    End()
             );
    }
}
