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
            Yell("Salut !") *
            Yell("Besoin de quelque chose ?") *
            Show() *
            Options
            (
                Option("ouai une arme pour combattre") *
                    Hide() *
                    Yell("desole je suis pacifiste") *
                    End() *

                Option("non ca va je me ballade") *
                    Hide() *
                    Yell("comme tu veux") *
                    Yell("a tres vite !") *
                    End()
            );
    }
}
