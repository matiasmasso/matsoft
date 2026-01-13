Imports Winforms

Public Class Frm_ProductSku

    Private _ProductSku As DTOProductSku
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProductSku)
        MyBase.New()
        Me.InitializeComponent()
        _ProductSku = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _ProductSku
            Dim exs As New List(Of Exception)
            If FEB2.ProductSku.Load(_ProductSku, exs, oMgz:=GlobalVariables.Emp.Mgz) Then
                Me.Text = .id & " " & .nomLlarg.Tradueix(Current.Session.Lang)
                If .Category IsNot Nothing Then
                    TextBoxBrand.Text = .category.brand.nom.Tradueix(Current.Session.Lang)
                    Xl_LookupCategory.Load(.Category, DTOProduct.SelectionModes.SelectCategory, True)
                End If
                TextBoxNomCurt.Text = .nom.Tradueix(Current.Session.Lang)
                TextBoxNomLlarg.Text = .nomLlarg.Tradueix(Current.Session.Lang)
                TextBoxRefProveidor.Text = .RefProveidor
                If .Ean13 IsNot Nothing Then
                    TextBoxEAN.Text = .Ean13.Value
                End If
                TextBoxNomProveidor.Text = .NomProveidor
                CheckBoxObsoleto.Checked = .Obsoleto

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            Else
                UIHelper.WarnError(exs)
            End If
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomCurt.TextChanged,
         TextBoxNomLlarg.TextChanged,
          TextBoxRefProveidor.TextChanged,
           TextBoxEAN.TextChanged,
            TextBoxNomProveidor.TextChanged,
             CheckBoxObsoleto.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductSku
            .Category = Xl_LookupCategory.Product
            .nom.Esp = TextBoxNomCurt.Text
            .nomLlarg.Esp = TextBoxNomLlarg.Text
            .RefProveidor = TextBoxRefProveidor.Text
            If TextBoxEAN.Text = "" Then
                .Ean13 = Nothing
            Else
                .Ean13 = DTOEan.Factory(TextBoxEAN.Text)
            End If
            .NomProveidor = TextBoxNomProveidor.Text
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.ProductSku.Update(_ProductSku, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductSku))
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
            If Await FEB2.ProductSku.Delete(_ProductSku, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductSku))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub onCategorySelected(sender As Object, e As MatEventArgs)
        Dim oCategory As DTOProductCategory = e.Argument
        TextBoxBrand.Text = oCategory.brand.nom.Tradueix(Current.Session.Lang)
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub Xl_LookupCategory_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupCategory.RequestToLookup
        Dim exs As New List(Of Exception)
        Dim oCatalog = Await FEB2.ProductCategories.CompactTree(exs, GlobalVariables.Emp, Current.Session.Lang)
        Dim oBrand = oCatalog.FirstOrDefault(Function(x) x.Equals(_ProductSku.Category.Brand))
        Dim oSingleBrandCatalog = {oBrand}.ToList

        Dim oFrm As New Frm_ProductCategories(DTOProduct.SelectionModes.SelectCategory, _ProductSku.Category, oCatalog:=oSingleBrandCatalog)
        AddHandler oFrm.OnItemSelected, AddressOf onCategorySelected
        oFrm.Show()
    End Sub

    Private Sub ButtonNomCurt_Click(sender As Object, e As EventArgs) Handles ButtonNomCurt.Click, ButtonNomLlarg.Click
        Dim oFrm As New Frm_ProductDescription(_ProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaLangTexts
        oFrm.Show()
    End Sub

    Private Sub refrescaLangTexts(sender As Object, e As MatEventArgs)
        _ProductSku = e.Argument
        TextBoxNomCurt.Text = _ProductSku.Nom.Tradueix(Current.Session.Lang)
        TextBoxNomLlarg.Text = _ProductSku.NomLlarg.Tradueix(Current.Session.Lang)

    End Sub
End Class


