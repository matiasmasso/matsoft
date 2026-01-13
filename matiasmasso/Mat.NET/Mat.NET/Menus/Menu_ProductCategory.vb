Public Class Menu_ProductCategory
    Inherits Menu_Base

    Private _ProductCategory As DTOProductCategory
    Private _ProductCategories As List(Of DTOProductCategory)
    Private mStp As Stp
    Private mStps As Stps

#Region "Constructors"
    Public Sub New(ByVal oProductCategory As DTOProductCategory)
        MyBase.New()
        _ProductCategory = oProductCategory
        _ProductCategories = New List(Of DTOProductCategory)
        mStp = New Stp(oProductCategory.Guid)
        mStp.Nom = _ProductCategory.Nom
        mStps = New Stps
        mStps.Add(mStp)
    End Sub

    Public Sub New(ByVal oProductCategories As List(Of DTOProductCategory))
        MyBase.New()
        _ProductCategory = oProductCategories(0)
        _ProductCategories = oProductCategories
        mStps = New Stps
        For Each item As DTOProductCategory In oProductCategories
            Dim oStp As New Stp(item.Guid)
            oStp.Nom = _ProductCategory.Nom
            mStps.Add(oStp)
        Next
        mStp = mStps(0)
    End Sub

    Public Sub New(ByVal oStp As Stp, Optional ByVal oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        mStp = oStp
        mStps = New Stps
        mStps.Add(oStp)
        _ProductCategory = New DTOProductCategory(oStp.Guid)
        _ProductCategory.Nom = oStp.Nom
        _ProductCategories = New List(Of DTO.DTOProductCategory)
        _ProductCategories.Add(_ProductCategory)
    End Sub

#End Region

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Atlas(), _
        MenuItem_Incidencies(), _
        MenuItem_Ranking(), _
        MenuItem_Repeticions(), _
        MenuItem_Stats(), _
        MenuItem_Historial(), _
        MenuItem_Incentius(), _
        MenuItem_Forecast(), _
        MenuItem_Chart(), _
        MenuItem_Delete(), _
        MenuItem_Online(), _
        MenuItem_Circulars(), _
        MenuItem_Arts()})
    End Function


#Region "Items"

