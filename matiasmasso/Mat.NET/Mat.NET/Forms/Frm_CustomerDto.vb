Public Class Frm_CustomerDto
    Private _CustomerDto As DTOCustomerTarifaDto
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCustomerTarifaDto)
        MyBase.New()
        Me.InitializeComponent()
        _CustomerDto = value
        BLL.BLLCustomerTarifaDto.Load(_CustomerDto)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _CustomerDto
            Me.Text = .Customer.FullNom
            DateTimePicker1.Value = .Fch
            If .Product Is Nothing Then
                RadioButtonAllBrands.Checked = True
            Else
                RadioButtonBrand.Checked = True
                Xl_LookupProduct1.Product = .Product
                Xl_LookupProduct1.Enabled = True
            End If

            Xl_PercentDto.Value = .Dto
            TextBoxObs.Text = .Obs

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) _
       Handles DateTimePicker1.ValueChanged, _
         Xl_LookupProduct1.AfterUpdate, _
          Xl_PercentDto.AfterUpdate, _
           TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _CustomerDto
            .Fch = DateTimePicker1.Value
            If RadioButtonAllBrands.Checked Then
                .Product = Nothing
            Else
                .Product = Xl_LookupProduct1.Product
            End If
            .Dto = Xl_PercentDto.Value
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLCustomerTarifaDto.Update(_CustomerDto, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CustomerDto))
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
            If BLL.BLLCustomerTarifaDto.Delete(_CustomerDto, exs) Then
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
End Class


