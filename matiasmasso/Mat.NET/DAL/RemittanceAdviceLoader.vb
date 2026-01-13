Public Class RemittanceAdviceLoader


    Shared Function FromCca(oCca As DTOCca) As DTORemittanceAdvice
        Dim retval As New DTORemittanceAdvice
        retval.Items = New List(Of DTORemittanceAdviceItem)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT pnd.ContactGuid, Pnd.Div, Pnd.Ad, Pnd.Eur, Pnd.Pts, Pnd.Fch, Pnd.Fra ")
        sb.AppendLine(", Cca.Hash, CliGral.LangId ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Pnd on Ccb.PndGuid=Pnd.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON Pnd.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Ccb.CcaGuid ='" & oCca.Guid.ToString & "'  ")
        sb.AppendLine("ORDER BY Pnd.Fch, Pnd.Fra ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If retval.Iban Is Nothing Then
                retval.Proveidor = New DTOProveidor(DirectCast(oDrd("ContactGuid"), Guid)) 'necessari per Lang
                retval.Proveidor.Lang = DTOLang.Factory(oDrd("LangId").ToString())
                retval.DocFile = oCca.DocFile
            End If
            Dim oCur As DTOCur = DTOCur.Factory(oDrd("div").ToString())
            Dim oItem As New DTORemittanceAdviceItem
            With oItem
                Select Case oDrd("ad").ToString
                    Case "D"
                        .Amt = DTOAmt.Factory(CDec(oDrd("eur")), oCur.Tag, CDec(oDrd("pts"))).Inverse
                    Case "A"
                        .Amt = DTOAmt.Factory(CDec(oDrd("eur")), oCur.Tag, CDec(oDrd("pts")))
                End Select

                .Fch = oDrd("Fch")
                .Fra = oDrd("Fra")
                If Not IsDBNull(oDrd("Hash")) Then
                    Dim sHash As String = oDrd("Hash").ToString
                    .DocFile = New DTODocFile(sHash)
                    '.Url = Febl.DocFile.DownloadUrl(oDocFile, True)
                End If
            End With
            retval.Items.Add(oItem)

        Loop
        oDrd.Close()


        Return retval
    End Function
End Class

