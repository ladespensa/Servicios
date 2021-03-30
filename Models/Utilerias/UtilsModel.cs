using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acmarkert.Models.Utilerias
{
    public class UtilsModel
    {
        public static DateTime obtenerFechaEntrega(DayOfWeek dia)
        {
            double valor = Double.Parse(VariablesModel.getVariableValue("HORAS_GRACIA"));
            DateTime date = DateTime.Now.AddHours(valor);

            if ((int)dia == (int)date.DayOfWeek)
            {
                date = date.AddDays(1);
            }
            while (date.DayOfWeek != dia)
            {
                date = date.AddDays(1);
            }

            return date;
        }
    }
}
