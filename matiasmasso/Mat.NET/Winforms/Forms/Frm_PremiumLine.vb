Imports Winforms

Public Class Frm_PremiumLine

    Private _PremiumLine As DTOPremiumLine
    Private _AllEmailRecipients As IEnumerable(Of DTOUser)
    Private _AllEmailContacts As IEnumerable(Of DTOContact)
    Private _CustomersLoaded As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Productes
        Customers
        Mailings
    End Enum

    Public Sub New(value As DTOPremiumLine)
        MyBase.New()
        Me.InitializeComponent()
        _PremiumLine = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.PremiumLine.Load(_PremiumLine, exs) Then
            With _PremiumLine
                TextBoxNom.Text = .Nom
                DateTimePicker1.Value = .fch
                Xl_ProductCategories1.AllowRemoveOnContextMenu = True
                Xl_ProductCategories1.load(.Products)
                ButtonOk.Enabled = .IsNew
                If Current.Session.Rol.IsMainboard Then
                    ButtonDel.Enabled = Not .IsNew
                End If
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         DateTimePicker1.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _PremiumLine
            .nom = TextBoxNom.Text
            .fch = DateTimePicker1.Value
            .products = Xl_ProductCategories1.Values
        End With

        Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB2.PremiumLine.Update(_PremiumLine, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PremiumLine))
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
            If Await FEB2.PremiumLine.Delete(_PremiumLine, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PremiumLine))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_ProductCategories1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToAddNew
        'Dim oFrm As New Frm_Cataleg(, Frm_Cataleg.SelModes.SelectProductCategory)
        Dim oFrm As New Frm_ProductCategories(DTOProduct.SelectionModes.SelectCategory)
        AddHandler oFrm.onItemSelected, AddressOf onProductAdded
        oFrm.Show()
    End Sub

    Private Sub onProductAdded(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProductCategory = e.Argument
        Dim oProducts As List(Of DTOProductCategory) = Xl_ProductCategories1.Values
        Dim duplicated As Boolean = oProducts.Any(Function(x) x.Equals(oProduct))
        If Not duplicated Then
            oProducts.Add(oProduct)
            Xl_ProductCategories1.load(oProducts)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_PremiumCustomers1.Filter = e.Argument
    End Sub

    Private Sub Xl_PremiumCustomers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PremiumCustomers1.RequestToAddNew
        If _PremiumLine.IsNew Then
            UIHelper.WarnError("Cal desar primer la gama de productes")
        Else
            Dim oPremiumCustomer As New DTOPremiumCustomer
            With oPremiumCustomer
                .PremiumLine = _PremiumLine
                .codi = DTOPremiumCustomer.Codis.included
            End With
            Dim oFrm As New Frm_PremiumCustomer(oPremiumCustomer)
            AddHandler oFrm.AfterUpdate, AddressOf onCustomerAdded
            oFrm.Show()
        End If
    End Sub

    Private Async Sub onCustomerAdded(sender As Object, e As MatEventArgs)
        Await refrescaCustomers()
    End Sub

    Private Async Function refrescaCustomers() As Task
        Dim exs As New List(Of Exception)
        Dim oPremiumCustomers = Await FEB2.premiumCustomers.All(exs, _PremiumLine)
        If exs.Count = 0 Then
            Xl_PremiumCustomers1.Load(oPremiumCustomers)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Customers
                If Not _CustomersLoaded Then
                    Await refrescaCustomers()
                    _CustomersLoaded = True
                End If
            Case Tabs.Mailings
                Await LoadMailings()
        End Select
    End Sub

    Private Async Function LoadMailings() As Task
        Dim exs As New List(Of Exception)
        If _AllEmailRecipients Is Nothing Then
            _AllEmailRecipients = Await FEB2.PremiumLine.EmailRecipients(exs, _PremiumLine)
            _AllEmailContacts = _AllEmailRecipients.GroupBy(Function(x) x.Contact.Guid).Select(Function(y) y.First).Select(Function(z) z.Contact).ToList
        End If
        Xl_LookupAtlas1.load(_AllEmailContacts, New List(Of DTOCustomer))
        Xl_LookupCustomersxChannel1.load(_AllEmailContacts, New List(Of DTOCustomer))
    End Function

    Private Async Sub Xl_PremiumCustomers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PremiumCustomers1.RequestToRefresh
        Await refrescaCustomers()
    End Sub

    Private Async Sub Xl_ProductCategories1_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToRemove
        Dim oCategory As DTOProductCategory = e.Argument
        _PremiumLine.Products.Remove(oCategory)
        Dim exs As New List(Of Exception)
        If Await FEB2.PremiumLine.Update(_PremiumLine, exs) Then
            Xl_ProductCategories1.load(_PremiumLine.Products)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckboxFilterArea_CheckedChanged(sender As Object, e As EventArgs) Handles CheckboxFilterArea.CheckedChanged
        Xl_LookupAtlas1.Visible = CheckboxFilterArea.Checked
        If CheckboxFilterArea.Checked Then
            Xl_LookupCustomersxChannel1.load(Xl_LookupAtlas1.SelectedContacts, Xl_LookupCustomersxChannel1.SelectedItems)
        Else
            Xl_LookupAtlas1.Clear()
        End If
        DisplayMailingSummary()
    End Sub

    Private Sub CheckBoxFilterChannel_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilterChannel.CheckedChanged
        Xl_LookupCustomersxChannel1.Visible = CheckBoxFilterChannel.Checked
        If CheckBoxFilterChannel.Checked Then
        Else
            Xl_LookupCustomersxChannel1.Clear()
        End If
        DisplayMailingSummary()
    End Sub

    Private Function MailingContacts() As IEnumerable(Of DTOContact)
        Dim retval As IEnumerable(Of DTOContact) = Nothing
        If CheckBoxFilterChannel.Checked Then
            retval = Xl_LookupCustomersxChannel1.SelectedItems
        ElseIf CheckboxFilterArea.Checked Then
            retval = Xl_LookupAtlas1.SelectedContacts
        Else
            retval = _AllEmailContacts
        End If
        Return retval
    End Function

    Private Function MailingRecipients() As IEnumerable(Of DTOUser)
        Dim oContacts = MailingContacts()
        Dim retval = _AllEmailRecipients.Where(Function(x) oContacts.Any(Function(y) y.Equals(x.Contact))).ToList
        Return retval
    End Function

    Private Sub ButtonExcelMailing_Click(sender As Object, e As EventArgs) Handles ButtonExcelMailing.Click
        Dim exs As New List(Of Exception)
        Dim oRecipients = MailingRecipients()
        Dim oSheet = FEB2.PremiumLine.ExcelEmailRecipients(oRecipients)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LookupAtlas1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupAtlas1.AfterUpdate
        Xl_LookupCustomersxChannel1.load(e.Argument, Xl_LookupCustomersxChannel1.SelectedItems)
        DisplayMailingSummary()
    End Sub

    Private Sub DisplayMailingSummary()
        Dim oContacts = MailingContacts()
        Dim oRecipients = MailingRecipients()
        LabelSummary.Text = String.Format("{0} destinataris de {1} clients", oRecipients.Count, oContacts.Count)
    End Sub
End Class


