Public Class FilterLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOFilter
        Dim retval As DTOFilter = Nothing
        Dim oFilter As New DTOFilter(oGuid)
        If Load(oFilter) Then
            retval = oFilter
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oFilter As DTOFilter) As Boolean
        If Not oFilter.IsLoaded And Not oFilter.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Filter.Guid, Filter.Ord ")
            sb.AppendLine(", FilterLangText.Esp AS FilterEsp ")
            sb.AppendLine(", FilterLangText.Cat AS FilterCat ")
            sb.AppendLine(", FilterLangText.Eng AS FilterEng ")
            sb.AppendLine(", FilterLangText.Por AS FilterPor ")
            sb.AppendLine(", FilterItem.Guid AS itemGuid ")
            sb.AppendLine(", ItemLangText.Esp AS ItemEsp ")
            sb.AppendLine(", ItemLangText.Cat AS ItemCat ")
            sb.AppendLine(", ItemLangText.Eng AS ItemEng ")
            sb.AppendLine(", ItemLangText.Por AS ItemPor ")
            sb.AppendLine("FROM Filter ")
            sb.AppendLine("LEFT OUTER JOIN FilterItem ON Filter.Guid = FilterItem.Filter ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS FilterLangText ON Filter.Guid = FilterLangText.Guid AND FilterLangText.Src = " & DTOLangText.Srcs.Filter & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS ItemLangText ON FilterItem.Guid = ItemLangText.Guid AND ItemLangText.Src = " & DTOLangText.Srcs.FilterItem & " ")
            sb.AppendLine("WHERE Filter.Guid='" & oFilter.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY FilterItem.Ord")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oFilter.IsLoaded Then
                    With oFilter
                        SQLHelper.LoadLangTextFromDataReader(.langText, oDrd, "FilterEsp", "FilterCat", "FilterEng", "FilterPor")
                        .ord = oDrd("Ord")
                        .items = New List(Of DTOFilter.Item)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("ItemGuid")) Then
                    Dim item As New DTOFilter.Item(oDrd("ItemGuid"))
                    SQLHelper.LoadLangTextFromDataReader(item.langText, oDrd, "ItemEsp", "ItemCat", "ItemEng", "ItemPor")
                    oFilter.items.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oFilter.IsLoaded
        Return retval
    End Function

    Shared Function Update(oFilter As DTOFilter, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oFilter, oTrans)
            oTrans.Commit()
            oFilter.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)
        UpdateHeader(oFilter, oTrans)
        LangTextLoader.Update(oFilter.langText, oTrans)
        UpdateItems(oFilter, oTrans)
    End Sub

    Shared Sub UpdateHeader(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Filter ")
        sb.AppendLine("WHERE Guid='" & oFilter.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oFilter.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oFilter
            oRow("Ord") = .ord
        End With

        oDA.Update(oDs)
    End Sub


    Shared Sub UpdateItems(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM FilterItem ")
        sb.AppendLine("WHERE Filter='" & oFilter.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim idx As Integer = 0

        Dim oMatchingItems As New List(Of DTOFilter.Item)
        For i As Integer = oTb.Rows.Count - 1 To 0 Step -1
            Dim oRow = oTb.Rows(i)
            Dim oMatchingItem = oFilter.items.FirstOrDefault(Function(x) x.Guid.Equals(oRow("Guid")))
            If oMatchingItem Is Nothing Then
                'Remove
                LangTextLoader.Delete(oMatchingItem.langText, oTrans)
                oTb.Rows.RemoveAt(i)
            Else
                'update
                oMatchingItems.Add(oMatchingItem)
                oRow("Ord") = i
                LangTextLoader.Update(oMatchingItem.langText, oTrans)
            End If
        Next

        For Each oItem In oFilter.items.Except(oMatchingItems).ToList()
            'insert
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oItem.Guid
            oRow("Filter") = oFilter.Guid
            oRow("Ord") = oFilter.items.IndexOf(oItem)
            LangTextLoader.Update(oItem.langText, oTrans)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oFilter As DTOFilter, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oFilter, oTrans)
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


    Shared Sub Delete(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)
        DeleteLangTexts(oFilter, oTrans)
        DeleteItems(oFilter, oTrans)
        DeleteHeader(oFilter, oTrans)
    End Sub

    Shared Sub DeleteHeader(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Filter WHERE Guid='" & oFilter.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE FilterItem WHERE Filter='" & oFilter.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteLangTexts(oFilter As DTOFilter, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE LangText ")
        sb.AppendLine("FROM LangText ")
        sb.AppendLine("INNER JOIN FilterItem ON LangText.Guid = FilterItem.Guid ")
        sb.AppendLine("WHERE FilterItem.Filter = '" & oFilter.Guid.ToString() & "'")
        Dim SQL = sb.ToString()
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        LangTextLoader.Delete(oFilter.langText, oTrans)
    End Sub

#End Region

End Class

Public Class FiltersLoader

    Shared Function All() As List(Of DTOFilter)
        Dim retval As New List(Of DTOFilter)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Filter.Guid, Filter.Ord ")
        sb.AppendLine(", FilterLangText.Esp AS FilterEsp ")
        sb.AppendLine(", FilterLangText.Cat AS FilterCat ")
        sb.AppendLine(", FilterLangText.Eng AS FilterEng ")
        sb.AppendLine(", FilterLangText.Por AS FilterPor ")
        sb.AppendLine(", FilterItem.Guid AS itemGuid ")
        sb.AppendLine(", ItemLangText.Esp AS ItemEsp ")
        sb.AppendLine(", ItemLangText.Cat AS ItemCat ")
        sb.AppendLine(", ItemLangText.Eng AS ItemEng ")
        sb.AppendLine(", ItemLangText.Por AS ItemPor ")
        sb.AppendLine("FROM Filter ")
        sb.AppendLine("LEFT OUTER JOIN FilterItem ON Filter.Guid = FilterItem.Filter ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS FilterLangText ON Filter.Guid = FilterLangText.Guid AND FilterLangText.Src = " & DTOLangText.Srcs.Filter & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS ItemLangText ON FilterItem.Guid = ItemLangText.Guid AND ItemLangText.Src = " & DTOLangText.Srcs.FilterItem & " ")
        sb.AppendLine("ORDER BY Filter.Ord, Filter.Guid, FilterItem.Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oFilter As New DTOFilter
        Do While oDrd.Read
            If Not oFilter.Guid.Equals(oDrd("Guid")) Then
                oFilter = New DTOFilter(oDrd("Guid"))
                SQLHelper.LoadLangTextFromDataReader(oFilter.langText, oDrd, "FilterEsp", "FilterCat", "FilterEng", "FilterPor")
                oFilter.ord = retval.Count
                retval.Add(oFilter)
            End If
            If Not IsDBNull(oDrd("itemGuid")) Then
                Dim item As New DTOFilter.Item(oDrd("itemGuid"))
                SQLHelper.LoadLangTextFromDataReader(item.langText, oDrd, "ItemEsp", "ItemCat", "ItemEng", "ItemPor")
                oFilter.items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oBrand As DTOProductBrand) As List(Of DTOFilter)
        Dim retval As New List(Of DTOFilter)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwFilter.* ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("INNER JOIN Art ON Stp.Guid = Art.Category ")
        sb.AppendLine("INNER JOIN FilterTarget ON (Stp.Guid = FilterTarget.Target OR Art.Guid = FilterTarget.Target) ")
        sb.AppendLine("INNER JOIN VwFilter ON FilterTarget.FilterItem = VwFilter.FilterItemGuid ")
        sb.AppendLine("WHERE Stp.Brand = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY VwFilter.FilterGuid, VwFilter.FilterOrd ")
        sb.AppendLine(", VwFilter.FilterGuid, VwFilter.FilterOrd, VwFilter.FilterEsp, VwFilter.FilterCat, VwFilter.FilterEng, VwFilter.FilterPor ")
        sb.AppendLine(", VwFilter.FilterItemGuid, VwFilter.FilterItemOrd, VwFilter.FilterItemEsp, VwFilter.FilterItemCat, VwFilter.FilterItemEng, VwFilter.FilterItemPor ")
        sb.AppendLine("ORDER BY VwFilter.FilterOrd, VwFilter.FilterItemOrd ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oFilter As New DTOFilter
        Do While oDrd.Read
            If Not oFilter.Guid.Equals(oDrd("Guid")) Then
                oFilter = New DTOFilter(oDrd("Guid"))
                SQLHelper.LoadLangTextFromDataReader(oFilter.langText, oDrd, "FilterEsp", "FilterCat", "FilterEng", "FilterPor")
                oFilter.ord = retval.Count
                retval.Add(oFilter)
            End If
            If Not IsDBNull(oDrd("itemGuid")) Then
                Dim item As New DTOFilter.Item(oDrd("itemGuid"))
                SQLHelper.LoadLangTextFromDataReader(item.langText, oDrd, "FilterItemEsp", "FilterItemCat", "FilterItemEng", "FilterItemPor")
                oFilter.items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oFilters As List(Of DTOFilter), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            For Each oFilter In oFilters
                FilterLoader.Update(oFilter, oTrans)
            Next
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
End Class
