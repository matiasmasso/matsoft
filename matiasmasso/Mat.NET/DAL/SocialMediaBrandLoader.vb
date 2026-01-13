Public Class SocialMediaWidgetLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOSocialMediaWidget
        Dim retval As DTOSocialMediaWidget = Nothing
        Dim oSocialMediaWidget As New DTOSocialMediaWidget(oGuid)
        If Load(oSocialMediaWidget) Then
            retval = oSocialMediaWidget
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSocialMediaWidget As DTOSocialMediaWidget) As Boolean
        If Not oSocialMediaWidget.IsLoaded And Not oSocialMediaWidget.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SocialMediaWidget.Brand,  ")
            sb.AppendLine(", BrandNom.Esp AS BrandNom ")
            sb.AppendLine(", SocialMediaWidget.Platform, SocialMediaWidget.WidgetId, SocialMediaWidget.Titular ")
            sb.AppendLine("FROM SocialMediaWidget ")
            sb.AppendLine("INNER JOIN Tpa ON SocialMediaWidget.Brand = Tpa.Guid ")
            sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
            sb.AppendLine("WHERE SocialMediaWidget.Guid='" & oSocialMediaWidget.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oSocialMediaWidget
                    If Not IsDBNull(oDrd("Brand")) Then
                        .Brand = New DTOProductBrand(oDrd("Brand"))
                        SQLHelper.LoadLangTextFromDataReader(.Brand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                    End If
                    .Platform = oDrd("Platform")
                    .WidgetId = oDrd("WidgetId")
                    .Titular = SQLHelper.GetStringFromDataReader(oDrd("Titular"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSocialMediaWidget.IsLoaded
        Return retval
    End Function

    Shared Function Update(oSocialMediaWidget As DTOSocialMediaWidget, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSocialMediaWidget, oTrans)
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


    Shared Sub Update(oSocialMediaWidget As DTOSocialMediaWidget, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SocialMediaWidget ")
        sb.AppendLine("WHERE Guid='" & oSocialMediaWidget.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSocialMediaWidget.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSocialMediaWidget
            oRow("Brand") = SQLHelper.NullableBaseGuid(.Brand)
            oRow("Platform") = .Platform
            oRow("WidgetId") = SQLHelper.GetStringFromDataReader(.WidgetId)
            oRow("Titular") = SQLHelper.GetStringFromDataReader(.Titular)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSocialMediaWidget As DTOSocialMediaWidget, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSocialMediaWidget, oTrans)
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


    Shared Sub Delete(oSocialMediaWidget As DTOSocialMediaWidget, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SocialMediaWidget WHERE Guid='" & oSocialMediaWidget.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function Widget(oUser As DTOUser, oPlatform As DTOSocialMediaWidget.Platforms, oProduct As DTOProduct) As DTOSocialMediaWidget
        Dim retval As DTOSocialMediaWidget = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * FROM SocialMediaWidget ")
        sb.AppendLine("WHERE SocialMediaWidget.Platform=" & oPlatform & " ")
        sb.AppendLine("AND (SocialMediaWidget.Brand IS NULL ")
        If oUser Is Nothing Then
            If oProduct IsNot Nothing Then
                sb.AppendLine("     OR SocialMediaWidget.Brand IN ")
                sb.AppendLine("         (SELECT VwProductParent.Parent FROM VwProductParent ")
                sb.AppendLine("         WHERE VwProductParent.Child ='" & oProduct.Guid.ToString & "' ")
                sb.AppendLine("         ) ")
            End If
        Else
            Select Case oUser.Rol.Id
                Case DTORol.Ids.Manufacturer
                    sb.AppendLine("     OR SocialMediaWidget.Brand IN ")
                    sb.AppendLine("         (SELECT Tpa.Guid FROM Tpa ")
                    sb.AppendLine("         INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid ")
                    sb.AppendLine("         WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                    sb.AppendLine("         ) ")
                Case Else
                    If oProduct IsNot Nothing Then
                        sb.AppendLine("     OR SocialMediaWidget.Brand IN ")
                        sb.AppendLine("         (SELECT VwProductParent.Parent FROM VwProductParent ")
                        sb.AppendLine("         WHERE VwProductParent.Child ='" & oProduct.Guid.ToString & "' ")
                        sb.AppendLine("         ) ")
                    End If
            End Select
        End If
        sb.AppendLine("     ) ")
        sb.AppendLine("ORDER BY SocialMediaWidget.Brand ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval = New DTOSocialMediaWidget(oDrd("Guid"))
            With retval
                If Not IsDBNull(oDrd("Brand")) Then
                    .Brand = New DTOProductBrand(oDrd("Brand"))
                End If
                .Platform = oDrd("Platform")
                .WidgetId = SQLHelper.GetStringFromDataReader(oDrd("WidgetId"))
                .Titular = SQLHelper.GetStringFromDataReader(oDrd("Titular"))
            End With
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

Public Class SocialMediaWidgetsLoader

    Shared Function All() As List(Of DTOSocialMediaWidget)
        Dim retval As New List(Of DTOSocialMediaWidget)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SocialMediaWidget.Guid, SocialMediaWidget.Brand ")
        sb.AppendLine(", BrandNom.Esp AS BrandNom ")
        sb.AppendLine(", SocialMediaWidget.Platform, SocialMediaWidget.WidgetId, SocialMediaWidget.Titular ")
        sb.AppendLine("FROM SocialMediaWidget ")
        sb.AppendLine("LEFT OUTER JOIN Tpa ON SocialMediaWidget.Brand = Tpa.Guid ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("ORDER BY BrandNom, Platform")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSocialMediaWidget(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("Brand")) Then
                    .Brand = New DTOProductBrand(oDrd("Brand"))
                    SQLHelper.LoadLangTextFromDataReader(.Brand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                End If
                .Platform = oDrd("Platform")
                .WidgetId = SQLHelper.GetStringFromDataReader(oDrd("WidgetId"))
                .Titular = SQLHelper.GetStringFromDataReader(oDrd("Titular"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
