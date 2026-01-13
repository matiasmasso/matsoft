Public Class Proveidor

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOProveidor
        Dim retval As DTOProveidor = ProveidorLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oProveidor As DTOProveidor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProveidorLoader.Update(oProveidor, exs)
        Return retval
    End Function

    Shared Function Delete(oProveidor As DTOProveidor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProveidorLoader.Delete(oProveidor, exs)
        Return retval
    End Function

    Shared Function CheckFacturaAlreadyExists(oProveidor As DTOContact, ByVal oExercici As DTOExercici, ByVal sFraNum As String) As DTOCca
        Return ProveidorLoader.CheckFacturaAlreadyExists(oProveidor, oExercici, sFraNum)
    End Function

    Shared Function SaveFactura(exs As List(Of Exception), ByRef oCca As DTOCca, oPnds As IEnumerable(Of DTOPnd), Optional oImportacio As DTOImportacio = Nothing) As Boolean
        Return ProveidorLoader.SaveFactura(exs, oCca, oPnds, oImportacio)
    End Function

#End Region

End Class


Public Class Proveidors

End Class
