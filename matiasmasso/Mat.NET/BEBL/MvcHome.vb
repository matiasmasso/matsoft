Public Class MvcHome

    Shared Function Model(oEmp As DTOEmp, oLang As DTOLang, oUser As DTOUser) As MvcHomeModel
        Dim banners = BEBL.Banners.Active(oLang)
        Dim oportadaImgs = BEBL.PortadaImgs.All()
        Dim noticias = BEBL.Noticias.LastNoticias(oUser, oLang, take:=4)
        Dim blogposts = BEBL.BlogPosts.All(oLang, take:=4, onlyVisible:=True)
        Dim raffle = BEBL.Raffles.CurrentOrNextRaffle(oLang)
        Dim retval = MvcHomeModel.Factory(oLang, banners, oportadaImgs, noticias, blogposts, raffle)
        Return retval
    End Function

End Class
