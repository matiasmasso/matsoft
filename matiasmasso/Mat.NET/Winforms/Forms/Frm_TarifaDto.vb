Public Class Frm_TarifaDto
    Private _TarifaDto As DTOTarifaDto
    Private _masterDto As Decimal
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOTarifaDto)
        MyBase.New()
        Me.InitializeComponent()
        _TarifaDto = value
        _masterDto = BLLDefault.EmpDecimal(DTODefault.Codis.DtoTarifa)
        BLL.BLLTarifaDto.Load(_TarifaDto)
    End Sub

    Private Sub Frm_TarifaDto_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _TarifaDto
            If .Product IsNot Nothing Then
                CheckBoxProduct.Checked = True
                Xl_LookupProductBrand1.Visible = True
                Xl_LookupProductBrand1.Load(.Product)
            End If
            If .Contact IsNot Nothing Then
                CheckBoxContact.Checked = True
                Xl_Contact21.Visible = True
                Xl_Contact21.Contact = .Contact
            End If
            If .Fch = Nothing Then
                DateTimePicker1.Value = Today
            Else
                DateTimePicker1.Value = .Fch
            End If
            TextBoxMasterDto.Text = String.Format("{0:0.00}%", _masterDto)
            Xl_PercentDto.Value = .Value
            TextBoxResultDto.Text = String.Format("{0:0.00}%", _masterDto + .Value)
            TextBoxObs.Text = .Obs
            If .UsrCreated Is Nothing Then
                TextBoxUsr.Text = String.Format("(nou descompte creat per {0})", BLLUser.NicknameOrElse(BLLSession.Current.User))
            Else
                TextBoxUsr.Text = String.Format("Creat per {0} el {1:dd/MM/yy} a les {1:HH}:{1:mm}", BLLUser.NicknameOrElse(.UsrCreated), .FchCreated)
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxObs.TextChanged,
         DateTimePicker1.ValueChanged,
          Xl_LookupProductBrand1.AfterUpdate,
           Xl_Contact21.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _TarifaDto
            .Contact = IIf(CheckBoxContact.Checked, Xl_Contact21.Contact, Nothing)
            .Product = IIf(CheckBoxProduct.Checked, Xl_LookupProductBrand1.Product, Nothing)
            .Fch = DateTimePicker1.Value
            .Value = Xl_PercentDto.Value
            .Obs = TextBoxObs.Text
            .UsrCreated = BLLSession.Current.User
            .FchCreated = Now
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLTarifaDto.Update(_TarifaDto, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_TarifaDto))
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
            If BLL.BLLTarifaDto.Delete(_TarifaDto, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_TarifaDto))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxProduct_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProduct.CheckedChanged
        If _AllowEvents Then
            Xl_LookupProductBrand1.Visible = CheckBoxProduct.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxContact_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxContact.CheckedChanged
        If _AllowEvents Then
            Xl_Contact21.Visible = CheckBoxContact.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_PercentDto_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentDto.AfterUpdate
        If _AllowEvents Then
            TextBoxResultDto.Text = String.Format("{0:0.00}%", _masterDto + Xl_PercentDto.Value)
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


