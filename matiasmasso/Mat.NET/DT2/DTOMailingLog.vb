Public Class DTOMailingLog
    Property user As DTOUser
    Property fch As Date

    Public Function roundedFch() As Date 'arrodoneix als 5 minuts mes propers
        ' split into date + time parts
        Dim datepart = _fch.Date
        Dim timepart = _fch.TimeOfDay

        ' round time to the nearest 5 minutes
        timepart = TimeSpan.FromMinutes(Math.Floor((timepart.TotalMinutes + 2.5) / 5.0) * 5.0)

        ' combine the parts
        Dim retval = datepart.Add(timepart)
        Return retval
    End Function
End Class

