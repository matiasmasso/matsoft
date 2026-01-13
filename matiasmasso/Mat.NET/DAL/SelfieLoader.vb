Public Class SelfiePlayerLoader
    Shared Function Find(oGuid As Guid) As DTOSelfiePlayer
        Dim retval As DTOSelfiePlayer = Nothing
        Dim oPlayer As New DTOSelfiePlayer(oGuid)
        If Load(oPlayer) Then
            retval = oPlayer
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPlayer As DTOSelfiePlayer) As Boolean
        If Not oPlayer.IsLoaded And Not oPlayer.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT SorteoLeads.Sorteo, SorteoLeads.Fch, SorteoLeads.Lead, SorteoLeads.Distributor, SorteoLeads.Category, SorteoLeads.Answer ")
            sb.AppendLine(", Sorteos.Title, Sorteos.Answers, Sorteos.RightAnswer, Sorteos.FchFrom ")
            sb.AppendLine(", Email.adr, Email.Nom, Email.Cognoms, Email.Lang, Email.Tel, Email.FchCreated ")
            sb.AppendLine("FROM SorteoLeads ")
            sb.AppendLine("INNER JOIN Sorteos ON SorteoLeads.Sorteo=Sorteos.Guid ")
            sb.AppendLine("INNER JOIN Email ON SorteoLeads.Lead=Email.Guid ")
            sb.AppendLine("WHERE SorteoLeads.Guid=@Guid ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL, "@Guid", oPlayer.Guid.ToString())
            If oDrd.Read Then
                With oPlayer
                    .Raffle = New DTORaffle(oDrd("Sorteo"))
                    With .Raffle
                        .FchFrom = oDrd("FchFrom")
                        .Title = oDrd("Title")
                        If Not IsDBNull(oDrd("Answers")) Then
                            .Answers = TextHelper.StringListFromMultiline(oDrd("Answers"))
                        End If
                        .RightAnswer = oDrd("RightAnswer")
                    End With
                    .Fch = oDrd("Fch")
                    .User = New DTOUser(DirectCast(oDrd("Lead"), Guid))
                    With .User
                        .EmailAddress = oDrd("Adr")
                        If Not IsDBNull(oDrd("Nom")) Then
                            .Nom = oDrd("Nom")
                        End If
                        If Not IsDBNull(oDrd("Cognoms")) Then
                            .Cognoms = oDrd("Cognoms")
                        End If
                        If Not IsDBNull(oDrd("Tel")) Then
                            .Tel = oDrd("Tel")
                        End If
                        .Lang = DTOLang.Factory(oDrd("Lang").ToString())
                        .FchCreated = oDrd("FchCreated")
                    End With
                    .Answer = oDrd("Answer")
                    If Not IsDBNull(oDrd("Distributor")) Then
                        .Distribuidor = New DTOContact(oDrd("Distributor"))
                        If Not IsDBNull(oDrd("Clx")) Then
                            .Distribuidor.FullNom = oDrd("Clx")
                        End If
                    End If
                    .Category = oDrd("Category")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPlayer.IsLoaded
        Return retval
    End Function


    Shared Function Update(oSelfie As DTOSelfiePlayer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oSelfie, oTrans)
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

    Shared Sub Update(oPlayer As DTOSelfiePlayer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM ContestSelfie WHERE Hash=@Hash"
        Dim oDA As SqlDataAdapter = sqlhelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPlayer.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPlayer.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPlayer
            'Dim oImageStream As Byte() = ImageHelper.GetByteArrayFromImg(.Image)
            'oRow("Player") = .Player.Guid
            'oRow("Image") = oImageStream
            'oRow("Thumbnail") = ImageHelper.GetByteArrayFromImg(.Thumbnail)
            'oRow("Width") = .Image.Width
            'oRow("Height") = .Image.Height
            'oRow("Length") = oImageStream.Length
            'oRow("Mime") = .Mime
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function HasUploadedAnySelfies(oPlayer As DTOSelfiePlayer) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Hash ")
        sb.AppendLine("FROM ContestSelfie ")
        sb.AppendLine("WHERE ContestSelfie.Player = @Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL, "@Guid", oPlayer.Guid.ToString())
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class SelfieLoader
    Shared Function Find(sHash As String) As DTOSelfie
        Dim retval As DTOSelfie = Nothing
        Dim oSelfie As New DTOSelfie
        oSelfie.Hash = sHash
        If Load(oSelfie) Then
            retval = oSelfie
        End If
        Return retval
    End Function

    Shared Function Thumbnail(sHash As String) As Image
        Dim retval As Image = Nothing
        Dim SQL As String = "SELECT Thumbnail FROM ContestSelfie WHERE Hash=@Hash"
        Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL, "@Hash", sHash)
        If oDrd.Read Then
            retval = ImageHelper.GetImgFromByteArray(oDrd("Thumbnail"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Image(sHash As String) As Image
        Dim retval As Image = Nothing
        Dim SQL As String = "SELECT Image FROM ContestSelfie WHERE Hash=@Hash"
        Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL, "@Hash", sHash)
        If oDrd.Read Then
            retval = ImageHelper.GetImgFromByteArray(oDrd("Image"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oSelfie As DTOSelfie) As Boolean
        If Not oSelfie.IsLoaded And Not oSelfie.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ContestSelfie.Hash, ContestSelfie.mime, ContestSelfie.Width, ContestSelfie.Height, ContestSelfie.Length, ContestSelfie.Fch, ContestSelfie.Rating ")
            sb.AppendLine(", ContestSelfie.Thumbnail, ContestSelfie.Player, SorteoLeads.Lead,SorteoLeads.Category ")
            sb.AppendLine(", email.adr, email.nom, email.cognoms ")
            sb.AppendLine("FROM ContestSelfie ")
            sb.AppendLine("INNER JOIN SorteoLeads ON ContestSelfie.Player = SorteoLeads.Guid ")
            sb.AppendLine("INNER JOIN Email ON SorteoLeads.Lead = Email.Guid ")
            sb.AppendLine("WHERE Hash=@Hash")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL, "@Hash", oSelfie.Hash)
            If oDrd.Read Then
                Dim oUser As New DTOUser(DirectCast(oDrd("Lead"), Guid))
                oUser.EmailAddress = oDrd("Adr")
                oUser.Nom = oDrd("Nom")
                oUser.Cognoms = oDrd("Cognoms")

                Dim oPlayer As New DTOSelfiePlayer(oDrd("Player"))
                oPlayer.User = oUser
                oPlayer.Category = oDrd("Category")
                With oSelfie
                    .Player = oPlayer
                    .Hash = oDrd("Hash")
                    .Mime = DirectCast(oDrd("Mime"), MimeCods)
                    .Width = oDrd("Width")
                    .Height = oDrd("Height")
                    .Length = oDrd("Length")
                    .Fch = oDrd("Fch")
                    .Thumbnail = ImageHelper.GetImgFromByteArray(oDrd("Thumbnail"))
                    .Rating = oDrd("Rating")
                    .IsLoaded = True
                    '.Image = ImageHelper.GetImgFromByteArray(oDrd("Image"))
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oSelfie.IsLoaded
        Return retval
    End Function


    Shared Function Update(oSelfie As DTOSelfie, ByRef exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oSelfie, oTrans)
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

    Shared Sub Update(oSelfie As DTOSelfie, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM ContestSelfie WHERE Hash=@Hash"
        Dim oDA As SqlDataAdapter = sqlhelper.GetSQLDataAdapter(SQL, oTrans, "@Hash", oSelfie.Hash)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Hash") = oSelfie.Hash
        Else
            oRow = oTb.Rows(0)
        End If

        With oSelfie
            Dim oImageStream As Byte() = ImageHelper.GetByteArrayFromImg(.Image)
            oRow("Player") = .Player.Guid
            oRow("Thumbnail") = ImageHelper.GetByteArrayFromImg(.Thumbnail)
            If .Image IsNot Nothing Then
                oRow("Image") = oImageStream
                oRow("Width") = .Image.Width
                oRow("Height") = .Image.Height
                oRow("Length") = oImageStream.Length
            End If
            oRow("Mime") = .Mime
            oRow("Rating") = .Rating
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSelfie As DTOSelfie, ByRef exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSelfie, oTrans)
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

    Shared Sub Delete(oSelfie As DTOSelfie, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ContestSelfie WHERE Hash=@Hash"
        sqlhelper.ExecuteNonQuery(SQL, oTrans, "@Hash", oSelfie.Hash)
    End Sub

End Class

Public Class SelfiesLoader

    Shared Function All(Optional oPlayer As DTORaffleParticipant = Nothing, _
                        Optional oCategory As DTOSelfiePlayer.Categories = DTOSelfiePlayer.Categories.NotSet, _
                        Optional iRating As Integer = -1) As List(Of DTOSelfie)

        Dim retval As New List(Of DTOSelfie)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Hash,Mime,Width,Height,Length,ContestSelfie.Fch,Rating ")
        sb.AppendLine("FROM ContestSelfie ")
        sb.AppendLine("INNER JOIN SorteoLeads ON ContestSelfie.Player = SorteoLeads.Guid ")
        sb.AppendLine("WHERE SorteoLeads.Sorteo='" & oPlayer.Raffle.Guid.ToString & "' ")
        If oPlayer IsNot Nothing Then
            sb.AppendLine("AND Player='" & oPlayer.Guid.ToString & "' ")
        End If
        If oCategory <> DTOSelfiePlayer.Categories.NotSet Then
            sb.AppendLine("AND Category=" & CInt(oCategory) & " ")
        End If
        If iRating >= 0 Then
            sb.AppendLine("AND Rating=" & iRating & " ")
        End If
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSelfie As New DTOSelfie
            With oSelfie
                .Hash = oDrd("Hash")
                .Mime = DirectCast(oDrd("Mime"), MimeCods)
                .Width = oDrd("Width")
                .Height = oDrd("Height")
                .Length = oDrd("Length")
                .Fch = oDrd("Fch")
                .Rating = oDrd("Rating")
            End With
            retval.Add(oSelfie)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function TodayWinnersCount() As Integer
        Dim retval As Integer
        Dim sFchFrom As String = Format(Today, "yyyyMMdd") & "00:00:00.000"
        Dim sFchTo As String = Format(Today, "yyyyMMdd") & "23:59:59.999"
        Dim SQL As String = "SELECT COUNT(Hash) AS Winners FROM ContestSelfie WHERE Winner<>0 and Fch BETWEEN '" & sFchFrom & "' and '" & sFchTo & "' "
        Dim oDrd As SqlDataReader = sqlhelper.GetDataReader(SQL)
        oDrd.Read()
        If Not IsDBNull("Winners") Then
            retval = oDrd("Winners")
        End If
        oDrd.Close()
        Return retval
    End Function
End Class