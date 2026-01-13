Public Class BoxItemsLoader
    Shared Function BrandVideos(oLang As DTOLang) As List(Of DTOBoxItem) 'surt a www.matiasmasso.es/videos
        Dim retval As New List(Of DTOBoxItem)

        Dim SQL As String = "SELECT Tpa.Guid, BrandNom.Esp AS BrandNomEsp, max(YT.FchCreated) AS LastFch, count(DISTINCT YT.Guid) as VideosCount " _
        & "FROM YouTubeProducts YTP " _
        & "INNER JOIN Youtube YT ON YTP.YoutubeGuid=YT.Guid " _
        & "INNER JOIN VwProductParent PP ON YTP.ProductGuid = PP.Child " _
        & "INNER JOIN Tpa ON PP.Parent=TPA.Guid " _
        & "INNER JOIN VwLangText AS BrandNom ON TPA.Guid = BrandNom.Guid AND BrandNom.Src = 28 " _
        & "WHERE Tpa.Obsoleto = 0 " _
        & "AND Tpa.Web_Enabled_Consumer <>0 " _
        & "Group by Tpa.Guid, BrandNom.Esp " _
        & "Order by VideosCount DESC"


        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(oDrd("Guid"))
            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNomEsp", "BrandNomEsp", "BrandNomEsp", "BrandNomEsp")

            Dim sTitle As String
            Dim sFooter As String
            Dim iCount As Integer = CInt(oDrd("VideosCount"))
            Select Case iCount
                Case 1
                    sTitle = oLang.Tradueix("1 video", "1 video", "1 movie")
                    sFooter = oLang.Tradueix("subido {0}", "pujat {0}", "uploaded {0}")
                Case Else
                    sTitle = oLang.Tradueix(iCount & " videos", iCount & " videos", iCount & " movies")
                    sFooter = oLang.Tradueix("ultimo video subido {0}", "darrer video pujat {0}", "last upload {0}")
            End Select


            Dim oItem As New DTOBoxItem
            With oItem
                .Tag = oBrand
                .Title = String.Format(sTitle, oBrand.Nom)
                .Footer = String.Format(sFooter, CDate(oDrd("LastFch")).ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
