Public Class DTOBreadCrumb
    Public Property Items As List(Of BreadCrumbItem)

    Public Sub New(oLang As DTOLang, Optional sHomeEsp As String = "Inicio", Optional sHomeCat As String = "Inici", Optional sHomeEng As String = "Home", Optional sHomePor As String = "")
        MyBase.New()
        _Items = New List(Of BreadCrumbItem)
        AddItem(oLang.Tradueix(sHomeEsp, sHomeCat, sHomeEng, sHomePor), "/")
    End Sub

    Public Sub AddItem(sTitle As String, Optional sUrl As String = "")
        Dim oItem As New BreadCrumbItem(sTitle, sUrl)
        _Items.Add(oItem)
    End Sub

    Shared Function FromIncidencia(oLang As DTOLang) As DTOBreadCrumb
        Dim retval As New DTOBreadCrumb(oLang, "Formularios", "Formularis", "Forms")
        retval.AddItem(oLang.Tradueix("Incidencias", "Incidències", "Incidences"), "/incidencias")
        retval.AddItem(oLang.Tradueix("Incidencia", "Incidència", "Incidence"))
        Return retval
    End Function

    Shared Function FromCategoriaDeNoticia(oCategoria As DTOCategoriaDeNoticia) As DTOBreadCrumb
        Dim retval As New DTOBreadCrumb(DTOApp.current.lang)
        retval.AddItem("Noticias", "/News")
        retval.AddItem(oCategoria.Nom)
        Return retval
    End Function

    Shared Function FromNoticia(oLang As DTOLang) As DTOBreadCrumb
        Dim retval As New DTOBreadCrumb(oLang, "Inicio", "Inici", "Home")
        retval.AddItem(oLang.Tradueix("Noticias", "Notícies", "News"), "/News")
        Return retval
    End Function

    Shared Function FromEvento(oLang As DTOLang) As DTOBreadCrumb
        Dim retval As New DTOBreadCrumb(oLang, "Inicio", "Inici", "Home")
        retval.AddItem(oLang.Tradueix("Eventos", "Esdeveniments", "Events"), "/Eventos")
        Return retval
    End Function
End Class

Public Class BreadCrumbItem
    Public Property Title As String
    Public Property Url As String

    Public Sub New(sTitle As String, Optional sUrl As String = "")
        MyBase.New()
        _Title = sTitle
        _Url = sUrl
    End Sub
End Class


