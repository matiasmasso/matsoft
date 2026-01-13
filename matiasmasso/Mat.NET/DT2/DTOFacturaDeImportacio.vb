Public Class DTOFacturaDeImportacio
    Property Cca As DTOCca
    Property Importacio As DTOImportacio
    Property Pnd As DTOPnd

    Shared Function Factory(ByRef oCca As DTOCca, oImportacio As DTOImportacio, oPnd As DTOPnd) As DTOFacturaDeImportacio
        Dim retval As New DTOFacturaDeImportacio
        With retval
            .Cca = oCca
            .Importacio = oImportacio
            .Pnd = oPnd
        End With
        Return retval
    End Function
End Class