#End Region

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Atlas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Atlas"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Atlas
        Return oMenuItem
    End Function

    Private Function MenuItem_Incidencies() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencies"
        oMenuItem.Image = My.Resources.Spv
        AddHandler oMenuItem.Click, AddressOf Do_Incidencies
        Return oMenuItem
    End Function

    Private Function MenuItem_Ranking() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ranking"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Ranking
        Return oMenuItem
    End Function

    Private Function MenuItem_Repeticions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Repeticions"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Repeticions
        Return oMenuItem
    End Function

    Private Function MenuItem_Stats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Estadistiques"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Stat
        Return oMenuItem
    End Function

    Private Function MenuItem_Historial() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Historial"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Historial
        Return oMenuItem
    End Function

    Private Function MenuItem_Forecast() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Forecast"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Forecast
        Return oMenuItem
    End Function

    Private Function MenuItem_Incentius() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "incentius"
        AddHandler oMenuItem.Click, AddressOf Do_Incentius
        Return oMenuItem
    End Function

    Private Function MenuItem_Chart() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Chart
        oMenuItem.Text = "Trajectoria"
        oMenuItem.Image = My.Resources.notepad
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        oMenuItem.Enabled = mStps(0).AllowDelete
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    Private Function MenuItem_Online() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Online"
        With oMenuItem.DropDownItems
            .Add(MenuItem_Web)
            .Add(MenuItem_CopyLink)
            .Add(MenuItem_CopyUrlBuilderEmails)
            .Add(MenuItem_UrlBuilderBlogComments)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "navegar a Web"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a Web"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyUrlBuilderEmails() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç per email"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_UrlBuilderEmails
        Return oMenuItem
    End Function

    Private Function MenuItem_UrlBuilderBlogComments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç per comentari blog"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_UrlBuilderBlogComments
        Return oMenuItem
    End Function


    Private Function MenuItem_Circulars() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Circulars..."
        oMenuItem.Image = My.Resources.MailSobreGroc
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
                oMenuItem.DropDownItems.Add(MenuItem_ExcelClientsSortits)
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelClientsSortits() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "excel amb clients en actiu sortits"
        AddHandler oMenuItem.Click, AddressOf Do_Excel_ClientsSortits
        Return oMenuItem
    End Function

    Private Function MenuItem_Arts() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Articles"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ShowArts
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Stp(mStp, BLL.Defaults.SelectionModes.Browse)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _ProductCategory.Nom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLProductCategory.Delete(_ProductCategory, exs) Then
                MyBase.RefreshRequest(Me, New System.EventArgs)
            Else
                UIHelper.WarnError(exs, "error al eliminar la categoría")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCategory As DTOProductCategory = BLL.BLLProductCategory.Find(mStps(0).Guid)
        Dim oStat As New DTOStat
        With oStat
            .Lang = BLL.BLLApp.Lang
            .Product = oCategory
            .ConceptType = DTOStat.ConceptTypes.Geo
            .ExpandToLevel = 5
            .Format = DTOStat.Formats.Units
        End With
        Dim oFrm As New Frm_Stats(oStat)
        oFrm.Show()
        'oFrm.Show(root.ShowStatGeoMes(, mStps(0)))
    End Sub

    Private Sub Do_Incidencies()
        Dim oQuery As DTOIncidenciaQuery = BLL_Incidencies.DefaultQuery(BLL.BLLSession.Current.User)
        With oQuery
            .Src = DTOIncidencia.Srcs.Producte
            .Product = New DTOProductCategory(mStps(0).Guid)
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Private Sub Do_Ranking(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowArtxStpRanking(mStps(0))
    End Sub

    Public Sub Do_Repeticions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.ShowArtxStpRepeticions(mStps(0))
    End Sub

    Private Sub Do_Stat(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowStatArtsMes(mStps(0))
    End Sub

    Private Sub Do_Historial(sender As Object, e As System.EventArgs)
        Dim oFrm As New Frm_StpHistorial(mStps(0))
        oFrm.Show()
    End Sub

    Private Sub Do_Forecast(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowForecast(mStps(0))
    End Sub

    Private Sub Do_Chart(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oProduct As New Product(mStps(0))
        Dim oFrm As New Frm_ProductChart(oProduct)
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Process.Start("IExplore.exe", mStps(0).RoutingUrl(True))
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mStps(0).RoutingUrl(True), True)
    End Sub

    Private Sub Do_UrlBuilderBlogComments(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mStps(0).UrlBuilderForBlogComments, True)
    End Sub

    Private Sub Do_UrlBuilderEmails(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mStps(0).UrlBuilderForEmails, True)
    End Sub



    Private Sub Do_Excel_ClientsSortits()
        Dim oCsv As New FF(DTO.DTOFlatFile.ids.Csv)
        Dim oProduct As New Product(mStps(0))
        For Each oClient As Client In oProduct.ClientsAmbSortides()
            If Not oClient.Obsoleto Then
                For Each oemail As Email In oClient.Emails
                    If Not oemail.NoNews And oemail.xObsoleto = MaxiSrvr.TriState.Falso Then
                        oCsv.AddValues(oClient.Nom_o_NomComercial, oClient.Adr.Text, oClient.Adr.Zip.ZipyCit, oClient.DefaultTel, oemail.Adr)
                    End If
                Next
            End If
        Next
        Dim sFilename As String = oCsv.Save
        Process.Start("Excel.exe", sFilename)
    End Sub


    Private Sub Do_ShowArts(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Stp_Arts(mStps(0))
        oFrm.Show()
    End Sub

    Private Sub Do_Incentius()
        Dim oProduct As New Product(mStps(0))
        Dim oFrm As New Frm_Incentius(oProduct)
        oFrm.Show()
    End Sub



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================






End Class

