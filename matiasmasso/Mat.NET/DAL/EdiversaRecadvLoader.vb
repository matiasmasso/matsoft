Public Class EdiversaRecadvLoader

    Shared Function Find(oGuid As Guid) As DTOEdiversaRecadv
        Dim retval As DTOEdiversaRecadv = Nothing
        Dim oEdiversaRecadv As New DTOEdiversaRecadv(oGuid)
        If Load(oEdiversaRecadv) Then
            retval = oEdiversaRecadv
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaRecadv As DTOEdiversaRecadv) As Boolean
        If Not oEdiversaRecadv.IsLoaded And Not oEdiversaRecadv.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT EdiversaRecadvHdr.Bgm, EdiversaRecadvHdr.Dtm, EdiversaRecadvHdr.NadBy ")
            sb.AppendLine(", EdiversaRecadvHdr.RffOn, EdiversaRecadvHdr.Rf_Dq ")
            sb.AppendLine(", EdiversaRecadvItem.Lin, EdiversaRecadvItem.Ean, EdiversaRecadvItem.PiaLin, EdiversaRecadvItem.QtyLin ")
            sb.AppendLine(", CliGral.Guid AS CustomerGuid, CliGral.FullNom AS CustomerNom, VwSkuNom.SkuGuid, VwSkuNom.SkuNomLlargEsp ")
            sb.AppendLine("FROM EdiversaRecadvHdr ")
            sb.AppendLine("LEFT OUTER JOIN EdiversaRecadvItem ON EdiversaRecadvHdr.Guid = EdiversaRecadvItem.Parent ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON EdiversaRecadvHdr.Nadby = CliGral.Gln ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON EdiversaRecadvItem.Ean = VwSkuNom.Ean ")
            sb.AppendLine("WHERE EdiversaRecadvHdr.Guid='" & oEdiversaRecadv.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY EdiversaRecadvItem.Lin ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oEdiversaRecadv
                    If Not .IsLoaded Then
                        .Bgm = SQLHelper.GetStringFromDataReader(oDrd("Bgm"))
                        .Dtm = SQLHelper.GetFchFromDataReader(oDrd("Dtm"))
                        .NadBy = SQLHelper.GetStringFromDataReader(oDrd("NadBy"))
                        .RffOn = SQLHelper.GetStringFromDataReader(oDrd("RffOn"))
                        .RffDq = SQLHelper.GetStringFromDataReader(oDrd("RffDq"))
                        If Not IsDBNull(oDrd("CustomerGuid")) Then
                            .Customer = New DTOGuidNom(oDrd("CustomerGuid"), oDrd("CustomerNom"))
                        End If
                        .IsLoaded = True
                    End If
                    Dim item As New DTOEdiversaRecadv.Item()
                    With item
                        .Lin = SQLHelper.GetIntegerFromDataReader(oDrd("Lin"))
                        .Ean = SQLHelper.GetStringFromDataReader(oDrd("Ean"))
                        .PiaLin = SQLHelper.GetStringFromDataReader(oDrd("PiaLin"))
                        .QtyLin = SQLHelper.GetIntegerFromDataReader(oDrd("QtyLin"))
                        If Not IsDBNull(oDrd("SkuGuid")) Then
                            .Sku = New DTOGuidNom(oDrd("SkuGuid"), oDrd("SkuNomLlargEsp"))
                        End If
                    End With
                    .Items.Add(item)
                End With
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oEdiversaRecadv.IsLoaded
        Return retval
    End Function

    Shared Function Update(oEdiversaRecadv As DTOEdiversaRecadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEdiversaRecadv, oTrans)
            oTrans.Commit()
            oEdiversaRecadv.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oEdiversaRecadv As DTOEdiversaRecadv, ByRef oTrans As SqlTransaction)
        UpdateHdr(oEdiversaRecadv, oTrans)
        DeleteItms(oEdiversaRecadv, oTrans)
        UpdateItms(oEdiversaRecadv, oTrans)
    End Sub
    Shared Sub UpdateHdr(oEdiversaRecadv As DTOEdiversaRecadv, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaRecadvHdr ")
        sb.AppendLine("WHERE Guid='" & oEdiversaRecadv.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oEdiversaRecadv.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oEdiversaRecadv
            oRow("Bgm") = SQLHelper.NullableString(.Bgm)
            oRow("Dtm") = SQLHelper.NullableFch(.Dtm)
            oRow("NadBy") = SQLHelper.NullableString(.NadBy)
            oRow("RffOn") = SQLHelper.NullableString(.RffOn)
            oRow("RffDq") = SQLHelper.NullableString(.RffDq)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItms(oEdiversaRecadv As DTOEdiversaRecadv, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM EdiversaRecadvItm ")
        sb.AppendLine("WHERE Parent='" & oEdiversaRecadv.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In oEdiversaRecadv.Items
            Dim oRow = oTb.NewRow()
            oTb.Rows.Add(oRow)
            oRow("Parent") = oEdiversaRecadv.Guid
            oRow("Lin") = item.Lin
            oRow("Ean") = item.Ean
            oRow("PiaLin") = item.PiaLin
            oRow("QtyLin") = item.QtyLin
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oEdiversaRecadv As DTOEdiversaRecadv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEdiversaRecadv, oTrans)
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


    Shared Sub Delete(oEdiversaRecadv As DTOEdiversaRecadv, ByRef oTrans As SqlTransaction)
        DeleteItms(oEdiversaRecadv, oTrans)
        DeleteHdr(oEdiversaRecadv, oTrans)
    End Sub

    Shared Sub DeleteHdr(oEdiversaRecadv As DTOEdiversaRecadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaRecadvHdr WHERE Guid='" & oEdiversaRecadv.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
    Shared Sub DeleteItms(oEdiversaRecadv As DTOEdiversaRecadv, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE EdiversaRecadvItm WHERE Parent='" & oEdiversaRecadv.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class EdiversaRecadvsLoader

    Shared Function All() As List(Of DTOEdiversaRecadv)
        Dim retval As New List(Of DTOEdiversaRecadv)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT EdiversaRecadvHdr.Guid, EdiversaRecadvHdr.Bgm, EdiversaRecadvHdr.Dtm ")
        sb.AppendLine(", EdiversaRecadvHdr.RffOn, EdiversaRecadvHdr.RffDq ")
        sb.AppendLine(", CliGral.Guid AS CustomerGuid, CliGral.FullNom AS CustomerNom ")
        sb.AppendLine("FROM EdiversaRecadvHdr ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON EdiversaRecadvHdr.Nadby = CliGral.Gln ")
        sb.AppendLine("ORDER BY EdiversaRecadvHdr.Dtm DESC, EdiversaRecadvHdr.Bgm ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEdiversaRecadv As New DTOEdiversaRecadv(oDrd("Guid"))
            With oEdiversaRecadv
                .Bgm = SQLHelper.GetStringFromDataReader(oDrd("Bgm"))
                .Dtm = SQLHelper.GetFchFromDataReader(oDrd("Dtm"))
                .NadBy = SQLHelper.GetStringFromDataReader(oDrd("NadBy"))
                .RffOn = SQLHelper.GetStringFromDataReader(oDrd("RffOn"))
                .RffDq = SQLHelper.GetStringFromDataReader(oDrd("RffDq"))
                If Not IsDBNull(oDrd("CustomerGuid")) Then
                    .Customer = New DTOGuidNom(oDrd("CustomerGuid"), oDrd("CustomerNom"))
                End If
            End With
            retval.Add(oEdiversaRecadv)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
