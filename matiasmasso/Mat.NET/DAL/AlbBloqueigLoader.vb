Public Class AlbBloqueigLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAlbBloqueig
        Dim retval As DTOAlbBloqueig = Nothing
        Dim oAlb_Bloqueig As New DTOAlbBloqueig(oGuid)
        If Load(oAlb_Bloqueig) Then
            retval = oAlb_Bloqueig
        End If
        Return retval
    End Function

    Shared Function Search(oContact As DTOContact, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As DTOAlbBloqueig
        Dim retVal As DTOAlbBloqueig = Nothing
        Try
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Alb_Bloqueig.*, CliGral.FullNom, Email.Adr, Email.Nickname ")
            sb.AppendLine("FROM Alb_Bloqueig ")
            sb.AppendLine("INNER JOIN CliGral ON Alb_Bloqueig.Contact=CliGral.Guid ")
            sb.AppendLine("INNER JOIN Email ON Alb_Bloqueig.UserGuid=Email.Guid ")
            sb.AppendLine("WHERE (Alb_Bloqueig.Contact = '" & oContact.Guid.ToString & "' OR Alb_Bloqueig.Contact = '" & System.Guid.Empty.ToString & "' )")
            'sb.AppendLine("AND Alb_Bloqueig.Cod = '" & oCodi.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oUser As New DTOUser(DirectCast(oDrd("UserGuid"), Guid))
                oUser.EmailAddress = oDrd("adr")
                oUser.NickName = oDrd("Nickname")

                retVal = New DTOAlbBloqueig(oDrd("Guid"))
                With retVal
                    .User = oUser
                    .Contact = oContact
                    .Codi = [Enum].Parse(GetType(DTOAlbBloqueig.Codis), oDrd("Cod"))
                    .Fch = oDrd("Fch")
                    .IsLoaded = True
                End With
                oDrd.Close()
            End If
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retVal
    End Function

    Shared Function Load(ByRef oAlb_Bloqueig As DTOAlbBloqueig) As Boolean
        If Not oAlb_Bloqueig.IsLoaded And Not oAlb_Bloqueig.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Alb_Bloqueig.*, CliGral.FullNom, Email.Adr ")
            sb.AppendLine("FROM Alb_Bloqueig ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON B.Contact=CliGral.Guid ")
            sb.AppendLine("INNER JOIN Email ON B.UserGuid=Email.Guid ")
            sb.AppendLine("WHERE Alb_Bloqueig.Guid = '" & oAlb_Bloqueig.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oUser As New DTOUser(DirectCast(oDrd("UserGuid"), Guid))
                oUser.EmailAddress = oDrd("adr")

                Dim oContact As DTOContact = Nothing
                If Not IsDBNull(oDrd("Contact")) Then
                    oContact = New DTOContact(oDrd("Contact"))
                    oContact.FullNom = oDrd("FullNom")
                End If

                With oAlb_Bloqueig
                    .User = oUser
                    .Contact = oContact
                    .Codi = oDrd("Cod")
                    .Fch = oDrd("Fch")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oAlb_Bloqueig.IsLoaded
        Return retval
    End Function

    Shared Function Update(oAlb_Bloqueig As DTOAlbBloqueig, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAlb_Bloqueig, oTrans)
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


    Shared Sub Update(oAlb_Bloqueig As DTOAlbBloqueig, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Alb_Bloqueig ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oAlb_Bloqueig.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAlb_Bloqueig.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAlb_Bloqueig
            oRow("UserGuid") = .User.Guid
            oRow("Contact") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("Cod") = .Codi
            oRow("Fch") = .Fch
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAlb_Bloqueig As DTOAlbBloqueig, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAlb_Bloqueig, oTrans)
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


    Shared Sub Delete(oAlb_Bloqueig As DTOAlbBloqueig, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Alb_Bloqueig WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oAlb_Bloqueig.Guid.ToString())
    End Sub

    Shared Function BloqueigEnd(oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Alb_Bloqueig ")
        sb.AppendLine("WHERE UserGuid='" & oAlbBloqueig.User.Guid.ToString & "' ")
        If oAlbBloqueig.Contact Is Nothing Then
            sb.AppendLine("AND Contact IS NULL ")
        Else
            sb.AppendLine("AND Contact ='" & oAlbBloqueig.Contact.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND Cod='" & oAlbBloqueig.Codi.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        retval = (exs.Count = 0)

        Return retval
    End Function



#End Region


End Class

Public Class AlbBloqueigsLoader

    Shared Function All(Optional oEmp As DTOEmp = Nothing) As List(Of DTOAlbBloqueig)
        Dim retval As New List(Of DTOAlbBloqueig)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb_Bloqueig.*, CliGral.FullNom, Email.Adr, Email.Nickname ")
        sb.AppendLine("FROM Alb_Bloqueig ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Alb_Bloqueig.Contact=CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email ON Alb_Bloqueig.UserGuid=Email.Guid ")
        sb.AppendLine("ORDER BY CliGral.FullNom, Alb_Bloqueig.Cod")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("UserGuid"), Guid))
            With oUser
                .EmailAddress = oDrd("adr")
                .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
            End With

            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("Contact")) Then
                oContact = New DTOContact(oDrd("Contact"))
                oContact.FullNom = oDrd("FullNom")
            End If

            Dim item As New DTOAlbBloqueig(oDrd("Guid"))
            With item
                .User = oUser
                .Contact = oContact
                .Codi = [Enum].Parse(GetType(DTOAlbBloqueig.Codis), oDrd("Cod"))
                .Fch = oDrd("Fch")
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop

        oDrd.Close()

        Return retval
    End Function
End Class
