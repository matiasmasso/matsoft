Public Class Frm_ProductBrand
    Private _ProductBrand As DTOProductBrand
    Private _CategoriesLoaded As Boolean
    Private _ZonasLoaded As Boolean
    Private _ChannelsLoaded As Boolean
    Private _RepsLoaded As Boolean

    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Categories
        Zonas
        Channels
        Reps
    End Enum

    Public Sub New(value As DTOProductBrand)
        MyBase.New()
        Me.InitializeComponent()
        _ProductBrand = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ProductBrand.Load(_ProductBrand, exs) Then
            With _ProductBrand
                TextBoxNom.Text = .nom.Tradueix(Current.Session.Lang)
                Xl_ContactProveidor.Contact = .Proveidor
                ComboBoxDistribucio.SelectedIndex = .CodDist
                CheckBoxObsoleto.Checked = .Obsoleto
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged, _
         Xl_ContactProveidor.AfterUpdate, _
          ComboBoxDistribucio.SelectedIndexChanged, _
           CheckBoxObsoleto.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oProveidor As DTOProveidor = Nothing
        If Xl_ContactProveidor.Contact IsNot Nothing Then
            oProveidor = New DTOProveidor(Xl_ContactProveidor.Contact.Guid)
        End If
        With _ProductBrand
            .nom.Esp = TextBoxNom.Text
            .Proveidor = oProveidor
            .CodDist = ComboBoxDistribucio.SelectedIndex
            .Obsoleto = CheckBoxObsoleto.Checked
        End With
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.ProductBrand.Update(_ProductBrand, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductBrand))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.ProductBrand.Delete(_ProductBrand, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductBrand))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case DirectCast(TabControl1.SelectedIndex, Tabs)
            Case Tabs.Categories
                If Not _CategoriesLoaded Then
                    _CategoriesLoaded = True
                    Dim oCategories = Await FEB.ProductCategories.All(exs, _ProductBrand, IncludeObsolets:=True)
                    If exs.Count = 0 Then
                        Xl_ProductCategories1.load(oCategories)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Zonas
                If Not _ZonasLoaded Then
                    _ZonasLoaded = True
                    Dim oAreas = Await FEB.BrandAreas.All(_ProductBrand, exs)
                    If exs.Count = 0 Then
                        Xl_BrandAreas1.Load(oAreas)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Channels
                If Not _ChannelsLoaded Then
                    _ChannelsLoaded = True
                    Dim oProductChannels = Await FEB.ProductChannels.All(exs, _ProductBrand)
                    If exs.Count = 0 Then
                        Xl_ProductChannels1.Load(oProductChannels, Xl_ProductChannels.Modes.ChannelsPerProduct)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Reps
                If Not _RepsLoaded Then
                    _RepsLoaded = True
                    Dim oRepProducts = Await FEB.RepProducts.All(exs, _ProductBrand, True)
                    If exs.Count = 0 Then
                        Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByProduct)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If

        End Select
    End Sub
End Class