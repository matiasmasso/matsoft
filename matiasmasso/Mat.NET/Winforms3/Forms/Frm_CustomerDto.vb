Public Class Frm_CustomerDto
    Private _CustomerDto As DTOCustomerTarifaDto
    Private _AllowEvents As Boolean
    Private _Iva As DTOTax
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCustomerTarifaDto)
        MyBase.New()
        Me.InitializeComponent()
        _CustomerDto = value
        _Iva = DTOTax.closest(DTOTax.Codis.iva_Standard, DTO.GlobalVariables.Today())
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.CustomerTarifaDto.Load(_CustomerDto, exs) Then
            With _CustomerDto
                Me.Text = .CustomerOrChannelNom
                If _CustomerDto.Src = DTOCustomerTarifaDto.Srcs.Client Then
                    RadioButtonBrand.Checked = True
                End If
                DateTimePicker1.Value = .Fch
                If .Product Is Nothing Then
                    RadioButtonAllBrands.Checked = True
                Else
                    RadioButtonBrand.Checked = True
                    Dim oProducts As New List(Of DTOProduct)
                    If .Product IsNot Nothing Then oProducts.Add(.Product)
                    Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                    Xl_LookupProduct1.Enabled = True
                End If

                Xl_PercentDtoBrut.Load(.Dto, 2)
                If .Dto <> 0 Then
                    Xl_PercentDtoNet.Load(Net(.Dto), 2)
                End If
                TextBoxObs.Text = .Obs

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) _
       Handles DateTimePicker1.ValueChanged,
         Xl_LookupProduct1.AfterUpdate,
           TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _CustomerDto
            .Fch = DateTimePicker1.Value
            If RadioButtonAllBrands.Checked Then
                .Product = Nothing
            Else
                .Product = Xl_LookupProduct1.Product
            End If
            .Dto = Xl_PercentDtoBrut.Value
            .Obs = TextBoxObs.Text
        End With

        'If TypeOf _CustomerDto.CustomerOrChannel Is DTOCustomer And Xl_LookupProduct1.Product Is Nothing Then
        ' UIHelper.WarnError("Cal sel·leccionar una marca comercial")
        'Else
        Dim exs As New List(Of Exception)
        If Await FEB.CustomerTarifaDto.Update(_CustomerDto, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerDto))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
        'End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.CustomerTarifaDto.Delete(_CustomerDto, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerDto))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub RadioButtonBrand_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonBrand.CheckedChanged
        If _AllowEvents Then
            Xl_LookupProduct1.Enabled = RadioButtonBrand.Checked
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub Xl_PercentDtoBrut_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentDtoBrut.AfterUpdate
        If _AllowEvents Then
            Dim DcBrut As Decimal = Xl_PercentDtoBrut.Value
            Xl_PercentDtoNet.Value = Net(DcBrut)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_PercentDtoNet_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentDtoNet.AfterUpdate
        If _AllowEvents Then
            Dim DcNet As Decimal = Xl_PercentDtoNet.Value
            Xl_PercentDtoBrut.Value = Brut(DcNet)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Function Net(DcBrut As Decimal) As Decimal
        Dim pvp As Decimal = 100
        Dim dto As Decimal = pvp * DcBrut / 100
        Dim preuNet As Decimal = pvp - dto
        Dim tax As Decimal = pvp - pvp / (1 + _Iva.Tipus / 100)
        Dim netDeIva As Decimal = pvp - tax
        Dim retval As Decimal = 100 * (1 - preuNet / netDeIva)
        Return retval
    End Function

    Private Function Brut(DcNet As Decimal) As Decimal
        Dim pvp As Decimal = 100
        Dim tax As Decimal = pvp - pvp / (1 + _Iva.Tipus / 100)
        Dim netDeIva As Decimal = pvp - tax
        Dim dto As Decimal = netDeIva * DcNet / 100
        Dim preuNet As Decimal = netDeIva - dto
        Dim retval As Decimal = 100 * (1 - preuNet / pvp)
        Return retval
    End Function


End Class


