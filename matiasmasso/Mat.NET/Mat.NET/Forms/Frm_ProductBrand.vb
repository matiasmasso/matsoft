Public Class Frm_ProductBrand
    Private _ProductBrand As DTOProductBrand
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Categories
        Zonas
        Reps
    End Enum

    Public Sub New(value As DTOProductBrand)
        MyBase.New()
        Me.InitializeComponent()
        _ProductBrand = value
        BLL.BLLProductBrand.Load(_ProductBrand)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _ProductBrand
            TextBoxNom.Text = .Nom
            Xl_ContactProveidor.Contact = .Proveidor
            ComboBoxDistribucio.SelectedIndex = .CodDist
            CheckBoxObsoleto.Checked = .Obsoleto
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
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

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductBrand
            .Nom = TextBoxNom.Text
            .Proveidor = Xl_ContactProveidor.Contact
            .CodDist = ComboBoxDistribucio.SelectedIndex
            .Obsoleto = CheckBoxObsoleto.Checked
        End With
        Dim exs As New List(Of Exception)
        If BLL.BLLProductBrand.Update(_ProductBrand, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductBrand))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLProductBrand.Delete(_ProductBrand, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductBrand))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.Categories
                Static CategoriesLoaded As Boolean
                If Not CategoriesLoaded Then
                    CategoriesLoaded = True
                    Dim oCategories As List(Of DTOProductCategory) = BLL.BLLProductCategories.All(_ProductBrand, True)
                    Xl_ProductCategories1.Load(oCategories)
                End If
            Case Tabs.Zonas
                Static ZonasLoaded As Boolean
                If Not ZonasLoaded Then
                    ZonasLoaded = True
                    Dim oAreas As List(Of DTOBrandArea) = BLL.BLLBrandAreas.All(_ProductBrand)
                    Xl_BrandAreas1.Load(oAreas)
                End If
            Case Tabs.Reps
                Static RepsLoaded As Boolean
                If Not RepsLoaded Then
                    RepsLoaded = True
                    Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(_ProductBrand, True)
                    Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByProduct)
                End If

        End Select
    End Sub
End Class