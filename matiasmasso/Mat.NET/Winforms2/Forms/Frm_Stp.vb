

Imports System.ComponentModel

Public Class Frm_Stp

    Private _Category As DTOProductCategory
    Private mInclouObsolets As Boolean
    Private mCancel As Boolean
    Private mSortDirty As Boolean
    Private mDirtyThumbnail As Boolean
    Private mAllowEvents As Boolean
    Private mLastMouseDownRectangle As System.Drawing.Rectangle
    Private CleanTab(20) As Boolean
    Private mDirtyDsc As Boolean
    Private mSelectionMode As DTO.Defaults.SelectionModes
    Private _FiltersLoaded As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        General
        Logistics
        Accessories
        Plugins
        Spares
        Downloads
        MediaResources
        Movies
        Blogs
        Filters
    End Enum


    Public Sub New(ByVal oCategory As DTOProductCategory, ByVal oSelectionMode As DTO.Defaults.SelectionModes)
        MyBase.New
        InitializeComponent()
        mSelectionMode = oSelectionMode
        _Category = oCategory

    End Sub

    Private Async Sub Frm_Stp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Category.IsNew Then
            Dim oProductChannels = Await FEB.ProductChannels.All(exs, _Category.brand)
            If exs.Count = 0 Then
                Xl_ProductChannels1.Load(oProductChannels, Xl_ProductChannels.Modes.ChannelsPerProduct)
            Else
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        Else
            If FEB.ProductCategory.Load(_Category, exs) Then
                Await RefrescaChannels()
            Else
                UIHelper.WarnError(exs)
            End If
        End If


        With _Category
            Me.Text = Me.Text & " " & _Category.brand.nom.Tradueix(Current.Session.Lang) & "/" & _Category.nom.Tradueix(Current.Session.Lang)
            Xl_LookupProductTpa1.Load(_Category.brand)
            TextBoxNomEsp.Text = .Nom.Tradueix(Current.Session.Lang)

            Xl_LookupCnap1.Load(.cNap)
            ComboBoxCodi.SelectedIndex = .codi
            CheckBoxDscPropagateToChildren.Checked = .dsc_PropagateToChildren
            CheckBoxWebEnabledPro.Checked = .enabledxPro
            CheckBoxWebEnabledConsumer.Checked = .enabledxConsumer
            CheckBoxNoStk.Checked = .NoStk
            CheckBoxIsBundle.Checked = .IsBundle
            CheckBoxObsoleto.Checked = .obsoleto
            Xl_LookupCountryMadeIn.Country = DTOProductCategory.MadeInOrInherited(_Category)
            Xl_Image_Image.LoadAsync(_Category.ImageUrl())

            If .launchment Is Nothing Then
                CheckBoxLaunchment.Checked = False
                Xl_YearMonthLaunchment.Visible = False
            Else
                CheckBoxLaunchment.Checked = True
                Xl_YearMonthLaunchment.Visible = True
                Xl_YearMonthLaunchment.YearMonth = .launchment
            End If

            If .hideUntil <> Nothing Then
                CheckBoxHideUntil.Checked = True
                DateTimePickerHideUntil.Visible = True
                DateTimePickerHideUntil.Value = .hideUntil
            End If

            ButtonDel.Enabled = Not .IsNew
        End With


        mAllowEvents = True
    End Sub



    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        With _Category
            .brand = Xl_LookupProductTpa1.Brand
            .cNap = Xl_LookupCnap1.Cnap
            .codi = ComboBoxCodi.SelectedIndex
            .dsc_PropagateToChildren = CheckBoxDscPropagateToChildren.Checked
            .noStk = CheckBoxNoStk.Checked
            .enabledxPro = CheckBoxWebEnabledPro.Checked
            .EnabledxConsumer = CheckBoxWebEnabledConsumer.Checked
            .IsBundle = CheckBoxIsBundle.Checked
            .obsoleto = CheckBoxObsoleto.Checked
            If Xl_LookupCountryMadeIn.Country Is Nothing Then
                .madeIn = .brand.madeIn
            Else
                If Xl_LookupCountryMadeIn.Country.Equals(.brand.madeIn) Then
                    .madeIn = Nothing
                Else
                    .madeIn = Xl_LookupCountryMadeIn.Country
                End If
            End If


            If mDirtyThumbnail Then
                .Image = LegacyHelper.ImageHelper.Converter(Xl_Image_Image.Bitmap)
            End If

            If CheckBoxLaunchment.Checked Then
                .launchment = Xl_YearMonthLaunchment.YearMonth
            Else
                .launchment = Nothing
            End If

            If CheckBoxHideUntil.Checked Then
                .hideUntil = DateTimePickerHideUntil.Value
            Else
                .hideUntil = Nothing
            End If

            If Xl_ArtLogistics1.IsDirty Then
                .dimensionL = Xl_ArtLogistics1.Dimensions.DimensionLargo
                .dimensionW = Xl_ArtLogistics1.Dimensions.DimensionAncho
                .dimensionH = Xl_ArtLogistics1.Dimensions.DimensionAlto
                .noDimensions = Xl_ArtLogistics1.Dimensions.NoDimensions
                .codiMercancia = Xl_ArtLogistics1.Dimensions.CodiMercancia
                .forzarInnerPack = Xl_ArtLogistics1.Dimensions.ForzarInnerPack
                .innerPack = Xl_ArtLogistics1.Dimensions.InnerPack
                .outerPack = Xl_ArtLogistics1.Dimensions.OuterPack
                .kgBrut = Xl_ArtLogistics1.Dimensions.KgBrut
                .kgNet = Xl_ArtLogistics1.Dimensions.KgNet
                .volumeM3 = Xl_ArtLogistics1.Dimensions.M3
            End If

            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.ProductCategory.Update(_Category, exs) Then
                If Xl_CheckedFilters1.isDirty Then
                    If Await FEB.FilterTargets.Update(exs, _Category, Xl_CheckedFilters1.SelectedValues) Then
                        Xl_CheckedFilters1.isDirty = False
                    End If
                End If
            End If



        End With

        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Category))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la categoría de producte")
        End If

    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem " & _Category.nom.Tradueix(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.ProductCategory.Delete(_Category, exs) Then
                MsgBox(_Category.nom.Tradueix(Current.Session.Lang) & " eliminada", MsgBoxStyle.Information, "MAT.NET")
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
        Dim BlEnableButtons As Boolean = True
        If Not mAllowEvents Then BlEnableButtons = False

        If BlEnableButtons Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxNom_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxNomEsp.KeyPress
        Select Case e.KeyChar
            Case "&", ":", "/", "?"
                e.KeyChar = "-"
        End Select
    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNomEsp.TextChanged,
    CheckBoxDscPropagateToChildren.CheckedChanged,
    CheckBoxWebEnabledPro.CheckedChanged,
    CheckBoxWebEnabledConsumer.CheckedChanged,
    ComboBoxCodi.SelectedIndexChanged,
    CheckBoxObsoleto.CheckedChanged,
    CheckBoxNoStk.CheckedChanged,
    CheckBoxIsBundle.CheckedChanged,
     Xl_YearMonthLaunchment.AfterUpdate,
     Xl_LookupProductTpa1.AfterUpdate,
      Xl_LookupCountryMadeIn.AfterUpdate,
      Xl_LookupCnap1.AfterUpdate,
      DateTimePickerHideUntil.ValueChanged

        SetDirty()
    End Sub



    Private Sub DscChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_ArtLogistics1.AfterUpdate
        mDirtyDsc = True
        SetDirty()
    End Sub


    Private Sub Xl_Image_Thumbnail_AfterUpdate1(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            Xl_Image_Image.AfterUpdate
        mDirtyThumbnail = True
        SetDirty()
    End Sub



    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Logistics
                    Xl_ArtLogistics1.Target = _Category
                Case Tabs.Accessories
                    Await LoadAccessories()
                Case Tabs.Plugins
                    Await LoadPlugins()
                Case Tabs.Spares
                    Await LoadSpares()
                Case Tabs.Downloads
                    Await LoadProductDownloads()
                Case Tabs.MediaResources
                    Await LoadMediaResources()
                Case Tabs.Movies
                    Await LoadMovies()
                Case Tabs.Blogs
                    Await LoadBloggerPosts()
                Case Tabs.Filters
                    Await LoadFilters()
            End Select
            CleanTab(iTab) = True
        End If
    End Sub

    Private Async Function LoadMediaResources() As Task
        Dim exs As New List(Of Exception)
        ProgressBarMediaResources.Visible = True
        Dim items = Await FEB.MediaResources.AllWithThumbnails(exs, _Category)
        ProgressBarMediaResources.Visible = False
        If exs.Count = 0 Then
            Xl_MediaResources1.Load(_Category, items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Function LoadAccessories() As Task
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _Category, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_ProductSkusExtendedAccessories.Load(oAccessories,,, DTOProduct.Relateds.Accessories)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub LoadPlugins(sender As Object, e As MatEventArgs)
        Await LoadPlugins()
    End Sub

    Private Async Function LoadPlugins() As Task
        Dim exs As New List(Of Exception)
        Dim oPlugins = Await FEB.ProductPlugins.All(exs, _Category)
        If exs.Count = 0 Then
            Xl_ProductPlugins1.Load(oPlugins)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_ProductPlugins1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductPlugins1.RequestToRefresh
        Await LoadPlugins()
    End Sub


    Private Async Function LoadSpares() As Task
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Spares, _Category, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_ProductSkusExtendedSpares.Load(oSpares,,, DTOProduct.Relateds.Spares)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub CheckBoxLaunchment_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLaunchment.CheckedChanged
        If mAllowEvents Then
            Xl_YearMonthLaunchment.Visible = CheckBoxLaunchment.Checked
            SetDirty()
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_onFileDropped(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.onFileDropped
        Dim oDocFile As DTODocFile = e.Argument
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.target = _Category
        oProductDownload.docFile = oDocFile

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim exs As New List(Of Exception)
        Dim oProductDownload = DTOProductDownload.Factory(_Category, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_ProductDownload(oProductDownload)
            AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        Await LoadProductDownloads()
    End Sub

    Private Async Sub LoadProductDownloads(sender As Object, e As MatEventArgs)
        Await LoadProductDownloads()
    End Sub

    Private Async Function LoadProductDownloads() As Task
        Dim exs As New List(Of Exception)
        Dim oDownloads As List(Of DTOProductDownload) = Await FEB.Downloads.FromProductOrParent(exs, _Category)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub Xl_MediaResources1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MediaResources1.RequestToRefresh
        Await LoadMediaResources()
    End Sub


    Private Sub Xl_ProductSkusExtendedAccessories_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedAccessories.RequestToAddNewSku
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku, _Category.brand)
        AddHandler oFrm.OnItemSelected, AddressOf onAccessorySelected
        oFrm.Show()
    End Sub

    Private Async Sub onAccessorySelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _Category, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oAccessory As DTOProductSku = e.Argument
            If oAccessories.Exists(Function(x) x.Equals(oAccessory)) Then
                MsgBox(oAccessory.NomLlarg.Tradueix(Current.Session.Lang) & " ja está registrat com a accessori de " & _Category.Nom.Tradueix(Current.Session.Lang))
            Else
                oAccessories.Add(e.Argument)
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Accessories, _Category, oAccessories) Then
                    Await LoadAccessories()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedSpares_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedSpares.RequestToAddNewSku
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Spares, _Category, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oSpare As DTOProductSku = e.Argument
            If oSpares.Exists(Function(x) x.Equals(oSpare)) Then
                MsgBox(oSpare.NomLlarg.Tradueix(Current.Session.Lang) & " ja está registrat com a recanvi de " & _Category.Nom.Tradueix(Current.Session.Lang))
            Else
                oSpares.Add(e.Argument)
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Spares, _Category, oSpares) Then
                    Await LoadSpares()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedAccessories_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedAccessories.RequestToRemove
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _Category, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oAccessory As DTOProductSku = e.Argument
            If oAccessories.Exists(Function(x) x.Equals(oAccessory)) Then
                oAccessories.RemoveAll(Function(x) x.Equals(oAccessory))
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Accessories, _Category, oAccessories) Then
                    Await LoadAccessories()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox(oAccessory.NomLlarg.Tradueix(Current.Session.Lang) & " no estava registrat com a accessori de " & _Category.Nom.Tradueix(Current.Session.Lang))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedSpares_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedSpares.RequestToRemove
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Spares, _Category, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oSpare As DTOProductSku = e.Argument
            If oSpares.Exists(Function(x) x.Equals(oSpare)) Then
                oSpares.RemoveAll(Function(x) x.Equals(oSpare))
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Spares, _Category, oSpares) Then
                    Await LoadSpares()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox(oSpare.NomLlarg.Tradueix(Current.Session.Lang) & " no estava registrat com a recanvi de " & _Category.Nom.Tradueix(Current.Session.Lang))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Xl_ProductChannels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToAddNew
        Dim item As New DTOProductChannel
        With item
            .Product = _Category
            _Category.Nom.Esp = TextBoxNomEsp.Text
            .Cod = DTOProductChannel.Cods.Inclou
        End With
        Dim oFrm As New Frm_ProductChannel(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaChannels
        oFrm.Show()
    End Sub

    Private Async Sub LoadMovies(sender As Object, e As MatEventArgs)
        Await LoadMovies()
    End Sub
    Private Async Function LoadMovies() As Task
        Dim exs As New List(Of Exception)

        Dim oMovies = Await FEB.YouTubeMovies.All(exs, Current.Session.User, _Category)
        If exs.Count = 0 Then
            Xl_ProductMovies1.Load(oMovies)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_ProductChannels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToRefresh
        Await RefrescaChannels()
    End Sub

    Private Async Sub RefrescaChannels(sender As Object, e As MatEventArgs)
        Await RefrescaChannels()
    End Sub

    Private Async Function RefrescaChannels() As Task
        Dim exs As New List(Of Exception)
        Dim oProductChannels As List(Of DTOProductChannel) = Await FEB.ProductChannels.All(exs, _Category)
        If exs.Count = 0 Then
            Xl_ProductChannels1.Load(oProductChannels, Xl_ProductChannels.Modes.ChannelsPerProduct)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_ProductMovies1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductMovies1.RequestToAddNew
        Dim oMovie = DTOYouTubeMovie.Factory(_Category)
        Dim oFrm As New Frm_Youtube(oMovie)
        AddHandler oFrm.AfterUpdate, AddressOf LoadMovies
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductMovies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductMovies1.RequestToRefresh
        Await LoadMovies()
    End Sub

    Private Sub Xl_ProductBloggerPosts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductBloggerPosts1.RequestToAddNew
        Dim oBloggerPost As New DTOBloggerPost()
        With oBloggerPost
            .Products.Add(_Category)
            .Fch = DTO.GlobalVariables.Today()
        End With
        Dim oFrm As New Frm_BloggerPost(oBloggerPost)
        AddHandler oFrm.AfterUpdate, AddressOf LoadBloggerPosts
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductBloggerPosts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductBloggerPosts1.RequestToRefresh
        Await LoadBloggerPosts()
    End Sub

    Private Async Sub LoadBloggerPosts(sender As Object, e As MatEventArgs)
        Await LoadBloggerPosts()
    End Sub

    Private Async Function LoadBloggerPosts() As Task
        Dim exs As New List(Of Exception)
        Dim oBlogs = Await FEB.BloggerPosts.FromProductOrParent(_Category, exs)
        If exs.Count = 0 Then
            Xl_ProductBloggerPosts1.Load(oBlogs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadFilters() As Task
        Dim exs As New List(Of Exception)
        If Not _FiltersLoaded Then
            Dim oAllFilters = Await FEB.Filters.All(exs)
            If exs.Count = 0 Then
                Dim oCheckedItems = Await FEB.FilterTargets.All(exs, _Category)
                If exs.Count = 0 Then
                    Xl_CheckedFilters1.Load(oAllFilters, oCheckedItems)
                    _FiltersLoaded = True
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub CheckBoxHideUntil_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHideUntil.CheckedChanged
        If mAllowEvents Then
            DateTimePickerHideUntil.Visible = CheckBoxHideUntil.Checked
            SetDirty()
        End If
    End Sub

    Private Sub Xl_ProductSkusExtendedRelateds_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductPlugins1.RequestToAddNew
        Dim oProductPlugin = DTOProductPlugin.Factory(_Category, Current.Session.User)
        Dim oFrm As New Frm_ProductPlugin(oProductPlugin)
        AddHandler oFrm.AfterUpdate, AddressOf LoadPlugins
        oFrm.Show()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        mCancel = True
        Me.Close()
    End Sub


    Private Sub Xl_CheckedFilters1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CheckedFilters1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub Frm_Stp_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'If Xl_CheckedFilters1.isDirty Then
        'Dim rc = MsgBox("tanquem sense desar els canvis en els filtres?", MsgBoxStyle.OkCancel)
        'e.Cancel = (rc = MsgBoxResult.Cancel)
        'End If
    End Sub

    Private Sub ButtonShowLangTexts_Click(sender As Object, e As EventArgs) Handles ButtonShowLangTexts.Click
        Dim oFrm As New Frm_ProductDescription(_Category)
        oFrm.Show()
    End Sub


End Class
