Public Class WebMenuItemLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWebMenuItem
        Dim retval As DTOWebMenuItem = Nothing
        Dim oWebMenuItem As New DTOWebMenuItem(oGuid)
        If Load(oWebMenuItem) Then
            retval = oWebMenuItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWebMenuItem As DTOWebMenuItem) As Boolean
        If Not oWebMenuItem.IsLoaded And Not oWebMenuItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WebMenuItems.Esp AS ItemEsp, WebMenuItems.Cat AS ItemCat, WebMenuItems.Eng AS ItemEng, WebMenuItems.Por AS ItemPor, WebMenuItems.Url AS ItemUrl, WebMenuItems.Ord as ItemOrd, WebMenuItems.Actiu as ItemActiu ")
            sb.AppendLine(", WebMenuItems.WebMenuGroup, WebMenuItemsxRol.Rol ")
            sb.AppendLine(", WebMenuGroups.Esp AS GroupEsp, WebMenuGroups.Cat AS GroupCat, WebMenuGroups.Eng AS GroupEng, WebMenuGroups.Por AS GroupPor, WebMenuGroups.Ord as GroupOrd ")
            sb.AppendLine("FROM WebMenuItems ")
            sb.AppendLine("INNER JOIN WebmenuGroups ON Webmenuitems.Webmenugroup = WebMenuGroups.Guid ")
            sb.AppendLine("LEFT OUTER JOIN WebMenuItemsxRol ON Webmenuitems.Guid = WebMenuItemsxRol.WebMenuItem ")
            sb.AppendLine("WHERE WebMenuItems.Guid='" & oWebMenuItem.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oWebMenuItem.IsLoaded Then
                    Dim oGroup As New DTOWebMenuGroup(oDrd("WebMenuGroup"))
                    With oGroup
                        .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "GroupEsp", "GroupCat", "GroupEng", "GroupPor")
                        .Ord = oDrd("GroupOrd")
                    End With

                    With oWebMenuItem
                        .Group = oGroup
                        .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ItemEsp", "ItemCat", "ItemEng", "ItemPor")
                        .Ord = oDrd("ItemOrd")
                        .LangUrl = New DTOLangText(oDrd("ItemUrl").ToString)
                        .Actiu = oDrd("ItemActiu")
                        .Rols = New List(Of DTORol)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("Rol")) Then
                    Dim oRol As New DTORol(oDrd("Rol"))
                    oWebMenuItem.Rols.Add(oRol)
                End If
            Loop
            oDrd.Close()
        End If

        Dim retval As Boolean = oWebMenuItem.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWebMenuItem As DTOWebMenuItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWebMenuItem, oTrans)
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

    Shared Sub Update(oWebMenuItem As DTOWebMenuItem, ByRef oTrans As SqlTransaction)
        UpdateHeader(oWebMenuItem, oTrans)
        If oWebMenuItem.Rols IsNot Nothing Then
            DeleteRols(oWebMenuItem, oTrans)
            UpdateRols(oWebMenuItem, oTrans)
        End If
    End Sub

    Shared Sub UpdateHeader(oWebMenuItem As DTOWebMenuItem, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebMenuItems ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oWebMenuItem.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow

        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWebMenuItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWebMenuItem
            oRow("ESP") = SQLHelper.NullableLangText(.LangText, DTOLang.ESP)
            oRow("CAT") = SQLHelper.NullableLangText(.LangText, DTOLang.CAT)
            oRow("ENG") = SQLHelper.NullableLangText(.LangText, DTOLang.ENG)
            oRow("POR") = SQLHelper.NullableLangText(.LangText, DTOLang.POR)
            oRow("Url") = SQLHelper.NullableLangText(.LangUrl, DTOLang.ESP())
            oRow("WebMenuGroup") = .Group.Guid
            If .Ord = Nothing Then .Ord = 99
            oRow("Ord") = .Ord
            oRow("Actiu") = .Actiu
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateRols(oWebMenuItem As DTOWebMenuItem, ByRef oTrans As SqlTransaction)
        DeleteRols(oWebMenuItem, oTrans)

        Dim SQL As String = "SELECT * FROM WebMenuItemsxRol WHERE WebMenuItem='" & oWebMenuItem.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oRol As DTORol In oWebMenuItem.Rols
            Dim oRow As DataRow = oTb.NewRow
            oRow("Rol") = CInt(oRol.Id)
            oRow("WebMenuItem") = oWebMenuItem.Guid
            oTb.Rows.Add(oRow)
        Next
        oDA.Update(oTb)
    End Sub

    Shared Function Delete(oWebMenuItem As DTOWebMenuItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWebMenuItem, oTrans)
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


    Shared Sub Delete(oWebMenuItem As DTOWebMenuItem, ByRef oTrans As SqlTransaction)
        DeleteRols(oWebMenuItem, oTrans)
        DeleteHeader(oWebMenuItem, oTrans)
    End Sub

    Shared Sub DeleteHeader(oWebMenuItem As DTOWebMenuItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebMenuItems WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oWebMenuItem.Guid.ToString())
    End Sub

    Shared Sub DeleteRols(oWebMenuItem As DTOWebMenuItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebMenuItemsxRol WHERE WebMenuItem=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oWebMenuItem.Guid.ToString())
    End Sub

#End Region



    Shared Function Rols(oWebMenuItem As DTOWebMenuItem) As List(Of DTORol)
        Dim retval As New List(Of DTORol)
        Dim SQL As String = "SELECT Rol FROM WebMenuItemsxRol WHERE WebMenuItem=@Guid"
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oWebMenuItem.Guid.ToString())
        Do While oDrd.Read
            Dim oRolId As DTORol.Ids = DirectCast(oDrd("Rol"), DTORol.Ids)
            Dim oRol As New DTORol(oRolId)
            retval.Add(oRol)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class WebMenuItemsLoader

    Shared Function All(oGroup As DTOWebMenuGroup, oUser As DTOUser) As List(Of DTOWebMenuItem)
        Dim retval As New List(Of DTOWebMenuItem)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT I.Guid As IGuid, I.Url, I.Esp As IEsp, I.Cat As ICat, I.Eng As IEng, I.Por AS IPor, I.Ord As IOrd, I.Actiu As IActiu ")
        sb.Append("FROM WebMenuItems I ")
        If oUser IsNot Nothing Then
            sb.Append("INNER JOIN WebMenuItemsxRol R On I.Guid = R.WebMenuItem ")
            sb.Append("WHERE R.Rol = " & CInt(oUser.Rol.Id) & " ")
            sb.Append("AND I.WebMenuGroup = '" & oGroup.Guid.ToString & "' ")
            sb.Append("GROUP BY I.Guid, I.Url, I.Esp, I.Cat, I.Eng, I.Por, I.Ord, I.Actiu ")
        End If
        sb.Append("ORDER BY I.Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTOWebMenuItem(DirectCast(oDrd("IGuid"), Guid))
            With oItem
                .Group = oGroup
                .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "IEsp", "ICat", "IEng", "IPor")
                .LangUrl = New DTOLangText(oDrd("Url").ToString)
                .Ord = oDrd("IOrd")
                .Actiu = oDrd("IActiu")
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
Public Class WebMenuGroupLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid, Optional oRol As DTORol = Nothing) As DTOWebMenuGroup
        Dim retval As DTOWebMenuGroup = Nothing
        Dim oWebMenuGroup As New DTOWebMenuGroup(oGuid)
        If Load(oWebMenuGroup, oRol) Then
            retval = oWebMenuGroup
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWebMenuGroup As DTOWebMenuGroup, Optional oRol As DTORol = Nothing) As Boolean
        If Not oWebMenuGroup.IsLoaded And Not oWebMenuGroup.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WebMenuGroups.Guid AS GroupGuid, WebMenuGroups.Esp as GroupESP, WebMenuGroups.CAT AS GroupCat, WebMenuGroups.Eng as GroupEng, WebMenuGroups.Por as GroupPor ")
            'sb.AppendLine(", WebMenuGroups.Private AS GroupPrivate, WebMenuGroups.Ord as GroupOrd ")
            sb.AppendLine(", WebMenuGroups.Ord as GroupOrd ")
            sb.AppendLine(", WebMenuItems.Guid AS ItemGuid, WebMenuItems.ESP AS ItemESP, WebMenuItems.Cat AS ItemCAT, WebMenuItems.Eng AS ItemENG, WebMenuItems.Por AS ItemPor ")
            sb.AppendLine(", WebMenuItems.Ord AS ItemOrd, WebMenuItems.Url AS ItemUrl, WebMenuItems.Actiu AS ItemActiu ")
            sb.AppendLine("FROM WebMenuGroups ")

            If oRol Is Nothing Then
                sb.AppendLine("LEFT OUTER JOIN WebMenuItems On WebMenuGroups.Guid = WebMenuItems.WebMenuGroup ")
            Else
                sb.AppendLine("INNER JOIN WebMenuItems On WebMenuGroups.Guid = WebMenuItems.WebMenuGroup ")
                sb.AppendLine("INNER JOIN WebMenuItemsxRol On WebMenuItems.Guid = WebMenuItemsxRol.WebMenuItem AND WebMenuItemsxRol.Rol = " & oRol.Id & " ")
            End If
            sb.AppendLine("WHERE WebMenuGroups.Guid='" & oWebMenuGroup.Guid.ToString & "' ")


            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oWebMenuGroup.IsLoaded Then
                    With oWebMenuGroup
                        .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "GroupESP", "GroupCAT", "GroupENG", "GroupPOR")
                        '.Private = oDrd("GroupPrivate").ToString
                        .Ord = CInt(oDrd("GroupOrd"))
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("ItemGuid")) Then
                    Dim item As New DTOWebMenuItem(oDrd("ItemGuid"))
                    oWebMenuGroup.Items.Add(item)
                    With item
                        .Group = oWebMenuGroup
                        .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "ItemEsp", "ItemCat", "ItemEng", "ItemPor")
                        .Ord = oDrd("ItemOrd")
                        .LangUrl = New DTOLangText(oDrd("ItemUrl").ToString)
                        .Actiu = oDrd("ItemActiu")
                        .Rols = New List(Of DTORol)
                        .IsLoaded = True
                    End With
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oWebMenuGroup.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWebMenuGroup As DTOWebMenuGroup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWebMenuGroup, oTrans)
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

    Shared Sub Update(oWebMenuGroup As DTOWebMenuGroup, ByRef oTrans As SqlTransaction)
        UpdateHeader(oWebMenuGroup, oTrans)
        If oWebMenuGroup.Items IsNot Nothing Then
            UpdateItems(oWebMenuGroup, oTrans)
        End If
    End Sub

    Shared Sub UpdateHeader(oWebMenuGroup As DTOWebMenuGroup, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select * ")
        sb.AppendLine("FROM WebMenuGroups ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oWebMenuGroup.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWebMenuGroup.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWebMenuGroup
            oRow("ESP") = SQLHelper.NullableLangText(.LangText, DTOLang.ESP)
            oRow("CAT") = SQLHelper.NullableLangText(.LangText, DTOLang.CAT)
            oRow("ENG") = SQLHelper.NullableLangText(.LangText, DTOLang.ENG)
            oRow("POR") = SQLHelper.NullableLangText(.LangText, DTOLang.POR)
            'oRow("Private") = .Private
            oRow("Ord") = .Ord
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oWebMenuGroup As DTOWebMenuGroup, ByRef oTrans As SqlTransaction)
        DeleteItems(oWebMenuGroup, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select * ")
        sb.AppendLine("FROM WebMenuItems ")
        sb.AppendLine("WHERE WebMenuGroup=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oWebMenuGroup.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim idx As Integer = 0
        For Each item As DTOWebMenuItem In oWebMenuGroup.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With item
                oRow("Guid") = .Guid
                oRow("ESP") = SQLHelper.NullableLangText(.LangText, DTOLang.ESP)
                oRow("CAT") = SQLHelper.NullableLangText(.LangText, DTOLang.CAT)
                oRow("ENG") = SQLHelper.NullableLangText(.LangText, DTOLang.ENG)
                oRow("POR") = SQLHelper.NullableLangText(.LangText, DTOLang.POR)
                oRow("Url") = SQLHelper.NullableLangText(.LangUrl, DTOLang.ESP)
                oRow("WebMenuGroup") = oWebMenuGroup.Guid
                oRow("Ord") = idx
                oRow("Actiu") = .Actiu
                idx += 1
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWebMenuGroup As DTOWebMenuGroup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWebMenuGroup, oTrans)
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

    Shared Sub Delete(oWebMenuGroup As DTOWebMenuGroup, ByRef oTrans As SqlTransaction)
        DeleteItems(oWebMenuGroup, oTrans)
        DeleteHeader(oWebMenuGroup, oTrans)
    End Sub

    Shared Sub DeleteItems(oWebMenuGroup As DTOWebMenuGroup, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebMenuItems WHERE WebMenuGroup=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oWebMenuGroup.Guid.ToString())
    End Sub
    Shared Sub DeleteHeader(oWebMenuGroup As DTOWebMenuGroup, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebMenuGroups WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oWebMenuGroup.Guid.ToString())
    End Sub

#End Region


End Class

Public Class WebMenuGroupsLoader
    Shared Function BoxNodeModels(oUser As DTOUser, oLang As DTOLang) As BoxNodeModel.Collection
        Dim retval = BoxNodeModel.Collection.Factory(oLang)
        Dim sb As New System.Text.StringBuilder
        sb.Append("Select G.Guid As GGuid, G.Esp As GEsp, G.Cat As GCat, G.Eng As GEng, G.Por As GPor, G.Ord As GOrd ") ', G.Private As GPrivate")
        sb.Append(", I.Guid As IGuid, I.Url, I.Esp As IEsp, I.Cat As ICat, I.Eng As IEng, I.Por As IPor, I.Ord As IOrd, I.Actiu As IActiu ")
        sb.Append("FROM WebMenuItems I ")
        sb.Append("INNER JOIN WebMenuGroups G On I.WebMenuGroup=G.Guid ")
        sb.Append("INNER JOIN WebMenuItemsxRol R On I.Guid = R.WebMenuItem ")
        sb.Append("WHERE R.Rol = " & CInt(oUser.Rol.id) & " ")
        sb.Append("AND I.Actiu = 1 ")
        'sb.Append("GROUP BY G.Guid, G.Esp, G.Cat, G.Eng, G.Por, G.Ord, G.Private, I.Guid, I.Url, I.Esp, I.Cat, I.Eng, I.Por, I.Ord, I.Actiu ")
        sb.Append("GROUP BY G.Guid, G.Esp, G.Cat, G.Eng, G.Por, G.Ord, I.Guid, I.Url, I.Esp, I.Cat, I.Eng, I.Por, I.Ord, I.Actiu ")
        sb.Append("ORDER BY G.Ord, I.Ord")
        Dim SQL As String = sb.ToString
        Dim oGroup As New BoxNodeModel
        Dim oGroupGuid As Guid
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("GGuid")
            If Not oGuid.Equals(oGroupGuid) Then
                oGroupGuid = oGuid
                'Dim oCod = DTO.NavViewModel.Cod(oGroupGuid)
                Dim oLangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "GEsp", "GCat", "GEng", "GPor")
                oGroup = BoxNodeModel.Factory(oLangNom.Tradueix(oLang))
                retval.Add(oGroup)
            End If

            Dim oChildNom = SQLHelper.GetLangTextFromDataReader(oDrd, "IEsp", "ICat", "IEng", "IPor")

            oGroup.Children.Add(oChildNom.Tradueix(oLang), oDrd("Url"), oDrd("Guid").ToString())
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(Optional oUser As DTOUser = Nothing, Optional JustActiveItems As Boolean = False) As List(Of DTOWebMenuGroup)
        Dim retval As New List(Of DTOWebMenuGroup)
        Dim sb As New System.Text.StringBuilder
        'sb.Append("Select G.Guid As GGuid, G.Esp As GEsp, G.Cat As GCat, G.Eng As GEng, G.Por As GPor, G.Ord As GOrd, G.Private As GPrivate")
        sb.Append("Select G.Guid As GGuid, G.Esp As GEsp, G.Cat As GCat, G.Eng As GEng, G.Por As GPor, G.Ord As GOrd ")
        sb.Append(", I.Guid As IGuid, I.Url, I.Esp As IEsp, I.Cat As ICat, I.Eng As IEng, I.Por As IPor, I.Ord As IOrd, I.Actiu As IActiu ")
        sb.Append("FROM WebMenuItems I ")
        sb.Append("INNER JOIN WebMenuGroups G On I.WebMenuGroup=G.Guid ")
        sb.Append("INNER JOIN WebMenuItemsxRol R On I.Guid = R.WebMenuItem ")
        If oUser Is Nothing Then
            'sb.Append("WHERE R.Rol = " & CInt(DTORol.Ids.unregistered) & " ")
        Else
            sb.Append("WHERE R.Rol = " & CInt(oUser.Rol.id) & " ")
        End If
        If JustActiveItems Then
            sb.Append("AND I.Actiu = 1 ")
        End If
        'sb.Append("GROUP BY G.Guid, G.Esp, G.Cat, G.Eng, G.Por, G.Ord, G.Private, I.Guid, I.Url, I.Esp, I.Cat, I.Eng, I.Por, I.Ord, I.Actiu ")
        sb.Append("GROUP BY G.Guid, G.Esp, G.Cat, G.Eng, G.Por, G.Ord, I.Guid, I.Url, I.Esp, I.Cat, I.Eng, I.Por, I.Ord, I.Actiu ")
        sb.Append("ORDER BY G.Ord, I.Ord")
        Dim SQL As String = sb.ToString
        Dim oGroup As New DTOWebMenuGroup
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("GGuid")
            If Not oGuid.Equals(oGroup.Guid) Then
                oGroup = New DTOWebMenuGroup(oGuid)
                With oGroup
                    .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "GEsp", "GCat", "GEng", "GPor")
                    .Ord = oDrd("GOrd")
                    '.Private = oDrd("GPrivate")
                    .Items = New List(Of DTOWebMenuItem)
                End With
                retval.Add(oGroup)
            End If
            Dim oItem As New DTOWebMenuItem(DirectCast(oDrd("IGuid"), Guid))
            With oItem
                .Group = oGroup
                .LangText = SQLHelper.GetLangTextFromDataReader(oDrd, "IEsp", "ICat", "IEng", "IPor")
                .LangUrl = New DTOLangText(oDrd("Url").ToString())
                .Ord = oDrd("IOrd")
                .Actiu = oDrd("IActiu")
            End With
            oGroup.Items.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
