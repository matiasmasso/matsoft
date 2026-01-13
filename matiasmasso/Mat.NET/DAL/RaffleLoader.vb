Public Class RaffleLoader

    Shared Function Find(oGuid As Guid) As DTORaffle
        Dim retval As DTORaffle = Nothing
        Dim oRaffle As New DTORaffle(oGuid)
        If Load(oRaffle) Then
            retval = oRaffle
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRaffle As DTORaffle) As Boolean
        If Not oRaffle.IsLoaded And Not oRaffle.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Sorteos.Title, Sorteos.Lang, Sorteos.Country, Sorteos.Art  ")
            sb.AppendLine(", Sorteos.FchFrom, Sorteos.FchTo, Sorteos.Question, Sorteos.Answers, Sorteos.RightAnswer ")
            sb.AppendLine(", Sorteos.Winner, Sorteos.WinnerNom, Sorteos.Bases, Sorteos.Visible, Sorteos.UrlExterna ")
            sb.AppendLine(", Sorteos.FchWinnerReaction, Sorteos.fchDistributorReaction, Sorteos.FchDelivery, Sorteos.FchPicture ")
            sb.AppendLine(", Sorteos.Delivery, Sorteos.CostPrize, Sorteos.CostPubli ")
            sb.AppendLine(", Country.Nom_Esp AS CountryEsp ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine(", Tpa.WebAtlasRafflesDeadline ")
            sb.AppendLine(", Winner.Lead, Winner.Num, Winner.Distributor ")
            sb.AppendLine(", Email.adr AS WinnerEmail, Email.Tel AS WinnerTel, Email.Nom AS WinnerNom, Email.Cognoms AS WinnerCognoms, Email.Lang ")
            sb.AppendLine(", WinnerDistributor.RaoSocial, WinnerDistributor.NomCom, WinnerDistributor.LangId AS DistributorLang ")
            sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", X.Participants ")
            sb.AppendLine("FROM Sorteos ")
            sb.AppendLine("LEFT OUTER JOIN Country ON Sorteos.Country=Country.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Sorteos.Art=VwProductNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN SorteoLeads Winner ON Sorteos.Winner=Winner.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email ON Winner.Lead=Email.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Winner.Distributor = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON Winner.Distributor = VwAddress.SrcGuid AND VwAddress.Cod = 1 ")
            sb.AppendLine("LEFT OUTER JOIN (")
            sb.AppendLine("     SELECT SorteoLeads.Sorteo, COUNT(DISTINCT SorteoLeads.Guid) AS Participants ")
            sb.AppendLine("     FROM SorteoLeads ")
            sb.AppendLine("     GROUP BY SorteoLeads.Sorteo ")
            sb.AppendLine("     ) X ON Sorteos.Guid = X.Sorteo ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS WinnerDistributor ON Winner.Distributor=WinnerDistributor.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Tpa ON VwProductNom.BrandGuid = Tpa.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Sorteos.Art = Url.Guid ")
            sb.AppendLine("WHERE Sorteos.Guid='" & oRaffle.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Load(oDrd, oRaffle)
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRaffle.IsLoaded
        Return retval
    End Function

    Shared Function Load(oDrd As SqlDataReader, ByRef oRaffle As DTORaffle) As Boolean
        With oRaffle
            .Title = oDrd("Title").ToString
            .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            If Not IsDBNull(oDrd("Country")) Then
                .Country = New DTOCountry(oDrd("Country"))
                .Country.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp")
            End If
            .Product = SQLHelper.GetProductFromDataReader(oDrd)
            If .Product IsNot Nothing Then
                .Product.UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                .Brand = DTOProduct.Brand(.Product)
                If .Brand IsNot Nothing Then
                    Try 'per si la SQL no treu aquest camp
                        .Brand.WebAtlasRafflesDeadline = oDrd("WebAtlasRafflesDeadline")
                    Catch ex As Exception
                    End Try
                End If
            End If
            .Bases = oDrd("Bases").ToString

            If Not IsDBNull(oDrd("Winner")) Then
                .Winner = New DTORaffleParticipant(oDrd("Winner"))
                .Winner.Num = SQLHelper.GetIntegerFromDataReader(oDrd("Num"))
                .Winner.User = New DTOUser(DirectCast(oDrd("Lead"), Guid))
                With .Winner.User
                    .Lang = DTOLang.Factory(oDrd("Lang").ToString())
                    If Not IsDBNull(oDrd("WinnerEmail")) Then
                        .nom = SQLHelper.GetStringFromDataReader(oDrd("WinnerNom"))
                        .cognoms = SQLHelper.GetStringFromDataReader(oDrd("WinnerCogNoms"))
                        .tel = SQLHelper.GetStringFromDataReader(oDrd("WinnerTel"))
                        .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("WinnerEmail"))
                    End If
                End With
                If Not IsDBNull(oDrd("Distributor")) Then
                    .Winner.Distribuidor = New DTOContact(oDrd("Distributor"))
                    .Winner.Distribuidor.lang = DTOLang.Factory(oDrd("DistributorLang"))
                    .Winner.Distribuidor.address = SQLHelper.GetAddressFromDataReader(oDrd)
                End If
            End If

            If Not IsDBNull(oDrd("UrlExterna")) Then
                .UrlExterna = oDrd("UrlExterna").ToString
            End If

            If Not IsDBNull(oDrd("Question")) Then
                .Question = oDrd("Question").ToString
            End If

            .RightAnswer = oDrd("RightAnswer")

            If Not IsDBNull(oDrd("Answers")) Then
                .Answers = TextHelper.StringListFromMultiline(oDrd("Answers").ToString())
            End If

            If Not IsDBNull(oDrd("FchFrom")) Then
                .FchFrom = CDate(oDrd("FchFrom"))
            End If
            If Not IsDBNull(oDrd("FchTo")) Then
                .FchTo = CDate(oDrd("FchTo"))
            End If
            .Visible = oDrd("Visible")
            If Not IsDBNull(oDrd("FchWinnerReaction")) Then
                .FchWinnerReaction = CDate(oDrd("FchWinnerReaction"))
            End If
            If Not IsDBNull(oDrd("FchDistributorReaction")) Then
                .FchDistributorReaction = CDate(oDrd("FchDistributorReaction"))
            End If
            If Not IsDBNull(oDrd("FchDelivery")) Then
                .FchDelivery = CDate(oDrd("FchDelivery"))
            End If
            If Not IsDBNull(oDrd("FchPicture")) Then
                .FchPicture = CDate(oDrd("FchPicture"))
            End If
            If Not IsDBNull(oDrd("Delivery")) Then
                .Delivery = New DTODelivery(oDrd("Delivery"))
            End If
            If Not IsDBNull(oDrd("CostPrize")) Then
                .CostPrize = DTOAmt.Factory(CDec(oDrd("CostPrize")))
            End If
            If Not IsDBNull(oDrd("CostPubli")) Then
                .CostPubli = DTOAmt.Factory(CDec(oDrd("CostPubli")))
            End If
            If SQLHelper.FieldExists(oDrd, "Participants") Then
                .participantsCount = SQLHelper.GetIntegerFromDataReader(oDrd("Participants"))
            End If
            .IsLoaded = True
        End With
        oDrd.Close()
        Return True
    End Function

    Shared Function ImgBanner600(ByRef oRaffle As DTORaffle) As ImageMime
        Dim retval As ImageMime = Nothing
        If Not oRaffle.IsLoaded And Not oRaffle.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Sorteos.ImgBanner600 ")
            sb.AppendLine("FROM Sorteos ")
            sb.AppendLine("WHERE Sorteos.Guid='" & oRaffle.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                If Not IsDBNull(oDrd("ImgBanner600")) Then
                    retval = New ImageMime()
                    retval.ByteArray = oDrd("ImgBanner600")
                End If
            End If

            oDrd.Close()
        End If

        Return retval
    End Function
    Shared Function ImgCallToAction500(ByRef oRaffle As DTORaffle) As ImageMime
        Dim retval As ImageMime = Nothing
        If Not oRaffle.IsLoaded And Not oRaffle.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Sorteos.ImgCallToAction500 ")
            sb.AppendLine("FROM Sorteos ")
            sb.AppendLine("WHERE Sorteos.Guid='" & oRaffle.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                If Not IsDBNull(oDrd("ImgCallToAction500")) Then
                    retval = New ImageMime()
                    retval.ByteArray = oDrd("ImgCallToAction500")
                End If
            End If

            oDrd.Close()
        End If

        Return retval
    End Function

    Shared Function ImgFbFeatured116(ByRef oRaffle As DTORaffle) As ImageMime
        Dim retval As ImageMime = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Sorteos.ImgFbFeatured116 ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("WHERE Sorteos.Guid='" & oRaffle.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("ImgFbFeatured116")) Then
                retval = New ImageMime()
                retval.ByteArray = oDrd("ImgFbFeatured116")
            End If
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function ImgWinner(ByRef oRaffle As DTORaffle) As ImageMime
        Dim retval As ImageMime = Nothing
        If Not oRaffle.IsLoaded And Not oRaffle.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Sorteos.ImgWinner ")
            sb.AppendLine("FROM Sorteos ")
            sb.AppendLine("WHERE Sorteos.Guid='" & oRaffle.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                If Not IsDBNull(oDrd("ImgWinner")) Then
                    retval = New ImageMime()
                    retval.ByteArray = oDrd("ImgWinner")
                End If
            End If

            oDrd.Close()
        End If

        Return retval
    End Function



    Shared Function Update(oRaffle As DTORaffle, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oRaffle, oTrans)
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


    Shared Sub Update(oRaffle As DTORaffle, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Sorteos WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oRaffle.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRaffle.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRaffle
            oRow("Title") = .Title
            oRow("Lang") = .Lang.Tag
            oRow("Country") = SQLHelper.NullableBaseGuid(.Country)
            If .Product Is Nothing Then
                oRow("Art") = System.DBNull.Value
            Else
                oRow("Art") = .Product.Guid
            End If

            oRow("Bases") = .Bases

            If .Winner Is Nothing Then
                oRow("Winner") = System.DBNull.Value
            Else
                oRow("winner") = .Winner.Guid
                oRow("ImgWinner") = .ImageWinner

                If .Winner.User.Nom = "" Then
                    oRow("WinnerNom") = System.DBNull.Value
                Else
                    oRow("WinnerNom") = .Winner.User.Nom
                End If

            End If

            If .UrlExterna = "" Then
                oRow("UrlExterna") = System.DBNull.Value
            Else
                oRow("UrlExterna") = .UrlExterna
            End If

            If .Question = "" Then
                oRow("Question") = System.DBNull.Value
            Else
                oRow("Question") = .Question
            End If

            If .Answers IsNot Nothing Then
                If .Answers.Count = 0 Then
                    oRow("Answers") = System.DBNull.Value
                Else
                    oRow("Answers") = TextHelper.StringListToMultiline(.Answers)
                End If
            End If

            oRow("RightAnswer") = .RightAnswer

            If .ImageFbFeatured IsNot Nothing Then
                oRow("ImgFbFeatured116") = .ImageFbFeatured
            End If
            If .ImageBanner600 IsNot Nothing Then
                oRow("ImgBanner600") = .ImageBanner600
            End If
            If .ImageCallToAction500 IsNot Nothing Then
                oRow("ImgCallToAction500") = .ImageCallToAction500
            End If


            If .FchTo = Date.MinValue Then
                oRow("FchTo") = System.DBNull.Value
            Else
                oRow("FchTo") = .FchTo
            End If

            If .FchFrom = Date.MinValue Then
                oRow("FchFrom") = System.DBNull.Value
            Else
                oRow("FchFrom") = .FchFrom
            End If

            If .FchWinnerReaction = Date.MinValue Then
                oRow("FchWinnerReaction") = System.DBNull.Value
            Else
                oRow("FchWinnerReaction") = .FchWinnerReaction
            End If

            If .FchDistributorReaction = Date.MinValue Then
                oRow("FchDistributorReaction") = System.DBNull.Value
            Else
                oRow("FchDistributorReaction") = .FchDistributorReaction
            End If

            If .FchDelivery = Date.MinValue Then
                oRow("FchDelivery") = System.DBNull.Value
            Else
                oRow("FchDelivery") = .FchDelivery
            End If

            If .FchPicture = Date.MinValue Then
                oRow("FchPicture") = System.DBNull.Value
            Else
                oRow("FchPicture") = .FchPicture
            End If

            oRow("Visible") = .Visible
            If .Delivery Is Nothing Then
                oRow("Delivery") = System.DBNull.Value
            Else
                oRow("Delivery") = .Delivery.Guid
            End If

            If .CostPrize Is Nothing Then
                oRow("CostPrize") = System.DBNull.Value
            Else
                oRow("CostPrize") = .CostPrize.Eur
            End If

            If .CostPubli Is Nothing Then
                oRow("CostPubli") = System.DBNull.Value
            Else
                oRow("CostPubli") = .CostPubli.Eur
            End If

        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oRaffle As DTORaffle, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = sqlhELPER.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRaffle, oTrans)
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


    Shared Sub Delete(oRaffle As DTORaffle, ByRef oTrans As SqlTransaction)
        With oRaffle
            Dim SQL As String = "DELETE Sorteos WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oRaffle.Guid.ToString())
        End With
    End Sub

    Shared Function Zonas(oRaffle As DTORaffle) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Web.AreaGuid, Web.AreaNom ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("INNER JOIN VwProductParent ON Sorteos.art=VwProductParent.Child ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent=Tpa.Guid ")
        sb.AppendLine("INNER JOIN Web ON VwProductParent.Parent=Web.Brand AND Web.Country = Sorteos.Country ")
        sb.AppendLine("WHERE Sorteos.Guid='" & oRaffle.Guid.ToString & "' ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")
        sb.AppendLine("AND Web.Raffles = 1 ")
        sb.AppendLine("AND Web.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY Web.AreaGuid, Web.AreaNom, Tpa.WebAtlasRafflesDeadline ")
        sb.AppendLine("HAVING MAX(Web.LastFch)>DATEADD(d,-Tpa.WebAtlasRafflesDeadline,GETDATE()) ")
        sb.AppendLine("ORDER BY Web.AreaNom, Web.AreaGuid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOGuidNom(oDrd("AreaGuid"), oDrd("AreaNom"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class


Public Class RafflesLoader

    Shared Function RafflesCount(oLang As DTOLang) As Integer
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT COUNT(Sorteos.Guid) AS Raffles ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("WHERE Lang = '" & oLang.Tag & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        Dim retval As Integer = oDrd("Raffles")
        oDrd.Close()
        Return retval
    End Function

    Shared Function All() As List(Of DTORaffle)
        Dim retval As New List(Of DTORaffle)

        Dim oRaffle As New DTORaffle
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Sorteos.Guid AS RaffleGuid, Sorteos.Lang AS RaffleLang, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.UrlExterna, Sorteos.Winner ")
        sb.AppendLine(", Sorteos.FchWinnerReaction, Sorteos.FchDistributorReaction, Sorteos.FchDelivery, Sorteos.FchPicture, Sorteos.Delivery ")
        sb.AppendLine(", Sorteos.CostPrize, Sorteos.CostPubli, Sorteos.Visible ")
        sb.AppendLine(", SorteoLeads.Guid AS LeadGuid, SorteoLeads.Num, SorteoLeads.Lead, SorteoLeads.Fch ")
        sb.AppendLine(", Email.Adr AS EmailAdr, Email.Lang as EmailLang, Email.Nom, Email.Cognoms, Email.FchCreated as EmailFchCreated ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("LEFT OUTER JOIN SorteoLeads ON SorteoLeads.Sorteo = Sorteos.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON SorteoLeads.Lead=Email.Guid ")
        sb.AppendLine("GROUP BY Sorteos.Guid, Sorteos.Lang, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.UrlExterna, Sorteos.Winner ")
        sb.AppendLine(", SorteoLeads.Guid, SorteoLeads.Num, SorteoLeads.Lead, SorteoLeads.Fch ")
        sb.AppendLine(", Sorteos.FchWinnerReaction, Sorteos.FchDistributorReaction, Sorteos.FchDelivery, Sorteos.FchPicture, Sorteos.Delivery ")
        sb.AppendLine(", Sorteos.CostPrize, Sorteos.CostPubli, Sorteos.Visible ")
        sb.AppendLine(", Email.Adr, Email.Lang, Email.Nom, Email.Cognoms, Email.FchCreated ")
        'sb.AppendLine("ORDER BY Sorteos.FchTo DESC, Sorteos.FchFrom DESC, SorteoLeads.Fch DESC")
        sb.AppendLine("ORDER BY Sorteos.FchFrom DESC, Sorteos.FchTo DESC, SorteoLeads.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oRaffle.Guid.Equals(oDrd("RaffleGuid")) Then
                oRaffle = New DTORaffle(oDrd("RaffleGuid"))

                With oRaffle
                    .Title = oDrd("Title")
                    .Lang = SQLHelper.GetLangFromDataReader(oDrd("RaffleLang"))
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = oDrd("FchTo")
                    If Not IsDBNull(oDrd("UrlExterna")) Then
                        .UrlExterna = oDrd("UrlExterna")
                    End If
                    If Not IsDBNull(oDrd("CostPrize")) Then
                        .CostPrize = DTOAmt.Factory(CDec(oDrd("CostPrize")))
                    End If
                    If Not IsDBNull(oDrd("CostPubli")) Then
                        .CostPubli = DTOAmt.Factory(CDec(oDrd("CostPubli")))
                    End If
                    .Participants = New List(Of DTORaffleParticipant)

                    If Not IsDBNull(oDrd("FchWinnerReaction")) Then
                        .FchWinnerReaction = CDate(oDrd("FchWinnerReaction"))
                    End If
                    If Not IsDBNull(oDrd("FchDistributorReaction")) Then
                        .FchDistributorReaction = CDate(oDrd("FchDistributorReaction"))
                    End If
                    If Not IsDBNull(oDrd("FchDelivery")) Then
                        .FchDelivery = CDate(oDrd("FchDelivery"))
                    End If
                    If Not IsDBNull(oDrd("FchPicture")) Then
                        .FchPicture = CDate(oDrd("FchPicture"))
                    End If
                    If Not IsDBNull(oDrd("Delivery")) Then
                        .Delivery = New DTODelivery(oDrd("Delivery"))
                    End If
                End With
                retval.Add(oRaffle)

            End If


            If Not IsDBNull(oDrd("LeadGuid")) Then
                Dim oUser As New DTOUser(DirectCast(oDrd("Lead"), Guid))
                With oUser
                    If IsDBNull(oDrd("EmailLang")) Then
                        .Lang = DTOLang.ESP
                    Else
                        .Lang = DTOLang.Factory(oDrd("EmailLang").ToString())
                    End If
                    If Not IsDBNull(oDrd("EmailFchCreated")) Then
                        .FchCreated = oDrd("EmailFchCreated")
                    End If
                    If Not IsDBNull(oDrd("EmailAdr")) Then
                        .EmailAddress = oDrd("EmailAdr")
                    End If
                    If Not IsDBNull(oDrd("Nom")) Then
                        .Nom = oDrd("Nom")
                    End If
                    If Not IsDBNull(oDrd("Cognoms")) Then
                        .Cognoms = oDrd("Cognoms")
                    End If
                End With

                Dim oItem As New DTORaffleParticipant(oDrd("LeadGuid"))
                oItem.Raffle = oRaffle
                oRaffle.Participants.Add(oItem)
                If Not IsDBNull(oDrd("Winner")) Then
                    If oItem.Guid.Equals(oDrd("Winner")) Then
                        oRaffle.Winner = oItem
                    End If
                End If

                With oItem
                    .Fch = oDrd("Fch")
                    .User = oUser
                End With

            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Headers(oLang As DTOLang, Optional OnlyVisible As Boolean = False) As List(Of DTORaffle)
        Dim retval As New List(Of DTORaffle)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Sorteos.Guid AS RaffleGuid, Sorteos.Lang, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.Visible, Sorteos.Winner, Sorteos.UrlExterna, CostPrize, CostPubli ")
        sb.AppendLine(", FchPicture, FchDelivery, FchDistributorReaction, FchWinnerReaction ")
        sb.AppendLine(", SorteoLeads.Lead AS WinnerUserGuid, WinnerUser.Nom, WinnerUser.Cognoms, WinnerUser.LocationNom, WinnerUser.ProvinciaNom ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("LEFT OUTER JOIN SorteoLeads ON Sorteos.Winner=SorteoLeads.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS WinnerUser ON SorteoLeads.Lead=WinnerUser.Guid ")
        sb.AppendLine("WHERE Sorteos.Lang='" & oLang.Tag & "' ")
        If OnlyVisible Then
            sb.AppendLine("AND Sorteos.Visible<>0 ")
        End If
        sb.AppendLine("ORDER BY Sorteos.FchFrom DESC, Sorteos.FchTo DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oRaffle As New DTORaffle(oDrd("RaffleGuid"))

            With oRaffle
                .Title = oDrd("Title")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .FchFrom = oDrd("FchFrom")
                .FchTo = oDrd("FchTo")
                .Visible = oDrd("Visible")
                .UrlExterna = SQLHelper.GetStringFromDataReader(oDrd("UrlExterna"))
                .CostPrize = SQLHelper.GetAmtFromDataReader(oDrd("CostPrize"))
                .CostPubli = SQLHelper.GetAmtFromDataReader(oDrd("CostPubli"))

                If Not IsDBNull(oDrd("Winner")) Then
                    .Winner = New DTORaffleParticipant(oDrd("Winner"))
                    If Not IsDBNull(oDrd("WinnerUserGuid")) Then
                        .Winner.User = New DTOUser(oDrd("WinnerUserGuid"))
                        With .Winner.User
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                            .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                            .LocationNom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                            .ProvinciaNom = SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom"))
                        End With
                    End If
                End If

                .FchPicture = SQLHelper.GetFchFromDataReader(oDrd("FchPicture"))
                .FchDelivery = SQLHelper.GetFchFromDataReader(oDrd("FchDelivery"))
                .FchDistributorReaction = SQLHelper.GetFchFromDataReader(oDrd("FchDistributorReaction"))
                .FchWinnerReaction = SQLHelper.GetFchFromDataReader(oDrd("FchWinnerReaction"))

            End With
            retval.Add(oRaffle)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CompactHeaders(oLang As DTOLang) As List(Of DTORaffle.Compact)
        Dim retval As New List(Of DTORaffle.Compact)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Sorteos.Guid AS RaffleGuid, Sorteos.Lang AS RaffleLang, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.Visible, Sorteos.Winner, Sorteos.UrlExterna, Sorteos.CostPrize, Sorteos.CostPubli ")
        sb.AppendLine(", FchPicture, FchDelivery, FchDistributorReaction, FchWinnerReaction ")
        sb.AppendLine(", Sorteos.Art, VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine(", WinnerLead.Lead AS WinnerUserGuid, WinnerUser.Nom, WinnerUser.Cognoms, WinnerUser.LocationNom, WinnerUser.ProvinciaNom ")
        sb.AppendLine(", VwFeedbackSum.Shares ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("LEFT OUTER JOIN SorteoLeads AS WinnerLead ON Sorteos.Winner=WinnerLead.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS WinnerUser ON WinnerLead.Lead=WinnerUser.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom On Sorteos.Art=VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwFeedbackSum On Sorteos.Guid = VwFeedbackSum.Target ")
        sb.AppendLine("WHERE Sorteos.Visible<>0 ")
        Select Case oLang.id
            Case DTOLang.Ids.POR
                sb.AppendLine("AND Sorteos.Lang = 'POR' ")
            Case Else
                sb.AppendLine("AND Sorteos.Lang = 'ESP' ")
        End Select
        sb.AppendLine("ORDER BY Sorteos.FchFrom DESC, Sorteos.FchTo DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oRaffle As New DTORaffle.Compact()

            With oRaffle
                .Guid = oDrd("RaffleGuid")
                .Title = oDrd("Title")
                .FchFrom = oDrd("FchFrom")
                .FchTo = oDrd("FchTo")

                If Not IsDBNull(oDrd("Winner")) Then
                    .Winner = DTORaffleParticipant.Compact.Factory(oDrd("Winner"), oDrd("WinnerUserGuid"), SQLHelper.GetStringFromDataReader(oDrd("Nom")), SQLHelper.GetStringFromDataReader(oDrd("Cognoms")), SQLHelper.GetStringFromDataReader(oDrd("LocationNom")), SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom")))
                End If


            End With
            retval.Add(oRaffle)
        Loop
        oDrd.Close()

        Return retval

    End Function

    Shared Function DueRaffles() As List(Of DTORaffle)
        Dim retval As New List(Of DTORaffle)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Sorteos.Guid  ")
        sb.AppendLine("FROM Sorteos  ")
        sb.AppendLine("WHERE Sorteos.Winner IS NULL  ")
        sb.AppendLine("AND Sorteos.FchTo < GETDATE() ")
        sb.AppendLine("AND YEAR(Sorteos.FchTo) > 2014 ")
        sb.AppendLine("ORDER BY Sorteos.FchTo")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRaffle As New DTORaffle(oDrd("Guid"))
            retval.Add(oRaffle)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function AllRaffleHeaders(Optional OnlyVisible As Boolean = False, Optional IncludeSummaries As Boolean = True, Optional oLang As DTOLang = Nothing, Optional year As Integer = 0) As List(Of DTORaffle)
        Dim retval As New List(Of DTORaffle)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Sorteos.Guid AS RaffleGuid, Sorteos.Lang AS RaffleLang, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.Visible, Sorteos.Winner,  Sorteos.UrlExterna, Sorteos.CostPrize, Sorteos.CostPubli ")
        sb.AppendLine(", FchPicture, FchDelivery, FchDistributorReaction, FchWinnerReaction ")
        sb.AppendLine(", Sorteos.Art, VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine(", WinnerLead.Lead AS WinnerUserGuid, WinnerUser.Nom, WinnerUser.Cognoms, WinnerUser.LocationNom, WinnerUser.ProvinciaNom ")
        sb.AppendLine(", VwFeedbackSum.Shares ")
        If IncludeSummaries Then
            sb.AppendLine(", COUNT(DISTINCT SorteoLeads.Guid) AS Participants ")
            sb.AppendLine(", COUNT(CASE WHEN Email.FchCreated > Sorteos.FchFrom THEN SorteoLeads.Guid ELSE NULL END) As NewParticipants ")
        End If
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("LEFT OUTER JOIN SorteoLeads AS WinnerLead ON Sorteos.Winner=WinnerLead.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS WinnerUser ON WinnerLead.Lead=WinnerUser.Guid ")
        If IncludeSummaries Then
            sb.AppendLine("LEFT OUTER JOIN SorteoLeads On SorteoLeads.Sorteo = Sorteos.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email On SorteoLeads.Lead=Email.Guid ")
        End If
        sb.AppendLine("LEFT OUTER JOIN VwProductNom On Sorteos.Art=VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwFeedbackSum On Sorteos.Guid = VwFeedbackSum.Target ")
        sb.AppendLine("WHERE 1=1 ")
        If OnlyVisible Then
            sb.AppendLine("AND Sorteos.Visible<>0 ")
        End If
        If year <> 0 Then
            sb.AppendLine("AND (YEAR(Sorteos.FchFrom)=" & year & " OR YEAR(Sorteos.FchTo)=" & year & ") ")
        End If
        If oLang IsNot Nothing Then
            Select Case oLang.Id
                Case DTOLang.Ids.POR
                    sb.AppendLine("AND Sorteos.Lang = 'POR' ")
                Case Else
                    sb.AppendLine("AND Sorteos.Lang = 'ESP' ")
            End Select
        End If
        If IncludeSummaries Then
            sb.AppendLine("GROUP BY Sorteos.Guid, Sorteos.Lang, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo, Sorteos.Visible, Sorteos.Winner, Sorteos.UrlExterna, CostPrize, CostPubli ")
            sb.AppendLine(", FchPicture, FchDelivery, FchDistributorReaction, FchWinnerReaction ")
            sb.AppendLine(", Sorteos.Art, VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine(", WinnerLead.Lead, WinnerUser.Nom, WinnerUser.Cognoms, WinnerUser.LocationNom, WinnerUser.ProvinciaNom ")
            sb.AppendLine(", VwFeedbackSum.Shares ")
        End If
        sb.AppendLine("ORDER BY Sorteos.FchFrom DESC, Sorteos.FchTo DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oRaffle As New DTORaffle(oDrd("RaffleGuid"))

            With oRaffle
                .Title = oDrd("Title")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("RaffleLang"))
                .FchFrom = oDrd("FchFrom")
                .FchTo = oDrd("FchTo")
                .Visible = oDrd("Visible")
                If Not IsDBNull(oDrd("Art")) Then
                    .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Art"), oDrd("SkuNom"))
                    .Brand = New DTOProductBrand(oDrd("BrandGuid"))
                    SQLHelper.LoadLangTextFromDataReader(.Brand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                End If
                .UrlExterna = SQLHelper.GetStringFromDataReader(oDrd("UrlExterna"))
                .CostPrize = SQLHelper.GetAmtFromDataReader(oDrd("CostPrize"))
                .CostPubli = SQLHelper.GetAmtFromDataReader(oDrd("CostPubli"))
                .Shares = SQLHelper.GetIntegerFromDataReader(oDrd("Shares"))

                If Not IsDBNull(oDrd("Winner")) Then
                    .Winner = New DTORaffleParticipant(oDrd("Winner"))
                    If Not IsDBNull(oDrd("WinnerUserGuid")) Then
                        .Winner.User = New DTOUser(oDrd("WinnerUserGuid"))
                        With .Winner.User
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                            .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                            .LocationNom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                            .ProvinciaNom = SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom"))
                        End With
                    End If
                End If


                .FchPicture = SQLHelper.GetFchFromDataReader(oDrd("FchPicture"))
                .FchDelivery = SQLHelper.GetFchFromDataReader(oDrd("FchDelivery"))
                .FchDistributorReaction = SQLHelper.GetFchFromDataReader(oDrd("FchDistributorReaction"))
                .FchWinnerReaction = SQLHelper.GetFchFromDataReader(oDrd("FchWinnerReaction"))

                If IncludeSummaries Then
                    .ParticipantsCount = SQLHelper.GetIntegerFromDataReader(oDrd("Participants"))
                    .NewParticipantsCount = SQLHelper.GetIntegerFromDataReader(oDrd("NewParticipants"))
                End If
            End With
            retval.Add(oRaffle)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function NextFchFrom(oLang As DTOLang) As Date
        Dim retval As Date = Nothing

        Dim oDomain = DTOWebDomain.Factory(oLang)
        Dim oRaffleLang = oDomain.DefaultLang()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT MIN(FchFrom) As NextFch ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("WHERE Lang='" & oRaffleLang.Tag & "' ")
        sb.AppendLine("AND FchFrom > GETDATE() ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("NextFch")) Then
                retval = oDrd("NextFch")
            End If
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function HeadersModel(oLang As DTOLang, take As Integer, takeFrom As Integer, Optional oUser As DTOUser = Nothing) As DTORaffle.HeadersModel
        Dim retval As New DTORaffle.HeadersModel
        Dim oDomain = DTOWebDomain.Factory(oLang)
        Dim oRaffleLang = oDomain.DefaultLang()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Sorteos.Guid, Sorteos.Title, Sorteos.FchFrom, Sorteos.FchTo ")
        sb.AppendLine(", Sorteos.Winner, Email.Nom AS WinnerNom, Email.Cognoms AS WinnerCognoms ")
        sb.AppendLine(", VwFeedback.Likes, VwFeedback.Shares, VwFeedback.HasLiked, VwFeedback.HasShared ")
        sb.AppendLine("FROM Sorteos ")
        sb.AppendLine("LEFT OUTER JOIN SorteoLeads ON Sorteos.Winner = SorteoLeads.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON SorteoLeads.Lead = Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwFeedback ON Sorteos.Guid = VwFeedback.Target ")
        If oUser Is Nothing Then
            sb.AppendLine("AND VwFeedback.UserOrCustomer IS NULL ")
        Else
            sb.AppendLine("AND VwFeedback.UserOrCustomer = '" & oUser.Guid.ToString() & "' ")
        End If
        sb.AppendLine("WHERE Sorteos.Visible<>0 ")
        sb.AppendLine("AND Sorteos.Lang='" & oRaffleLang.Tag & "' ")
        sb.AppendLine("AND FchFrom < GETDATE() ")
        sb.AppendLine("ORDER BY Sorteos.fchfrom DESC ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim idx As Integer
        Do While oDrd.Read
            If idx >= takeFrom And idx < (takeFrom + take) Then
                Dim item As New DTORaffle.HeadersModel.Item
                With item
                    .Guid = oDrd("Guid")
                    .Title = oDrd("Title")
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    If Not IsDBNull(oDrd("Winner")) Then
                        Dim sNom = SQLHelper.GetStringFromDataReader(oDrd("WinnerNom"))
                        Dim sCognoms = SQLHelper.GetStringFromDataReader(oDrd("WinnerCognoms"))
                        .Winner = String.Format("{0} {1}", sNom, sCognoms)
                    End If
                    .Feedback = New DTOFeedback.Model
                    .Feedback.Likes = SQLHelper.GetIntegerFromDataReader(oDrd("Likes"))
                    .Feedback.Shares = SQLHelper.GetIntegerFromDataReader(oDrd("Shares"))
                    .Feedback.HasLiked = SQLHelper.GetIntegerFromDataReader(oDrd("HasLiked")) > 0
                    .Feedback.HasShared = SQLHelper.GetIntegerFromDataReader(oDrd("HasShared")) > 0
                End With
                retval.Items.Add(item)
            End If
            idx += 1
        Loop
        oDrd.Close()

        If takeFrom = 0 Then
            'first time read next raffle countdown start
            retval.NextFch = NextFchFrom(oLang)
        End If

        retval.totalCount = idx
        Return retval

    End Function

End Class

Public Class RaffleParticipantLoader

    Shared Function Find(oGuid As Guid) As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = Nothing
        Dim oRaffleParticipant As New DTORaffleParticipant(oGuid)
        If Load(oRaffleParticipant) Then
            retval = oRaffleParticipant
        End If
        Return retval
    End Function

    Shared Function Find(oRaffle As DTORaffle, oUser As DTOUser) As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select SorteoLeads.* ")
        sb.AppendLine("FROM SorteoLeads ")
        sb.AppendLine("WHERE SorteoLeads.Sorteo='" & oRaffle.Guid.ToString & "' And SorteoLeads.Lead='" & oUser.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = RaffleParticipantLoader.Find(oDrd("Guid"))
            With retval
                .Raffle = oRaffle
                .Fch = oDrd("Fch")
                .User = oUser
                .Answer = oDrd("Answer")
                If Not IsDBNull(oDrd("Distributor")) Then
                    .Distribuidor = New DTOContact(oDrd("Distributor"))
                End If
                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Num"))
                .IsLoaded = True
            End With

        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Load(ByRef oRaffleParticipant As DTORaffleParticipant) As Boolean
        If Not oRaffleParticipant.IsLoaded And Not oRaffleParticipant.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("Select SorteoLeads.Num, SorteoLeads.Sorteo, SorteoLeads.Fch, SorteoLeads.Lead, SorteoLeads.Distributor, SorteoLeads.Answer ")
            sb.AppendLine(", Sorteos.Lang AS RaffleLang, Sorteos.Title, Sorteos.Answers, Sorteos.RightAnswer, Sorteos.FchFrom, Sorteos.FchTo ")
            sb.AppendLine(", Email.adr, Email.Nom, Email.Cognoms, Email.Lang, Email.Tel ")
            sb.AppendLine(", Email.address, Email.Zipcod, Email.location, Email.LocationNom, Email.ProvinciaNom, Email.Birthday, Email.Childcount, Email.LastChildBirthday, Email.FchCreated ")
            sb.AppendLine(", CliGral.FullNom ")
            sb.AppendLine(", Sorteos.Art AS SkuGuid, VwSkuNom.CategoryGuid, VwSkuNom.BrandGuid ")
            sb.AppendLine(", VwZip.LocationNom As LocationNom2, VwZip.ZonaNom ")
            sb.AppendLine(", VwProductNom.* ")
            sb.AppendLine("FROM SorteoLeads ")
            sb.AppendLine("INNER JOIN Sorteos On SorteoLeads.Sorteo=Sorteos.Guid ")
            sb.AppendLine("INNER JOIN VwSkuNom On Sorteos.Art=VwSkuNom.SkuGuid ")
            sb.AppendLine("INNER JOIN Email On SorteoLeads.Lead=Email.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom On Sorteos.Art=VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral On SorteoLeads.Distributor=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwZip On Email.Pais=VwZip.CountryISO And Email.ZipCod=VwZip.ZipCod ")
            sb.AppendLine("WHERE SorteoLeads.Guid='" & oRaffleParticipant.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRaffleParticipant
                    .Raffle = New DTORaffle(oDrd("Sorteo"))
                    With .Raffle
                        .FchFrom = oDrd("FchFrom")
                        .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                        .Lang = SQLHelper.GetLangFromDataReader(oDrd("RaffleLang"))
                        .Product = SQLHelper.GetProductFromDataReader(oDrd)
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
                        If Not IsDBNull(oDrd("address")) Then
                            .Adr = oDrd("address")
                        End If
                        If Not IsDBNull(oDrd("zipcod")) Then
                            .ZipCod = oDrd("zipcod")
                        End If
                        If Not IsDBNull(oDrd("Location")) Then
                            .Location = New DTOLocation(oDrd("Location"))
                        End If
                        If IsDBNull(oDrd("LocationNom")) Then
                            If Not IsDBNull(oDrd("LocationNom2")) Then
                                Dim tmp As String = oDrd("LocationNom2")
                                If Not IsDBNull(oDrd("ZonaNom")) Then
                                    If tmp <> oDrd("ZonaNom") Then
                                        tmp = tmp & " (" & oDrd("ZonaNom") & ")"
                                    End If
                                End If
                                .LocationNom = tmp
                            End If
                        Else
                            .LocationNom = oDrd("LocationNom")
                        End If
                        If Not IsDBNull(oDrd("ProvinciaNom")) Then
                            .ProvinciaNom = oDrd("ProvinciaNom")
                        End If
                        If Not IsDBNull(oDrd("Birthday")) Then
                            .Birthday = oDrd("Birthday")
                        End If
                        If Not IsDBNull(oDrd("ChildCount")) Then
                            .ChildCount = oDrd("Childcount")
                        End If
                        If Not IsDBNull(oDrd("LastChildBirthday")) Then
                            .LastChildBirthday = oDrd("LastChildBirthday")
                        End If
                        If Not IsDBNull(oDrd("FchCreated")) Then
                            .FchCreated = oDrd("FchCreated")
                        End If
                    End With
                    .Answer = oDrd("Answer")
                    If Not IsDBNull(oDrd("Distributor")) Then
                        .Distribuidor = New DTOContact(oDrd("Distributor"))
                        .Distribuidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    End If
                    .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Num"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRaffleParticipant.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRaffleParticipant As DTORaffleParticipant, ByRef exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            If oRaffleParticipant.IsNew Then
                oRaffleParticipant.Num = LastId(oRaffleParticipant.Raffle, oTrans) + 1
            End If
            Update(oRaffleParticipant, oTrans)
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

    Shared Sub Update(oRaffleParticipant As DTORaffleParticipant, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SorteoLeads ")
        sb.AppendLine("WHERE SorteoLeads.Guid='" & oRaffleParticipant.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRaffleParticipant.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRaffleParticipant
            oRow("Sorteo") = .Raffle.Guid
            oRow("Lead") = .User.Guid
            oRow("Fch") = .Fch
            oRow("Answer") = .Answer
            If .Distribuidor IsNot Nothing Then
                oRow("Distributor") = .Distribuidor.Guid
            End If
            oRow("Num") = SQLHelper.NullableInt(.Num)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function LastId(oRaffle As DTORaffle, oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Num) AS LastId FROM SorteoLeads WHERE Sorteo='" & oRaffle.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.Rows(0)
        If Not IsDBNull(oRow("LastId")) Then
            retval = CInt(oRow("LastId"))
        End If
        Return retval
    End Function

    Shared Function Delete(oRaffleParticipant As DTORaffleParticipant, ByRef exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRaffleParticipant, oTrans)
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


    Shared Sub Delete(oRaffleParticipant As DTORaffleParticipant, ByRef oTrans As SqlTransaction)
        With oRaffleParticipant
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE SorteoLeads ")
            sb.AppendLine("WHERE SorteoLeads.Guid=@Guid ")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oRaffleParticipant.Guid.ToString())
        End With
    End Sub


End Class

Public Class RaffleParticipantsLoader
    Shared Function Compact(oRaffle As DTORaffle) As List(Of DTORaffleParticipant.Compact)
        Dim retval As New List(Of DTORaffleParticipant.Compact)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SorteoLeads.Guid, SorteoLeads.Num, SorteoLeads.Fch, SorteoLeads.Lead ")
        sb.AppendLine(", Email.adr, Email.Nom, Email.Cognoms,Email.LocationNom, Email.ProvinciaNom, Email.Lang, CliGral.FullNom, Email.FchCreated ")
        sb.AppendLine("FROM SorteoLeads ")
        sb.AppendLine("INNER JOIN Sorteos ON SorteoLeads.Sorteo=Sorteos.Guid ")
        sb.AppendLine("INNER JOIN Email ON SorteoLeads.Lead=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON SorteoLeads.Distributor=CliGral.Guid ")
        sb.AppendLine("WHERE SorteoLeads.Sorteo=@Guid ")
        sb.AppendLine("ORDER BY SorteoLeads.Fch DESC ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oRaffle.Guid.ToString())
        Do While oDrd.Read
            Dim oRaffleParticipant = DTORaffleParticipant.Compact.Factory(oDrd("Guid"), oDrd("Lead"), SQLHelper.GetStringFromDataReader(oDrd("Nom")), SQLHelper.GetStringFromDataReader(oDrd("Cognoms")), SQLHelper.GetStringFromDataReader(oDrd("LocationNom")), SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom")))
            With oRaffleParticipant
                .User.FchCreated = oDrd("FchCreated")
                .User.Lang = DTOLang.Factory(oDrd("Lang"))
                .User.value = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                .Fch = oDrd("Fch")
                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Num"))
            End With
            retval.Add(oRaffleParticipant)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function FromRaffle(oRaffle As DTORaffle) As List(Of DTORaffleParticipant)
        Dim retval As New List(Of DTORaffleParticipant)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SorteoLeads.Guid, SorteoLeads.Num, SorteoLeads.Fch, SorteoLeads.Lead, SorteoLeads.Distributor, SorteoLeads.Answer ")
        sb.AppendLine(", Email.adr, Email.Nom, Email.Cognoms,Email.LocationNom, Email.Lang, CliGral.FullNom, Email.FchCreated ")
        sb.AppendLine("FROM SorteoLeads ")
        sb.AppendLine("INNER JOIN Sorteos ON SorteoLeads.Sorteo=Sorteos.Guid ")
        sb.AppendLine("INNER JOIN Email ON SorteoLeads.Lead=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON SorteoLeads.Distributor=CliGral.Guid ")
        sb.AppendLine("WHERE SorteoLeads.Sorteo=@Guid ")
        sb.AppendLine("ORDER BY SorteoLeads.Fch DESC ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oRaffle.Guid.ToString())
        Do While oDrd.Read
            Dim oRaffleParticipant As New DTORaffleParticipant(oDrd("Guid"))
            With oRaffleParticipant
                .Raffle = oRaffle
                .Fch = oDrd("Fch")
                .User = New DTOUser(DirectCast(oDrd("Lead"), Guid))
                With .User
                    .EmailAddress = oDrd("Adr")
                    .Lang = DTOLang.Factory(oDrd("Lang").ToString())
                    If Not IsDBNull(oDrd("Nom")) Then
                        .Nom = oDrd("Nom")
                    End If
                    If Not IsDBNull(oDrd("CogNoms")) Then
                        .Cognoms = oDrd("CogNoms")
                    End If
                    If Not IsDBNull(oDrd("LocationNom")) Then
                        .LocationNom = oDrd("LocationNom")
                    End If
                    If Not IsDBNull(oDrd("FchCreated")) Then
                        .FchCreated = oDrd("FchCreated")
                    End If
                End With
                If Not IsDBNull(oDrd("Answer")) Then
                    .Answer = oDrd("Answer")
                End If
                If Not IsDBNull(oDrd("Distributor")) Then
                    .Distribuidor = New DTOContact(oDrd("Distributor"))
                    .Distribuidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Num"))
                .IsLoaded = True
            End With
            retval.Add(oRaffleParticipant)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function Valids(oRaffle As DTORaffle) As List(Of DTORaffleParticipant)
        Dim retval As New List(Of DTORaffleParticipant)

        RaffleLoader.Load(oRaffle)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT SorteoLeads.Guid, SorteoLeads.Num, SorteoLeads.Fch, SorteoLeads.Lead, SorteoLeads.Distributor ")
        sb.AppendLine(", SorteoLeads.Answer, Email.adr, Email.Lang, CliGral.FullNom ")
        sb.AppendLine(", VwAddress.ZipGuid, VwAddress.LocationGuid, VwAddress.ZonaGuid, VwAddress.CountryGuid, VwAddress.CountryISO ")
        sb.AppendLine("FROM SorteoLeads ")
        sb.AppendLine("INNER JOIN Email ON SorteoLeads.Lead = Email.Guid ")
        sb.AppendLine("INNER JOIN Sorteos ON SorteoLeads.Sorteo = Sorteos.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Sorteos.Art=VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN (SELECT Web.Client, Web.Brand FROM Web WHERE Impagat=0 AND Blocked=0 AND Obsoleto=0) X ON X.Client=SorteoLeads.Distributor AND X.Brand=VwSkuNom.BrandGuid ")
        sb.AppendLine("INNER JOIN VwAddress ON SorteoLeads.Distributor=VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON SorteoLeads.Distributor=CliGral.Guid ")
        'provisional: accepta nomes botigues
        sb.AppendLine("INNER JOIN VwContactChannel ON SorteoLeads.Distributor=VwContactChannel.Contact AND VwContactChannel.Channel='EF72040D-8F5D-40C7-B4CE-AB069656858D' ")

        sb.AppendLine("WHERE SorteoLeads.Sorteo='" & oRaffle.Guid.ToString & "' ")
        sb.AppendLine("AND Email.FchActivated IS NOT NULL ")
        sb.AppendLine("AND Email.FchDeleted IS NULL ")
        sb.AppendLine("AND Email.Nom IS NOT NULL ")
        sb.AppendLine("AND Email.Cognoms IS NOT NULL ")
        If oRaffle.RightAnswer > 0 Then
            sb.AppendLine("AND SorteoLeads.Answer = " & (oRaffle.RightAnswer - 1).ToString & " ")
        End If
        sb.AppendLine("GROUP BY SorteoLeads.Guid, SorteoLeads.Num, SorteoLeads.Fch, SorteoLeads.Lead, SorteoLeads.Distributor ")
        sb.AppendLine(", SorteoLeads.Answer, Email.adr, Email.Lang, CliGral.FullNom ")
        sb.AppendLine(", VwAddress.ZipGuid, VwAddress.LocationGuid, VwAddress.ZonaGuid, VwAddress.CountryGuid, VwAddress.CountryISO ")
        sb.AppendLine("ORDER BY SorteoLeads.Fch ")


        Dim oGuids As New List(Of Guid)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Sorteo", oRaffle.Guid.ToString())
        Do While oDrd.Read
            Dim oRaffleParticipant As New DTORaffleParticipant(oDrd("Guid"))
            With oRaffleParticipant
                .Raffle = oRaffle
                .Fch = oDrd("Fch")
                .User = New DTOUser(DirectCast(oDrd("Lead"), Guid))
                With .User
                    .EmailAddress = oDrd("Adr")
                    .Lang = DTOLang.Factory(oDrd("Lang").ToString())
                End With
                If Not IsDBNull(oDrd("Answer")) Then
                    .Answer = oDrd("Answer")
                End If
                If Not IsDBNull(oDrd("Distributor")) Then
                    .Distribuidor = New DTOContact(oDrd("Distributor"))
                    .Distribuidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Distribuidor.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                End If
                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Num"))
                .IsLoaded = True
            End With

            Dim CountryValidated As Boolean
            Dim countryIso As String = oRaffleParticipant.Distribuidor.Address.Zip.Location.Zona.Country.ISO
            Select Case oRaffle.Lang.id
                Case DTOLang.Ids.ESP, DTOLang.Ids.CAT
                    CountryValidated = countryIso = "ES"
                Case DTOLang.Ids.POR
                    CountryValidated = countryIso = "PT"
                Case Else
                    CountryValidated = False
            End Select

            If DTORaffleParticipant.ExcludedZonas.Any(Function(x) x.Equals(DTOAddress.Zona(oRaffleParticipant.Distribuidor.Address))) Then
                CountryValidated = False
            End If

            If CountryValidated Then
                retval.Add(oRaffleParticipant)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Delete(oRaffleParticipants As List(Of DTORaffleParticipant), exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE SorteoLeads ")
        sb.AppendLine("WHERE (")
        For i As Integer = 0 To oRaffleParticipants.Count - 1
            If i > 0 Then sb.Append("OR ")
            sb.AppendLine("SorteoLeads.Guid='" & oRaffleParticipants(i).Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

End Class