
Public Class Frm_Catalog
    Private _DefaultProducts As List(Of DTOProduct)
    Private _SelectionMode As DTOProduct.SelectionModes
    Private _IncludeObsoletos As Boolean
    Private _CustomCatalog As List(Of DTOProductBrand)

    Private WithEvents _Cache As Models.ClientCache
    Private _AllowEvents As Boolean

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)
    Public Event OnItemsSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.Browse, Optional oDefaultProducts As List(Of DTOProduct) = Nothing, Optional IncludeObsoletos As Boolean = False, Optional oCustomCatalog As List(Of DTOProductBrand) = Nothing)
        MyBase.New
        InitializeComponent()
        Me.MaximumSize = New Size(600, Integer.MaxValue) ' Screen.PrimaryScreen.WorkingArea.Height)
        _DefaultProducts = oDefaultProducts
        _SelectionMode = oSelectionMode
        _IncludeObsoletos = IncludeObsoletos
        _CustomCatalog = oCustomCatalog
    End Sub

    Private Async Sub Frm_Catalog_Load(sender As Object, e As EventArgs) Handles Me.Load
        ProgressBar1.Visible = True
        Await Refresca()
    End Sub

    Private Sub onCacheLoaded(sender As Object, e As EventArgs)
        Try
            Invoke(Sub() RefrescaAfterLoadingCache())
            'If InvokeRequired Then Invoke(Sub() RefrescaAfterLoadingCache()) Else RefrescaAfterLoadingCache()
        Catch ex As Exception
            'maybe the user closed the form in the meantime
        End Try
    End Sub

    Private Sub RefrescaAfterLoadingCache()
        ToolStripTextBoxFchLastUpdate.Text = _Cache.LastCatalogUpdate().ToString("dd/MM/yy HH:mm")
        Xl_Catalog1.Load(_Cache, HideObsoleteSkus.Checked, HideObsoleteBrands.Checked, _SelectionMode)
        ProgressBar1.Visible = False
        _AllowEvents = True
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)
        If exs.Count = 0 Then
            If _Cache.IsLoading Then
                AddHandler _Cache.AfterUpdate, AddressOf onCacheLoaded
            Else
                ToolStripTextBoxFchLastUpdate.Text = _Cache.LastCatalogUpdate().ToString("dd/MM/yy HH:mm")
                Xl_Catalog1.Load(_Cache, HideObsoleteSkus.Checked, HideObsoleteBrands.Checked, _SelectionMode)
                ProgressBar1.Visible = False
                _AllowEvents = True
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await Refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Catalog1.Filter = e.Argument
    End Sub

    Private Async Sub HideObsoleteSkusToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles HideObsoleteSkus.CheckedChanged
        If _AllowEvents Then
            ProgressBar1.Visible = True
            Application.DoEvents()
            Await Refresca()
            ProgressBar1.Visible = False
        End If
    End Sub

    Private Sub Xl_Catalog1_RequestToShowObsolets(sender As Object, e As MatEventArgs) Handles Xl_Catalog1.RequestToShowObsolets
        HideObsoleteSkus.Checked = Not HideObsoleteSkus.Checked
    End Sub

    Private Async Sub HideObsoleteBrandsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideObsoleteBrands.Click
        If _AllowEvents Then
            ProgressBar1.Visible = True
            Application.DoEvents()
            Await Refresca()
            ProgressBar1.Visible = False
        End If
    End Sub

    Private Sub ToolStripMenuItemHome_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemHome.Click
        Xl_Catalog1.Home()
    End Sub

    Private Sub Xl_Catalog1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Catalog1.OnItemSelected
        RaiseEvent OnItemSelected(sender, e)
        Me.Close()
    End Sub

    Private Sub Xl_Catalog1_OnItemsSelected(sender As Object, e As MatEventArgs) Handles Xl_Catalog1.OnItemsSelected
        RaiseEvent OnItemsSelected(sender, e)
        Me.Close()
    End Sub

    Private Async Sub Xl_Catalog1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Catalog1.RequestToRefresh
        Await Refresca()
    End Sub


End Class