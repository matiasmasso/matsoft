using System;
using System.Collections.Generic;

namespace DTO
{
    // Public Delegate Sub ProgressBarHandler(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)

    public delegate void SetInvoiceSiiLogHandler(Defaults.Entornos entorno, ref object oFra, string Csv, List<Exception> exs);

    public delegate List<DTOBookFra> FindBookFrasHandler(string NifEmisor, string Num, int Year);
}
