

Public Class Frm_CliDtos

    Private _Customer As DTOCustomer
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCustomer As DTOCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _Customer = oCustomer
        BLLContact.Load(_Customer)
        BLLCustomer.Load(_Customer)
        Dim items As List(Of DTOProductDto) = BLLCustomerBrandDtos.All(oCustomer)

        If _Customer.GlobalDto > 0 Then
            RadioButtonDtoGlobal.Checked = True
            Xl_PercentDtoGlobal.Visible = True
            Xl_ProductDtos1.Visible = False
            Xl_PercentDtoGlobal.Value = _Customer.GlobalDto
        ElseIf items.Count > 0 Then
            RadioButtonDtoTpa.Checked = True
            Xl_PercentDtoGlobal.Visible = False
            Xl_ProductDtos1.Visible = True
        End If

        TextBoxCliNom.Text = _Customer.FullNom
        Xl_ProductDtos1.Load(items)
        _AllowEvents = True
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim items As List(Of DTOProductDto) = Nothing

        If RadioButtonDtoTpa.Checked Then
            _Customer.GlobalDto = 0
            items = Xl_ProductDtos1.Values

        ElseIf RadioButtonDtoGlobal.Checked Then
            _Customer.GlobalDto = Xl_PercentDtoGlobal.Value
            items = New List(Of DTOProductDto)
        End If

        Dim exs As New List(Of Exception)
        If BLLCustomerBrandDtos.Update(_Customer, items, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Customer))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub EnableButtons()
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                ButtonOk.Enabled = True
        End Select
    End Sub



    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonDtoGlobal.CheckedChanged, RadioButtonDtoTpa.CheckedChanged
        If _AllowEvents Then
            If RadioButtonDtoTpa.Checked Then
                Xl_ProductDtos1.Visible = True
                Xl_PercentDtoGlobal.Visible = False
            Else
                Xl_ProductDtos1.Visible = False
                Xl_PercentDtoGlobal.Visible = True
            End If
            EnableButtons()
        End If

    End Sub



    Private Sub Xl_ProductDtos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDtos1.RequestToAddNew
        Dim oFrm As New Frm_Products(, Frm_Products.SelModes.SelectProduct)
        AddHandler oFrm.onItemSelected, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(sender As Object, e As MatEventArgs)
        Dim items As List(Of DTOProductDto) = Xl_ProductDtos1.Values
        Dim oProduct As DTOProduct = e.Argument
        If Not items.Exists(Function(x) x.Product.Equals(oProduct)) Then
            Dim item As New DTOProductDto
            item.Product = oProduct
            items.Add(item)
            Xl_ProductDtos1.Load(items)
            EnableButtons()
        End If
    End Sub

    Private Sub Control_AfterUpdate(sender As Object, e As MatEventArgs) Handles _
        Xl_PercentDtoGlobal.AfterUpdate,
        Xl_ProductDtos1.AfterUpdate
        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub

End Class