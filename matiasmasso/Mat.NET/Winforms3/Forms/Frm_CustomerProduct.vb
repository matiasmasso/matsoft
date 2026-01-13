

Public Class Frm_CustomerProduct
    Private _CustomerProduct As DTOCustomerProduct
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCustomerProduct)
        MyBase.New()
        Me.InitializeComponent()
        _CustomerProduct = value

    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.CustomerProduct.Load(_CustomerProduct, exs) Then
            With _CustomerProduct
                Xl_Contact21.Contact = .Customer
                Dim oProducts As New List(Of DTOProduct)
                If .Sku IsNot Nothing Then oProducts.Add(.Sku)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                TextBoxRef.Text = .Ref
                Xl_DUN141.Value = .DUN14
                TextBoxDsc.Text = .Dsc
                TextBoxColor.Text = .Color
                Xl_LookupEciDept1.EciDept = .EciDept
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate,
         Xl_LookupProduct1.AfterUpdate,
         TextBoxRef.TextChanged,
          Xl_DUN141.AfterUpdate,
           TextBoxDsc.TextChanged,
            TextBoxColor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _CustomerProduct
            .Customer = New DTOCustomer(Xl_Contact21.Contact.Guid)
            .Sku = Xl_LookupProduct1.Product
            .Ref = TextBoxRef.Text
            .DUN14 = Xl_DUN141.Value
            .Dsc = TextBoxDsc.Text
            .Color = TextBoxColor.Text
            .EciDept = Xl_LookupEciDept1.EciDept
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.CustomerProduct.Update(_CustomerProduct, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerProduct))
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
            If Await FEB.CustomerProduct.Delete(_CustomerProduct, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerProduct))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupEciDept1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupEciDept1.RequestToLookup
        Dim oFrm As New Frm_EciDepts(Xl_LookupEciDept1.EciDept, Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf onDeptUpdate
        oFrm.Show()
    End Sub

    Private Sub onDeptUpdate(sender As Object, e As MatEventArgs)
        Xl_LookupEciDept1.EciDept = e.Argument
        ButtonOk.Enabled = True
    End Sub
End Class


