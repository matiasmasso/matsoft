Imports Winforms

Public Class Frm_ContactPurchaseOrderItems
    Private _Contact As DTOContact
    Private _Items As List(Of DTOPurchaseOrderItem)

    Public Sub New(oContact As DTOContact)
        MyBase.New
        Me.InitializeComponent()

        _Contact = oContact
    End Sub

    Private Async Sub Frm_ContactPurchaseOrderItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            Me.Text = "Historial de " & _Contact.FullNom

            ProgressBar1.Visible = True
            _Items = Await FEB2.PurchaseOrderItems.All(exs, _Contact)
            ProgressBar1.Visible = False

            If exs.Count = 0 Then
            Else
                UIHelper.WarnError(exs)
            End If
            Xl_ContactPurchaseOrderItems1.Load(_Items)
            Xl_LookupProduct1.Load(Nothing, DTOProduct.SelectionModes.SelectAny, CustomLookup:=True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilter.CheckedChanged
        Select Case CheckBoxFilter.Checked
            Case True
                Xl_LookupProduct1.Visible = True
                Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
                If oProduct IsNot Nothing Then
                    Xl_ContactPurchaseOrderItems1.ProductFilter = oProduct
                End If
            Case Else
                Xl_LookupProduct1.Visible = False
                Xl_ContactPurchaseOrderItems1.ClearProductFilter()
        End Select
    End Sub


    Private Sub onProductSelected(sender As Object, e As MatEventArgs)
        Xl_ContactPurchaseOrderItems1.ProductFilter = e.Argument
        Xl_LookupProduct1.Product = e.Argument
    End Sub


    Private Async Sub Xl_LookupProduct1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.RequestToLookup
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProduct = e.Argument
        If oProduct Is Nothing Then
            Dim sGuid As String = UIHelper.GetSetting(DTOSession.Settings.Last_Product_Selected)
            If GuidHelper.IsGuid(sGuid) Then
                oProduct = Await FEB2.Product.Find(exs, New Guid(sGuid))
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                    Exit Sub
                End If
            End If
        End If

        Dim oCatalog = DTOPurchaseOrderItem.Catalog(_Items)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectAny, oProduct, oCustomCatalog:=oCatalog)
            AddHandler oFrm.OnItemSelected, AddressOf onProductSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_ContactPurchaseOrderItems1.Filter = e.Argument
    End Sub
End Class