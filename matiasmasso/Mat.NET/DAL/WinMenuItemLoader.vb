Public Class WinMenuItemLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWinMenuItem
        Dim retval As DTOWinMenuItem = Nothing
        Dim oWinMenuItem As New DTOWinMenuItem(oGuid)
        If Load(oWinMenuItem) Then
            retval = oWinMenuItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWinMenuItem As DTOWinMenuItem) As Boolean
        If Not oWinMenuItem.IsLoaded And Not oWinMenuItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WinMenuItem.Parent, WinMenuItem.Ord, WinMenuItem.Cod, WinMenuItem.ActionProcedure, WinMenuItem.CustomTarget, WinMenuItem.Mime, WinMenuItem.Emps ")
            sb.AppendLine(", WinMenuItem.NomEsp, WinMenuItem.NomCat, WinMenuItem.NomEng, WinMenuItem.NomPor ")
            sb.AppendLine(", WinMenuItemRol.Rol ")
            sb.AppendLine(", Parent.NomEsp AS ParentNomEsp, Parent.NomCat AS ParentNomCat, Parent.NomEng AS ParentNomEng, Parent.NomPor AS ParentNomPor ")
            sb.AppendLine("FROM WinMenuItem ")
            sb.AppendLine("LEFT OUTER JOIN WinMenuItem Parent ON WinMenuItem.Parent = Parent.Guid ")
            sb.AppendLine("LEFT OUTER JOIN WinMenuItemRol ON WinMenuItem.Guid= WinMenuItemRol.MenuItem ")
            sb.AppendLine("WHERE WinMenuItem.Guid='" & oWinMenuItem.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oWinMenuItem
                    If Not oWinMenuItem.IsLoaded Then
                        If Not IsDBNull(oDrd("Parent")) Then
                            .parent = New DTOWinMenuItem(oDrd("Parent"))
                            .parent.langText = SQLHelper.GetLangTextFromDataReader(oDrd, "ParentNomEsp", "ParentNomCat", "ParentNomEng", "ParentNomPor")
                        End If
                        .langText = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                        .ord = oDrd("Ord")
                        .cod = CInt(oDrd("Cod"))
                        .Mime = CInt(oDrd("Mime"))
                        .action = oDrd("ActionProcedure")
                        .customTarget = oDrd("CustomTarget")
                        '.icon = oDrd("Icon")
                        .rols = New List(Of DTORol)

                        Dim sEmps() As String = oDrd("Emps").ToString.Split("-")
                        .emps = New List(Of DTOEmp)
                        For Each s In sEmps
                            If IsNumeric(s) Then
                                .emps.Add(New DTOEmp(CInt(s)))
                            End If
                        Next

                        .IsLoaded = True
                    End If
                    If Not IsDBNull(oDrd("Rol")) Then
                        .rols.Add(New DTORol(oDrd("Rol")))
                    End If
                End With
            Loop
            oDrd.Close()
        End If

        Dim retval As Boolean = oWinMenuItem.IsLoaded
        Return retval
    End Function


    Shared Function Icon(guid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WinMenuItem.Icon, WinMenuItem.Mime ")
        sb.AppendLine("FROM WinMenuItem ")
        sb.AppendLine("WHERE WinMenuItem.Guid='" & guid.ToString() & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Icon")) Then
                retval = MatHelperStd.ImageMime.Factory(oDrd("Icon"), oDrd("Mime"))
            End If
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oWinMenuItem As DTOWinMenuItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWinMenuItem, oTrans)
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


    Shared Sub Update(oWinMenuItem As DTOWinMenuItem, ByRef oTrans As SqlTransaction)
        UpdateHeader(oWinMenuItem, oTrans)
        If oWinMenuItem.rols IsNot Nothing Then
            UpdateRols(oWinMenuItem, oTrans)
        End If
    End Sub

    Shared Sub UpdateHeader(oWinMenuItem As DTOWinMenuItem, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WinMenuItem ")
        sb.AppendLine("WHERE Guid='" & oWinMenuItem.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWinMenuItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWinMenuItem
            oRow("Parent") = SQLHelper.NullableBaseGuid(.parent)
            oRow("NomEsp") = SQLHelper.NullableLangText(.langText, DTOLang.ESP)
            oRow("NomCat") = SQLHelper.NullableLangText(.langText, DTOLang.CAT)
            oRow("NomEng") = SQLHelper.NullableLangText(.langText, DTOLang.ENG)
            oRow("NomPor") = SQLHelper.NullableLangText(.langText, DTOLang.POR)
            oRow("Ord") = .ord
            oRow("Cod") = .cod
            oRow("ActionProcedure") = .action
            oRow("CustomTarget") = .customTarget
            oRow("Icon") = .icon
            oRow("Mime") = .Mime

            Dim sb2 As New Text.StringBuilder
            For Each oEmp In .emps
                sb2.Append("-" & CInt(oEmp.Id))
            Next
            sb2.Append("-")
            oRow("Emps") = sb2.ToString
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateRols(oWinMenuItem As DTOWinMenuItem, ByRef oTrans As SqlTransaction)
        If Not oWinMenuItem.IsNew Then DeleteRols(oWinMenuItem, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WinMenuItemRol ")
        sb.AppendLine("WHERE MenuItem=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oWinMenuItem.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRol As DTORol In oWinMenuItem.rols
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("MenuItem") = oWinMenuItem.Guid
            oRow("Rol") = oRol.id
        Next
        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWinMenuItem As DTOWinMenuItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWinMenuItem, oTrans)
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


    Shared Sub Delete(oWinMenuItem As DTOWinMenuItem, ByRef oTrans As SqlTransaction)
        DeleteRols(oWinMenuItem, oTrans)
        DeleteHeader(oWinMenuItem, oTrans)
    End Sub

    Shared Sub DeleteHeader(oWinMenuItem As DTOWinMenuItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WinMenuItem WHERE Guid='" & oWinMenuItem.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteRols(oWinMenuItem As DTOWinMenuItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WinMenuItemRol WHERE MenuItem='" & oWinMenuItem.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class WinMenuItemsLoader

    Shared Function All(oUser As DTOUser) As List(Of DTOWinMenuItem)
        Dim sb As New System.Text.StringBuilder
        Dim sOrd As String = oUser.Lang.Tradueix("NomEsp", "NomCat", "NomEng", "NomPor")
        sb.AppendLine("SELECT WinMenuItem.* ")
        sb.AppendLine(", Parent.NomEsp AS ParentNomEsp, Parent.NomCat AS ParentNomCat, Parent.NomEng AS ParentNomEng, Parent.NomPor AS ParentNomPor ")
        sb.AppendLine("FROM WinMenuItem ")
        sb.AppendLine("LEFT OUTER JOIN WinMenuItem Parent ON WinMenuItem.Parent = Parent.Guid ")
        If oUser.Rol.id <> DTORol.Ids.superUser Then
            sb.AppendLine("INNER JOIN WinMenuItemRol On WinMenuItem.Guid = WinMenuItemRol.MenuItem And WinMenuItemRol.Rol=" & CInt(oUser.Rol.id) & " ")
        End If
        sb.AppendLine("WHERE WinMenuItem.Emps like '%-" & oUser.Emp.Id & "-%' ")
        sb.AppendLine("ORDER BY WinMenuItem.Parent, WinMenuItem.Ord, " & sOrd & " ")

        Dim SQL As String = sb.ToString

        Dim retval As New List(Of DTOWinMenuItem)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWinMenuItem(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("Parent")) Then
                    .parent = New DTOWinMenuItem(oDrd("Parent"))
                    .parent.langText = SQLHelper.GetLangTextFromDataReader(oDrd, "ParentNomEsp", "ParentNomCat", "ParentNomEng", "ParentNomPor")
                End If
                .langText = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                .ord = oDrd("Ord")
                .cod = oDrd("Cod")
                .Mime = oDrd("Mime")
                .action = SQLHelper.GetStringFromDataReader(oDrd("Actionprocedure"))
                '.Icon = SQLHelper.GetImageFromDatareader(oDrd("Icon"))
                .customTarget = oDrd("CustomTarget")

                Dim sEmps() As String = oDrd("Emps").ToString.Split("-")
                .emps = New List(Of DTOEmp)
                For Each s In sEmps
                    If IsNumeric(s) Then
                        .emps.Add(New DTOEmp(CInt(s)))
                    End If
                Next

            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function SpriteImages(oGuids As List(Of Guid)) As List(Of Byte())
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT WinMenuItem.Icon, WinMenuItem.Mime ")
        sb.AppendLine("FROM WinMenuItem ")
        sb.AppendLine("INNER JOIN @Table X ON WinMenuItem.Guid = X.Guid ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of Byte())
        Do While oDrd.Read
            If IsDBNull(oDrd("Icon")) Then
                retval.Add(New Byte() {})
            Else
                Dim bytes As Byte() = oDrd("Icon")
                Dim mime As MimeCods = oDrd("Mime")
                If mime <> MimeCods.Jpg Then
                    Dim srcMs As New System.IO.MemoryStream(bytes)
                    Dim srcImg = Image.FromStream(srcMs)
                    Dim jpgFormat As Imaging.ImageFormat = Imaging.ImageFormat.Jpeg
                    Dim dstMs As New IO.MemoryStream()
                    srcImg.Save(dstMs, jpgFormat)
                    bytes = dstMs.ToArray()
                End If
                retval.Add(bytes)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SpriteImages(oUser As DTOUser) As List(Of Byte()) 'TO DEPRECATE

        Dim sb As New System.Text.StringBuilder
        Dim sOrd As String = oUser.Lang.Tradueix("NomEsp", "NomCat", "NomEng", "NomPor")
        sb.AppendLine("SELECT WinMenuItem.Guid, WinMenuItem.Icon, NomEsp ")
        sb.AppendLine("FROM WinMenuItem ")
        If oUser.Rol.id <> DTORol.Ids.superUser Then
            sb.AppendLine("INNER JOIN WinMenuItemRol On WinMenuItem.Guid = WinMenuItemRol.MenuItem And WinMenuItemRol.Rol=" & CInt(oUser.Rol.id) & " ")
        End If
        sb.AppendLine("WHERE WinMenuItem.Emps like '%-" & oUser.Emp.Id & "-%' ")
        sb.AppendLine("AND WinMenuItem.Cod = " & DTOWinMenuItem.Cods.item & " ")
        sb.AppendLine("ORDER BY WinMenuItem.Parent, WinMenuItem.Ord, " & sOrd & " ")

        Dim SQL As String = sb.ToString

        Dim retval As New List(Of Byte())
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImage = oDrd("Icon")
            retval.Add(oImage)
        Loop
        oDrd.Close()

        Return retval
    End Function



    Shared Function All(Optional oEmp As DTOEmp = Nothing, Optional oRol As DTORol = Nothing, Optional oCod As DTOWinMenuItem.Cods = DTOWinMenuItem.Cods.notSet, Optional oParent As DTOWinMenuItem = Nothing) As List(Of DTOWinMenuItem)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("Select Guid, Parent, NomEsp, NomCat, NomEng, NomPor, Ord, Cod, ActionProcedure, Icon, CustomTarget ")
        sb.AppendLine("FROM WinMenuItem ")
        If oRol IsNot Nothing AndAlso oRol.id <> DTORol.Ids.superUser Then
            sb.AppendLine("INNER JOIN WinMenuItemRol On WinMenuItem.Guid = WinMenuItemRol.MenuItem And WinMenuItemRol.Rol=" & CInt(oRol.id) & " ")
        End If
        sb.AppendLine("WHERE 1=1 ")
        If oEmp IsNot Nothing Then
            sb.AppendLine("AND WinMenuItem.Emps like '%-" & oEmp.Id & "-%' ")
        End If
        If oCod <> DTOWinMenuItem.Cods.notSet Then
            sb.AppendLine("And Cod=" & CInt(oCod) & " ")
        End If
        If oParent IsNot Nothing Then
            sb.AppendLine("And WinMenuItem.Parent='" & oParent.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY WinMenuItem.Parent, WinMenuItem.Ord, WinMenuItem.NomEsp")

        Dim SQL As String = sb.ToString

        Dim retval As New List(Of DTOWinMenuItem)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWinMenuItem(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("Parent")) Then
                    .parent = New DTOWinMenuItem(oDrd("Parent"))
                End If
                .langText = SQLHelper.GetLangTextFromDataReader(oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                .ord = oDrd("Ord")
                .cod = oDrd("Cod")
                .action = SQLHelper.GetStringFromDataReader(oDrd("Actionprocedure"))
                .icon = oDrd("Icon")
                .customTarget = oDrd("CustomTarget")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function SaveOrder(items As List(Of DTOWinMenuItem), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oFirstItem = items.First
        If items.Any(Function(x) x.parent.UnEquals(oFirstItem.parent)) Then
            exs.Add(New Exception("barrejats de diferents carpetes"))
        Else

            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try

                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT * ")
                sb.AppendLine("FROM WinMenuItem ")
                sb.AppendLine("WHERE WinMenuItem.Parent = '" & oFirstItem.parent.Guid.ToString & "' ")
                sb.AppendLine("ORDER BY WinMenuItem.Ord ")
                Dim SQL As String = sb.ToString

                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                For Each oRow In oTb.Rows
                    Dim oGuid As Guid = oRow("Guid")
                    Dim item = items.FirstOrDefault(Function(x) x.Guid.Equals(oGuid))
                    item.ord = items.IndexOf(item)
                    oRow("Ord") = item.ord
                Next
                oDA.Update(oDs)

                oTrans.Commit()
                retval = True
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try

        End If
        Return retval

    End Function

End Class
