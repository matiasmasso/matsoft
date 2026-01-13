Public Class Frm_Cur
    Private _Cur As DTOCur

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oCur As DTOCur)
        MyBase.New
        InitializeComponent()

        _Cur = oCur
    End Sub

    Private Sub Frm_Cur_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Cur
            TextBoxISO.Text = .Tag
            TextBoxNomEsp.Text = .Nom.Esp
            TextBoxNomCat.Text = .Nom.Cat
            TextBoxNomEng.Text = .Nom.Eng
            TextBoxSymbol.Text = .Symbol
            NumericUpDown1.Value = .Decimals
            TextBoxExchangeRate.Text = _Cur.ExchangeText
            CheckBoxObsoleto.Checked = .Obsoleto
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Cur
            .Tag = TextBoxISO.Text
            .Nom.esp = TextBoxNomEsp.Text
            .Nom.cat = TextBoxNomCat.Text
            .Nom.eng = TextBoxNomEng.Text
            .Symbol = TextBoxSymbol.Text
            .Decimals = NumericUpDown1.Value
            .Obsoleto = CheckBoxObsoleto.Checked
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Cur.Update(_Cur, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cur))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class