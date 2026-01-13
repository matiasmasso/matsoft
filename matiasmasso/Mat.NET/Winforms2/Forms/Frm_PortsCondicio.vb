Public Class Frm_PortsCondicio
    Private _PortsCondicio As DTOPortsCondicio
    Private _LoadedCustomers As Boolean

    Private _AllowEvents As Boolean

    Private Enum Tabs
        General
        Clients
    End Enum

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPortsCondicio)
        MyBase.New()
        Me.InitializeComponent()
        _PortsCondicio = value
    End Sub

    Private Sub Frm_PortsCondicio_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PortsCondicio.Load(exs, _PortsCondicio) Then
            With _PortsCondicio
                TextBoxNom.Text = .Nom
                Select Case .Cod
                    Case DTOPortsCondicio.Cods.portsPagats
                        RadioButtonPagats.Checked = True
                        GroupBoxMoq.Visible = True
                        If .PdcMinVal IsNot Nothing OrElse .Fee IsNot Nothing Then
                            CheckBoxMOQ.Checked = True
                            PanelMoq.Enabled = True
                            Xl_AmountMOQ.Amt = .PdcMinVal
                            Xl_AmountFee.Amt = .Fee
                        End If
                    Case DTOPortsCondicio.Cods.carrecEnFactura
                        RadioButtonCarrec.Checked = True
                        GroupBoxMoq.Visible = False
                    Case DTOPortsCondicio.Cods.portsDeguts
                        RadioButtonDeguts.Checked = True
                        GroupBoxMoq.Visible = False
                    Case DTOPortsCondicio.Cods.reculliran
                        RadioButtonReculliran.Checked = True
                        GroupBoxMoq.Visible = False
                End Select
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub



    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
        RadioButtonPagats.CheckedChanged,
        RadioButtonCarrec.CheckedChanged,
        RadioButtonDeguts.CheckedChanged,
        RadioButtonReculliran.CheckedChanged,
        Xl_AmountMOQ.AfterUpdate,
        Xl_AmountFee.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
            GroupBoxMoq.Visible = RadioButtonPagats.Checked = True
        End If
    End Sub

    Private Sub CheckBoxMOQ_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMOQ.CheckedChanged
        If _AllowEvents Then
            PanelMoq.Enabled = CheckBoxMOQ.Checked = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _PortsCondicio
            .Nom = TextBoxNom.Text
            If RadioButtonPagats.Checked Then
                .Cod = DTOPortsCondicio.Cods.portsPagats
                .PdcMinVal = IIf(CheckBoxMOQ.Checked, Xl_AmountMOQ.Amt, Nothing)
                .Fee = IIf(CheckBoxMOQ.Checked, Xl_AmountFee.Amt, Nothing)
            ElseIf RadioButtonCarrec.Checked Then
                .Cod = DTOPortsCondicio.Cods.carrecEnFactura
                .PdcMinVal = Nothing
                .Fee = Nothing
            ElseIf RadioButtonDeguts.Checked Then
                .Cod = DTOPortsCondicio.Cods.portsDeguts
                .PdcMinVal = Nothing
                .Fee = Nothing
            ElseIf RadioButtonReculliran.Checked Then
                .Cod = DTOPortsCondicio.Cods.reculliran
                .PdcMinVal = Nothing
                .Fee = Nothing
            End If

        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.PortsCondicio.Update(exs, _PortsCondicio) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PortsCondicio))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta condició?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.PortsCondicio.Delete(exs, _PortsCondicio) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PortsCondicio))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Clients
                If Not _LoadedCustomers Then
                    Dim oCustomers = Await FEB.PortsCondicio.Customers(exs, _PortsCondicio)
                    If exs.Count = 0 Then
                        Await Xl_Contacts1.Load(oCustomers)
                        _LoadedCustomers = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Async Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Await Xl_Contacts1.SetFilter(e.Argument)
    End Sub
End Class