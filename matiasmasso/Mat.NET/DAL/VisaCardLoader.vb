Public Class VisaCardLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOVisaCard
        Dim retval As DTOVisaCard = Nothing
        Dim oVisaCard As New DTOVisaCard(oGuid)
        If Load(oVisaCard) Then
            retval = oVisaCard
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oVisaCard As DTOVisaCard) As Boolean
        If Not oVisaCard.IsLoaded And Not oVisaCard.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT VisaCard.*, CliGral.FullNom, CliBnc.Abr, VisaEmisor.Nom as VisaNom, CliBnc.Abr as BncAbr ")
            sb.AppendLine("FROM VisaCard ")
            sb.AppendLine("INNER JOIN VisaEmisor ON VisaCard.Emisor = VisaEmisor.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON VisaCard.Titular=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliBnc ON VisaCard.Banc=CliBnc.Guid ")
            sb.AppendLine("WHERE VisaCard.Guid='" & oVisaCard.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oVisaCard.Guid.ToString())
            If oDrd.Read Then
                With oVisaCard
                    .Titular = New DTOContact(oDrd("Titular"))
                    With .Titular
                        .FullNom = oDrd("FullNom")
                    End With
                    .Emisor = New DTOVisaEmisor(oDrd("Emisor"))
                    With .Emisor
                        .Nom = oDrd("VisaNom")
                    End With
                    .Banc = New DTOBanc(oDrd("Banc"))
                    With .Banc
                        .Abr = oDrd("BncAbr")
                    End With

                    .Digits = oDrd("Digits")
                    .Nom = oDrd("Alias")
                    .Caduca = oDrd("Caduca")
                    .Limit = DTOAmt.Factory(CDec(oDrd("Limit")))

                    .Caduca = SQLHelper.GetStringFromDataReader(oDrd("Caduca"))
                    .Baja = SQLHelper.GetFchFromDataReader(oDrd("FchCanceled"))
                    .CCID = SQLHelper.GetStringFromDataReader(oDrd("CCID"))

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oVisaCard.IsLoaded
        Return retval
    End Function

    Shared Function Update(oVisaCard As DTOVisaCard, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oVisaCard, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oVisaCard As DTOVisaCard, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VisaCard ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oVisaCard.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oVisaCard.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oVisaCard
            oRow("Alias") = .Nom
            oRow("Titular") = .Titular.Guid
            If .Limit IsNot Nothing Then
                oRow("Limit") = .Limit.Eur
            End If
            oRow("Digits") = .Digits
            oRow("Caduca") = .Caduca
            oRow("FchCanceled") = SQLHelper.NullableFch(.Baja)
            oRow("Banc") = .Banc.Guid
            oRow("Emisor") = .Emisor.Guid
            oRow("CCID") = .CCID
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oVisaCard As DTOVisaCard, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oVisaCard, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oVisaCard As DTOVisaCard, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE VisaCard WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oVisaCard.Guid.ToString())
    End Sub

#End Region

End Class

Public Class VisaCardsLoader

    Shared Function All(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional Active As Boolean = True) As List(Of DTOVisaCard)
        Dim retval As New List(Of DTOVisaCard)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VisaCard.Guid, VisaCard.[Alias], VisaCard.Digits, VisaCard.Titular, CliGral.FullNom, CliBnc.Abr, VisaCard.Emisor, VisaEmisor.Nom as VisaNom, Caduca, FchCanceled ")
        sb.AppendLine("FROM VisaCard ")
        sb.AppendLine("INNER JOIN VisaEmisor ON VisaCard.Emisor = VisaEmisor.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON VisaCard.Titular=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliBnc ON VisaCard.Banc=CliBnc.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND VisaCard.Titular = '" & oContact.Guid.ToString & "' ")
        End If
        If Active Then
            sb.AppendLine("AND (Caduca<='0000' OR Caduca IS NULL OR (SUBSTRING(Caduca,3,2)+SUBSTRING(Caduca,1,2) > CONVERT(varchar(10),getdate(),12))) ")
        End If
        sb.AppendLine("ORDER BY CliGral.FullNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOVisaCard(oDrd("Guid"))
            With item
                .Nom = oDrd("Alias")
                .Digits = oDrd("Digits")
                .Titular = New DTOContact(oDrd("Titular"))
                .Titular.FullNom = oDrd("FullNom")
                .Emisor = New DTOVisaEmisor(oDrd("Emisor"))
                .Emisor.Nom = oDrd("VisaNom")
                .Baja = SQLHelper.GetFchFromDataReader(oDrd("FchCanceled"))
                .Caduca = SQLHelper.GetStringFromDataReader(oDrd("Caduca"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
