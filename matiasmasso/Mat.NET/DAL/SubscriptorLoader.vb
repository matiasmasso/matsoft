Public Class SubscriptorLoader
    Shared Function Find(oUserGuid As Guid, oSsc As DTOSubscription) As DTOSubscriptor
        Dim retval As DTOSubscriptor = Nothing
        Dim oSubscriptor As New DTOSubscriptor(oUserGuid, oSsc)
        If Load(oSubscriptor) Then
            retval = oSubscriptor
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSubscriptor As DTOSubscriptor) As Boolean
        If Not oSubscriptor.IsLoaded And Not oSubscriptor.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Email.Adr, Email.Lang, Email.Rol ")
            sb.AppendLine(", SscEmail.FchCreated, Ssc.Emp ")
            sb.AppendLine(", LangTextNom.Esp AS NomEsp, LangTextNom.Cat AS NomCat, LangTextNom.Eng AS NomEng, LangTextNom.Por AS NomPor ")
            sb.AppendLine(", LangTextDsc.Esp AS DscEsp, LangTextDsc.Cat AS DscCat, LangTextDsc.Eng AS DscEng, LangTextDsc.Por AS DscPor ")
            sb.AppendLine("FROM SscEmail ")
            sb.AppendLine("INNER JOIN Ssc ON SscEmail.SscGuid = Ssc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextNom ON Ssc.Guid = LangTextNom.Guid AND LangTextNom.Src = " & DTOLangText.Srcs.SubscriptionNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTextDsc ON Ssc.Guid = LangTextDsc.Guid AND LangTextDsc.Src = " & DTOLangText.Srcs.SubscriptionDsc & " ")
            sb.AppendLine("INNER JOIN Email ON SscEmail.Email = Email.Guid ")
            sb.AppendLine("WHERE SscEmail.Email='" & oSubscriptor.Guid.ToString() & "' ")
            sb.AppendLine("AND SscEmail.SscGuid='" & oSubscriptor.Subscription.Guid.ToString() & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oSubscriptor
                    With .Subscription
                        .Emp = New DTOEmp(oDrd("Emp"))
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                        SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DscEsp", "DscCat", "DscEng", "DscPor")
                    End With
                    .emailAddress = oDrd("adr")
                    .lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                    .rol = New DTORol(SQLHelper.GetIntegerFromDataReader(oDrd("Rol")))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSubscriptor.IsLoaded
        Return retval
    End Function
End Class
