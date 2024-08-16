using System.ComponentModel;

namespace EstudoMVC.DataContent.Enum
{
    public enum ExperienceType
    {
        [Description("Artísitico")] Artistico,
        [Description("Histórico")] Historico,
        Natural,
        Cultural,
        Exploracao,
        Radical,
        Outros
    }
}
