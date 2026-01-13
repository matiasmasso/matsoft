Public Class SearchRequestLoader
    Shared Function Find(oGuid As Guid) As DTOSearchRequest
        Dim retval As DTOSearchRequest = Nothing
        Dim oSearchRequest As New DTOSearchRequest(oGuid)
        If Load(oSearchRequest) Then
            retval = oSearchRequest
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSearchRequest As DTOSearchRequest) As Boolean
        If Not oSearchRequest.IsLoaded Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT SearchRequest.SearchKey, SearchRequest.Fch, SearchRequest.Email ")
            sb.AppendLine(", Email.adr ")
            sb.AppendLine(", SearchResults.Source, SearchResults.Cod ")
            sb.AppendLine("FROM SearchRequest")
            sb.AppendLine("LEFT OUTER JOIN Email ON SearchRequest.Email = Email.Guid ")
            sb.AppendLine("LEFT OUTER JOIN SearchResults ON SearchRequest.Guid = SearchResults.SearchRequest ")
            sb.AppendLine("WHERE SearchRequest.Guid='" & oSearchRequest.Guid.ToString & "'")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oSearchRequest.Guid.ToString())
            Do While oDrd.Read
                If Not oSearchRequest.IsLoaded Then
                    With oSearchRequest
                        .SearchKey = oDrd("SearchKey")
                        .Fch = oDrd("Fch")
                        If Not IsDBNull(oDrd("email")) Then
                            .User = New DTOUser(DirectCast(oDrd("email"), Guid))
                            With .User
                                .EmailAddress = oDrd("Adr").ToString
                            End With
                        End If
                        .Results = New List(Of DTOSearchRequest.Result)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("Source")) Then
                    Dim item As New DTOSearchRequest.Result
                    Select Case item.Cod
                        Case DTOSearchRequest.Result.Cods.Brand
                            item.BaseGuid = New DTOProductBrand(oDrd("Source"))
                        Case DTOSearchRequest.Result.Cods.Category
                            item.BaseGuid = New DTOProductCategory(oDrd("Source"))
                        Case DTOSearchRequest.Result.Cods.Contact
                            item.BaseGuid = New DTOContact(oDrd("Source"))
                        Case DTOSearchRequest.Result.Cods.Location
                            item.BaseGuid = New DTOLocation(oDrd("Source"))
                        Case DTOSearchRequest.Result.Cods.Noticia
                            item.BaseGuid = New DTONoticia(oDrd("Source"))
                    End Select
                    oSearchRequest.Results.Add(item)
                End If

            Loop
            oDrd.Close()

        End If

        Dim retval As Boolean = oSearchRequest.IsLoaded
        Return retval
    End Function

    Shared Function Log(oRequest As DTOSearchRequest) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = Update(oRequest, exs)
        Return retval
    End Function

    Shared Function Update(oSearchRequest As DTOSearchRequest, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oSearchRequest, oTrans)
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

    Protected Shared Sub Update(oSearchRequest As DTOSearchRequest, ByRef oTrans As SqlTransaction)
        If Not oSearchRequest.IsNew Then DeleteDetails(oSearchRequest, oTrans)
        UpdateHeader(oSearchRequest, oTrans)
        UpdateDetails(oSearchRequest, oTrans)
    End Sub

    Protected Shared Sub UpdateHeader(oSearchRequest As DTOSearchRequest, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM SearchRequest WHERE Guid='" & oSearchRequest.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSearchRequest.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSearchRequest
            oRow("SearchKey") = .SearchKey
            oRow("Fch") = .Fch
            If .User IsNot Nothing Then
                oRow("email") = .User.Guid
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateDetails(oSearchRequest As DTOSearchRequest, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM SearchResults WHERE SearchRequest='" & oSearchRequest.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOSearchRequest.Result In oSearchRequest.Results
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("SearchRequest") = oSearchRequest.Guid
            If item.BaseGuid Is Nothing Then
                oRow("Source") = Guid.NewGuid
            Else
                oRow("Source") = item.BaseGuid.Guid
            End If
            oRow("Cod") = item.Cod
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSearchRequest As DTOSearchRequest, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oSearchRequest, oTrans)
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

    Shared Sub Delete(oSearchRequest As DTOSearchRequest, ByRef oTrans As SqlTransaction)
        DeleteDetails(oSearchRequest, oTrans)
        DeleteHeader(oSearchRequest, oTrans)
    End Sub

    Protected Shared Sub DeleteDetails(oSearchRequest As DTOSearchRequest, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SearchResults WHERE SearchRequest=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oSearchRequest.Guid.ToString())
    End Sub

    Protected Shared Sub DeleteHeader(oSearchRequest As DTOSearchRequest, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SearchRequest WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oSearchRequest.Guid.ToString())
    End Sub

End Class


Public Class SearchRequestsLoader

    Shared Function All(Optional oEmp As DTOEmp = Nothing) As List(Of DTOSearchRequest)
        Dim retval As New List(Of DTOSearchRequest)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT SearchRequest.Guid, SearchRequest.SearchKey, SearchRequest.Fch, SearchRequest.Email ")
        sb.AppendLine(", Email.adr ")
        sb.AppendLine(", SearchResults.Source, SearchResults.Cod ")
        sb.AppendLine("FROM SearchRequest")
        sb.AppendLine("LEFT OUTER JOIN Email ON SearchRequest.Email = Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN SearchResults ON SearchRequest.Guid = SearchResults.SearchRequest ")
        sb.AppendLine("ORDER BY SearchRequest.Fch DESC ")
        Dim SQL As String = sb.ToString
        Dim oSearchRequest As New DTOSearchRequest
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oSearchRequest.Guid.ToString())
        Do While oDrd.Read
            If Not oSearchRequest.Guid.Equals(oDrd("Guid")) Then
                oSearchRequest = New DTOSearchRequest(oDrd("Guid"))
                With oSearchRequest
                    .SearchKey = oDrd("SearchKey")
                    .Fch = oDrd("Fch")
                    If Not IsDBNull(oDrd("email")) Then
                        .User = New DTOUser(DirectCast(oDrd("email"), Guid))
                        With .User
                            .emailAddress = oDrd("Adr").ToString
                        End With
                    End If
                    .Results = New List(Of DTOSearchRequest.Result)
                    .IsLoaded = True
                End With
                retval.Add(oSearchRequest)
            End If

            If Not IsDBNull(oDrd("Source")) Then
                Dim item As New DTOSearchRequest.Result
                Select Case item.Cod
                    Case DTOSearchRequest.Result.Cods.Brand
                        item.BaseGuid = New DTOProductBrand(oDrd("Source"))
                    Case DTOSearchRequest.Result.Cods.Category
                        item.BaseGuid = New DTOProductCategory(oDrd("Source"))
                    Case DTOSearchRequest.Result.Cods.Contact
                        item.BaseGuid = New DTOContact(oDrd("Source"))
                    Case DTOSearchRequest.Result.Cods.Location
                        item.BaseGuid = New DTOLocation(oDrd("Source"))
                    Case DTOSearchRequest.Result.Cods.Noticia
                        item.BaseGuid = New DTONoticia(oDrd("Source"))
                End Select
                oSearchRequest.Results.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class






