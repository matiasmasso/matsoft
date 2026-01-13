Imports LegacyHelper

Public Class WebPortadaBrandLoader


#Region "CRUD"

    Shared Function Find(oBrand As DTOProductBrand) As DTOWebPortadaBrand
        Dim retval As DTOWebPortadaBrand = Nothing
        Dim oWebPortadaBrand As New DTOWebPortadaBrand(oBrand)
        If Load(oWebPortadaBrand) Then
            retval = oWebPortadaBrand
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWebPortadaBrand As DTOWebPortadaBrand) As Boolean
        If Not oWebPortadaBrand.isLoaded And Not oWebPortadaBrand.isNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WebPortadaBrand.*, Tpa.Dsc AS BrandNom ")
            sb.AppendLine("FROM WebPortadaBrand ")
            sb.AppendLine("INNER JOIN Tpa ON WebPortadaBrand.Guid = Tpa.Guid ")
            sb.AppendLine("WHERE WebPortadaBrand.Guid='" & oWebPortadaBrand.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oWebPortadaBrand
                    .Ord = oDrd("Ord")
                    .Hide = SQLHelper.GetBooleanFromDatareader(oDrd("Hide"))
                    '.Image = SQLHelper.GetLegacyImageFromDatareader(oDrd("Image"))
                    If .Brand.nom = "" Then .Brand.nom = oDrd("BrandNom")
                    .isLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oWebPortadaBrand.isLoaded
        Return retval
    End Function

    Shared Function Image(ByRef oWebPortadaBrand As DTOWebPortadaBrand) As Image
        Dim retval As Image = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WebPortadaBrand.Image ")
        sb.AppendLine("FROM WebPortadaBrand ")
        sb.AppendLine("WHERE WebPortadaBrand.Guid='" & oWebPortadaBrand.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetImageFromDatareader(oDrd("Image"))
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oWebPortadaBrand As DTOWebPortadaBrand, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWebPortadaBrand, oTrans)
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


    Shared Sub Update(oWebPortadaBrand As DTOWebPortadaBrand, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebPortadaBrand ")
        sb.AppendLine("WHERE Guid='" & oWebPortadaBrand.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWebPortadaBrand.Brand.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWebPortadaBrand
            oRow("Hide") = .Hide
            oRow("Ord") = .Ord
            oRow("Image") = SQLHelper.NullableImage(.Image)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWebPortadaBrand As DTOWebPortadaBrand, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWebPortadaBrand, oTrans)
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


    Shared Sub Delete(oWebPortadaBrand As DTOWebPortadaBrand, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebPortadaBrand WHERE Guid='" & oWebPortadaBrand.Brand.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class WebPortadaBrandsLoader

    Shared Function All(Optional oChannel As DTODistributionChannel = Nothing) As List(Of DTOWebPortadaBrand)
        Dim retval As New List(Of DTOWebPortadaBrand)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WebPortadaBrand.Guid, WebPortadaBrand.Ord ")
        sb.AppendLine(", Tpa.Dsc AS Nom ")
        sb.AppendLine("FROM WebPortadaBrand ")
        sb.AppendLine("INNER JOIN Tpa ON WebPortadaBrand.Guid=Tpa.Guid ")

        If oChannel IsNot Nothing Then
            sb.AppendLine("WHERE (WebPortadaBrand.Hide IS NULL OR WebPortadaBrand.Hide=0) AND Tpa.Guid IN ( ")
            sb.AppendLine("SELECT Product FROM ProductChannel WHERE DistributionChannel='" & oChannel.Guid.ToString & "' AND Cod=0 ")
            sb.AppendLine(") ")
        End If
        sb.AppendLine("ORDER BY WebPortadaBrand.Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oBrand As New DTOProductBrand(oGuid)
            oBrand.nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
            Dim item As New DTOWebPortadaBrand(oBrand)
            With item
                .Ord = oDrd("Ord")
                '.Image = SQLHelper.GetImageFromDatareader(oDrd("Image"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Sprite(Optional oChannel As DTODistributionChannel = Nothing) As List(Of Image)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WebPortadaBrand.Image ")
        sb.AppendLine("FROM WebPortadaBrand ")
        sb.AppendLine("INNER JOIN Tpa ON WebPortadaBrand.Guid=Tpa.Guid ")

        If oChannel IsNot Nothing Then
            sb.AppendLine("WHERE (WebPortadaBrand.Hide IS NULL OR WebPortadaBrand.Hide=0) AND Tpa.Guid IN ( ")
            sb.AppendLine("SELECT Product FROM ProductChannel WHERE DistributionChannel='" & oChannel.Guid.ToString & "' AND Cod=0 ")
            sb.AppendLine(") ")
        End If
        sb.AppendLine("ORDER BY WebPortadaBrand.Ord")
        Dim retval As New List(Of Image)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImage = SQLHelper.GetImageFromDatareader(oDrd("Image"))
            retval.Add(oImage)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Sort(values As List(Of DTOWebPortadaBrand), exs As List(Of Exception)) As Boolean

        Dim idx As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE WebPortadaBrand ")
        sb.AppendLine("SET Ord=(CASE ")
        For Each value As DTOWebPortadaBrand In values
            sb.AppendLine("WHEN Guid='" & value.Guid.ToString & "' THEN " & idx & " ")
            idx += 1
        Next
        sb.AppendLine("END) ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function
End Class

