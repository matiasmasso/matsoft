Public Class Frm_CustomerProducts
    Private _Customer As DTOCustomer
    Private _Sku As DTOProductSku
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _Modalitat As Modalitats
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Modalitats
        PerCustomer
        PerSku
    End Enum

    Public Sub New(oCustomer As DTOCustomer, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Customer = oCustomer
        _SelectionMode = oSelectionMode
        _Modalitat = Modalitats.PerCustomer
        Me.InitializeComponent()
    End Sub

    Public Sub New(oSku As DTOProductSku, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Sku = oSku
        _SelectionMode = oSelectionMode
        _Modalitat = Modalitats.PerSku
        Me.InitializeComponent()
    End Sub


    Private Async Sub Frm_CustomerProducts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_CustomerProducts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CustomerProducts1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_CustomerProducts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CustomerProducts1.RequestToAddNew
        Dim oCustomerProduct As New DTOCustomerProduct
        Select Case _Modalitat
            Case Modalitats.PerCustomer
                oCustomerProduct.Customer = _Customer
            Case Modalitats.PerSku
                oCustomerProduct.Sku = _Sku
        End Select
        Dim oFrm As New Frm_CustomerProduct(oCustomerProduct)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CustomerProducts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CustomerProducts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oCustomerProducts = Await FEB2.CustomerProducts.All(exs, _Customer, _Sku)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Xl_CustomerProducts1.Load(oCustomerProducts, _Modalitat, , _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_CustomerProducts1_RequestToImportExcel(sender As Object, e As MatEventArgs) Handles Xl_CustomerProducts1.RequestToImportExcel
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Excel Ean/ref"
            .Filter = "fitxers Excel|*.xlsx"
            If .ShowDialog Then
                If .FileName > "" Then
                    Dim exs As New List(Of Exception)
                    If FileSystemHelper.IsFileLocked(.FileName, IO.FileMode.Open, IO.FileAccess.Read, exs) Then
                        UIHelper.WarnError(exs)
                    Else
                        'Dim oExcel  = MatExcel.Read2(.FileName)
                        Dim sFields() As String = {"Ean", "Ref.Client"}
                        Dim oFrm As New Frm_ExcelColumsMapping(sFields, .FileName)
                        AddHandler oFrm.AfterUpdate, AddressOf Do_ImportExcel
                        oFrm.Show()
                    End If
                End If
            End If
        End With
    End Sub

    Private Async Sub Do_ImportExcel(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = e.Argument
        Dim oSkus As New List(Of DTOProductSku)
        For Each oRow As MatHelperStd.ExcelHelper.Row In oSheet.Rows
            If IsNumeric(oRow.Cells(0).Content) Then
                If Not String.IsNullOrEmpty(oRow.Cells(1).Content) Then
                    Dim oEan = DTOEan.Factory(oRow.Cells(0).Content)
                    Dim oSku = Await FEB2.ProductSku.FromEan(exs, oEan)
                    If exs.Count = 0 Then
                        If oSku IsNot Nothing Then
                            oSku.refCustomer = oRow.Cells(1).Content
                            oSkus.Add(oSku)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        Next

        For Each oSku As DTOProductSku In oSkus
            If Not Await FEB2.CustomerProduct.SaveIfMissing(_Customer, oSku, oSku.RefCustomer, exs) Then
                exs.Add(New Exception("error al desar " & oSku.RefCustomer))
            End If
        Next
        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_CustomerProducts1.Filter = e.Argument
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oSheet As New MatHelperStd.ExcelHelper.Sheet("referencias " & _Customer.FullNom)
        With oSheet
            .AddColumn("Referencia")
            .AddColumn("EAN 13")
            .AddColumn("producto")
        End With
        For Each oProduct In Xl_CustomerProducts1.FilteredValues
            Dim oRow As MatHelperStd.ExcelHelper.Row = oSheet.AddRow
            oRow.AddCell(oProduct.Ref)
            oRow.AddCell(oProduct.Sku.Ean13.Value)
            oRow.addCell(oProduct.sku.nomLlarg.Tradueix(Current.Session.Lang))
        Next

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class