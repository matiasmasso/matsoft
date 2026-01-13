Public Class SpriteLoader

#Region "CRUD"

    Shared Function Find(sHash As String) As DTOSprite
        Dim retval As DTOSprite = Nothing
        Dim oSprite As New DTOSprite(sHash)
        If Load(oSprite) Then
            retval = oSprite
        End If
        Return retval
    End Function


    Shared Function Load(ByRef oSprite As DTOSprite) As Boolean
        Dim retval As Boolean

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Sprite ")
        sb.AppendLine("WHERE Hash='" & oSprite.Hash & "' ")

        Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
            With oSprite
                .Image = SQLHelper.GetImageFromDatareader(oDrd("Image"))
                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                .itemsCount = SQLHelper.GetIntegerFromDataReader(oDrd("ItemsCount"))
            End With
            retval = True
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oSprite As DTOSprite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSprite, oTrans)
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


    Shared Sub Update(oSprite As DTOSprite, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Sprite ")
        sb.AppendLine("WHERE Hash='" & oSprite.Hash & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Hash") = oSprite.Hash
        Else
            oRow = oTb.Rows(0)
        End If

        With oSprite
            oRow("Image") = SQLHelper.NullableImage(.Image)
            oRow("Cod") = SQLHelper.NullableInt(.Cod)
            oRow("ColWidth") = SQLHelper.NullableInt(.ColWidth)
            If .Items IsNot Nothing Then
                oRow("ItemsCount") = .Items.Count
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSprite As DTOSprite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSprite, oTrans)
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


    Shared Sub Delete(oSprite As DTOSprite, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Sprite WHERE Hash='" & oSprite.Hash & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class
