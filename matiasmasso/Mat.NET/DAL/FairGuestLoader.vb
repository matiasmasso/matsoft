Public Class FairGuestLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOFairGuest
        Dim retval As DTOFairGuest = Nothing
        Dim oFairGuest As New DTOFairGuest(oGuid)
        If Load(oFairGuest) Then
            retval = oFairGuest
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oFairGuest As DTOFairGuest) As Boolean
        If Not oFairGuest.IsLoaded And Not oFairGuest.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT FairGuest.*, Email.Adr AS UserEmailAddress ")
            sb.AppendLine(", Country.ISO, Country.Nom_ESP AS CountryEsp, Country.Nom_CAT AS CountryCat, Country.Nom_ENG as CountryEng ")
            sb.AppendLine(", Noticia.TitleEsp, Noticia.TitleCat, Noticia.TitleEng, Noticia.TitlePor ")
            sb.AppendLine("FROM FairGuest ")
            sb.AppendLine("INNER JOIN Noticia ON FairGuest.Event=Noticia.Guid ")
            sb.AppendLine("INNER JOIN Country ON FairGuest.Country=Country.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email ON FairGuest.EmailCreated=Email.Guid ")
            sb.AppendLine("WHERE FairGuest.Guid='" & oFairGuest.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oFairGuest.Guid.ToString)
            If oDrd.Read Then
                With oFairGuest
                    .Evento = New DTOEvento(oDrd("Event"))
                    .Evento.Title = SQLHelper.GetLangTextFromDataReader(oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    .FirstName = oDrd("FirstName")
                    .LastName = oDrd("LastName")
                    .Position = oDrd("Position")
                    .NIF = oDrd("NIF")
                    .RaoSocial = oDrd("RaoSocial")
                    .ActivityCode = oDrd("ActivityCode")
                    .Address = oDrd("Address")
                    .Zip = oDrd("Zip")
                    .Location = oDrd("Location")
                    .Country = New DTOCountry(oDrd("Country"))
                    With .Country
                        .ISO = oDrd("ISO")
                        .Nom.Esp = oDrd("CountryEsp")
                        .Nom.Cat = oDrd("CountryCat")
                        .Nom.Eng = oDrd("CountryEng")
                    End With
                    .Phone = oDrd("Phone")
                    .CellPhone = oDrd("CellPhone")
                    .Fax = oDrd("Fax")
                    .Email = oDrd("Email")
                    .web = oDrd("web")
                    .CodeDistance = oDrd("Distance")
                    .FchCreated = oDrd("FchCreated")
                    If Not IsDBNull(oDrd("EmailCreated")) Then
                        .UserCreated = New DTOUser(CType(oDrd("EmailCreated"), Guid))
                        .UserCreated.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UserEmailAddress"))
                    End If

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oFairGuest.IsLoaded
        Return retval
    End Function

    Shared Function Update(oFairGuest As DTOFairGuest, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oFairGuest, oTrans)
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


    Shared Sub Update(oFairGuest As DTOFairGuest, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM FairGuest ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oFairGuest.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oFairGuest.Guid
            oFairGuest.FchCreated = Now
        Else
            oRow = oTb.Rows(0)
        End If

        With oFairGuest
            oRow("Event") = SQLHelper.NullableBaseGuid(.Evento)
            oRow("FirstName") = .FirstName
            oRow("LastName") = .LastName
            oRow("Position") = .Position
            oRow("NIF") = .NIF
            oRow("RaoSocial") = .RaoSocial
            oRow("ActivityCode") = .ActivityCode
            oRow("Address") = .Address
            oRow("Zip") = .Zip
            oRow("Location") = .Location
            oRow("Country") = SQLHelper.NullableBaseGuid(.Country)
            oRow("Phone") = .Phone
            oRow("CellPhone") = .CellPhone
            oRow("Fax") = .Fax
            oRow("Email") = .Email
            oRow("Web") = .web
            oRow("Distance") = .CodeDistance
            oRow("FchCreated") = .FchCreated
            oRow("Yea") = Today.Year
            oRow("EmailCreated") = SQLHelper.NullableBaseGuid(.UserCreated)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oFairGuest As DTOFairGuest, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oFairGuest, oTrans)
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


    Shared Sub Delete(oFairGuest As DTOFairGuest, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE FairGuest WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oFairGuest.Guid.ToString)
    End Sub

#End Region

End Class

Public Class FairGuestsLoader

    Shared Function Years() As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Yea ")
        sb.AppendLine("FROM FairGuest ")
        sb.AppendLine("GROUP BY Yea ")
        sb.AppendLine("ORDER BY Yea")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Yea"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEvento As DTOEvento) As List(Of DTOFairGuest)
        Dim retval As New List(Of DTOFairGuest)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT FairGuest.*, Noticia.Title AS EventoNom, Email.Adr AS UserEmailAddress ")
        sb.AppendLine(", Country.ISO, Country.Nom_ESP AS CountryEsp, Country.Nom_CAT AS CountryCat, Country.Nom_ENG as CountryEng, Country.PrefixeTelefonic ")
        sb.AppendLine("FROM FairGuest ")
        sb.AppendLine("INNER JOIN Noticia ON FairGuest.Event=Noticia.Guid ")
        sb.AppendLine("INNER JOIN Country ON FairGuest.Country=Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON FairGuest.EmailCreated=Email.Guid ")
        sb.AppendLine("WHERE FairGuest.Event = '" & oEvento.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY FairGuest.FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOFairGuest(CType(oDrd("Guid"), Guid))
            With item
                .FirstName = oDrd("FirstName")
                .LastName = oDrd("LastName")
                .Position = oDrd("Position")
                .NIF = oDrd("NIF")
                .RaoSocial = oDrd("RaoSocial")
                .ActivityCode = oDrd("ActivityCode")
                .Address = oDrd("Address")
                .Zip = oDrd("Zip")
                .Location = oDrd("Location")
                .Country = New DTOCountry(oDrd("Country"))
                With .Country
                    .ISO = oDrd("ISO")
                    .Nom.Esp = oDrd("CountryEsp")
                    .Nom.Cat = oDrd("CountryCat")
                    .Nom.Eng = oDrd("CountryEng")
                    .PrefixeTelefonic = oDrd("PrefixeTelefonic")
                End With
                .Position = oDrd("Position")
                .Phone = oDrd("Phone")
                .CellPhone = oDrd("CellPhone")
                .Fax = oDrd("Fax")
                .Email = oDrd("Email")
                .web = oDrd("web")
                .CodeDistance = oDrd("Distance")
                .FchCreated = oDrd("FchCreated")
                If Not IsDBNull(oDrd("EmailCreated")) Then
                    .UserCreated = New DTOUser(CType(oDrd("EmailCreated"), Guid))
                    .UserCreated.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UserEmailAddress"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
