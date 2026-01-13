Public Class Frm_Amortization

    Private _Amortization As DTOAmortization
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAmortization)
        MyBase.New()
        Me.InitializeComponent()
        _Amortization = value
        _Amortization.IsLoaded = False
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Amortization.Load(_Amortization, exs) Then
            With _Amortization
                TextBoxNom.Text = .Dsc
                If .Fch > DateTimePicker1.MinDate Then
                    DateTimePicker1.Value = .Fch
                End If
                Xl_LookupPgcCta1.PgcCta = .Cta
                Xl_AmountCur1.Amt = .Amt
                Xl_Percent1.Value = .Tipus
                Xl_LookupCcaAlta.Cca = .Alta
                If .Baixa IsNot Nothing Then
                    CheckBoxBaixa.Checked = True
                    Xl_LookupCcaBaixa.Visible = True
                    Xl_LookupCcaBaixa.Cca = .Baixa
                End If

                Xl_AmortizationItems1.Load(.Items)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         Xl_AmountCur1.AfterUpdate,
          Xl_LookupPgcCta1.AfterUpdate,
           Xl_LookupCcaAlta.AfterUpdate,
            Xl_LookupCcaBaixa.AfterUpdate,
             Xl_Percent1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Amortization
            .Emp = GlobalVariables.Emp
            .Dsc = TextBoxNom.Text
            .Fch = DateTimePicker1.Value
            .Cta = Xl_LookupPgcCta1.PgcCta
            .Amt = Xl_AmountCur1.Amt
            .Tipus = Xl_Percent1.Value
            .Alta = Xl_LookupCcaAlta.Cca
            .Alta.UsrLog.usrLastEdited = Current.Session.User
            If CheckBoxBaixa.Checked Then
                .Baixa = Xl_LookupCcaBaixa.Cca
            Else
                .Baixa = Nothing
            End If
            .Items = Xl_AmortizationItems1.Values
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Amortization.Update(_Amortization, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Amortization))
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
            If Await FEB2.Amortization.Delete(_Amortization, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Amortization))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxBaixa_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxBaixa.CheckedChanged
        Xl_LookupCcaBaixa.Visible = CheckBoxBaixa.Checked
    End Sub

    Private Sub Xl_AmortizationItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AmortizationItems1.RequestToRefresh
        _Amortization.IsLoaded = False
        Dim exs As New List(Of Exception)
        If FEB2.Amortization.Load(_Amortization, exs) Then
            Xl_AmortizationItems1.Load(_Amortization.Items)
            RaiseEvent AfterUpdate(Me, e)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LookupCcaAlta_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupCcaAlta.RequestToLookup
        Dim oFrm As New Frm_Extracte(Nothing, _Amortization.Cta, Nothing, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onCcaSelected
        oFrm.Show()
    End Sub

    Private Sub onCcaSelected(sender As Object, e As MatEventArgs)
        Dim oCcb As DTOCcb = e.Argument
        Xl_LookupCcaAlta.Cca = oCcb.Cca
        DateTimePicker1.Value = oCcb.Cca.Fch
        TextBoxNom.Text = oCcb.Cca.Concept

        If _Amortization.Cta IsNot Nothing Then
            Dim oCcbs As List(Of DTOCcb) = oCcb.Cca.Items.Where(Function(x) x.Cta.Equals(_Amortization.Cta)).ToList
            Dim DcDebe As Decimal = oCcbs.Where(Function(x) x.Dh = DTOCcb.DhEnum.Debe).Sum(Function(y) y.Amt.Eur)
            Dim DcHaber As Decimal = oCcbs.Where(Function(x) x.Dh = DTOCcb.DhEnum.Haber).Sum(Function(y) y.Amt.Eur)
            Dim oAmt = DTOAmt.Factory(DcDebe - DcHaber)
            Xl_AmountCur1.Amt = oAmt
        End If

    End Sub
End Class


