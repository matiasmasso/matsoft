Public Class AeatMod347

#Region "Flat File"

    Shared Function Factory(oExercici As DTOExercici, exs As List(Of Exception)) As DTOAeatMod347
        Dim retval As New DTOAeatMod347
        With retval
            .Exercici = oExercici
            .Items = New List(Of DTOAeatMod347Item)
            .Items.AddRange(AeatMod347Loader.Compres(retval, exs))
            .Items.AddRange(AeatMod347Loader.Vendes(retval, exs))
        End With

        Return retval
    End Function

    Shared Function VendesFromUser(exs As List(Of Exception), oUser As DTOUser, year As Integer) As List(Of DTOAeatMod347Item)
        Return AeatMod347Loader.VendesFromUser(exs, oUser, year)
    End Function


    Shared Function CompresDetall(oExercici As DTOExercici, oContact As DTOContact, exc As List(Of Exception)) As List(Of DTOAeatMod347Cca)
        Dim retval As List(Of DTOAeatMod347Cca) = AeatMod347Loader.CompresDetall(oExercici, oContact, exc)
        Return retval
    End Function

    Shared Function VendesDetall(oExercici As DTOExercici, oContact As DTOContact, exc As List(Of Exception)) As List(Of DTOAeatMod347Cca)
        Dim retval As List(Of DTOAeatMod347Cca) = AeatMod347Loader.VendesDetall(oExercici, oContact, exc)
        Return retval
    End Function


    Shared Function Csv(oMod347 As DTOAeatMod347, exs As List(Of Exception)) As DTOCsv
        Dim retval As New DTOCsv("M+O 347.csv")
        Dim oRow = retval.AddRow()
        oRow.AddCell("Nif")
        oRow.AddCell("Declarat")
        oRow.AddCell("Codi Provincia")
        oRow.AddCell("Codi Pais")
        oRow.AddCell("Clau Operacio")
        oRow.AddCell("Total anual")
        oRow.AddCell("Trimestre 1")
        oRow.AddCell("Trimestre 2")
        oRow.AddCell("Trimestre 3")
        oRow.AddCell("Trimestre 4")
        For Each item As DTOAeatMod347Item In oMod347.Items
            If Declarable(item, exs) Then
                oRow = retval.AddRow()
                oRow.AddCell(DTOAeatMod347.Nif(item.Contact))
                oRow.AddCell(Left(TextHelper.RemoveAccents(item.Contact.Nom), 40).ToUpper)
                oRow.AddCell(item.CodProvincia)
                oRow.AddCell(item.CodPais)
                oRow.AddCell(DTOAeatMod347.ClauOperacio(item))
                oRow.AddCell(item.T1 + item.T2 + item.T3 + item.T4)
                oRow.AddCell(item.T1)
                oRow.AddCell(item.T2)
                oRow.AddCell(item.T3)
                oRow.AddCell(item.T4)
            End If
        Next
        Return retval
    End Function



#End Region

    Shared Function MinimDeclarable(exs As List(Of Exception)) As Decimal
        Static retval As Decimal
        If retval = 0 Then retval = BEBL.Default.Find(DTODefault.Codis.Min347).Value
        Return retval
    End Function

    Shared Function Declarable(oItem As DTOAeatMod347Item, exs As List(Of Exception)) As Boolean
        Dim dcMinimDeclarable = MinimDeclarable(exs)
        Dim retval = DTOAeatMod347Item.Declarable(oItem, dcMinimDeclarable, exs)
        Return retval
    End Function
End Class
