Imports Winforms

Public Class Frm_IbanCheck
    Private _Iban As DTOIban

    Public Sub New(Src As String)
        MyBase.New
        InitializeComponent()
        TextBoxCcc.Text = Src
    End Sub

    Private Async Sub Frm_IbanCheck_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TextBoxCcc.Text > "" Then
            Await refresca()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Iban.Update(_Iban, exs) Then
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxCcc_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCcc.TextChanged
        ButtonCheck.Enabled = True
    End Sub

    Private Async Sub ButtonCheck_Click(sender As Object, e As EventArgs) Handles ButtonCheck.Click
        Await refresca()
    End Sub

    Private Sub Xl_Lookup_BankBranch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Lookup_BankBranch1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _Iban = Await FEB2.Iban.FromCcc(exs, TextBoxCcc.Text)
        If exs.Count = 0 Then
            If _Iban Is Nothing Then
                PictureBoxWarn.Visible = True
                TextBoxObs.Text = "No consta a la base de dades"
            Else
                Await Xl_Lookup_BankBranch1.Load(_Iban)
                If _Iban.BankBranch Is Nothing Then
                    LabelBranch.Visible = True
                    Xl_Lookup_BankBranch1.Visible = True
                    PictureBoxWarn.Visible = True
                    TextBoxObs.Text = "Cal sel·leccionar la oficina bancària"
                    PictureBoxWarn.Visible = False
                Else
                    TextBoxObs.Text = _Iban.Titular.FullNom & vbCrLf & DTOIban.BankNom(_Iban) & vbCrLf & DTOIban.BranchLocationAndAdr(_Iban)
                End If
                TextBoxCcc.Text = DTOIban.Formated(TextBoxCcc.Text)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub TextBoxObs_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxObs.DoubleClick
        If _Iban IsNot Nothing Then
            Dim oFrm As New Frm_Iban(_Iban)
            oFrm.Show()
        End If
    End Sub
End Class