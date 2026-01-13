Public Class SoftwareDownloadsController
    Inherits _MatController

    Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            ViewBag.Title = ContextHelper.Tradueix("Descarga de software", "Descàrrega de software", "Software downloads")
            Return View("SoftwareDownloads", items())

        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If

        Return retval



    End Function

    Function items() As DTOSoftwareDownload.Collection
        Dim retval As New DTOSoftwareDownload.Collection()
        Dim title = DTOLangText.Factory("Antivirus ESET")
        Dim excerpt = DTOLangText.Factory("Antivirus para PCs Windows de 64 bit", "Antivirus per PCs Windows de 64 bit", "Antivirus for Windows 64 bit PCs")
        Dim url = "https://www.matiasmasso.es/matsoft/av/ees_nt64.msi"
        Dim item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Supremo")
        excerpt = DTOLangText.Factory("Escritorio remoto para PCs", "Escritorio remot per PCs", "PC remote desktop")
        url = "https://www.supremocontrol.com/download.aspx?file=Supremo.exe&id_sw=7&ws=supremocontrol.com"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Mat.Net")
        excerpt = DTOLangText.Factory("Software corporativo de gestión", "Software corporatiu de gestió", "Corporative ERP")
        url = "https://www.matiasmasso.es/matsoft/mat.net/setup.exe"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Spv")
        excerpt = DTOLangText.Factory("Software taller", "Software taller", "Workshop software")
        url = "https://www.matiasmasso.es/matsoft/spvcli/publish.htm" 'C:\Public\Matsoft\SpvCli
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Ghostscript 32 bit")
        excerpt = DTOLangText.Factory("Complemento para generación de Pdf", "Complement per generació de Pdf", "Pdf manager complement")
        url = "https://www.matiasmasso.es/matsoft/ghostscript/gs926w32.exe"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Ghostscript 64 bit")
        excerpt = DTOLangText.Factory("Complemento para generación de Pdf", "Complement per generació de Pdf", "Pdf manager complement")
        url = "https://www.matiasmasso.es/matsoft/ghostscript/gs926w64.exe"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Windows Erp")
        excerpt = DTOLangText.Factory("Gestion pública y corporativa para escritorio Windows", "Gestió pública i corporativa per escritori Windows", "Public and private management forWindows desktop")
        url = "https://www.matiasmasso.es/matsoft/erp/Erp.msix"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Certificat Windows Erp")
        excerpt = DTOLangText.Factory("Instalar a Autoridades de Certificación raíz de confianza")
        url = "https://www.matiasmasso.es/matsoft/erp/Erp.cer"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        title = DTOLangText.Factory("Android Erp")
        excerpt = DTOLangText.Factory("Gestion pública y corporativa para móviles Android", "Gestió pública i corporativa per mobils Android", "Public and private management for Android mobiles")
        url = "https://www.matiasmasso.es/matsoft/erp/es.matiasmasso.erp-Signed.apk"
        item = DTOSoftwareDownload.Factory(title, excerpt, url)
        retval.Add(item)

        Return retval
    End Function

End Class
