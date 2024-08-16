using System.ComponentModel;

namespace EstudoMVC.DataContent.Enum
{
    public enum Score
    {
        [Description("Muito Satisfeito")] MuitoSatisfeito = 5,
        [Description("Satisfeito")] Satisfeito = 4,
        [Description("Neutro")] Neutro = 3,
        [Description("Insatisfeito")] Insatisfeito = 2,
        [Description("Muito Insatisfeito")] MuitoInsatisfeito = 1
    }
}
