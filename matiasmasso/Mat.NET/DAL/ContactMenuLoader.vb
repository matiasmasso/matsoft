Public Class ContactMenuLoader
    Shared Function Find(ByRef oGuid As Guid) As DTOContactMenu
        Dim retval As DTOContactMenu = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwContactMenu.IsObsoleto, VwContactMenu.IsInsolvent, VwContactMenu.IsClient, VwContactMenu.IsProveidor, VwContactMenu.IsRep, VwContactMenu.IsStaff, VwContactMenu.IsTransportista ")
        sb.AppendLine(", VwTelsYEmails.Cod, VwTelsYEmails.Privat ")
        sb.AppendLine(", VwTelsYEmails.NumGuid, VwTelsYEmails.Num, VwTelsYEmails.PrefixeTelefonic, VwTelsYEmails.Obs, VwTelsYEmails.Rol ")
        sb.AppendLine("FROM VwContactMenu ")
        sb.AppendLine("LEFT OUTER JOIN VwTelsYEmails ON VwContactMenu.Guid = VwTelsYEmails.ContactGuid ")
        sb.AppendLine("WHERE Guid='" & oGuid.ToString & "' ")
        sb.AppendLine("ORDER BY VwTelsYEmails.Cod, VwTelsYEmails.Privat, VwTelsYEmails.Ord ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If retval Is Nothing Then
                retval = New DTOContactMenu
                With retval
                    .IsObsoleto = oDrd("IsObsoleto")
                    .IsInsolvent = oDrd("IsInsolvent")
                    .IsClient = oDrd("IsClient")
                    .IsProveidor = oDrd("IsProveidor")
                    .IsRep = oDrd("IsRep")
                    .IsStaff = oDrd("IsStaff")
                    .IsTransportista = oDrd("IsTransportista")
                End With
            End If
            If Not IsDBNull(oDrd("Cod")) Then
                Select Case oDrd("Cod")
                    Case 0
                        Dim oTel As New DTOContactTel(oDrd("NumGuid"))
                        With oTel
                            .Value = oDrd("Num")
                            .Privat = SQLHelper.GetBooleanFromDatareader(oDrd("Privat"))
                            .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                            .Country = New DTOCountry(Guid.Empty)
                            .Country.PrefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
                        End With
                        retval.Tels.Add(oTel)
                    Case 1
                        Dim oEmail As New DTOUser(oDrd("NumGuid"))
                        With oEmail
                            .EmailAddress = oDrd("Num")
                            .Privat = SQLHelper.GetBooleanFromDatareader(oDrd("Privat"))
                            If Not IsDBNull(oDrd("Rol")) Then
                                .Rol = New DTORol(oDrd("Rol"))
                            End If
                        End With
                        retval.Emails.Add(oEmail)
                End Select
            End If
        Loop

        oDrd.Close()

        Return retval
    End Function
End Class
