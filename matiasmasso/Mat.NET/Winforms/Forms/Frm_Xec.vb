Public Class Frm_Xec

    Private _Xec As DTOXec
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOXec)
        MyBase.New()
        Me.InitializeComponent()
        _Xec = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Xec.Load(_Xec, exs) Then
            With _Xec
                TextBoxLliurador.Text = .Lliurador.FullNom
                TextBoxXecNum.Text = .XecNum
                If .CcaRebut IsNot Nothing Then
                    If .CcaRebut.Fch <> Nothing Then
                        DateTimePicker1.Value = .CcaRebut.Fch
                    End If
                End If
                Xl_Amount1.Amt = .Amt
                Await Xl_Iban1.Load(.Iban)
                TextBoxRebut.Text = DTOCca.FullNom(.CcaRebut)
                TextBoxPresentat.Text = DTOCca.FullNom(.CcaPresentacio)
                TextBoxVençut.Text = DTOCca.FullNom(.CcaVto)
                If .Vto <> Nothing Then
                    DateTimePickerVto.Value = .Vto
                End If
                UIHelper.LoadComboFromEnum(ComboBoxPresentacio, GetType(DTOXec.ModalitatsPresentacio), "(triar modalitat)", .CodPresentacio)
                Xl_Pnds1.Load(.Pnds, New List(Of DTOImpagat))
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxXecNum.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Xec
            .XecNum = TextBoxXecNum.Text
            .Iban = Xl_Iban1.Value
            .Amt = Xl_Amount1.Amt
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Xec.Update(_Xec, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Xec))
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
            If Await FEB2.Xec.Delete(_Xec, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Xec))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TextBoxRebut_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxRebut.DoubleClick
        Dim oCca As DTOCca = _Xec.CcaRebut
        If oCca IsNot Nothing Then
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub

    Private Sub TextBoxPresentat_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxPresentat.DoubleClick
        Dim oCca As DTOCca = _Xec.CcaPresentacio
        If oCca IsNot Nothing Then
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub

    Private Sub TextBoxVençut_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxVençut.DoubleClick
        Dim oCca As DTOCca = _Xec.CcaVto
        If oCca IsNot Nothing Then
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub
End Class


