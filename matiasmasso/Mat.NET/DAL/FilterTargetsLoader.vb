Public Class FilterTargetsLoader


    Shared Function Filters(targets As List(Of Guid)) As List(Of DTOFilter)
        Dim retval As New List(Of DTOFilter)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In targets
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine("SELECT VwFilter.*, FilterTarget.Target ")
        sb.AppendLine("FROM VwFilter ")
        sb.AppendLine("INNER JOIN FilterTarget ON VwFilter.FilterItemGuid = FilterTarget.FilterItem ")
        sb.AppendLine("INNER JOIN @Table X ON FilterTarget.Target = X.Guid ")
        sb.AppendLine("ORDER BY VwFilter.FilterOrd, VwFilter.FilterGuid, VwFilter.FilterItemOrd ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oFilter As New DTOFilter
        Dim oFilterItem As New DTOFilter.Item
        Do While oDrd.Read
            If Not oFilterItem.Guid.Equals(oDrd("FilterItemGuid")) Then
                If Not oFilter.Guid.Equals(oDrd("FilterGuid")) Then
                    oFilter = New DTOFilter(oDrd("FilterGuid"))
                    oFilter.langText = SQLHelper.GetLangTextFromDataReader(oDrd, "FilterEsp", "FilterCat", "FilterEng", "FilterPor")
                    retval.Add(oFilter)
                End If
                oFilterItem = New DTOFilter.Item(oDrd("FilterItemGuid"))
                oFilterItem.langText = SQLHelper.GetLangTextFromDataReader(oDrd, "FilterItemEsp", "FilterItemCat", "FilterItemEng", "FilterItemPor")
                oFilter.items.Add(oFilterItem)
            End If
            oFilterItem.targetGuids.Add(oDrd("Target"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oTarget As DTOBaseGuid) As List(Of DTOFilter.Item)
        Dim retval As New List(Of DTOFilter.Item)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT FilterItem.Guid ")
        sb.AppendLine(", VwLangText.Esp ")
        sb.AppendLine(", VwLangText.Cat ")
        sb.AppendLine(", VwLangText.Eng ")
        sb.AppendLine(", VwLangText.Por ")
        sb.AppendLine("FROM FilterItem ")
        sb.AppendLine("INNER JOIN Filter ON FilterItem.Filter = Filter.Guid ")
        sb.AppendLine("INNER JOIN FilterTarget ON FilterItem.Guid = FilterTarget.FilterItem ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON FilterItem.Guid = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.FilterItem & " ")
        sb.AppendLine("WHERE FilterTarget.Target = '" & oTarget.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Filter.Ord, Filter.Guid, FilterItem.Ord ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOFilter.Item(oDrd("Guid"))
            item.langText = SQLHelper.GetLangTextFromDataReader(oDrd)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(exs As List(Of Exception), oTarget As DTOBaseGuid, oItems As List(Of DTOFilter.Item)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTarget, oItems, oTrans)
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


    Shared Function Update(oTarget As DTOBaseGuid, oItems As List(Of DTOFilter.Item), oTrans As SqlTransaction) As Boolean
        Dim SQLDel = "DELETE FilterTarget WHERE FilterTarget.Target = '" & oTarget.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQLDel, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT FilterTarget.* ")
        sb.AppendLine("FROM FilterTarget ")
        sb.AppendLine("WHERE FilterTarget.Target = '" & oTarget.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oItem In oItems
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("FilterItem") = oItem.Guid
            oRow("Target") = oTarget.Guid
        Next

        oDA.Update(oDs)

        Return True
    End Function

    Shared Function Delete(exs As List(Of Exception), oTarget As DTOBaseGuid) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oTarget, oTrans)
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

    Shared Function Delete(oTarget As DTOBaseGuid, oTrans As SqlTransaction) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE FilterTarget ")
        sb.AppendLine("WHERE FilterTarget.Target = '" & oTarget.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return True
    End Function

End Class
