Public Class Frm_ProductCategory

    Private _ProductCategory As DTOProductCategory
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Skus
        Accessories
        Spares
    End Enum

    Public Sub New(value As DTOProductCategory)
        MyBase.New()
        Me.InitializeComponent()
        _ProductCategory = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _ProductCategory
            TextBoxNom.Text = .Nom
            Xl_LookupProductBrand.Product = .Brand
            CheckBoxObsoleto.Checked = .Obsoleto

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
         TextBoxNom.TextChanged, _
          Xl_LookupProductBrand.AfterUpdate, _
           CheckBoxObsoleto.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductCategory
            .Nom = TextBoxNom.Text
            .Brand = Xl_LookupProductBrand.Product
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLProductCategory.Update(_ProductCategory, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductCategory))
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
        Dim rc As MsgBoxResult = MsgBox("eliminem " & _ProductCategory.Nom & "?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLProductCategory.Delete(_ProductCategory, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductCategory))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la categoría")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.Skus
                Static SkusLoaded As Boolean
                If Not SkusLoaded Then
                    SkusLoaded = True
                    Dim oSkus As List(Of DTOProductSku) = BLL.BLLProductSkus.All(_ProductCategory, True)
                    Xl_ProductSkus1.Load(oSkus)
                End If
            Case Tabs.Accessories
                Static AccessoriesLoaded As Boolean
                If Not AccessoriesLoaded Then
                    AccessoriesLoaded = True
                    'Dim oAreas As List(Of DTOBrandArea) = BLL.BLLBrandAreas.All(_ProductBrand)
                    'Xl_BrandAreas1.Load(oAreas)
                End If
            Case Tabs.Spares
                Static SparesLoaded As Boolean
                If Not SparesLoaded Then
                    SparesLoaded = True
                    'Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(_ProductBrand, True)
                    'Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByProduct)
                End If

        End Select
    End Sub
End Class


