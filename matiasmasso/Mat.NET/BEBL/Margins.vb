Public Class Margins

    Shared Function Fetch(oEmp As DTOEmp, year As Integer, mode As Models.MarginsModel.Modes, Optional target As Nullable(Of Guid) = Nothing) As Models.MarginsModel
        Return MarginsLoader.Fetch(oEmp, year, mode, target)
    End Function
End Class
