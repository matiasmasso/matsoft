using MatHelperStd;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTONumSs
    {
        private string _cleanValue;

        public string CleanValue
        {
            get
            {
                return _cleanValue;
            }
        }

        public DTONumSs(string rawValue)
        {
            _cleanValue = Clean(rawValue);
        }

        public bool IsValid(List<Exception> exs)
        {
            return Validate(_cleanValue, exs);
        }

        public string Formatted()
        {
            string retval = string.Format("{0}/{1}/{2}", Provincia(_cleanValue), Core(_cleanValue), ControlDigits(_cleanValue));
            return retval;
        }

        public bool Equals(string candidate)
        {
            var sCleanCandidate = Clean(candidate);
            var retval = sCleanCandidate == _cleanValue;
            return retval;
        }



        private static string Clean(string sRawNumSS)
        {
            string retval = TextHelper.LeaveJustNumericDigits(sRawNumSS);
            return retval;
        }

        private static string Provincia(string sCleanNumSS)
        {
            string retval = "";
            if (sCleanNumSS.Length > 2)
                retval = sCleanNumSS.Substring(0, 2);
            return retval;
        }

        private static string Core(string sCleanNumSS)
        {
            string retval = "";
            if (sCleanNumSS.Length > 4)
                retval = sCleanNumSS.Substring(2, sCleanNumSS.Length - 4);
            return retval;
        }

        private static string ControlDigits(string sCleanNumSS)
        {
            string retval = "";
            if (sCleanNumSS.Length > 4)
                retval = sCleanNumSS.Substring(sCleanNumSS.Length - 2);
            return retval;
        }



        private static bool Validate(string src, List<Exception> exs)
        {
            // El número de afiliación a la Seguridad Social tiene el siguiente formato: 
            // Código de la provincia donde se asigna el número de la Seguridad Social al trabajador o empresa (2 dígitos)
            // Número secuencial asignado (7 u 8 dígitos según sea una empresa o un trabajador)
            // Dígitos de control (2 dígitos)
            bool retval = false;
            string cleanResult = Clean(src);
            if (cleanResult.Length == 10 | cleanResult.Length == 11 | cleanResult.Length == 12)
            {
                string sProvincia = Provincia(cleanResult);
                string sCore = Core(cleanResult);
                string sControlDigits = ControlDigits(cleanResult);
                string sCheckedControlDigits = CalcControlDigits(sProvincia + sCore);
                if (sCheckedControlDigits == sControlDigits)
                    retval = true;
                else
                    exs.Add(new Exception("El número Seg.Social '" + src + "' te els digits de control erronis"));
            }
            else
                exs.Add(new Exception("El número Seg.Social '" + src + "' no te la longitut correcte"));
            return retval;
        }



        private static string CalcControlDigits(string numSegSocial, bool esNumEmpresa = false)
        {

            // Si hay más de 10 dígitos en el número se devolverá una excepción de
            // argumentos no permitidos.
            // 
            if ((numSegSocial.Length > 10) || (numSegSocial.Length == 0))
                throw new System.ArgumentException();

            // Si algún carácter no es un número, abandono la función.
            // 
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]");
            if ((regex.IsMatch(numSegSocial)))
                throw new System.ArgumentException();

            try
            {
                // Obtengo el número correspondiente a la Provincia
                // 
                string dcProv = numSegSocial.Substring(0, 2);

                // Obtengo el resto del número
                // 
                string numero = numSegSocial.Substring(2, numSegSocial.Length - 2);

                switch (numero.Length)
                {
                    case 8:
                        {
                            if ((esNumEmpresa))
                                // Si el número es de una empresa, no puede tener 8 dígitos.
                                return string.Empty;
                            else
                              // Compruebo si es un NAF nuevo o antiguo.
                              if (numero.StartsWith("0"))
                                // Es un número de afiliación antiguo. Lo formateo
                                // a siete dígitos, eliminando el primer cero.
                                numero = numero.Remove(0, 1);
                            break;
                        }

                    case 7:
                        {
                            // Puede ser un NAF antiguo o un CCC nuevo o viejo.
                            if ((esNumEmpresa))
                            {
                                // Si el primer dígito es un cero, es un CCC antiguo,
                                // por lo que lo formateo a seis dígitos, eliminando
                                // el primer cero.
                                if (numero.StartsWith("0"))
                                    numero = numero.Remove(0, 1);
                            }

                            break;
                        }

                    case 6:
                        {
                            // Si se trata del número de una empresa,
                            // es un CCC antiguo.
                            if ((!(esNumEmpresa)))
                                // Es un NAF antiguo, por lo que lo debo
                                // de formatear a 7 dígitos.
                                numero = numero.PadLeft(7, '0');
                            break;
                        }

                    default:
                        {
                            // Todos los demás casos, serán números antiguos
                            if ((esNumEmpresa))
                                // Lo formateo a seis dígitos.
                                numero = numero.PadLeft(6, '0');
                            else
                                // Lo formateo a siete dígitos.
                                numero = numero.PadLeft(7, '0');
                            break;
                        }
                }

                // Completo el número de Seguridad Social
                // 
                Int64 naf = Convert.ToInt64(dcProv + numero);

                // Calculo el Dígito de Control. Tengo que operar con números
                // Long, para evitar el error de desbordamiento que se puede
                // producir con los nuevos números de Seguridad Social
                // 
                naf = naf - (naf / 97) * 97;

                // Devuelvo el Dígito de Control formateado
                // 
                return string.Format("{0:00}", naf);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
