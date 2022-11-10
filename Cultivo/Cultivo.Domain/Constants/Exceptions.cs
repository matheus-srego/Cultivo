using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Domain.Constants
{
    public static class Exceptions
    {
        public const string MESSAGE_REGISTER = "Registros não detectados.";
        public const string MESSAGE_USER_NOT_EXIST = "Credenciais não coincidem com o que há no banco.";
        public const string MESSAGE_INCOMPLETE_INFORMATION = "As informações recebidas estão incompletas.";
    }
}
