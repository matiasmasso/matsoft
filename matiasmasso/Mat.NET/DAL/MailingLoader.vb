Public Class MailingLoader
    Shared Sub Load(ByRef oMailing As DTOMailing, oEmp As DTOEmp)
        oMailing.Users = New List(Of DTOUser)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid AS UserGuid, Email.adr, Email.Nickname ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN VwAreaNom ON VwAreaNom.ZipCod=email.ZipCod and VwAreaNom.CountryISO=Email.Pais ")
        sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ChildGuid=VwAreaNom.ZipGuid")
        sb.AppendLine("WHERE Email.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Email.Obsoleto=0 AND Email.BadmailGuid IS NULL ")

        If oMailing.Areas.Count > 0 Then
            sb.AppendLine("AND (")
            For i As Integer = 0 To oMailing.Areas.Count - 1
                If i > 0 Then sb.Append("OR ")
                sb.AppendLine("VwAreaParent.ParentGuid = '" & oMailing.Areas(i).Guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If oMailing.Rols.Count > 0 Then
            sb.AppendLine("AND (")
            For i As Integer = 0 To oMailing.Rols.Count - 1
                If i > 0 Then sb.Append("OR ")
                sb.AppendLine("Email.Rol =" & CInt(oMailing.Rols(i).Id) & " ")
            Next
            sb.AppendLine(") ")
        End If


        sb.AppendLine("GROUP by Email.Guid, Email.adr, Email.Nickname ")
        'sb.AppendLine("ORDER BY Email.Adr ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("UserGuid"), Guid))
            With oUser
                .EmailAddress = oDrd("adr")
                .NickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
            End With
            oMailing.Users.Add(oUser)
        Loop
        oDrd.Close()
    End Sub


    Shared Function Log(oGuid As Guid, oUsers As List(Of DTOSubscriptor), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Log(oGuid, oUsers, oTrans)
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

    Shared Sub Log(oGuid As Guid, oUsers As IEnumerable(Of DTOUser), oTrans As SqlTransaction)
        Dim SQL As String = "SELECT Guid, Usuari FROM MailingLog WHERE Guid='" & oGuid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oUsuari As DTOUser In oUsers
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oGuid
            oRow("Usuari") = oUsuari.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub Log(oGuid As Guid, oUsers As List(Of DTOSubscriptor), oTrans As SqlTransaction)
        Dim SQL As String = "SELECT Guid, Usuari FROM MailingLog WHERE Guid='" & oGuid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oUsuari As DTOUser In oUsers
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oGuid
            oRow("Usuari") = oUsuari.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function XarxaDistribuidorsTancada() As List(Of DTOLeadChecked)
        Dim retval As New List(Of DTOLeadChecked)
        Dim oSscNoticias = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Noticias)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, Email.Guid AS EmailGuid, Email.Adr, Email.Lang ")
        sb.AppendLine(", Email_Clis.ContactGuid, CliGral.ContactClass, ContactClass.DistributionChannel ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN CliTpa ON Tpa.Guid=CliTpa.ProductGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliTpa.CliGuid=Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid=Email.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Email_Clis.ContactGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass=ContactClass.Guid ")
        sb.AppendLine("LEFT OUTER JOIN SscEmail ON SscEmail.SscGuid = '" & oSscNoticias.Guid.ToString() & "' AND SscEmail.Email=Email.Guid ")
        sb.AppendLine("WHERE (CliTpa.Cod = 1 Or CliTpa.Cod = 4) ")
        sb.AppendLine("AND TPA.CodDist = 1 ")
        sb.AppendLine("AND CliGral.Obsoleto=0 ")
        sb.AppendLine("AND Email.NoNews=0 ")
        sb.AppendLine("AND Email.Privat=0 ")
        sb.AppendLine("AND Email.Obsoleto=0 ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        'sb.AppendLine("AND Email.NoNews=0 ")
        sb.AppendLine("AND SscEmail.FchCreated IS NULL ")
        sb.AppendLine("AND ContactClass.DistributionChannel IS NOT NULL ")
        sb.AppendLine("AND (Email.Rol=" & CInt(DTORol.Ids.CliFull) & " OR Email.Rol=" & CInt(DTORol.Ids.CliLite) & ") ")
        sb.AppendLine("GROUP BY Tpa.Guid, Email.Guid, Email.Adr, Email.Lang ")
        sb.AppendLine(", Email_Clis.ContactGuid, CliGral.ContactClass, ContactClass.DistributionChannel ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOLeadChecked(oDrd("EmailGuid"))
            With item
                .EmailAddress = oDrd("Adr")
                .Lang = DTOLang.Factory(oDrd("Lang"))
                .Brand = New DTOProductBrand(oDrd("BrandGuid"))
                .Contact = New DTOContact(oDrd("ContactGuid"))
                .Contact.ContactClass = New DTOContactClass(oDrd("ContactClass"))
                .Contact.ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function XarxaDistribuidorsOberta(DtFch As Date) As List(Of DTOLeadChecked)
        Dim retval As New List(Of DTOLeadChecked)
        Dim oSscNoticias = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Noticias)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, Email.Guid AS EmailGuid, Email.Adr, Email.Lang ")
        sb.AppendLine(", Email_Clis.ContactGuid, CliGral.ContactClass, ContactClass.DistributionChannel ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
        sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
        sb.AppendLine("INNER JOIN Pnc ON Art.Guid=Pnc.ArtGuid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid AND Pdc.Fch>='" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid AND CliGral.Obsoleto=0 ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass=ContactClass.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid=Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN SscEmail ON SscEmail.SscGuid = '" & oSscNoticias.Guid.ToString() & "' AND SscEmail.Email=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliTpa ON Stp.Brand=CliTpa.ProductGuid AND CliTpa.CliGuid=CliGral.Guid AND (CliTpa.Cod=2 OR CliTpa.Cod=3) ")
        sb.AppendLine("WHERE CliTpa.CliGuid IS NULL ")
        sb.AppendLine("AND TPA.CodDist = 0 ")
        sb.AppendLine("AND CliGral.Obsoleto=0 ")
        sb.AppendLine("AND Email.NoNews=0 ")
        sb.AppendLine("AND Email.Privat=0 ")
        sb.AppendLine("AND Email.Obsoleto=0 ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        'sb.AppendLine("AND Email.NoNews=0 ")
        sb.AppendLine("AND SscEmail.FchCreated IS NULL ")
        sb.AppendLine("AND (Email.Rol=" & CInt(DTORol.Ids.CliFull) & " OR Email.Rol=" & CInt(DTORol.Ids.CliLite) & ") ")
        sb.AppendLine("AND ContactClass.DistributionChannel IS NOT NULL ")
        sb.AppendLine("GROUP BY Tpa.Guid, Email.Guid, Email.Adr, Email.Lang ")
        sb.AppendLine(", Email_Clis.ContactGuid, CliGral.ContactClass, ContactClass.DistributionChannel ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOLeadChecked(oDrd("EmailGuid"))
            With item
                .EmailAddress = oDrd("Adr")
                .Lang = DTOLang.Factory(oDrd("Lang"))
                .Brand = New DTOProductBrand(oDrd("BrandGuid"))
                .Contact = New DTOContact(oDrd("ContactGuid"))
                .Contact.ContactClass = New DTOContactClass(oDrd("ContactClass"))
                .Contact.ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Reps(oChannels As List(Of DTODistributionChannel), oBrands As List(Of DTOProductBrand)) As List(Of DTOLeadChecked)
        Dim retval As New List(Of DTOLeadChecked)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.EmailGuid, Email.Emp, Email.Adr, Email.Lang ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON RepProducts.Product = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
        sb.AppendLine("WHERE (RepProducts.FchTo IS NULL OR RepProducts.FchTo >GETDATE()) ")

        If oChannels.Count > 0 Then
            sb.AppendLine("AND ( ")
            For Each oChannel As DTODistributionChannel In oChannels
                If oChannel.UnEquals(oChannels.First) Then sb.Append("OR ")
                sb.AppendLine("RepProducts.DistributionChannel = '" & oChannel.Guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        If oBrands.Count > 0 Then
            sb.AppendLine("AND ( ")
            For Each oBrand As DTOProductBrand In oBrands
                If oBrand.UnEquals(oBrands.First) Then sb.Append("OR ")
                sb.AppendLine("Tpa.Guid = '" & oBrand.Guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
        End If

        sb.AppendLine("GROUP BY Email_Clis.EmailGuid, Email.Emp, Email.Adr, Email.Lang ")
        sb.AppendLine("ORDER BY Email.Adr ")
        Dim SQL As String = sb.ToString
        Dim oRep As New DTORep
        Dim item As New DTOLeadChecked()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not item.Guid.Equals(oDrd("EmailGuid")) Then
                item = New DTOLeadChecked(oDrd("EmailGuid"))
                With item
                    .EmailAddress = oDrd("Adr")
                    .Lang = DTOLang.Factory(oDrd("Lang"))
                    .Contacts = New List(Of DTOContact)
                    .Checked = True
                End With
                retval.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
