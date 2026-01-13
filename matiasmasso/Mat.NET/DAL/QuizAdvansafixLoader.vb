Public Class QuizAdvansafixLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOQuizAdvansafix
        Dim retval As DTOQuizAdvansafix = Nothing
        Dim oQuizAdvansafix As New DTOQuizAdvansafix(oGuid)
        If Load(oQuizAdvansafix) Then
            retval = oQuizAdvansafix
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oQuizAdvansafix As DTOQuizAdvansafix) As Boolean
        If Not oQuizAdvansafix.IsLoaded And Not oQuizAdvansafix.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT QuizAdvansafix.*, Email.adr, Clx.Clx ")
            sb.AppendLine("FROM QuizAdvansafix ")
            sb.AppendLine("LEFT OUTER JOIN Email ON QuizAdvansafix.LastUser=Email.Guid ")
            sb.AppendLine("INNER JOIN Clx ON QuizAdvansafix.Customer=Clx.Guid ")
            sb.AppendLine("WHERE QuizAdvansafix.Guid='" & oQuizAdvansafix.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oQuizAdvansafix
                    .Customer = New DTOCustomer(oDrd("Customer"))
                    .Customer.FullNom = oDrd("Clx")
                    .QtySICT = oDrd("QtySICT")
                    .QtyNoSICT = oDrd("QtyNoSICT")
                    .SICTPurchased = oDrd("SICTPurchased")
                    .NoSICTPurchased = oDrd("NoSICTPurchased")
                    If Not IsDBNull(oDrd("LastUser")) Then
                        .LastUser = New DTOUser(CType(oDrd("LastUser"), Guid))
                        .LastUser.EmailAddress = oDrd("adr")
                    End If
                    .SplioOpen = oDrd("SplioOpen")
                    .FchBrowse = SQLHelper.GetFchFromDataReader(oDrd("FchBrowse"))
                    .FchConfirmed = SQLHelper.GetFchFromDataReader(oDrd("FchConfirmed"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oQuizAdvansafix.IsLoaded
        Return retval
    End Function

    Shared Function FromCustomer(oCustomer As DTOCustomer) As DTOQuizAdvansafix
        Dim retval As DTOQuizAdvansafix = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT QuizAdvansafix.Guid ")
        sb.AppendLine("FROM QuizAdvansafix ")
        sb.AppendLine("WHERE QuizAdvansafix.Customer='" & oCustomer.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOQuizAdvansafix(oDrd("Guid"))
            oDrd.Close()
        End If

        Return retval
    End Function


    Shared Function Update(oQuizAdvansafix As DTOQuizAdvansafix, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oQuizAdvansafix, oTrans)
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


    Shared Sub Update(oQuizAdvansafix As DTOQuizAdvansafix, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM QuizAdvansafix ")
        sb.AppendLine("WHERE Guid='" & oQuizAdvansafix.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oQuizAdvansafix.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oQuizAdvansafix
            oRow("Customer") = .Customer.Guid
            oRow("QtySICT") = .QtySICT
            oRow("QtyNoSICT") = .QtyNoSICT
            oRow("SICTPurchased") = .SICTPurchased
            oRow("NoSICTPurchased") = .NoSICTPurchased
            oRow("LastUser") = SQLHelper.NullableBaseGuid(.LastUser)
            oRow("SplioOpen") = .SplioOpen
            oRow("FchBrowse") = SQLHelper.NullableFch(.FchBrowse)
            oRow("FchConfirmed") = SQLHelper.NullableFch(.FchConfirmed)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oQuizAdvansafix As DTOQuizAdvansafix, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oQuizAdvansafix, oTrans)
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


    Shared Sub Delete(oQuizAdvansafix As DTOQuizAdvansafix, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE QuizAdvansafix WHERE Guid='" & oQuizAdvansafix.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function Customers(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT EMAIL_CLIS.ContactGuid, CliGral.RaoSocial, CliGral.NomCom, CliGral.Nif ")
        sb.Append(", CliAdr.Adr, CliAdr.Zip, Zip.ZipCod, Zip.Location ")
        sb.Append(", Location.Nom as LocationNom, Location.Zona, Zona.Nom as ZonaNom, Zona.Country ")
        sb.Append(", Country.ISO, Country.Nom_ESP AS CountryNom ")
        sb.Append(", CliClient.Ref, CliClient.CcxGuid ")
        sb.Append("FROM EMAIL_CLIS ")
        sb.Append("INNER JOIN QuizAdvansafix ON EMAIL_CLIS.ContactGuid=QuizAdvansafix.Customer ")
        sb.Append("INNER JOIN CliGral ON EMAIL_CLIS.ContactGuid=CliGral.Guid ")
        sb.Append("INNER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")
        sb.Append("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.Append("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.Append("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.Append("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.Append("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.Append("WHERE EMAIL_CLIS.EmailGuid=@Guid ")
        sb.Append("AND CliGral.Obsoleto=0 ")
        sb.Append("AND (CliGral.Rol = " & DTORol.Ids.CliFull & " OR CliGral.Rol = " & DTORol.Ids.CliLite & ") ")
        sb.Append("ORDER BY CliGral.RaoSocial")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oUser.Guid.ToString)
        Do While oDrd.Read

            Dim oCountry As New DTOCountry(oDrd("Country"))
            With oCountry
                .ISO = oDrd("ISO")
                .Nom.Esp = oDrd("CountryNom")
            End With
            Dim oZona As New DTOZona(oDrd("Zona"))
            With oZona
                .Country = oCountry
                .Nom = oDrd("ZonaNom")
            End With
            Dim oLocation As New DTOLocation(oDrd("Location"))
            With oLocation
                .Zona = oZona
                .Nom = oDrd("LocationNom")
            End With
            Dim oZip As New DTOZip(oDrd("Zip"))
            With oZip
                .Location = oLocation
                .ZipCod = oDrd("ZipCod")
            End With
            Dim oAddress As New DTOAddress
            With oAddress
                .Text = oDrd("Adr")
                .Zip = oZip
            End With

            Dim oCustomer = New DTOCustomer(oDrd("ContactGuid"))
            With oCustomer
                .Nif = oDrd("Nif")
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")

                .Address = oAddress
                If Not IsDBNull(oDrd("CcxGuid")) Then
                    .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                End If
            End With

            retval.Add(oCustomer)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

Public Class QuizAdvansafixsLoader

    Shared Function All() As List(Of DTOQuizAdvansafix)
        Dim retval As New List(Of DTOQuizAdvansafix)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT QuizAdvansafix.*, Email.adr, Clx.Clx ")
        sb.AppendLine("FROM QuizAdvansafix ")
        sb.AppendLine("LEFT OUTER JOIN Email ON QuizAdvansafix.LastUser=Email.Guid ")
        sb.AppendLine("INNER JOIN Clx ON QuizAdvansafix.Customer=Clx.Guid ")
        sb.AppendLine("ORDER BY Clx.Clx")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOQuizAdvansafix(oDrd("Guid"))
            With item
                .Customer = New DTOCustomer(oDrd("Customer"))
                .Customer.FullNom = oDrd("Clx")
                .QtySICT = oDrd("QtySICT")
                .QtyNoSICT = oDrd("QtyNoSICT")
                .SICTPurchased = oDrd("SICTPurchased")
                .NoSICTPurchased = oDrd("NoSICTPurchased")
                If Not IsDBNull(oDrd("LastUser")) Then
                    .LastUser = New DTOUser(CType(oDrd("LastUser"), Guid))
                    .LastUser.EmailAddress = oDrd("adr")
                End If
                .SplioOpen = oDrd("SplioOpen")
                .FchBrowse = SQLHelper.GetFchFromDataReader(oDrd("FchBrowse"))
                .FchConfirmed = SQLHelper.GetFchFromDataReader(oDrd("FchConfirmed"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Reset(exs As List(Of Exception)) As Boolean
        SQLHelper.ExecuteNonQuery("DELETE QuizAdvansafix", exs)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("INSERT INTO QuizAdvansafix(Customer,NoSICTPurchased,SICTPurchased) ")
        sb.AppendLine("SELECT Alb.CliGuid ")
        sb.AppendLine(", SUM(CASE WHEN ((Art.Art BETWEEN 21389 And 21394) Or (Art.Art = 21402)) Then Arc.Qty Else 0 End)  ")
        sb.AppendLine(", SUM(CASE WHEN Art.Art BETWEEN 21395 AND 21401 THEN Arc.Qty ELSE 0 END)  ")
        sb.AppendLine("FROM Art  ")
        sb.AppendLine("INNER JOIN Arc ON Art.Guid=Arc.ArtGuid AND Arc.Cod>=50 ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid=Alb.Guid ")
        sb.AppendLine("WHERE Art.Art BETWEEN 21389 AND 21402 ")
        sb.AppendLine("GROUP BY Alb.CliGuid")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function Recipients(Optional OnlyPending As Boolean = False) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.EmailGuid, Email.adr ")
        sb.AppendLine("FROM QuizAdvansafix ")
        sb.AppendLine("INNER JOIN Email_Clis ON QuizAdvansafix.Customer = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("WHERE Email.Obsoleto = 0 ")
        sb.AppendLine("AND Email.Privat=0 ")
        sb.AppendLine("AND Email.Badmail=0 ")
        sb.AppendLine("AND Email.NoNews=0 ")
        If OnlyPending Then
            sb.AppendLine("AND QuizAdvansafix.FchConfirmed IS NULL ")
        End If
        sb.AppendLine("ORDER BY Email.adr ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(CType(oDrd("EmailGuid"), Guid))
            With oUser
                .EmailAddress = oDrd("Adr")
            End With
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function PendingQuizList(oUser As DTOUser) As List(Of DTOQuizAdvansafix)
        Dim retval As New List(Of DTOQuizAdvansafix)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT QuizAdvansafix.Guid, QuizAdvansafix.Customer, Area2.ZonaGuid, Area2.ZonaNom, Clx.Clx, NoSICTPurchased, SICTPurchased ")
        sb.AppendLine("FROM QuizAdvansafix ")
        sb.AppendLine("INNER JOIN CliAdr ON QuizAdvansafix.Customer=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("LEFT OUTER JOIN Area2 ON CliAdr.Zip = Area2.Guid ")
        sb.AppendLine("INNER JOIN Clx ON QuizAdvansafix.Customer= Clx.Guid ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("INNER JOIN RepProducts ON Email_Clis.ContactGuid = RepProducts.Rep AND RepProducts.Area = Area2.ZonaGuid AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<GETDATE()) AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo<=GETDATE()) ")
        End Select

        sb.AppendLine("WHERE FchConfirmed IS NULL ")
        sb.AppendLine("GROUP BY QuizAdvansafix.Guid, QuizAdvansafix.Customer, Area2.ZonaGuid, Area2.ZonaNom, Clx.Clx, NoSICTPurchased, SICTPurchased ")
        sb.AppendLine("ORDER BY Area2.ZonaNom, Clx.Clx")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOQuizAdvansafix(oDrd("Guid"))
            With item
                .Customer = New DTOCustomer(oDrd("Customer"))
                .Customer.FullNom = oDrd("Clx")
                .SICTPurchased = oDrd("SICTPurchased")
                .NoSICTPurchased = oDrd("NoSICTPurchased")
                If Not IsDBNull(oDrd("ZonaGuid")) Then
                    .Zona = New DTOZona(oDrd("ZonaGuid"))
                    .Zona.Nom = oDrd("ZonaNom")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Credits() As List(Of DTOQuizAdvansafix)
        Dim retval As New List(Of DTOQuizAdvansafix)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("select Customer, Clx.Clx, FchConfirmed, QtySICT,  SUM(X.Qty), (CASE WHEN SUM(X.QTY)<QTYSICT THEN SUM(X.QTY) ELSE QTYSICT END) AS DEFT,X.Eur, X.Dto ")
        sb.AppendLine("From quizadvansafix  ")
        sb.AppendLine("LEFT OUTER JOIN( ")
        sb.AppendLine("SELECT Pdc.CliGuid,  Arc.Qty, Arc.Eur, Arc.Dto ")
        sb.AppendLine("FROM Art  ")
        sb.AppendLine("INNER JOIN Arc ON Art.Guid = Arc.ArtGuid ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("INNER JOIN Pnc ON Arc.PncGuid=Pnc.Guid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("WHERE Art.Category='3597A159-52D2-421E-95AA-967692450390' AND Pdc.Fch<'20151103' and arc.eur<>209.47 ")
        sb.AppendLine(") X ON QuizAdvansafix.Customer=X.CliGuid ")
        sb.AppendLine("LEFT OUTER JOIN Clx ON QuizAdvansafix.Customer=Clx.Guid ")
        sb.AppendLine("WHERE  qtysict>0 ")
        sb.AppendLine("GROUP BY Customer, Clx.Clx, FchConfirmed, QtySICT, X.Eur, X.Dto ")
        sb.AppendLine("ORDER BY CLX,EUR,DTO ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOQuizAdvansafix
            With item
                .Customer = New DTOCustomer(oDrd("Customer"))
                .CreditQty = oDrd("Deft")
                .Eur = Defaults.GetAmt(oDrd("Eur"))
                .Dto = oDrd("Dto")
                .FchConfirmed = SQLHelper.GetFchFromDataReader(oDrd("FchConfirmed"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
