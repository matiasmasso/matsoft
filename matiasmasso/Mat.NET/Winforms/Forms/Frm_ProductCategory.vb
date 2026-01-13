Public Class Frm_ProductCategory

    Private _ProductCategory As DTOProductCategory
    Private _SkusLoaded As Boolean
    Private _AccessoriesLoaded As Boolean
    Private _SparesLoaded As Boolean
    Private _FiltersLoaded As Boolean

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Skus
        Accessories
        Spares
        Filters
    End Enum

    Public Sub New(value As DTOProductCategory)
        MyBase.New()
        Me.InitializeComponent()
        _ProductCategory = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _ProductCategory
            TextBoxNom.Text = .nom.Tradueix(Current.Session.Lang)
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

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductCategory
            .Nom.Esp = TextBoxNom.Text
            .Brand = Xl_LookupProductBrand.Product
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.ProductCategory.Update(_ProductCategory, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductCategory))
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
        Dim rc As MsgBoxResult = MsgBox("eliminem " & _ProductCategory.nom.Tradueix(Current.Session.Lang) & "?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.ProductCategory.Delete(_ProductCategory, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductCategory))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la categoría")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case DirectCast(TabControl1.SelectedIndex, Tabs)
            Case Tabs.Skus
                If Not _SkusLoaded Then
                    _SkusLoaded = True
                    Dim oSkus = Await FEB2.ProductSkus.All(exs, _ProductCategory, GlobalVariables.Emp.Mgz, True)
                    If exs.Count = 0 Then
                        Xl_ProductSkus1.Load(oSkus)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.Accessories
                If Not _AccessoriesLoaded Then
                    _AccessoriesLoaded = True
                    'Dim oAreas As List(Of DTOBrandArea) = BLL.BLLBrandAreas.All(_ProductBrand)
                    'Xl_BrandAreas1.Load(oAreas)
                End If
            Case Tabs.Spares
                If Not _SparesLoaded Then
                    _SparesLoaded = True
                    'Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(_ProductBrand, True)
                    'Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByProduct)
                End If
            Case Tabs.Filters
                If Not _FiltersLoaded Then
                    Dim oAllFilters = Await FEB2.Filters.All(exs)
                    If exs.Count = 0 Then
                        Dim oCheckedItems = Await FEB2.FilterTargets.All(exs, _ProductCategory)
                        If exs.Count = 0 Then

                            _FiltersLoaded = True
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

End Class


