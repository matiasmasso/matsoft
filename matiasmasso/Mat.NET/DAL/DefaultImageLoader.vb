Public Class DefaultImageLoader

    Shared Function Find(oId As DTO.Defaults.ImgTypes) As DTODefaultImage
        Dim retval As DTODefaultImage = Nothing
        Dim SQL As String = "SELECT Image FROM DefaultImage WHERE Id=@Id"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Id", CInt(oId).ToString())
        If oDrd.Read Then
            retval = DTODefaultImage.Factory(oId, oDrd("Image"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Image(oId As DTO.Defaults.ImgTypes) As Byte()
        Dim retval As Byte() = Nothing
        Dim SQL As String = "SELECT Image FROM DefaultImage WHERE Id=" & oId
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Image")
        End If
        oDrd.Close()
        Return retval
    End Function



    Shared Function Update(value As DTODefaultImage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(value, oTrans)
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

    Shared Sub Update(value As DTODefaultImage, oTrans As SqlTransaction)

        Dim SQL As String = "SELECT Id,Image FROM DefaultImage WHERE Id=" & value.Id

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = value.Id
        Else
            oRow = oTb.Rows(0)
        End If

        oRow("Image") = value.Image

        oDA.Update(oDs)

    End Sub


    Shared Function Delete(value As DTODefaultImage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(value, oTrans)
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


    Shared Sub Delete(value As DTODefaultImage, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE DefaultImage WHERE Id=" & value.Id
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


End Class


