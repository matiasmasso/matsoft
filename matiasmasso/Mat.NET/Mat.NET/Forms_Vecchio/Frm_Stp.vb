

Public Class Frm_Stp

    Private mStp As MaxiSrvr.Stp
    Private mInclouObsolets As Boolean
    Private mCancel As Boolean
    Private mChanged As Boolean
    Private mSortDirty As Boolean
    Private mDirtyThumbnail As Boolean
    Private mAllowEvents As Boolean
    Private mAllowUpdate As Boolean = BLL.BLLRol.AllowWrite(BLL.BLLSession.Current.User.Rol, "Cataleg")
    Private mLastMouseDownRectangle As System.Drawing.Rectangle
    Private CleanTab(20) As Boolean
    Private mDirtyDsc As Boolean
    Private mSelectionMode As BLL.Defaults.SelectionModes
    Private mDirtyFeatures As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        General
        Logistics
        Features
        Accessories
        Spares
        Downloads
        Galeria
        Movies
        Blogs
    End Enum

    Private Enum Cols
        Id
        Ico
        Nom
        Stk
        Obsoleto
    End Enum

    Private Enum ColsSpares
        Id
        Nom
        Stk
        Pn2
    End Enum

    Public Sub New(ByVal oStp As Stp, ByVal oSelectionMode As BLL.Defaults.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionMode
        mStp = oStp
        With mStp
            Me.Text = Me.Text & " " & .Id & " " & .Nom
            RefrescaTpa()
            TextBoxNom.Text = .Nom
            Xl_CnapLookup1.Cnap = .Cnap
            ComboBoxCodi.SelectedIndex = .Codi
            TextBoxDscESP.Text = .Dsc_ESP
            TextBoxDscCAT.Text = .Dsc_CAT
            TextBoxDscENG.Text = .Dsc_ENG
            CheckBoxDscPropagateToChildren.Checked = .Dsc_PropagateToChildren
            CheckBoxPortsUnit.Checked = .Ports
            CheckBoxWebEnabledPro.Checked = .WebEnabledPro
            CheckBoxWebEnabledConsumer.Checked = .WebEnabledConsumer
            CheckBoxBloqEShops.Checked = .BloqEShops
            CheckBoxNoStk.Checked = .NoStk
            CheckBoxHideStatistics.Checked = .HideStatistics
            CheckBoxObsoleto.Checked = .Obsoleto
            Xl_Color1.Color = .Color
            TextBoxClauPrefix.Text = .ClauPrefix
            Xl_Image_Thumbnail.Bitmap = mStp.Thumbnail

            If .Launchment Is Nothing Then
                CheckBoxLaunchment.Checked = False
                Xl_YearMonthLaunchment.Visible = False
            Else
                CheckBoxLaunchment.Checked = True
                Xl_YearMonthLaunchment.Visible = True
                Xl_YearMonthLaunchment.YearMonth = .Launchment
            End If

            If .Exists Then
                ButtonDel.Enabled = .AllowDelete
            End If
        End With

        mAllowEvents = True

    End Sub

    Public ReadOnly Property Changed() As Boolean
        Get
            Return mChanged
        End Get
    End Property


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        mCancel = True
        mChanged = False
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mChanged Or mDirtyFeatures Then
            With mStp
                .Nom = TextBoxNom.Text
                .Dsc_ESP = TextBoxDscESP.Text
                .Dsc_CAT = TextBoxDscCAT.Text
                .Dsc_ENG = TextBoxDscENG.Text

                .Cnap = Xl_CnapLookup1.Cnap
                .Codi = ComboBoxCodi.SelectedIndex
                .Dsc_PropagateToChildren = CheckBoxDscPropagateToChildren.Checked
                .NoStk = CheckBoxNoStk.Checked
                .Color = Xl_Color1.Color
                .Ports = CheckBoxPortsUnit.Checked
                .WebEnabledPro = CheckBoxWebEnabledPro.Checked
                .WebEnabledConsumer = CheckBoxWebEnabledConsumer.Checked
                .BloqEShops = CheckBoxBloqEShops.Checked
                .HideStatistics = CheckBoxHideStatistics.Checked
                .ClauPrefix = TextBoxClauPrefix.Text
                .Obsoleto = CheckBoxObsoleto.Checked
                If mDirtyThumbnail Then
                    .Thumbnail = Xl_Image_Thumbnail.Bitmap
                End If

                If CheckBoxLaunchment.Checked Then
                    .Launchment = Xl_YearMonthLaunchment.YearMonth
                Else
                    .Launchment = Nothing
                End If

                If Xl_ArtLogistics1.IsDirty Then
                    .Dimensions = Xl_ArtLogistics1.Dimensions
                    .CodiMercancia = Xl_ArtLogistics1.CodiMercancia
                End If

                Dim exs as New List(Of exception)
                If StpLoader.Update(mStp, exs) Then
                    If mDirtyFeatures Then
                        Dim oFeatures As List(Of DTOProductFeatureImage) = Xl_ProductFeaturedImages1.Values
                        Dim oProduct As New DTO.DTOProduct(mStp.Guid)
                        If Not BLL.BLLProductFeaturedImages.Update(oProduct, oFeatures, exs) Then
                            UIHelper.WarnError("error al desar les imatges")
                        End If
                    End If
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(mStp))
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "error al desar la categoría de producte")
                End If

            End With
        End If

    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mStp.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & mStp.Nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                mStp.Delete()
                MsgBox(mStp.Nom & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(mStp, New System.EventArgs)
                Me.Close()
            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
            End If
        Else
            MsgBox(mStp.Nom & " no esta buida." & vbCrLf & "Operació cancelada.", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub SetDirty()
        Dim BlEnableButtons As Boolean = True
        'If Not mAllowUpdate Then BlEnableButtons = False
        If Not mAllowEvents Then BlEnableButtons = False

        If BlEnableButtons Then
            mChanged = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxNom_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxNom.KeyPress
        Select Case e.KeyChar
            Case "&", ":", "/", "?"
                e.KeyChar = "-"
        End Select
    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
    TextBoxClauPrefix.TextChanged, _
    CheckBoxDscPropagateToChildren.CheckedChanged, _
    CheckBoxPortsUnit.CheckedChanged, _
    CheckBoxWebEnabledPro.CheckedChanged, _
    CheckBoxWebEnabledConsumer.CheckedChanged, _
    ComboBoxCodi.SelectedIndexChanged, _
    CheckBoxObsoleto.CheckedChanged, _
    CheckBoxNoStk.CheckedChanged, _
    CheckBoxHideStatistics.CheckedChanged, _
     CheckBoxBloqEShops.CheckedChanged, _
     Xl_CnapLookup1.AfterUpdate, _
     Xl_YearMonthLaunchment.AfterUpdate

        SetDirty()
    End Sub



    Private Sub DscChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxDscESP.TextChanged, _
    TextBoxDscCAT.TextChanged, _
    TextBoxDscENG.TextChanged, _
    Xl_ArtLogistics1.AfterUpdate
        mDirtyDsc = True
        SetDirty()
    End Sub

    Private Sub Xl_Color1_AfterUpdate(ByVal oColor As System.Drawing.Color) Handles Xl_Color1.AfterUpdate
        SetDirty()
    End Sub








#Region "Tpa"

    Private Sub RefrescaTpa()
        Dim oBrand As New DTOProductBrand(mStp.Tpa.Guid)

        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Brand As New Menu_ProductBrand(oBrand)
        AddHandler oMenu_Brand.AfterUpdate, AddressOf RefreshRequestTpa
        oContextMenu.Items.AddRange(oMenu_Brand.Range)

        LabelTpa.Text = mStp.Tpa.Nom
        LabelTpa.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequestTpa(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefrescaTpa()
    End Sub

    Private Sub Tpa_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        LabelTpa.DoubleClick
        Dim oFrm As New Frm_Tpa(mStp.Tpa)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestTpa
        oFrm.Show()
    End Sub

#End Region


    Private Sub Xl_Image_Thumbnail_AfterUpdate1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Image_Thumbnail.AfterUpdate
        mDirtyThumbnail = True
        SetDirty()
    End Sub



    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Logistics
                    Xl_ArtLogistics1.Target = mStp
                Case Tabs.Features
                    'Dim oFeatures As Features = StpFeaturesLoader.FromProduct(New Product(mStp), True)
                    'Xl_StpFeatures1.Load(oFeatures)
                    Dim oProduct As New DTOProduct(mStp.Guid)
                    Dim oFeatures As List(Of DTOProductFeatureImage) = BLL.BLLProductFeaturedImages.FromProduct(oProduct)
                    Xl_ProductFeaturedImages1.Load(oFeatures)
                Case Tabs.Accessories
                    LoadAccessories()
                Case Tabs.Spares
                    LoadSpares()
                Case Tabs.Downloads
                    LoadProductDownloads()
                Case Tabs.Galeria
                    LoadHighResImages()
                Case Tabs.Movies
                    Xl_ProductMovies1.Product = New Product(mStp)
                Case Tabs.Blogs
                    Xl_ProductBlogs1.Product = New Product(mStp)
            End Select
            CleanTab(iTab) = True
        End If
    End Sub

    Private Sub LoadHighResImages()
        Dim oProduct As New DTOProduct(mStp.Guid)
        Dim oValues As HighResImages = HighResImagesLoader.FromProductOrChildren(oProduct)
        Xl_HighResImages1.Load(oValues)
    End Sub

    Private Sub LoadAccessories()
        Xl_ArtsAccessories.SetAccessoriesFrom(New Product(mStp))
    End Sub

    Private Sub LoadSpares()
        Xl_ArtsSpares.SetSparesFromStp(New Product(mStp))
    End Sub

    Private Sub CheckBoxLaunchment_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLaunchment.CheckedChanged
        If mAllowEvents Then
            Xl_YearMonthLaunchment.Visible = CheckBoxLaunchment.Checked
            SetDirty()
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_onFileDropped(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.onFileDropped
        Dim oProduct As New DTOProductSku(mStp.Guid)
        Dim oDocFile As DTODocFile = e.Argument
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Product = oProduct
        oProductDownload.DocFile = oDocFile

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim oProduct As New DTOProductSku(mStp.Guid)
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Product = oProduct

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        LoadProductDownloads()
    End Sub

    Private Sub LoadProductDownloads()
        Dim oProduct As New DTOProductSku(mStp.Guid)
        Dim oDownloads As List(Of DTOProductDownload) = BLL.BLLProductDownloads.FromProductOrParent(oProduct)
        Xl_ProductDownloads1.Load(oDownloads)
    End Sub


    Private Sub Xl_ProductFeaturedImages1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ProductFeaturedImages1.AfterUpdate
        mDirtyFeatures = True
        SetDirty()
    End Sub

    Private Sub ControlChanged(sender As Object, e As MatEventArgs)

    End Sub
End Class
