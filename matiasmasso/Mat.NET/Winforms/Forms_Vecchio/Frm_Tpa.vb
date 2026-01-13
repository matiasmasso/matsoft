
Imports Microsoft.Office.Interop

Public Class Frm_Tpa

    Private _Brand As DTOProductBrand

    Private mCancel As Boolean
    Private mChanged As Boolean
    Private mDefaultTab As Tabs = Tabs.General
    Private mDirtyWebThumbnail As Boolean
    Private mDirtyLogoVectorial As Boolean
    Private mDirtyLogoDistribuidorOficial As Boolean
    Private mInclouDownloadObsolets As Boolean
    Private mAllowEvents As Boolean
    Private mDirtyEBook As Boolean = False
    Private mLastSelectedPictureBox As PictureBox
    Private CleanTab(20) As Boolean
    Private mSelectionMode As DTO.Defaults.SelectionModes


    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterSelect(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Enum Tabs
        General
        Stps
        Zonas
        Reps
        Web
        Downloads
        MediaResources
        Logistica
        EBook
    End Enum


    Private Enum ColsZonas
        IsoPais
        Zona
        RepId
        RepNom
    End Enum


    Public Sub New(ByVal oBrand As DTOProductBrand, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Brand = oBrand
        mSelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Tpa_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.ProductBrand.Load(_Brand, exs, IncludeImages:=True) Then
            Await refresca()

        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Function refresca() As Task
        With _Brand
            TextBoxNom.Text = .nom.Tradueix(Current.Session.Lang)

            TextBoxTagLine_ESP.Text = .Tagline_Esp
            TextBoxTagLine_CAT.Text = .Tagline_Cat
            TextBoxTagLine_ENG.Text = .Tagline_Eng

            If Not .Proveidor Is Nothing Then Xl_ContactProveidor.Contact = .Proveidor
            If .ShowAtlas Then
                CheckBoxShowAtlas.Checked = True
                LabelWebAtlasDeadline1.Visible = True
                LabelWebatlasDeadline2.Visible = True
                NumericUpDownWebatlasDeadline.Visible = True
                NumericUpDownWebatlasDeadline.Value = .WebAtlasDeadline
                LabelWebAtlasRafflesDeadline1.Visible = True
                LabelWebAtlasRafflesDeadline2.Visible = True
                NumericUpDownWebatlasRafflesDeadline.Visible = True
                NumericUpDownWebatlasRafflesDeadline.Value = .WebAtlasRafflesDeadline
                CheckBoxRestrictAtlasToPremiumLine.Visible = True
                If .RestrictAtlasToPremiumLine IsNot Nothing Then
                    CheckBoxRestrictAtlasToPremiumLine.Checked = True
                    Xl_LookupPremiumLine1.Visible = True
                    Xl_LookupPremiumLine1.PremiumLine = .RestrictAtlasToPremiumLine
                End If
            End If
            CheckBoxDistribuidorsOficials.Checked = .CodDist

            CheckBoxObsoleto.Checked = .obsoleto
            Xl_Image_Logo.Load(.logo, 150, 48, "Logo " & .nom.Tradueix(Current.Session.Lang))
            If .IsNew Then
                Me.Text = "(Nova marca comercial)"
            Else
                Me.Text = .nom.Tradueix(Current.Session.Lang)
            End If
            'CheckReps()

            Xl_LookupCountryMadeIn.Country = .MadeIn
            CheckBoxWebEnabledPro.Checked = .EnabledxPro
            CheckBoxWebEnabledConsumer.Checked = .enabledxConsumer

            Xl_ImageLogoDistribuidorOficial.Load(.logoDistribuidorOficial, 0, 0, "Logo distribuidor oficial " & .nom.Tradueix(Current.Session.Lang))
            Xl_LookupCodiMercancia1.CodiMercancia = .CodiMercancia

            Await RefrescaChannels()

        End With

        mAllowEvents = True

        ButtonDel.Enabled = True

    End Function


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        mCancel = True
        mChanged = False
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Brand
            .nom.Esp = TextBoxNom.Text

            .Tagline_Esp = TextBoxTagLine_ESP.Text
            .Tagline_Cat = TextBoxTagLine_CAT.Text
            .Tagline_Eng = TextBoxTagLine_ENG.Text

            If Xl_ContactProveidor.Contact IsNot Nothing Then
                .Proveidor = DTOProveidor.FromContact(Xl_ContactProveidor.Contact)
            End If
            .ShowAtlas = CheckBoxShowAtlas.Checked
            .WebAtlasDeadline = NumericUpDownWebatlasDeadline.Value
            .WebAtlasRafflesDeadline = NumericUpDownWebatlasRafflesDeadline.Value
            .CodDist = IIf(CheckBoxDistribuidorsOficials.Checked, DTOProductBrand.CodDists.DistribuidorsOficials, DTOProductBrand.CodDists.Free)
            .Obsoleto = CheckBoxObsoleto.Checked
            'ImageList1.Images.Add("WARN", My.Resources.warn)
            .EnabledxPro = CheckBoxWebEnabledPro.Checked
            .EnabledxConsumer = CheckBoxWebEnabledConsumer.Checked
            .CodiMercancia = Xl_LookupCodiMercancia1.CodiMercancia

            If mDirtyLogoDistribuidorOficial Then
                .logoDistribuidorOficial = LegacyHelper.ImageHelper.Converter(Xl_ImageLogoDistribuidorOficial.Bitmap)
            End If


            .MadeIn = Xl_LookupCountryMadeIn.Country


            If CheckBoxRestrictAtlasToPremiumLine.Checked Then
                .RestrictAtlasToPremiumLine = Xl_LookupPremiumLine1.PremiumLine
            Else
                .RestrictAtlasToPremiumLine = Nothing
            End If

        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.ProductBrand.Update(_Brand, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Brand))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem " & _Brand.nom.Tradueix(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ProductBrand.Delete(_Brand, exs) Then
                MsgBox(_Brand.nom.Tradueix(Current.Session.Lang) & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            Dim BlEnableButtons As Boolean = True
            If Not mAllowEvents Then BlEnableButtons = False

            If BlEnableButtons Then
                mChanged = True
                ButtonOk.Enabled = True
            End If
        End If
    End Sub

    Private Sub Xl_Image_Logo_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Image_Logo.AfterUpdate
        Dim BlEnableButtons As Boolean = True
        If Not mAllowEvents Then BlEnableButtons = False

        If BlEnableButtons Then
            _Brand.logo = LegacyHelper.ImageHelper.Converter(Xl_Image_Logo.Bitmap)
            Xl_Image_Logo.Bitmap = LegacyHelper.ImageHelper.Converter(MatHelperStd.ImageHelper.GetThumbnailToFit(_Brand.logo, Xl_Image_Logo.Width, Xl_Image_Logo.Height))
            SetDirty()
        End If
    End Sub


    Private Sub Xl_ImageLogoDistribuidorOficial_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_ImageLogoDistribuidorOficial.AfterUpdate
        mDirtyLogoDistribuidorOficial = True
        SetDirty()
    End Sub



    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     CheckBoxObsoleto.CheckedChanged,
      Xl_ContactProveidor.AfterUpdate,
       TextBoxNom.TextChanged,
        CheckBoxWebEnabledPro.CheckedChanged,
                CheckBoxDistribuidorsOficials.CheckedChanged,
                 TextBoxTagLine_ESP.TextChanged,
                  TextBoxTagLine_CAT.TextChanged,
                   TextBoxTagLine_ENG.TextChanged,
                     Xl_LookupPremiumLine1.AfterUpdate,
                      Xl_LookupCountryMadeIn.AfterUpdate,
                       Xl_LookupCodiMercancia1.AfterUpdate,
                        NumericUpDownWebatlasDeadline.ValueChanged,
        NumericUpDownWebatlasRafflesDeadline.ValueChanged

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub Xl_Color1_AfterUpdate(ByVal oColor As System.Drawing.Color)
        SetDirty()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Stps
                    If Not Await LoadStps(exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Case Tabs.Zonas
                    Await LoadGridZonas()
                Case Tabs.Reps
                    Await LoadGridReps()
                Case Tabs.Web
                Case Tabs.Downloads
                    LoadProductDownloads()
                Case Tabs.MediaResources
                    Await LoadMediaResources()
                Case Tabs.Logistica
            End Select
            CleanTab(iTab) = True
        End If

    End Sub

#Region "Reps"

    Private Async Sub LoadGridReps(sender As Object, e As MatEventArgs)
        Await LoadGridReps()
    End Sub

    Private Async Function LoadGridReps() As Task
        Dim exs As New List(Of Exception)
        Dim oRepProducts = Await FEB2.RepProducts.All(exs, _Brand, True)
        If exs.Count = 0 Then
            Xl_RepProductsxRep1.Load(oRepProducts, Xl_RepProducts.Modes.ByProduct)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub ButtonMailTpaReps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMailTpaReps.Click
        Dim exs As New List(Of Exception)
        Dim oEmails = Await FEB2.Reps.Emails(exs, _Brand)
        If exs.Count = 0 Then
            Dim recipients As String = DTOEmail.Recipients(oEmails)
            Clipboard.SetDataObject(recipients, True)
            MsgBox("adreçes copiades a portapapers", MsgBoxStyle.Information, "MAT.NET")
        Else
            UIHelper.WarnError(exs)
        End If
        Exit Sub

    End Sub

#End Region


    Private Async Sub LoadStps(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oCategories = Await FEB2.ProductCategories.All(exs, _Brand, IncludeObsolets:=True)
        If exs.Count = 0 Then
            Xl_ProductCategories1.load(oCategories)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadStps(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oCategories = Await FEB2.ProductCategories.All(exs, _Brand, IncludeObsolets:=True)
        If exs.Count = 0 Then
            Xl_ProductCategories1.load(oCategories)
        Else
            UIHelper.WarnError(exs)
        End If
        Return exs.Count = 0
    End Function



    Private Sub Xl_ProductDownloads1_onFileDropped(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.onFileDropped
        Dim oDocFile As DTODocFile = e.Argument
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Target = _Brand
        oProductDownload.DocFile = oDocFile

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim exs As New List(Of Exception)
        Dim oProductDownload = DTOProductDownload.Factory(_Brand, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_ProductDownload(oProductDownload)
            AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        LoadProductDownloads()
    End Sub

    Private Async Sub LoadProductDownloads()
        Dim exs As New List(Of Exception)
        Dim oDownloads As List(Of DTOProductDownload) = Await FEB2.Downloads.FromProductOrParent(exs, _Brand)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadMediaResources() As Task
        Dim exs As New List(Of Exception)
        ProgressBarMediaResources.Visible = True
        Dim items = Await FEB2.MediaResources.AllWithThumbnails(exs, _Brand)
        ProgressBarMediaResources.Visible = False
        If exs.Count = 0 Then
            Xl_MediaResources1.Load(_Brand, items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_MediaResources1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MediaResources1.RequestToRefresh
        Await LoadMediaResources()
    End Sub

    Private Sub Xl_RepProductsxRep1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepProductsxRep1.RequestToAddNew
        Dim oRepProduct = DTORepProduct.Factory(_Brand)
        Dim oRepProducts As New List(Of DTORepProduct)
        oRepProducts.Add(oRepProduct)
        Dim oFrm As New Frm_RepProduct(oRepProducts)
        AddHandler oFrm.AfterUpdate, AddressOf LoadGridReps
        oFrm.Show()
    End Sub

    Private Async Sub Xl_RepProductsxRep1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepProductsxRep1.RequestToRefresh
        Await LoadGridReps()
    End Sub

    Private Async Sub CheckBoxOnlyEmpty_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOnlyEmpty.CheckedChanged
        If mAllowEvents Then
            Await LoadGridReps()
        End If
    End Sub

    Private Async Sub LoadGridZonas(sender As Object, e As MatEventArgs)
        Await LoadGridZonas()
    End Sub

    Private Async Function LoadGridZonas() As Task
        Dim exs As New List(Of Exception)
        Dim oBrandAreas = Await FEB2.BrandAreas.All(_Brand, exs)
        If exs.Count = 0 Then
            Xl_BrandAreas1.Load(oBrandAreas)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_BrandAreas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BrandAreas1.RequestToAddNew
        Dim oBrandArea = DTOBrandArea.Factory(_Brand)
        Dim oFrm As New frm_BrandArea(oBrandArea)
        AddHandler oFrm.afterupdate, AddressOf LoadGridZonas
        oFrm.show()
    End Sub


    Private Async Sub Xl_BrandAreas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BrandAreas1.RequestToRefresh
        Await LoadGridZonas()
    End Sub

    Private Sub ButtonCancel_Click1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_ProductChannels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToAddNew
        Dim item As New DTOProductChannel
        With item
            .Product = _Brand
            _Brand.nom.Esp = TextBoxNom.Text
            .Cod = DTOProductChannel.Cods.Inclou
        End With
        Dim oFrm As New Frm_ProductChannel(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaChannels
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductChannels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToRefresh
        Await RefrescaChannels()
    End Sub

    Private Async Sub RefrescaChannels(sender As Object, e As MatEventArgs)
        Await RefrescaChannels()
    End Sub
    Private Async Function RefrescaChannels() As Task
        Dim exs As New List(Of Exception)
        Dim oProductChannels = Await FEB2.ProductChannels.All(exs, _Brand)
        If exs.Count = 0 Then
            Xl_ProductChannels1.Load(oProductChannels, Xl_ProductChannels.Modes.ChannelsPerProduct)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_ProductCategories1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToRefresh
        Dim exs As New List(Of Exception)
        If Not Await LoadStps(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ProductCategories1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToAddNew
        Dim value As New DTOProductCategory
        With value
            .Brand = _Brand
            .Codi = DTOProductCategory.Codis.Standard
        End With
        Dim oFrm As New Frm_Stp(value, mSelectionMode)
        AddHandler oFrm.AfterUpdate, AddressOf LoadStps
        oFrm.Show()
    End Sub

    Private Sub CheckBoxShowAtlas_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowAtlas.CheckedChanged
        If mAllowEvents Then
            CheckBoxRestrictAtlasToPremiumLine.Visible = CheckBoxShowAtlas.Checked
            LabelWebAtlasDeadline1.Visible = CheckBoxShowAtlas.Checked
            LabelWebatlasDeadline2.Visible = CheckBoxShowAtlas.Checked
            LabelWebAtlasRafflesDeadline1.Visible = CheckBoxShowAtlas.Checked
            LabelWebAtlasRafflesDeadline2.Visible = CheckBoxShowAtlas.Checked
            NumericUpDownWebatlasRafflesDeadline.Visible = CheckBoxShowAtlas.Checked
            NumericUpDownWebatlasDeadline.Visible = CheckBoxShowAtlas.Checked
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxRestrictAtlasToPremiumLine_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRestrictAtlasToPremiumLine.CheckedChanged
        If mAllowEvents Then
            Xl_LookupPremiumLine1.Visible = CheckBoxRestrictAtlasToPremiumLine.Checked
            SetDirty()
        End If
    End Sub



    Private Sub ButtonShowLangTexts_Click(sender As Object, e As EventArgs) Handles ButtonShowLangTexts.Click
        Dim oFrm As New Frm_ProductDescription(_Brand)
        oFrm.Show()
    End Sub
End Class
